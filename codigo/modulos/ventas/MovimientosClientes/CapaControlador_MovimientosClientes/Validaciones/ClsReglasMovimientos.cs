using System;
using CapaModelo_MovimientosClientes;
using VentasValidaciones;

namespace CapaControlador_MovimientosClientes.Validaciones
{
    internal static class ClsReglasMovimientos
    {
        internal static void Usuario(this ClsMovimientosRepositorio Db, int Usuario)
        { ClsValidacionesVentas.Exigir(Usuario > 0 && Db.UsuarioActivo(Usuario), "Seleccione un responsable activo de Seguridad."); }
        internal static void BloquearMoneda(this ClsMovimientosRepositorio Db, int Moneda)
        {
            ClsValidacionesVentas.Exigir(Db.EnTransaccion, "La operacion requiere una transaccion.");
            ClsValidacionesVentas.Exigir(Moneda > 0 && Db.MonedaActiva(Moneda), "Seleccione una moneda activa.");
            Db.AsegurarBloqueoMoneda(Moneda);
        }
        internal static void PeriodoAbierto(this ClsMovimientosRepositorio Db, int Moneda, DateTime Fecha)
        {
            Db.BloquearMoneda(Moneda);
            object Ultimo=Db.UltimoCierre(Moneda);
            ClsValidacionesVentas.Exigir(Ultimo == null || Ultimo == DBNull.Value || Fecha.Date > Convert.ToDateTime(Ultimo).Date,
                "La fecha pertenece a un periodo cerrado. Registre el movimiento en un periodo abierto.");
        }
    }
}
