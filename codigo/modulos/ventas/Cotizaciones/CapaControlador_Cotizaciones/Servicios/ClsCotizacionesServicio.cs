// Jose Javier Torres Martinez | 0901-23-1091 | 22/10/2026
using CapaControlador_Cotizaciones.Validaciones;
using CapaModelo_Cotizaciones;
using System.Data;
using System.Linq;
using System;
using VentasValidaciones;

namespace CapaControlador_Cotizaciones.Servicios
{
    internal sealed class ClsCotizacionesServicio
    {
        // RegistroCotizaciones.
        public string VentasMetGuardar(int IdCotizacion, string Numero, int Cliente, int Vendedor, int Moneda, int Usuario, DateTime Fecha, DateTime Vigencia, decimal TipoCambio, string Estado, DataTable Lineas)
        {
            Numero = ClsValidacionesVentas.VentasFuncTexto(Numero, 40, "numero de cotizacion", false);
            ClsValidacionesVentas.VentasMetExigir(Fecha.Date <= Vigencia.Date && Vigencia.Date >= DateTime.Today && Fecha.Date <= DateTime.Today, "Revise la emision y vigencia; una vencida requiere una nueva cotizacion.");
            ClsValidacionesVentas.VentasMetExigir(Estado == "BORRADOR" || Estado == "EMITIDA", "Solo puede guardar como Borrador o Vigente.");
            ClsValidacionesVentas.VentasMetExigir(TipoCambio > 0 && TipoCambio <= 999999999999.999999m && TipoCambio == Math.Round(TipoCambio, 6), "El tipo de cambio debe ser positivo y tener hasta seis decimales.");
            ClsValidacionesVentas.VentasMetExigir(Lineas != null && Lineas.Rows.Count > 0, "Agregue productos a la cotizacion.");
            using (var Repositorio = new ClsCotizacionesRepositorio())
            {
                Repositorio.VentasMetIniciar();
                Repositorio.VentasMetUsuario(Usuario);
                Repositorio.VentasMetBloquearMoneda(Moneda);
                ClsValidacionesVentas.VentasMetExigir(Convert.ToInt32(Repositorio.VentasMetGuardarConsultarCliente(Cliente)) == 1, "Seleccione un cliente activo.");
                ClsValidacionesVentas.VentasMetExigir(Convert.ToInt32(Repositorio.VentasMetGuardarConsultarVendedor(Vendedor)) == 1, "Seleccione un vendedor activo.");
                DataTable L = VentasMetLista(Repositorio, Cliente, Moneda, Fecha);
                ClsValidacionesVentas.VentasMetExigir(L.Rows.Count == 1, "El cliente no tiene una lista de precios vigente para esta moneda.");
                if (IdCotizacion > 0)
                {
                    DataTable Actual = Repositorio.VentasMetGuardarConsultarCotizacion(IdCotizacion);
                    ClsValidacionesVentas.VentasMetExigir(Actual.Rows.Count == 1 && (string)Actual.Rows[0]["estadoCotizacion"] == "BORRADOR" && Convert.ToDateTime(Actual.Rows[0]["vigenteHasta"]).Date >= DateTime.Today, "La cotizacion fue emitida, convertida, anulada o vencio. No puede modificarse; consulte nuevamente.");
                    Numero = (string)Actual.Rows[0]["numeroCotizacion"];
                }
                else if (Numero.Length == 0)
                    Numero = Repositorio.VentasMetNumero("COT");
                decimal Subtotal = 0, Descuento = 0, Impuesto = 0, Total = 0;
                var Detalle = new DataTable();
                foreach (string Columna in new[]
                {
                    "idProducto",
                    "Cantidad",
                    "Precio",
                    "Descuento",
                    "Impuesto",
                    "Total",
                    "Tasa",
                    "Descripcion",
                    "Tipo"
                }

                )
                    Detalle.Columns.Add(Columna, Columna == "Descripcion" || Columna == "Tipo" ? typeof(string) : typeof(decimal));
                foreach (DataRow Fila in Lineas.Rows)
                {
                    int Producto = Convert.ToInt32(Fila["idProducto"]);
                    decimal Cantidad = Convert.ToDecimal(Fila["Cantidad"]), D = Convert.ToDecimal(Fila["Descuento"]);
                    DataTable P = Repositorio.VentasMetGuardarConsultarProductoventas(Producto, L.Rows[0]["idListaPrecio"]);
                    ClsValidacionesVentas.VentasMetExigir(P.Rows.Count == 1, "Un producto ya no esta disponible en la lista.");
                    decimal Precio = (decimal)P.Rows[0]["precioUnitario"], Tasa = (decimal)P.Rows[0]["porcentajeImpuesto"];
                    ClsValidacionesVentas.VentasMetExigir(Cantidad > 0 && Cantidad == Math.Round(Cantidad, 4) && Precio >= 0 && Tasa >= 0 && Tasa <= 100, "Cantidad, precio o tasa de impuesto invalida.");
                    ClsValidacionesVentas.VentasMetExigir(Precio == Convert.ToDecimal(Fila["Precio"]), "Un precio de la lista cambio. Recargue los productos antes de guardar.");
                    decimal Base = ClsValidacionesVentas.VentasFuncRedondear(Cantidad * Precio);
                    ClsValidacionesVentas.VentasMetExigir(D >= 0 && D <= Base && D == ClsValidacionesVentas.VentasFuncRedondear(D), "El descuento es un importe y no puede superar el subtotal.");
                    decimal I = ClsValidacionesVentas.VentasFuncRedondear((Base - D) * Tasa / 100m), T = Base - D + I;
                    ClsValidacionesVentas.VentasMetExigir(Base <= ClsValidacionesVentas.ImporteMaximo && T <= ClsValidacionesVentas.ImporteMaximo, "El importe de una linea supera la capacidad del documento.");
                    Subtotal += Base;
                    Descuento += D;
                    Impuesto += I;
                    Total += T;
                    Detalle.Rows.Add(Producto, Cantidad, Precio, D, I, T, Tasa, P.Rows[0]["nombreProducto"], P.Rows[0]["tipoItem"]);
                }

                ClsValidacionesVentas.VentasMetExigir(Subtotal <= ClsValidacionesVentas.ImporteMaximo && Total <= ClsValidacionesVentas.ImporteMaximo, "El total supera la capacidad del documento.");
                if (IdCotizacion == 0)
                {
                    Repositorio.VentasMetGuardarInsertarCotizacion(Numero, Cliente, Vendedor, Moneda, Fecha.Date, Vigencia.Date, TipoCambio, Estado, Subtotal, Descuento, Impuesto, Total, Usuario);
                    IdCotizacion = Repositorio.VentasFuncObtenerId();
                    Repositorio.VentasMetGuardarInsertarCotizacioncontrol(IdCotizacion, L.Rows[0]["idListaPrecio"]);
                }
                else
                {
                    Repositorio.VentasMetGuardarActualizarCotizacion(Cliente, Vendedor, Moneda, Fecha.Date, Vigencia.Date, TipoCambio, Estado, Subtotal, Descuento, Impuesto, Total, IdCotizacion);
                    Repositorio.VentasMetGuardarActualizarCotizacioncontrol(L.Rows[0]["idListaPrecio"], IdCotizacion);
                    Repositorio.VentasMetGuardarEliminarCotizaciondetalle(IdCotizacion);
                }

                int N = 0;
                foreach (DataRow Fila in Detalle.Rows)
                    Repositorio.VentasMetGuardarInsertarCotizaciondetalle(IdCotizacion, ++N, Fila["idProducto"], Fila["Descripcion"], Fila["Tipo"], Fila["Cantidad"], Fila["Precio"], Fila["Descuento"], Fila["Tasa"], Fila["Impuesto"], Fila["Total"]);
                Repositorio.VentasMetAuditar("COTIZACION", IdCotizacion, "GUARDAR", Numero, Usuario);
                Repositorio.VentasMetConfirmar();
                return Numero;
            }
        }

        public void VentasMetAnular(int IdCotizacion, int Usuario)
        {
            using (var Repositorio = new ClsCotizacionesRepositorio())
            {
                Repositorio.VentasMetIniciar();
                Repositorio.VentasMetUsuario(Usuario);
                DataTable T = Repositorio.VentasMetAnularConsultarCotizacion(IdCotizacion);
                ClsValidacionesVentas.VentasMetExigir(T.Rows.Count == 1 && new[] { "BORRADOR", "EMITIDA", "ACEPTADA" }.Contains((string)T.Rows[0][0]), "La cotizacion no se puede anular.");
                Repositorio.VentasMetAnularActualizarCotizacion(IdCotizacion);
                Repositorio.VentasMetAuditar("COTIZACION", IdCotizacion, "ANULAR", "Anulacion de cotizacion", Usuario);
                Repositorio.VentasMetConfirmar();
            }
        }

        // OperacionCotizaciones.
        private static DataTable VentasMetLista(ClsCotizacionesRepositorio Repositorio, int Cliente, int Moneda, DateTime Fecha)
        {
            return Repositorio.VentasMetListaConsultarClientelistaprecios(Cliente, Moneda, Moneda, Fecha.Date, Fecha.Date);
        }

        // ConversionCotizaciones.
        public string VentasMetConvertir(int IdCotizacion, bool AFactura, int Establecimiento, int Usuario)
        {
            using (var Repositorio = new ClsCotizacionesRepositorio())
            {
                Repositorio.VentasMetIniciar();
                Repositorio.VentasMetUsuario(Usuario);
                object Moneda = Repositorio.VentasMetConvertirConsultarCotizacion(IdCotizacion);
                ClsValidacionesVentas.VentasMetExigir(Moneda != null, "Primero guarde y emita la cotizacion.");
                Repositorio.VentasMetBloquearMoneda(Convert.ToInt32(Moneda));
                DataTable T = Repositorio.VentasMetConvertirConsultarCotizacion2(IdCotizacion);
                ClsValidacionesVentas.VentasMetExigir(T.Rows.Count == 1, "La cotizacion no tiene una lista de precios registrada.");
                DataRow C = T.Rows[0];
                string Columna = AFactura ? "idFactura" : "idPedido";
                if (C[Columna] != DBNull.Value)
                {
                    string Existente = Convert.ToString((AFactura ? Repositorio.VentasMetConvertirConsultarFactura(C[Columna]) : Repositorio.VentasMetConvertirConsultarPedido(C[Columna])));
                    Repositorio.VentasMetConfirmar();
                    return Existente;
                }

                ClsValidacionesVentas.VentasMetExigir(C[AFactura ? "idPedido" : "idFactura"] == DBNull.Value, "La cotizacion ya fue convertida a otro documento.");
                ClsValidacionesVentas.VentasMetExigir(new[] { "EMITIDA", "ACEPTADA" }.Contains((string)C["estadoCotizacion"]) && Convert.ToDateTime(C["fechaEmision"]).Date <= DateTime.Today && Convert.ToDateTime(C["vigenteHasta"]).Date >= DateTime.Today, "Solo se convierte una cotizacion emitida y vigente. Una vencida requiere una nueva con precios actuales.");
                string Numero = Repositorio.VentasMetNumero(AFactura ? "FCOT" : "PCOT");
                int Id;
                if (!AFactura)
                {
                    Repositorio.VentasMetConvertirInsertarPedido(Numero, IdCotizacion, C["idCliente"], C["idVendedor"], C["idMoneda"], DateTime.Today, C["tipoCambio"], C["subtotal"], C["descuentoTotal"], C["impuestoTotal"], C["total"], Usuario);
                    Id = Repositorio.VentasFuncObtenerId();
                    Repositorio.VentasMetConvertirInsertarPedidodetalle(Id, IdCotizacion);
                }
                else
                {
                    ClsValidacionesVentas.VentasMetExigir(Convert.ToInt32(Repositorio.VentasMetConvertirConsultarEstablecimientoventas(Establecimiento)) == 1, "Seleccione un establecimiento emisor activo.");
                    DataTable Clientes = Repositorio.VentasMetConvertirConsultarCliente(C["idCliente"]);
                    DataRow Cliente = Clientes.Rows[0];
                    int Dias = Convert.ToInt32(Cliente["diasCredito"]);
                    // Borrador con precios congelados: emitir, certificar y afectar inventarios corresponde a Facturas.
                    Repositorio.VentasMetConvertirInsertarFactura(Numero, Establecimiento, C["idCliente"], C["idVendedor"], C["idMoneda"], DateTime.Today, DateTime.Today.AddDays(Dias), Dias == 0 ? "CONTADO" : "CREDITO", Dias, C["tipoCambio"], Cliente["tipoIdentificacion"], Cliente["numeroIdentificacion"] == DBNull.Value ? "CF" : Cliente["numeroIdentificacion"], Cliente["nombreCliente"], Cliente["direccionCliente"], C["subtotal"], C["descuentoTotal"], C["impuestoTotal"], C["total"], "Origen cotizacion " + C["numeroCotizacion"], Usuario);
                    Id = Repositorio.VentasFuncObtenerId();
                    Repositorio.VentasMetConvertirInsertarFacturadetalle(Id, IdCotizacion);
                }

                if (AFactura)
                    Repositorio.VentasMetConvertirActualizarCotizacioncontrol(Id, IdCotizacion);
                else
                    Repositorio.VentasMetConvertirActualizarCotizacioncontrol2(Id, IdCotizacion);
                Repositorio.VentasMetConvertirActualizarCotizacion(IdCotizacion);
                Repositorio.VentasMetConfirmar();
                return AFactura ? "COT-" + Numero : Numero;
            }
        }

        // ConsultaCotizaciones.
        public DataTable VentasMetCatalogo(string Nombre)
        {
            using (var Repositorio = new ClsCotizacionesRepositorio())
                return Repositorio.VentasMetCatalogo(Nombre);
        }

        public DataTable VentasMetListaCliente(int Cliente, int Moneda, DateTime Fecha)
        {
            using (var Repositorio = new ClsCotizacionesRepositorio())
                return VentasMetLista(Repositorio, Cliente, Moneda, Fecha);
        }

        public DataTable VentasMetProductos(int Cliente, int Moneda, DateTime Fecha)
        {
            using (var Repositorio = new ClsCotizacionesRepositorio())
            {
                DataTable L = VentasMetLista(Repositorio, Cliente, Moneda, Fecha);
                if (L.Rows.Count != 1)
                    return new DataTable();
                return Repositorio.VentasMetProductosConsultarProductoventas(L.Rows[0]["idListaPrecio"]);
            }
        }

        public DataSet VentasMetConsultar(string Numero)
        {
            using (var Repositorio = new ClsCotizacionesRepositorio())
            {
                var Datos = new DataSet();
                DataTable Encabezado = Repositorio.VentasMetGeneralesConsultarConsultarCotizacion(Numero.Trim());
                ClsValidacionesVentas.VentasMetExigir(Encabezado.Rows.Count == 1, "No se encontro la cotizacion.");
                Datos.Tables.Add(Encabezado);
                Datos.Tables.Add(Repositorio.VentasMetGeneralesConsultarConsultarCotizaciondetalle(Encabezado.Rows[0]["idCotizacion"]));
                return Datos;
            }
        }

        public DataTable VentasMetListar(string Buscar)
        {
            Buscar = (Buscar ?? "").Trim();
            using (var Repositorio = new ClsCotizacionesRepositorio())
                return Repositorio.VentasMetListarConsultarCotizacion(Buscar, Buscar, Buscar);
        }
    }
}
