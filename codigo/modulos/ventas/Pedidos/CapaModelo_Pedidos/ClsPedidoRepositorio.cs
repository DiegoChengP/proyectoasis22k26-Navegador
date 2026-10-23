using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Globalization;
using System.Linq;

namespace CapaModelo_Pedidos
{
    public sealed class ClsPedidoRepositorio : ClsConexion
    {
        private OdbcConnection VentasFuncAbrir()
        {
            var Conexion = VentasFuncObtenerConexion();
            Conexion.Open();
            return Conexion;
        }

        private static OdbcCommand VentasFuncComando(OdbcConnection Conexion, OdbcTransaction Transaccion, string SentenciaSql, params object[] Valores)
        {
            var Comando = new OdbcCommand(SentenciaSql, Conexion, Transaccion);
            foreach (object Valor in Valores)
                Comando.Parameters.AddWithValue("?", Valor is decimal
                    ? ((decimal)Valor).ToString(CultureInfo.InvariantCulture)
                    : (Valor ?? DBNull.Value));
            return Comando;
        }

        private List<T> VentasFuncConsultar<T>(string SentenciaSql, Func<OdbcDataReader, T> Leer, params object[] Valores)
        {
            var Lista = new List<T>();
            using (var Conexion = VentasFuncAbrir())
            using (var Comando = VentasFuncComando(Conexion, null, SentenciaSql, Valores))
            using (var Lector = Comando.ExecuteReader())
                while (Lector.Read()) Lista.Add(Leer(Lector));
            return Lista;
        }

        public List<ClsCatalogoPedido> VentasFuncClientes()
        {
            return VentasFuncCatalogo("SELECT idCliente, CONCAT(codigoCliente, ' · ', nombreCliente) FROM tblcliente WHERE is_active=1 ORDER BY nombreCliente");
        }

        public List<ClsCatalogoPedido> VentasFuncVendedores()
        {
            return VentasFuncCatalogo("SELECT idVendedor, codigoVendedor FROM tblvendedor WHERE is_active=1 ORDER BY codigoVendedor");
        }

        private List<ClsCatalogoPedido> VentasFuncCatalogo(string SentenciaSql)
        {
            return VentasFuncConsultar(SentenciaSql, Lector => new ClsCatalogoPedido { Id = Lector.GetInt32(0), Nombre = Lector.GetString(1) });
        }

        public List<ClsLineaPedido> VentasFuncProductosPublicos(DateTime Fecha)
        {
            return VentasFuncConsultar(
                "SELECT p.idProducto,p.codigoProducto,p.nombreProducto,p.tipoItem,p.porcentajeImpuesto,d.precioUnitario " +
                "FROM tblproductoventas p JOIN tbllistapreciosdetalle d ON d.idProducto=p.idProducto AND d.is_active=1 " +
                "JOIN tbllistaprecios l ON l.idListaPrecio=d.idListaPrecio AND l.is_active=1 " +
                "WHERE p.is_active=1 AND UPPER(l.codigoListaPrecio)='PUBLICO' AND l.vigenteDesde<=? " +
                "AND (l.vigenteHasta IS NULL OR l.vigenteHasta>=?) ORDER BY p.nombreProducto",
                Lector => new ClsLineaPedido {
                    ProductoId = Lector.GetInt32(0), Codigo = Lector.GetString(1), Descripcion = Lector.GetString(2),
                    Tipo = Lector.GetString(3), PorcentajeImpuesto = Lector.GetDecimal(4), Precio = Lector.GetDecimal(5)
                }, Fecha.Date, Fecha.Date);
        }

        public int VentasFuncMonedaPublica()
        {
            using (var Conexion = VentasFuncAbrir())
            using (var Comando = VentasFuncComando(Conexion, null,
                "SELECT idMoneda FROM tbllistaprecios WHERE UPPER(codigoListaPrecio)='PUBLICO' AND is_active=1 LIMIT 1"))
            {
                object Valor = Comando.ExecuteScalar();
                return Valor == null || Valor == DBNull.Value ? 0 : Convert.ToInt32(Valor);
            }
        }

        public int VentasFuncGuardar(ClsPedido Pedido)
        {
            decimal Subtotal = Pedido.Lineas.Sum(Linea => Math.Round(Linea.Cantidad * Linea.Precio, 2, MidpointRounding.AwayFromZero));
            decimal Descuento = Pedido.Lineas.Sum(Linea => Linea.Descuento);
            decimal Impuesto = Pedido.Lineas.Sum(Linea => Linea.Impuesto);
            decimal Total = Pedido.Lineas.Sum(Linea => Linea.Total);
            using (var Conexion = VentasFuncAbrir())
            using (var Transaccion = Conexion.BeginTransaction())
            {
                try
                {
                    using (var Comando = VentasFuncComando(Conexion, Transaccion,
                        "INSERT INTO tblpedido (numeroPedido,idCotizacion,referenciaIndependiente,idCliente,idVendedor,idMoneda,fechaPedido,fechaEntrega,fechaVencimiento,tipoCambio,estadoPedido,subtotal,descuentoTotal,impuestoTotal,total) " +
                        "VALUES (?,?,?,?,?,?,?,?,?,?,'PENDIENTE',?,?,?,?)",
                        Pedido.Numero, Pedido.CotizacionId, Pedido.Referencia, Pedido.ClienteId, Pedido.VendedorId,
                        Pedido.MonedaId, Pedido.Fecha.Date, Pedido.Entrega.Date, Pedido.Vencimiento.Date,
                        Pedido.TipoCambio, Subtotal, Descuento, Impuesto, Total))
                        Comando.ExecuteNonQuery();
                    int Id;
                    using (var Comando = VentasFuncComando(Conexion, Transaccion, "SELECT LAST_INSERT_ID()")) Id = Convert.ToInt32(Comando.ExecuteScalar());
                    for (int Indice = 0; Indice < Pedido.Lineas.Count; Indice++)
                    {
                        ClsLineaPedido Linea = Pedido.Lineas[Indice];
                        using (var Comando = VentasFuncComando(Conexion, Transaccion,
                            "INSERT INTO tblpedidodetalle (idPedido,idCotizacionDetalle,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea) " +
                            "VALUES (?,?,?,?,?,?,?,?,?,?,?,?)", Id, Linea.CotizacionDetalleId, Indice + 1, Linea.ProductoId,
                            Linea.Descripcion, Linea.Tipo, Linea.Cantidad, Linea.Precio, Linea.Descuento, Linea.PorcentajeImpuesto, Linea.Impuesto, Linea.Total))
                            Comando.ExecuteNonQuery();
                    }
                    Transaccion.Commit();
                    return Id;
                }
                catch { try { Transaccion.Rollback(); } catch { } throw; }
            }
        }

    }
}
