// Jose Javier Torres Martinez | 0901-23-1091 | 23/10/2026
using System.Data;
using System;
using VentasDatos;

namespace CapaModelo_CierreMovimientos
{
    public sealed class ClsCierreRepositorio : IDisposable
    {
        public ClsContabilidadCierre Contabilidad { get; }
        public ClsConsultasCierre Consultas { get; }

        public ClsCierreRepositorio()
        {
            // Los grupos comparten la conexion y transaccion del repositorio.
            Contabilidad = new ClsContabilidadCierre(_Datos);
            Consultas = new ClsConsultasCierre(_Datos);
        }

        private readonly ClsDatosVentas _Datos = new ClsDatosVentas();

        public bool EnTransaccion
        {
            get
            {
                return _Datos.EnTransaccion;
            }
        }

        public void VentasMetIniciar()
        {
            _Datos.VentasMetIniciar();
        }

        public void VentasMetConfirmar()
        {
            _Datos.VentasMetConfirmar();
        }

        public DataTable VentasFuncCatalogo(string Nombre)
        {
            return _Datos.VentasFuncCatalogo(Nombre);
        }

        public int VentasFuncObtenerId()
        {
            return _Datos.VentasFuncObtenerId();
        }

        public string VentasFuncNumero(string Prefijo)
        {
            return _Datos.VentasFuncNumero(Prefijo);
        }

        public bool VentasFuncUsuarioActivo(int Usuario)
        {
            return Convert.ToInt32(_Datos.VentasFuncEscalar("SELECT COUNT(*) FROM tblusuario WHERE idUsuario=? AND is_active=1", Usuario)) == 1;
        }

        public bool VentasFuncMonedaActiva(int Moneda)
        {
            return Convert.ToInt32(_Datos.VentasFuncEscalar("SELECT COUNT(*) FROM tblmonedaventas WHERE idMoneda=? AND is_active=1", Moneda)) == 1;
        }

        public void VentasMetAsegurarBloqueoMoneda(int Moneda)
        {
            _Datos.VentasFuncEjecutar("INSERT INTO tblcontrolperiodocxc(idMoneda) VALUES (?) ON DUPLICATE KEY UPDATE idMoneda=VALUES(idMoneda)", Moneda);
            _Datos.VentasFuncTabla("SELECT idMoneda FROM tblcontrolperiodocxc WHERE idMoneda=? FOR UPDATE", Moneda);
        }

        public object VentasFuncUltimoCierre(int Moneda)
        {
            return _Datos.VentasFuncEscalar("SELECT fechaUltimoCierre FROM tblcontrolperiodocxc WHERE idMoneda=?", Moneda);
        }

        public void VentasMetAuditar(string Entidad, int Id, string Accion, string Detalle, int Usuario)
        {
            _Datos.VentasMetAuditar(Entidad, Id, Accion, Detalle, Usuario);
        }

        public void Dispose()
        {
            _Datos.Dispose();
        }

        // GuardarBorrador: consultas parametrizadas.
        public DataTable VentasFuncGuardarBorradorConsultarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"SELECT idMoneda,estadoCierre
    FROM tblcierremovimientos
    WHERE idCierre=?
    FOR UPDATE";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public object VentasFuncGuardarBorradorConsultarCierremovimientos2(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblcierremovimientos
    WHERE idMoneda=?
      AND tipoCierre=?
      AND fechaDesde=?
      AND fechaHasta=?
      AND estadoCierre IN ('BORRADOR','CONFIRMADO')
      AND idCierre<>?";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public int VentasFuncGuardarBorradorInsertarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblcierremovimientos(numeroCierre,fechaDesde,fechaHasta,fechaCorte,idMoneda,tipoCierre,estadoCierre,observaciones,idUsuarioResponsable)
    VALUES (?,?,?,?,?,?,'BORRADOR',?,?)";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public object VentasFuncGuardarBorradorConsultarCierremovimientos3(params object[] Parametros)
        {
            const string Consulta = @"SELECT numeroCierre
    FROM tblcierremovimientos
    WHERE idCierre=?";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public int VentasFuncGuardarBorradorActualizarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcierremovimientos
    SET tipoCierre=?,fechaDesde=?,fechaHasta=?,fechaCorte=?,observaciones=?
    WHERE idCierre=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        // Confirmar: consultas parametrizadas.
        public object VentasFuncConfirmarConsultarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblcierremovimientos
    WHERE idMoneda=?
      AND tipoCierre=?
      AND fechaDesde=?
      AND fechaHasta=?
      AND estadoCierre='CONFIRMADO'";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public object VentasFuncConfirmarConsultarCierremovimientos2(params object[] Parametros)
        {
            const string Consulta = @"SELECT MIN(fechaDesde)
    FROM tblcierremovimientos
    WHERE idMoneda=?
      AND tipoCierre='MENSUAL'
      AND estadoCierre='CONFIRMADO'";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public object VentasFuncConfirmarConsultarCierremovimientos3(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(DISTINCT MONTH(fechaDesde))
    FROM tblcierremovimientos
    WHERE idMoneda=?
      AND tipoCierre='MENSUAL'
      AND estadoCierre='CONFIRMADO'
      AND YEAR(fechaDesde)=?
      AND MONTH(fechaDesde)>=?";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public object VentasFuncConfirmarConsultarControlperiodocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT fechaUltimoCierre
    FROM tblcontrolperiodocxc
    WHERE idMoneda=?";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public object VentasFuncConfirmarConsultarMovimientocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT MIN(m.fechaMovimiento)
    FROM tblmovimientocxc m
    JOIN tblfactura f ON f.idFactura=m.idFactura
    WHERE f.idMoneda=?";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public object VentasFuncConfirmarConsultarFactura(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblfactura
    WHERE idMoneda=?
      AND fechaEmision BETWEEN ?
      AND ?
      AND estadoFactura='BORRADOR'";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public object VentasFuncConfirmarConsultarPago(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblpago
    WHERE idMoneda=?
      AND fechaPago BETWEEN ?
      AND ?
      AND estadoPago='BORRADOR'";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public DataTable VentasFuncConfirmarConsultarCierremovimientos4(params object[] Parametros)
        {
            const string Consulta = @"SELECT idCierre,numeroCierre
    FROM tblcierremovimientos
    WHERE idMoneda=?
      AND tipoCierre=?
      AND fechaDesde=?
      AND fechaHasta=?
      AND estadoCierre='BORRADOR'
    FOR UPDATE";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public int VentasFuncConfirmarActualizarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcierremovimientos
    SET documentosRevisados=1,idUsuarioResponsable=?
    WHERE idCierre=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncConfirmarInsertarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblcierremovimientos(numeroCierre,fechaDesde,fechaHasta,fechaCorte,idMoneda,tipoCierre,estadoCierre,documentosRevisados,pagosConciliados,idUsuarioResponsable)
    VALUES (?,?,?,?,?,?,'BORRADOR',1,0,?)";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncConfirmarInsertarCierresaldocliente(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblcierresaldocliente(idCierre,idCliente,codigoCliente,nombreCliente,saldoAnterior,cargosPeriodo,abonosPeriodo,cargosAcumulados,abonosAcumulados,saldoActual)
    VALUES (?,?,?,?,?,?,?,?,?,?)";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncConfirmarInsertarCierremovimientosdetalle(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblcierremovimientosdetalle(idCierre,idFactura,saldoInicial,cargosPeriodo,abonosPeriodo,saldoFinal) SELECT ?,m.idFactura,SUM(CASE WHEN m.fechaMovimiento<? THEN m.cargo-m.abono ELSE 0 END),SUM(CASE WHEN m.fechaMovimiento>=? THEN m.cargo ELSE 0 END),SUM(CASE WHEN m.fechaMovimiento>=? THEN m.abono ELSE 0 END),SUM(m.cargo-m.abono)
    FROM tblmovimientocxc m
    JOIN tblfactura f ON f.idFactura=m.idFactura
    WHERE f.idMoneda=?
      AND m.fechaMovimiento<=?
    GROUP BY m.idFactura";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncConfirmarActualizarCierremovimientos2(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcierremovimientos
    SET estadoCierre='CONFIRMADO',fechaConfirmacion=CURRENT_TIMESTAMP
    WHERE idCierre=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncConfirmarActualizarControlperiodocxc(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcontrolperiodocxc
    SET fechaUltimoCierre=?
    WHERE idMoneda=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        // Generales: consultas parametrizadas.
        public DataTable VentasFuncGeneralesEliminarBorradorConsultarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"SELECT idMoneda
    FROM tblcierremovimientos
    WHERE idCierre=?";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncGeneralesEliminarBorradorConsultarCierremovimientos2(params object[] Parametros)
        {
            const string Consulta = @"SELECT estadoCierre
    FROM tblcierremovimientos
    WHERE idCierre=?
    FOR UPDATE";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public int VentasFuncGeneralesEliminarBorradorActualizarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcierremovimientos
    SET estadoCierre='ANULADO',observaciones=?
    WHERE idCierre=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }
    }
}
