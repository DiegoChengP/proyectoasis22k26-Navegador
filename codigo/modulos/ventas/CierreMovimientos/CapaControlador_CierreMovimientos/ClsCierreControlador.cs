// Jose Javier Torres Martinez | 0901-23-1091 | 20/10/2026
using System;
using System.Data;
namespace CapaControlador_CierreMovimientos
{
    public sealed class ClsCierreControlador
    {
        private readonly CapaModelo_CierreMovimientos.ClsCierreRepositorio _Repositorio = new CapaModelo_CierreMovimientos.ClsCierreRepositorio();
        public DataTable Catalogo(string Nombre) { return _Repositorio.Catalogo(Nombre); }
        public DataTable Revisar(int Moneda, string Tipo, DateTime Desde, DateTime Hasta) { return _Repositorio.Revisar(Moneda, Tipo, Desde, Hasta); }
        public DataTable EstadoPeriodo(int Moneda, string Tipo, DateTime Desde, DateTime Hasta) { return _Repositorio.EstadoPeriodo(Moneda, Tipo, Desde, Hasta); }
        public string Confirmar(int Moneda, int Usuario, string Tipo, DateTime Desde, DateTime Hasta, bool Revisado) { return _Repositorio.Confirmar(Moneda, Usuario, Tipo, Desde, Hasta, Revisado); }
        public DataTable Historial(int Moneda, int Cliente, string Periodo) { return _Repositorio.Historial(Moneda, Cliente, Periodo); }
        public DataTable Partida(int IdCierre) { return _Repositorio.Partida(IdCierre); }
        public DataTable EstadoTraslado(int IdCierre) { return _Repositorio.EstadoTraslado(IdCierre); }
        public string Trasladar(int IdCierre, int Usuario, Func<string, int, DateTime, DataTable, string> Registrar)
        {
            if (Registrar == null) throw new InvalidOperationException("Configure el enlace de Contabilidad.");
            DataSet Datos = _Repositorio.PrepararTraslado(IdCierre, Usuario); DataRow H = Datos.Tables[0].Rows[0];
            if ((string)H["estado"] == "CONFIRMADO") return (string)H["referenciaContable"];
            // Sin transaccion abierta durante la llamada externa. El receptor debe deduplicar por clave.
            string Referencia = Registrar((string)H["claveIntegracion"], Convert.ToInt32(H["idMoneda"]), Convert.ToDateTime(H["fechaHasta"]), Datos.Tables[1]);
            if (string.IsNullOrWhiteSpace(Referencia)) throw new InvalidOperationException("Contabilidad no devolvio una confirmacion. La partida sigue pendiente con la misma clave para reintentar.");
            _Repositorio.ConfirmarTraslado(IdCierre, Usuario, Referencia); return Referencia;
        }
    }
}
