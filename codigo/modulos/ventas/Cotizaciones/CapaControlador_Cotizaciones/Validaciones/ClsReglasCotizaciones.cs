using System;
using System.Data;
using CapaModelo_Cotizaciones;
using VentasValidaciones;

namespace CapaControlador_Cotizaciones.Validaciones
{
    internal static class ClsReglasCotizaciones
    {
        internal static void Usuario(this ClsCotizacionesRepositorio Db, int Usuario)
        { ClsValidacionesVentas.Exigir(Usuario > 0 && Db.UsuarioActivo(Usuario), "Seleccione un responsable activo de Seguridad."); }
        internal static void BloquearMoneda(this ClsCotizacionesRepositorio Db, int Moneda)
        {
            ClsValidacionesVentas.Exigir(Db.EnTransaccion, "La operacion requiere una transaccion.");
            ClsValidacionesVentas.Exigir(Moneda > 0 && Db.MonedaActiva(Moneda), "Seleccione una moneda activa.");
            Db.AsegurarBloqueoMoneda(Moneda);
        }
        internal static void PeriodoAbierto(this ClsCotizacionesRepositorio Db, int Moneda, DateTime Fecha)
        {
            Db.BloquearMoneda(Moneda);
            object Ultimo=Db.UltimoCierre(Moneda);
            ClsValidacionesVentas.Exigir(Ultimo == null || Ultimo == DBNull.Value || Fecha.Date > Convert.ToDateTime(Ultimo).Date,
                "La fecha pertenece a un periodo cerrado. Registre el movimiento en un periodo abierto.");
        }
        internal static bool PuedeEditar(int Id, string Estado, DateTime Vigencia) { return Id == 0 || Estado == "BORRADOR" && Vigencia.Date >= DateTime.Today; }
        internal static void ValidarEdicion(int Id, string Estado, DateTime Vigencia) { ClsValidacionesVentas.Exigir(PuedeEditar(Id,Estado,Vigencia), "Solo se modifica una cotizacion en borrador y vigente."); }
        internal static object[] CalcularLinea(DataRow Producto, decimal Cantidad, decimal Descuento)
        {
            ClsValidacionesVentas.Exigir(Producto != null, "Seleccione un producto de la lista asignada al cliente.");
            decimal Precio=Convert.ToDecimal(Producto["Precio"]), Tasa=Convert.ToDecimal(Producto["Tasa"]);
            ClsValidacionesVentas.Exigir(Cantidad > 0 && Cantidad == Math.Round(Cantidad,4), "La cantidad debe ser positiva y tener hasta cuatro decimales.");
            decimal Base=Math.Round(Cantidad*Precio,2,MidpointRounding.AwayFromZero);
            ClsValidacionesVentas.Exigir(Descuento >= 0 && Descuento <= Base && Descuento == Math.Round(Descuento,2), "El descuento debe tener hasta dos decimales y no superar el subtotal.");
            decimal Impuesto=Math.Round((Base-Descuento)*Tasa/100m,2,MidpointRounding.AwayFromZero);
            return new object[] { Producto["Id"], Producto["Descripcion"], Producto["Tipo"], Cantidad, Precio, Descuento, Tasa, Base, Impuesto, Base-Descuento+Impuesto };
        }
    }
}
