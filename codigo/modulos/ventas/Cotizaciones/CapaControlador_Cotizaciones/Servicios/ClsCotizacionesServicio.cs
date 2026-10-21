// Jose Javier Torres Martinez | 0901-23-1091 | 20/10/2026
using System;
using System.Data;
using System.Linq;
using CapaModelo_Cotizaciones;
using CapaControlador_Cotizaciones.Validaciones;
using VentasValidaciones;

namespace CapaControlador_Cotizaciones.Servicios
{
    internal sealed partial class ClsCotizacionesServicio
    {
        public DataTable Catalogo(string Nombre) { using (var Db = new ClsCotizacionesRepositorio()) return Db.Catalogo(Nombre); }
        private static DataTable Lista(ClsCotizacionesRepositorio Db, int Cliente, int Moneda, DateTime Fecha)
        {
            return Db.Tabla(ConsultaCotizaciones.ListaConsultarClientelistaprecios, Cliente, Moneda, Moneda, Fecha.Date, Fecha.Date);
        }
        public DataTable ListaCliente(int Cliente, int Moneda, DateTime Fecha) { using (var Db = new ClsCotizacionesRepositorio()) return Lista(Db, Cliente, Moneda, Fecha); }
        public DataTable Productos(int Cliente, int Moneda, DateTime Fecha)
        {
            using (var Db = new ClsCotizacionesRepositorio())
            {
                DataTable L = Lista(Db, Cliente, Moneda, Fecha);
                if (L.Rows.Count != 1) return new DataTable();
                return Db.Tabla(ConsultaCotizaciones.ProductosConsultarProductoventas, L.Rows[0]["idListaPrecio"]);
            }
        }
        public DataSet Consultar(string Numero)
        {
            using (var Db = new ClsCotizacionesRepositorio())
            {
                var Datos = new DataSet();
                DataTable Encabezado = Db.Tabla(ConsultaCotizaciones.ConsultarConsultarCotizacion, Numero.Trim());
                ClsValidacionesVentas.Exigir(Encabezado.Rows.Count == 1, "No se encontro la cotizacion."); Datos.Tables.Add(Encabezado);
                Datos.Tables.Add(Db.Tabla(ConsultaCotizaciones.ConsultarConsultarCotizaciondetalle, Encabezado.Rows[0]["idCotizacion"]));
                return Datos;
            }
        }
        public string Guardar(int IdCotizacion, string Numero, int Cliente, int Vendedor, int Moneda, int Usuario, DateTime Fecha, DateTime Vigencia, decimal TipoCambio, string Estado, DataTable Lineas)
        {
            Numero = ClsValidacionesVentas.Texto(Numero, 40, "numero de cotizacion", false);
            ClsValidacionesVentas.Exigir(Fecha.Date <= Vigencia.Date && Vigencia.Date >= DateTime.Today && Fecha.Date <= DateTime.Today, "Revise la emision y vigencia; una vencida requiere una nueva cotizacion.");
            ClsValidacionesVentas.Exigir(Estado == "BORRADOR" || Estado == "EMITIDA", "Solo puede guardar como Borrador o Vigente.");
            ClsValidacionesVentas.Exigir(TipoCambio > 0 && TipoCambio <= 999999999999.999999m && TipoCambio == Math.Round(TipoCambio, 6), "El tipo de cambio debe ser positivo y tener hasta seis decimales.");
            ClsValidacionesVentas.Exigir(Lineas != null && Lineas.Rows.Count > 0, "Agregue productos a la cotizacion.");
            using (var Db = new ClsCotizacionesRepositorio())
            {
                Db.Iniciar(); Db.Usuario(Usuario); Db.BloquearMoneda(Moneda);
                ClsValidacionesVentas.Exigir(Convert.ToInt32(Db.Escalar(ConsultaCotizaciones.GuardarConsultarCliente, Cliente)) == 1, "Seleccione un cliente activo.");
                ClsValidacionesVentas.Exigir(Convert.ToInt32(Db.Escalar(ConsultaCotizaciones.GuardarConsultarVendedor, Vendedor)) == 1, "Seleccione un vendedor activo.");
                DataTable L = Lista(Db, Cliente, Moneda, Fecha); ClsValidacionesVentas.Exigir(L.Rows.Count == 1, "El cliente no tiene una lista de precios vigente para esta moneda.");
                if (IdCotizacion > 0)
                {
                    DataTable Actual = Db.Tabla(ConsultaCotizaciones.GuardarConsultarCotizacion, IdCotizacion);
                    ClsValidacionesVentas.Exigir(Actual.Rows.Count == 1 && (string)Actual.Rows[0]["estadoCotizacion"] == "BORRADOR" && Convert.ToDateTime(Actual.Rows[0]["vigenteHasta"]).Date >= DateTime.Today,
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
                    DataTable P = Db.Tabla(ConsultaCotizaciones.GuardarConsultarProductoventas, Producto, L.Rows[0]["idListaPrecio"]);
                    ClsValidacionesVentas.Exigir(P.Rows.Count == 1, "Un producto ya no esta disponible en la lista.");
                    decimal Precio = (decimal)P.Rows[0]["precioUnitario"], Tasa = (decimal)P.Rows[0]["porcentajeImpuesto"];
                    ClsValidacionesVentas.Exigir(Cantidad > 0 && Cantidad == Math.Round(Cantidad, 4) && Precio >= 0 && Tasa >= 0 && Tasa <= 100, "Cantidad, precio o tasa de impuesto invalida.");
                    ClsValidacionesVentas.Exigir(Precio == Convert.ToDecimal(Fila["Precio"]), "Un precio de la lista cambio. Recargue los productos antes de guardar.");
                    decimal Base = ClsValidacionesVentas.Redondear(Cantidad * Precio);
                    ClsValidacionesVentas.Exigir(D >= 0 && D <= Base && D == ClsValidacionesVentas.Redondear(D), "El descuento es un importe y no puede superar el subtotal.");
                    decimal I = ClsValidacionesVentas.Redondear((Base - D) * Tasa / 100m), T = Base - D + I;
                    ClsValidacionesVentas.Exigir(Base <= ClsValidacionesVentas.ImporteMaximo && T <= ClsValidacionesVentas.ImporteMaximo, "El importe de una linea supera la capacidad del documento.");
                    Subtotal += Base; Descuento += D; Impuesto += I; Total += T;
                    Detalle.Rows.Add(Producto, Cantidad, Precio, D, I, T, Tasa, P.Rows[0]["nombreProducto"], P.Rows[0]["tipoItem"]);
                }
                ClsValidacionesVentas.Exigir(Subtotal <= ClsValidacionesVentas.ImporteMaximo && Total <= ClsValidacionesVentas.ImporteMaximo, "El total supera la capacidad del documento.");
                if (IdCotizacion == 0)
                {
                    Db.Ejecutar(ConsultaCotizaciones.GuardarInsertarCotizacion,
                        Numero, Cliente, Vendedor, Moneda, Fecha.Date, Vigencia.Date, TipoCambio, Estado, Subtotal, Descuento, Impuesto, Total, Usuario);
                    IdCotizacion = Db.Id();
                    Db.Ejecutar(ConsultaCotizaciones.GuardarInsertarCotizacioncontrol, IdCotizacion, L.Rows[0]["idListaPrecio"]);
                }
                else
                {
                    Db.Ejecutar(ConsultaCotizaciones.GuardarActualizarCotizacion,
                        Cliente, Vendedor, Moneda, Fecha.Date, Vigencia.Date, TipoCambio, Estado, Subtotal, Descuento, Impuesto, Total, IdCotizacion);
                    Db.Ejecutar(ConsultaCotizaciones.GuardarActualizarCotizacioncontrol, L.Rows[0]["idListaPrecio"], IdCotizacion);
                    Db.Ejecutar(ConsultaCotizaciones.GuardarEliminarCotizaciondetalle, IdCotizacion);
                }
                int N = 0;
                foreach (DataRow Fila in Detalle.Rows) Db.Ejecutar(ConsultaCotizaciones.GuardarInsertarCotizaciondetalle,
                    IdCotizacion, ++N, Fila["idProducto"], Fila["Descripcion"], Fila["Tipo"], Fila["Cantidad"], Fila["Precio"], Fila["Descuento"], Fila["Tasa"], Fila["Impuesto"], Fila["Total"]);
                Db.Auditar("COTIZACION", IdCotizacion, "GUARDAR", Numero, Usuario);
                Db.Confirmar(); return Numero;
            }
        }

        public string Convertir(int IdCotizacion, bool AFactura, int Establecimiento, int Usuario)
        {
            using (var Db = new ClsCotizacionesRepositorio())
            {
                Db.Iniciar(); Db.Usuario(Usuario);
                object Moneda = Db.Escalar(ConsultaCotizaciones.ConvertirConsultarCotizacion, IdCotizacion);
                ClsValidacionesVentas.Exigir(Moneda != null, "Primero guarde y emita la cotizacion."); Db.BloquearMoneda(Convert.ToInt32(Moneda));
                DataTable T = Db.Tabla(ConsultaCotizaciones.ConvertirConsultarCotizacion2, IdCotizacion);
                ClsValidacionesVentas.Exigir(T.Rows.Count == 1, "La cotizacion no tiene una lista de precios registrada."); DataRow C = T.Rows[0];
                string Columna = AFactura ? "idFactura" : "idPedido";
                if (C[Columna] != DBNull.Value)
                {
                    string Existente = Convert.ToString(Db.Escalar(AFactura  ? ConsultaCotizaciones.ConvertirConsultarFactura : ConsultaCotizaciones.ConvertirConsultarPedido, C[Columna]));
                    Db.Confirmar(); return Existente;
                }
                ClsValidacionesVentas.Exigir(C[AFactura ? "idPedido" : "idFactura"] == DBNull.Value, "La cotizacion ya fue convertida a otro documento.");
                ClsValidacionesVentas.Exigir(new[] { "EMITIDA", "ACEPTADA" }.Contains((string)C["estadoCotizacion"]) && Convert.ToDateTime(C["fechaEmision"]).Date <= DateTime.Today && Convert.ToDateTime(C["vigenteHasta"]).Date >= DateTime.Today,
                    "Solo se convierte una cotizacion emitida y vigente. Una vencida requiere una nueva con precios actuales.");
                string Numero = Db.Numero(AFactura ? "FCOT" : "PCOT"); int Id;
                if (!AFactura)
                {
                    Db.Ejecutar(ConsultaCotizaciones.ConvertirInsertarPedido,
                        Numero, IdCotizacion, C["idCliente"], C["idVendedor"], C["idMoneda"], DateTime.Today, C["tipoCambio"], C["subtotal"], C["descuentoTotal"], C["impuestoTotal"], C["total"], Usuario);
                    Id = Db.Id();
                    Db.Ejecutar(ConsultaCotizaciones.ConvertirInsertarPedidodetalle, Id, IdCotizacion);
                }
                else
                {
                    ClsValidacionesVentas.Exigir(Convert.ToInt32(Db.Escalar(ConsultaCotizaciones.ConvertirConsultarEstablecimientoventas, Establecimiento)) == 1, "Seleccione un establecimiento emisor activo.");
                    DataTable Clientes = Db.Tabla(ConsultaCotizaciones.ConvertirConsultarCliente, C["idCliente"]); DataRow Cliente = Clientes.Rows[0];
                    int Dias = Convert.ToInt32(Cliente["diasCredito"]);
                    // Borrador con precios congelados: emitir, certificar y afectar inventarios corresponde a Facturas.
                    Db.Ejecutar(ConsultaCotizaciones.ConvertirInsertarFactura, Numero, Establecimiento, C["idCliente"], C["idVendedor"], C["idMoneda"], DateTime.Today, DateTime.Today.AddDays(Dias), Dias == 0 ? "CONTADO" : "CREDITO", Dias, C["tipoCambio"], Cliente["tipoIdentificacion"], Cliente["numeroIdentificacion"] == DBNull.Value ? "CF" : Cliente["numeroIdentificacion"], Cliente["nombreCliente"], Cliente["direccionCliente"], C["subtotal"], C["descuentoTotal"], C["impuestoTotal"], C["total"], "Origen cotizacion " + C["numeroCotizacion"], Usuario);
                    Id = Db.Id();
                    Db.Ejecutar(ConsultaCotizaciones.ConvertirInsertarFacturadetalle, Id, IdCotizacion);
                }
                Db.Ejecutar(AFactura  ? ConsultaCotizaciones.ConvertirActualizarCotizacioncontrol : ConsultaCotizaciones.ConvertirActualizarCotizacioncontrol2, Id, IdCotizacion);
                Db.Ejecutar(ConsultaCotizaciones.ConvertirActualizarCotizacion, IdCotizacion);
                Db.Confirmar(); return AFactura ? "COT-" + Numero : Numero;
            }
        }
        public void Anular(int IdCotizacion, int Usuario)
        {
            using (var Db = new ClsCotizacionesRepositorio())
            {
                Db.Iniciar(); Db.Usuario(Usuario);
                DataTable T = Db.Tabla(ConsultaCotizaciones.AnularConsultarCotizacion, IdCotizacion);
                ClsValidacionesVentas.Exigir(T.Rows.Count == 1 && new[] { "BORRADOR", "EMITIDA", "ACEPTADA" }.Contains((string)T.Rows[0][0]), "La cotizacion no se puede anular.");
                Db.Ejecutar(ConsultaCotizaciones.AnularActualizarCotizacion, IdCotizacion);
                Db.Auditar("COTIZACION", IdCotizacion, "ANULAR", "Anulacion de cotizacion", Usuario); Db.Confirmar();
            }
        }
    }
}
