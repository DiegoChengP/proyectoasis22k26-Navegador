// Jose Javier Torres Martinez | 0901-23-1091 | 20/10/2026
using System;
using System.Data;
namespace CapaControlador_CierreMovimientos
{
    public sealed class ClsCierreControlador
    {
        private readonly Servicios.ClsCierreServicio _Servicio = new Servicios.ClsCierreServicio();
        public DataTable Listar(int Moneda) { return _Servicio.Listar(Moneda); }
        public string GuardarBorrador(int Id, int Moneda, int Usuario, string Tipo, DateTime Desde, DateTime Hasta, string Observaciones) { return _Servicio.GuardarBorrador(Id, Moneda, Usuario, Tipo, Desde, Hasta, Observaciones); }
        public void EliminarBorrador(int Id, int Usuario, string Motivo) { _Servicio.EliminarBorrador(Id, Usuario, Motivo); }
        public DataTable Catalogo(string Nombre) { return _Servicio.Catalogo(Nombre); }
        public DataTable Revisar(int Moneda, string Tipo, DateTime Desde, DateTime Hasta) { return _Servicio.Revisar(Moneda, Tipo, Desde, Hasta); }
        public DataTable EstadoPeriodo(int Moneda, string Tipo, DateTime Desde, DateTime Hasta) { return _Servicio.EstadoPeriodo(Moneda, Tipo, Desde, Hasta); }
        public string Confirmar(int Moneda, int Usuario, string Tipo, DateTime Desde, DateTime Hasta, bool Revisado) { return _Servicio.Confirmar(Moneda, Usuario, Tipo, Desde, Hasta, Revisado); }
        public DataTable Historial(int Moneda, int Cliente, string Periodo) { return _Servicio.Historial(Moneda, Cliente, Periodo); }
        public DataTable Partida(int IdCierre) { return _Servicio.Partida(IdCierre); }
        public DataTable EstadoTraslado(int IdCierre) { return _Servicio.EstadoTraslado(IdCierre); }
        public string Trasladar(int IdCierre, int Usuario, Func<string, int, DateTime, DataTable, string> Registrar)
        {
            if (Registrar == null) throw new InvalidOperationException("Configure el enlace de Contabilidad.");
            DataSet Datos = _Servicio.PrepararTraslado(IdCierre, Usuario); DataRow H = Datos.Tables[0].Rows[0];
            if ((string)H["estado"] == "CONFIRMADO") return (string)H["referenciaContable"];
            // Sin transaccion abierta durante la llamada externa. El receptor debe deduplicar por clave.
            string Referencia = Registrar((string)H["claveIntegracion"], Convert.ToInt32(H["idMoneda"]), Convert.ToDateTime(H["fechaHasta"]), Datos.Tables[1]);
            if (string.IsNullOrWhiteSpace(Referencia)) throw new InvalidOperationException("Contabilidad no devolvio una confirmacion. La partida sigue pendiente con la misma clave para reintentar.");
            _Servicio.ConfirmarTraslado(IdCierre, Usuario, Referencia); return Referencia;
        }
        public void ValidarCaptura(bool Condicion, string Mensaje) { VentasValidaciones.ClsValidacionesVentas.Exigir(Condicion, Mensaje); }
        public decimal ImporteCelda(object Valor) { return VentasValidaciones.ClsValidacionesVentas.ImporteCelda(Valor); }
        public DateTime[] Periodo(int Anio, int Mes, bool Anual) { return Validaciones.ClsReglasCierre.Periodo(Anio,Mes,Anual); }

    }
}
