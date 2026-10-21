// Jose Javier Torres Martinez | 0901-23-1091 | 20/10/2026
using System;
using System.Data;
using System.Linq;
using VentasDatos;

namespace CapaModelo_Cotizaciones
{
    public sealed class ClsCotizacionesRepositorio
    {
        public DataTable Catalogo(string Nombre) { using (var Db = new ClsDatosVentas()) return Db.Catalogo(Nombre); }
        private static DataTable Lista(ClsDatosVentas Db, int Cliente, int Moneda, DateTime Fecha)
        {
            return Db.Tabla("SELECT l.idListaPrecio,l.nombreListaPrecio FROM tblclientelistaprecios c JOIN tbllistaprecios l ON l.idListaPrecio=c.idListaPrecio " +
                "WHERE c.idCliente=? AND c.idMoneda=? AND l.idMoneda=? AND l.is_active=1 AND l.vigenteDesde<=? AND (l.vigenteHasta IS NULL OR l.vigenteHasta>=?)", Cliente, Moneda, Moneda, Fecha.Date, Fecha.Date);
        }
        public DataTable ListaCliente(int Cliente, int Moneda, DateTime Fecha) { using (var Db = new ClsDatosVentas()) return Lista(Db, Cliente, Moneda, Fecha); }
        public DataTable Productos(int Cliente, int Moneda, DateTime Fecha)
        {
            using (var Db = new ClsDatosVentas())
            {
                DataTable L = Lista(Db, Cliente, Moneda, Fecha);
                if (L.Rows.Count != 1) return new DataTable();
                return Db.Tabla("SELECT p.idProducto Id,CONCAT(p.codigoProducto,' - ',p.nombreProducto) Nombre,p.nombreProducto Descripcion,p.tipoItem Tipo,p.porcentajeImpuesto Tasa,d.precioUnitario Precio " +
                    "FROM tblproductoventas p JOIN tbllistapreciosdetalle d ON d.idProducto=p.idProducto WHERE d.idListaPrecio=? AND d.is_active=1 AND p.is_active=1 ORDER BY p.nombreProducto", L.Rows[0]["idListaPrecio"]);
            }
        }
        public DataSet Consultar(string Numero)
        {
            using (var Db = new ClsDatosVentas())
            {
                var Datos = new DataSet();
                DataTable Encabezado = Db.Tabla("SELECT c.*,l.nombreListaPrecio,co.idListaPrecio,co.idPedido,co.idFactura," +
                    "CASE WHEN c.estadoCotizacion IN ('EMITIDA','ACEPTADA','BORRADOR') AND c.vigenteHasta<CURRENT_DATE() THEN 'VENCIDA' ELSE c.estadoCotizacion END EstadoVisual " +
                    "FROM tblcotizacion c LEFT JOIN tblcotizacioncontrol co ON co.idCotizacion=c.idCotizacion LEFT JOIN tbllistaprecios l ON l.idListaPrecio=co.idListaPrecio WHERE c.numeroCotizacion=?", Numero.Trim());
                ClsDatosVentas.Exigir(Encabezado.Rows.Count == 1, "No se encontro la cotizacion."); Datos.Tables.Add(Encabezado);
                Datos.Tables.Add(Db.Tabla("SELECT d.*,p.codigoProducto FROM tblcotizaciondetalle d JOIN tblproductoventas p ON p.idProducto=d.idProducto WHERE d.idCotizacion=? ORDER BY d.numeroLinea", Encabezado.Rows[0]["idCotizacion"]));
                return Datos;
            }
        }
        public string Guardar(int IdCotizacion, string Numero, int Cliente, int Vendedor, int Moneda, int Usuario, DateTime Fecha, DateTime Vigencia, decimal TipoCambio, string Estado, DataTable Lineas)
        {
            Numero = ClsDatosVentas.Texto(Numero, 40, "numero de cotizacion", false);
            ClsDatosVentas.Exigir(Fecha.Date <= Vigencia.Date && Vigencia.Date >= DateTime.Today && Fecha.Date <= DateTime.Today, "Revise la emision y vigencia; una vencida requiere una nueva cotizacion.");
            ClsDatosVentas.Exigir(Estado == "BORRADOR" || Estado == "EMITIDA", "Solo puede guardar como Borrador o Vigente.");
            ClsDatosVentas.Exigir(TipoCambio > 0 && TipoCambio <= 999999999999.999999m && TipoCambio == Math.Round(TipoCambio, 6), "El tipo de cambio debe ser positivo y tener hasta seis decimales.");
            ClsDatosVentas.Exigir(Lineas != null && Lineas.Rows.Count > 0, "Agregue productos a la cotizacion.");
            using (var Db = new ClsDatosVentas())
            {
                Db.Iniciar(); Db.Usuario(Usuario); Db.BloquearMoneda(Moneda);
                ClsDatosVentas.Exigir(Convert.ToInt32(Db.Escalar("SELECT COUNT(*) FROM tblcliente WHERE idCliente=? AND is_active=1", Cliente)) == 1, "Seleccione un cliente activo.");
                ClsDatosVentas.Exigir(Convert.ToInt32(Db.Escalar("SELECT COUNT(*) FROM tblvendedor WHERE idVendedor=? AND is_active=1", Vendedor)) == 1, "Seleccione un vendedor activo.");
                DataTable L = Lista(Db, Cliente, Moneda, Fecha); ClsDatosVentas.Exigir(L.Rows.Count == 1, "El cliente no tiene una lista de precios vigente para esta moneda.");
                if (IdCotizacion > 0)
                {
                    DataTable Actual = Db.Tabla("SELECT estadoCotizacion,numeroCotizacion,vigenteHasta FROM tblcotizacion WHERE idCotizacion=? FOR UPDATE", IdCotizacion);
                    ClsDatosVentas.Exigir(Actual.Rows.Count == 1 && (string)Actual.Rows[0]["estadoCotizacion"] == "BORRADOR" && Convert.ToDateTime(Actual.Rows[0]["vigenteHasta"]).Date >= DateTime.Today,
                        "La cotizacion fue emitida, convertida, anulada o vencio. No puede modificarse; consulte nuevamente.");
                    Numero = (string)Actual.Rows[0]["numeroCotizacion"];
                }
                else if (Numero.Length == 0) Numero = Db.Numero("COT");
                decimal Subtotal = 0, Descuento = 0, Impuesto = 0, Total = 0;
                var Detalle = new DataTable();
                foreach (string Columna in new[] { "idProducto", "Cantidad", "Precio", "Descuento", "Impuesto", "Total", "Tasa", "Descripcion", "Tipo" }) Detalle.Columns.Add(Columna, Columna == "Descripcion" || Columna == "Tipo" ? typeof(string) : typeof(decimal));
                foreach (DataRow Fila in Lineas.Rows)
                {
                    int Producto = Convert.ToInt32(Fila["idProducto"]); decimal Cantidad = Convert.ToDecimal(Fila["Cantidad"]), D = Convert.ToDecimal(Fila["Descuento"]);
                    DataTable P = Db.Tabla("SELECT p.nombreProducto,p.tipoItem,p.porcentajeImpuesto,d.precioUnitario FROM tblproductoventas p JOIN tbllistapreciosdetalle d ON d.idProducto=p.idProducto WHERE p.idProducto=? AND d.idListaPrecio=? AND p.is_active=1 AND d.is_active=1", Producto, L.Rows[0]["idListaPrecio"]);
                    ClsDatosVentas.Exigir(P.Rows.Count == 1, "Un producto ya no esta disponible en la lista.");
                    decimal Precio = (decimal)P.Rows[0]["precioUnitario"], Tasa = (decimal)P.Rows[0]["porcentajeImpuesto"];
                    ClsDatosVentas.Exigir(Cantidad > 0 && Cantidad == Math.Round(Cantidad, 4) && Precio >= 0 && Tasa >= 0 && Tasa <= 100, "Cantidad, precio o tasa de impuesto invalida.");
                    ClsDatosVentas.Exigir(Precio == Convert.ToDecimal(Fila["Precio"]), "Un precio de la lista cambio. Recargue los productos antes de guardar.");
                    decimal Base = ClsDatosVentas.Redondear(Cantidad * Precio);
                    ClsDatosVentas.Exigir(D >= 0 && D <= Base && D == ClsDatosVentas.Redondear(D), "El descuento es un importe y no puede superar el subtotal.");
                    decimal I = ClsDatosVentas.Redondear((Base - D) * Tasa / 100m), T = Base - D + I;
                    ClsDatosVentas.Exigir(Base <= ClsDatosVentas.ImporteMaximo && T <= ClsDatosVentas.ImporteMaximo, "El importe de una linea supera la capacidad del documento.");
                    Subtotal += Base; Descuento += D; Impuesto += I; Total += T;
                    Detalle.Rows.Add(Producto, Cantidad, Precio, D, I, T, Tasa, P.Rows[0]["nombreProducto"], P.Rows[0]["tipoItem"]);
                }
                ClsDatosVentas.Exigir(Subtotal <= ClsDatosVentas.ImporteMaximo && Total <= ClsDatosVentas.ImporteMaximo, "El total supera la capacidad del documento.");
                if (IdCotizacion == 0)
                {
                    Db.Ejecutar("INSERT INTO tblcotizacion(numeroCotizacion,idCliente,idVendedor,idMoneda,fechaEmision,vigenteHasta,tipoCambio,estadoCotizacion,subtotal,descuentoTotal,impuestoTotal,total,idUsuarioRegistro) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?)",
                        Numero, Cliente, Vendedor, Moneda, Fecha.Date, Vigencia.Date, TipoCambio, Estado, Subtotal, Descuento, Impuesto, Total, Usuario);
                    IdCotizacion = Db.Id();
                    Db.Ejecutar("INSERT INTO tblcotizacioncontrol(idCotizacion,idListaPrecio) VALUES (?,?)", IdCotizacion, L.Rows[0]["idListaPrecio"]);
                }
                else
                {
                    Db.Ejecutar("UPDATE tblcotizacion SET idCliente=?,idVendedor=?,idMoneda=?,fechaEmision=?,vigenteHasta=?,tipoCambio=?,estadoCotizacion=?,subtotal=?,descuentoTotal=?,impuestoTotal=?,total=? WHERE idCotizacion=?",
                        Cliente, Vendedor, Moneda, Fecha.Date, Vigencia.Date, TipoCambio, Estado, Subtotal, Descuento, Impuesto, Total, IdCotizacion);
                    Db.Ejecutar("UPDATE tblcotizacioncontrol SET idListaPrecio=? WHERE idCotizacion=?", L.Rows[0]["idListaPrecio"], IdCotizacion);
                    Db.Ejecutar("DELETE FROM tblcotizaciondetalle WHERE idCotizacion=?", IdCotizacion);
                }
                int N = 0;
                foreach (DataRow Fila in Detalle.Rows) Db.Ejecutar("INSERT INTO tblcotizaciondetalle(idCotizacion,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea) VALUES (?,?,?,?,?,?,?,?,?,?,?)",
                    IdCotizacion, ++N, Fila["idProducto"], Fila["Descripcion"], Fila["Tipo"], Fila["Cantidad"], Fila["Precio"], Fila["Descuento"], Fila["Tasa"], Fila["Impuesto"], Fila["Total"]);
                Db.Confirmar(); return Numero;
            }
        }

        public string Convertir(int IdCotizacion, bool AFactura, int Establecimiento, int Usuario)
        {
            using (var Db = new ClsDatosVentas())
            {
                Db.Iniciar(); Db.Usuario(Usuario);
                object Moneda = Db.Escalar("SELECT idMoneda FROM tblcotizacion WHERE idCotizacion=?", IdCotizacion);
                ClsDatosVentas.Exigir(Moneda != null, "Primero guarde y emita la cotizacion."); Db.BloquearMoneda(Convert.ToInt32(Moneda));
                DataTable T = Db.Tabla("SELECT c.*,co.idPedido,co.idFactura FROM tblcotizacion c JOIN tblcotizacioncontrol co ON co.idCotizacion=c.idCotizacion WHERE c.idCotizacion=? FOR UPDATE", IdCotizacion);
                ClsDatosVentas.Exigir(T.Rows.Count == 1, "La cotizacion no tiene una lista de precios registrada."); DataRow C = T.Rows[0];
                string Columna = AFactura ? "idFactura" : "idPedido";
                if (C[Columna] != DBNull.Value)
                {
                    string Existente = Convert.ToString(Db.Escalar(AFactura ? "SELECT CONCAT(serieFactura,'-',numeroFactura) FROM tblfactura WHERE idFactura=?" : "SELECT numeroPedido FROM tblpedido WHERE idPedido=?", C[Columna]));
                    Db.Confirmar(); return Existente;
                }
                ClsDatosVentas.Exigir(C[AFactura ? "idPedido" : "idFactura"] == DBNull.Value, "La cotizacion ya fue convertida a otro documento.");
                ClsDatosVentas.Exigir(new[] { "EMITIDA", "ACEPTADA" }.Contains((string)C["estadoCotizacion"]) && Convert.ToDateTime(C["fechaEmision"]).Date <= DateTime.Today && Convert.ToDateTime(C["vigenteHasta"]).Date >= DateTime.Today,
                    "Solo se convierte una cotizacion emitida y vigente. Una vencida requiere una nueva con precios actuales.");
                string Numero = Db.Numero(AFactura ? "FCOT" : "PCOT"); int Id;
                if (!AFactura)
                {
                    Db.Ejecutar("INSERT INTO tblpedido(numeroPedido,idCotizacion,idCliente,idVendedor,idMoneda,fechaPedido,tipoCambio,estadoPedido,subtotal,descuentoTotal,impuestoTotal,total,idUsuarioRegistro) VALUES (?,?,?,?,?,?,?,'CONFIRMADO',?,?,?,?,?)",
                        Numero, IdCotizacion, C["idCliente"], C["idVendedor"], C["idMoneda"], DateTime.Today, C["tipoCambio"], C["subtotal"], C["descuentoTotal"], C["impuestoTotal"], C["total"], Usuario);
                    Id = Db.Id();
                    Db.Ejecutar("INSERT INTO tblpedidodetalle(idPedido,idCotizacionDetalle,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea) " +
                        "SELECT ?,idCotizacionDetalle,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea FROM tblcotizaciondetalle WHERE idCotizacion=?", Id, IdCotizacion);
                }
                else
                {
                    ClsDatosVentas.Exigir(Convert.ToInt32(Db.Escalar("SELECT COUNT(*) FROM tblestablecimientoventas WHERE idEstablecimiento=? AND is_active=1", Establecimiento)) == 1, "Seleccione un establecimiento emisor activo.");
                    DataTable Clientes = Db.Tabla("SELECT * FROM tblcliente WHERE idCliente=?", C["idCliente"]); DataRow Cliente = Clientes.Rows[0];
                    int Dias = Convert.ToInt32(Cliente["diasCredito"]);
                    // Borrador con precios congelados: emitir, certificar y afectar inventarios corresponde a Facturas.
                    Db.Ejecutar("INSERT INTO tblfactura(serieFactura,numeroFactura,idEstablecimiento,idCliente,idVendedor,idMoneda,fechaEmision,fechaVencimiento,tipoDte,condicionPago,diasCreditoAplicados,tipoCambio,tipoIdentificacionReceptor,identificacionReceptor,nombreReceptor,direccionReceptor,subtotal,descuentoTotal,impuestoTotal,total,estadoFactura,observaciones,idUsuarioRegistro) " +
                        "VALUES ('COT',?,?,?,?,?,?,?,'PENDIENTE',?,?,?,?,?,?,?,?,?,?,?,'BORRADOR',?,?)", Numero, Establecimiento, C["idCliente"], C["idVendedor"], C["idMoneda"], DateTime.Today, DateTime.Today.AddDays(Dias), Dias == 0 ? "CONTADO" : "CREDITO", Dias, C["tipoCambio"], Cliente["tipoIdentificacion"], Cliente["numeroIdentificacion"] == DBNull.Value ? "CF" : Cliente["numeroIdentificacion"], Cliente["nombreCliente"], Cliente["direccionCliente"], C["subtotal"], C["descuentoTotal"], C["impuestoTotal"], C["total"], "Origen cotizacion " + C["numeroCotizacion"], Usuario);
                    Id = Db.Id();
                    Db.Ejecutar("INSERT INTO tblfacturadetalle(idFactura,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea) " +
                        "SELECT ?,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea FROM tblcotizaciondetalle WHERE idCotizacion=?", Id, IdCotizacion);
                }
                Db.Ejecutar(AFactura ? "UPDATE tblcotizacioncontrol SET idFactura=? WHERE idCotizacion=?" : "UPDATE tblcotizacioncontrol SET idPedido=? WHERE idCotizacion=?", Id, IdCotizacion);
                Db.Ejecutar("UPDATE tblcotizacion SET estadoCotizacion='CONVERTIDA' WHERE idCotizacion=?", IdCotizacion);
                Db.Confirmar(); return AFactura ? "COT-" + Numero : Numero;
            }
        }
        public void Anular(int IdCotizacion, int Usuario)
        {
            using (var Db = new ClsDatosVentas())
            {
                Db.Iniciar(); Db.Usuario(Usuario);
                DataTable T = Db.Tabla("SELECT estadoCotizacion FROM tblcotizacion WHERE idCotizacion=? FOR UPDATE", IdCotizacion);
                ClsDatosVentas.Exigir(T.Rows.Count == 1 && new[] { "BORRADOR", "EMITIDA", "ACEPTADA" }.Contains((string)T.Rows[0][0]), "La cotizacion no se puede anular.");
                Db.Ejecutar("UPDATE tblcotizacion SET estadoCotizacion='ANULADA' WHERE idCotizacion=?", IdCotizacion); Db.Confirmar();
            }
        }
    }
}
