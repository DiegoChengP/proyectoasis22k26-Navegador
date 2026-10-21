using System;
using CapaModelo_CierreMovimientos;
using VentasValidaciones;

namespace CapaControlador_CierreMovimientos.Validaciones
{
    internal static class ClsReglasCierre
    {
        internal static void Usuario(this ClsCierreRepositorio Db, int Usuario)
        { ClsValidacionesVentas.Exigir(Usuario > 0 && Db.UsuarioActivo(Usuario), "Seleccione un responsable activo de Seguridad."); }
        internal static void BloquearMoneda(this ClsCierreRepositorio Db, int Moneda)
        {
            ClsValidacionesVentas.Exigir(Db.EnTransaccion, "La operacion requiere una transaccion.");
            ClsValidacionesVentas.Exigir(Moneda > 0 && Db.MonedaActiva(Moneda), "Seleccione una moneda activa.");
            Db.AsegurarBloqueoMoneda(Moneda);
        }
        internal static void PeriodoAbierto(this ClsCierreRepositorio Db, int Moneda, DateTime Fecha)
        {
            Db.BloquearMoneda(Moneda);
            object Ultimo=Db.UltimoCierre(Moneda);
            ClsValidacionesVentas.Exigir(Ultimo == null || Ultimo == DBNull.Value || Fecha.Date > Convert.ToDateTime(Ultimo).Date,
                "La fecha pertenece a un periodo cerrado. Registre el movimiento en un periodo abierto.");
        }
        internal static DateTime[] Periodo(int Anio, int Mes, bool Anual)
        {
            ClsValidacionesVentas.Exigir(Anio >= 2000 && Anio <= 9998 && Mes >= 1 && Mes <= 12, "Seleccione un anio y mes validos.");
            DateTime Desde=new DateTime(Anio,Anual ? 1 : Mes,1);
            return new[] { Desde, Anual ? new DateTime(Anio,12,31) : Desde.AddMonths(1).AddDays(-1) };
        }
        internal static void ValidarPeriodo(string Tipo, DateTime Desde, DateTime Hasta)
        {
            bool Mensual = Tipo == "MENSUAL" && Desde.Day == 1 && Hasta.Date == Desde.Date.AddMonths(1).AddDays(-1);
            bool Anual = Tipo == "ANUAL" && Desde.Month == 1 && Desde.Day == 1 && Hasta.Date == Desde.Date.AddYears(1).AddDays(-1);
            ClsValidacionesVentas.Exigir(Mensual || Anual, "Seleccione un periodo mensual o anual completo.");
        }
    }
}
