using System;
using System.Data;
using VentasDatos;

namespace CapaModelo_Cotizaciones
{
    // El repositorio solo ejecuta consultas parametrizadas y mantiene la transaccion.
    public sealed class ClsCotizacionesRepositorio : IDisposable
    {
        private readonly ClsDatosVentas _Datos = new ClsDatosVentas();
        public bool EnTransaccion { get { return _Datos.Transaccion != null; } }
        public void Iniciar() { _Datos.Iniciar(); }
        public void Confirmar() { _Datos.Confirmar(); }
        public DataTable Tabla(ConsultaCotizaciones Consulta, params object[] Valores) { return _Datos.Tabla(SqlCotizaciones.Obtener(Consulta), Valores); }
        public object Escalar(ConsultaCotizaciones Consulta, params object[] Valores) { return _Datos.Escalar(SqlCotizaciones.Obtener(Consulta), Valores); }
        public int Ejecutar(ConsultaCotizaciones Consulta, params object[] Valores) { return _Datos.Ejecutar(SqlCotizaciones.Obtener(Consulta), Valores); }
        public DataTable Catalogo(string Nombre) { return _Datos.Catalogo(Nombre); }
        public int Id() { return _Datos.Id(); }
        public string Numero(string Prefijo) { return _Datos.Numero(Prefijo); }
        public bool UsuarioActivo(int Usuario) { return Convert.ToInt32(_Datos.Escalar("SELECT COUNT(*) FROM tblusuario WHERE idUsuario=? AND is_active=1", Usuario)) == 1; }
        public bool MonedaActiva(int Moneda) { return Convert.ToInt32(_Datos.Escalar("SELECT COUNT(*) FROM tblmonedaventas WHERE idMoneda=? AND is_active=1", Moneda)) == 1; }
        public void AsegurarBloqueoMoneda(int Moneda)
        {
            _Datos.Ejecutar("INSERT INTO tblcontrolperiodocxc(idMoneda) VALUES (?) ON DUPLICATE KEY UPDATE idMoneda=VALUES(idMoneda)", Moneda);
            _Datos.Tabla("SELECT idMoneda FROM tblcontrolperiodocxc WHERE idMoneda=? FOR UPDATE", Moneda);
        }
        public object UltimoCierre(int Moneda) { return _Datos.Escalar("SELECT fechaUltimoCierre FROM tblcontrolperiodocxc WHERE idMoneda=?", Moneda); }
        public void Auditar(string Entidad, int Id, string Accion, string Detalle, int Usuario) { _Datos.Auditar(Entidad, Id, Accion, Detalle, Usuario); }
        public void Dispose() { _Datos.Dispose(); }
    }
}
