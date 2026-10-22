// Jose Javier Torres Martinez | 0901-23-1091 | 22/10/2026
using System.Data;
using System;

namespace CapaControlador_CierreMovimientos
{
    public sealed class ClsCierreControlador
    {
        private readonly Servicios.ClsCierreServicio _Servicio = new Servicios.ClsCierreServicio();
        public DataTable VentasMetListar(int Moneda)
        {
            return _Servicio.VentasMetListar(Moneda);
        }

        public string VentasMetGuardarBorrador(int Id, int Moneda, int Usuario, string Tipo, DateTime Desde, DateTime Hasta, string Observaciones)
        {
            return _Servicio.VentasMetGuardarBorrador(Id, Moneda, Usuario, Tipo, Desde, Hasta, Observaciones);
        }

        public void VentasMetEliminarBorrador(int Id, int Usuario, string Motivo)
        {
            _Servicio.VentasMetEliminarBorrador(Id, Usuario, Motivo);
        }

        public DataTable VentasMetCatalogo(string Nombre)
        {
            return _Servicio.VentasMetCatalogo(Nombre);
        }

        public DataTable VentasMetRevisar(int Moneda, string Tipo, DateTime Desde, DateTime Hasta)
        {
            return _Servicio.VentasMetRevisar(Moneda, Tipo, Desde, Hasta);
        }

        public DataTable VentasMetEstadoPeriodo(int Moneda, string Tipo, DateTime Desde, DateTime Hasta)
        {
            return _Servicio.VentasMetEstadoPeriodo(Moneda, Tipo, Desde, Hasta);
        }

        public string VentasMetConfirmar(int Moneda, int Usuario, string Tipo, DateTime Desde, DateTime Hasta, bool Revisado)
        {
            return _Servicio.VentasMetConfirmar(Moneda, Usuario, Tipo, Desde, Hasta, Revisado);
        }

        public DataTable VentasMetHistorial(int Moneda, int Cliente, string Periodo)
        {
            return _Servicio.VentasMetHistorial(Moneda, Cliente, Periodo);
        }

        public DataTable VentasMetPartida(int IdCierre)
        {
            return _Servicio.VentasMetPartida(IdCierre);
        }

        public DataTable VentasMetEstadoTraslado(int IdCierre)
        {
            return _Servicio.VentasMetEstadoTraslado(IdCierre);
        }

        public string VentasMetTrasladar(int IdCierre, int Usuario, Func<string, int, DateTime, DataTable, string> Registrar)
        {
            if (Registrar == null)
                throw new InvalidOperationException("Configure el enlace de Contabilidad.");
            DataSet Datos = _Servicio.VentasMetPrepararTraslado(IdCierre, Usuario);
            DataRow H = Datos.Tables[0].Rows[0];
            if ((string)H["estado"] == "CONFIRMADO")
                return (string)H["referenciaContable"];
            // Sin transaccion abierta durante la llamada externa. El receptor debe deduplicar por clave.
            string Referencia = Registrar((string)H["claveIntegracion"], Convert.ToInt32(H["idMoneda"]), Convert.ToDateTime(H["fechaHasta"]), Datos.Tables[1]);
            if (string.IsNullOrWhiteSpace(Referencia))
                throw new InvalidOperationException("Contabilidad no devolvio una confirmacion. La partida sigue pendiente con la misma clave para reintentar.");
            _Servicio.VentasMetConfirmarTraslado(IdCierre, Usuario, Referencia);
            return Referencia;
        }

        public void VentasMetValidarCaptura(bool Condicion, string Mensaje)
        {
            VentasValidaciones.ClsValidacionesVentas.VentasMetExigir(Condicion, Mensaje);
        }

        public decimal VentasFuncImporteCelda(object Valor)
        {
            return VentasValidaciones.ClsValidacionesVentas.VentasFuncImporteCelda(Valor);
        }

        public DateTime[] VentasMetPeriodo(int Anio, int Mes, bool Anual)
        {
            return Validaciones.ClsReglasCierre.VentasMetPeriodo(Anio, Mes, Anual);
        }
    }
}
