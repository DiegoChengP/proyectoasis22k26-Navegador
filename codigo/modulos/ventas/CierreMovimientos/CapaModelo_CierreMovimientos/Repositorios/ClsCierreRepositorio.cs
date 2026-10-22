// Jose Javier Torres Martinez | 0901-23-1091 | 22/10/2026
using System.Data;
using System;
using VentasDatos;

namespace CapaModelo_CierreMovimientos
{
    public sealed class ClsCierreRepositorio : IDisposable
    {
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

        public DataTable VentasMetCatalogo(string Nombre)
        {
            return _Datos.VentasMetCatalogo(Nombre);
        }

        public int VentasFuncObtenerId()
        {
            return _Datos.VentasFuncObtenerId();
        }

        public string VentasMetNumero(string Prefijo)
        {
            return _Datos.VentasMetNumero(Prefijo);
        }

        public bool VentasMetUsuarioActivo(int Usuario)
        {
            return Convert.ToInt32(_Datos.VentasMetEscalar("SELECT COUNT(*) FROM tblusuario WHERE idUsuario=? AND is_active=1", Usuario)) == 1;
        }

        public bool VentasMetMonedaActiva(int Moneda)
        {
            return Convert.ToInt32(_Datos.VentasMetEscalar("SELECT COUNT(*) FROM tblmonedaventas WHERE idMoneda=? AND is_active=1", Moneda)) == 1;
        }

        public void VentasMetAsegurarBloqueoMoneda(int Moneda)
        {
            _Datos.VentasMetEjecutar("INSERT INTO tblcontrolperiodocxc(idMoneda) VALUES (?) ON DUPLICATE KEY UPDATE idMoneda=VALUES(idMoneda)", Moneda);
            _Datos.VentasMetTabla("SELECT idMoneda FROM tblcontrolperiodocxc WHERE idMoneda=? FOR UPDATE", Moneda);
        }

        public object VentasMetUltimoCierre(int Moneda)
        {
            return _Datos.VentasMetEscalar("SELECT fechaUltimoCierre FROM tblcontrolperiodocxc WHERE idMoneda=?", Moneda);
        }

        public void VentasMetAuditar(string Entidad, int Id, string Accion, string Detalle, int Usuario)
        {
            _Datos.VentasMetAuditar(Entidad, Id, Accion, Detalle, Usuario);
        }

        public void Dispose()
        {
            _Datos.Dispose();
        }

        // Saldos: consultas parametrizadas.
        public DataTable VentasMetSaldosConsultarMovimientocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT c.idCliente,c.codigoCliente Codigo,c.nombreCliente Nombre,SUM(CASE WHEN m.fechaMovimiento<? THEN m.cargo-m.abono ELSE 0 END) Anterior,SUM(CASE WHEN m.fechaMovimiento>=? THEN m.cargo ELSE 0 END) Cargos,SUM(CASE WHEN m.fechaMovimiento>=? THEN m.abono ELSE 0 END) Abonos,SUM(CASE WHEN m.fechaMovimiento>=? THEN m.cargo ELSE 0 END) CargosAcumulados,SUM(CASE WHEN m.fechaMovimiento>=? THEN m.abono ELSE 0 END) AbonosAcumulados,SUM(m.cargo-m.abono) Actual
    FROM tblmovimientocxc m
    JOIN tblfactura f ON f.idFactura=m.idFactura
    JOIN tblcliente c ON c.idCliente=f.idCliente
    WHERE f.idMoneda=?
      AND m.fechaMovimiento<=?
    GROUP BY c.idCliente,c.codigoCliente,c.nombreCliente
    ORDER BY c.codigoCliente";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // PrepararTraslado: consultas parametrizadas.
        public DataTable VentasMetPrepararTrasladoConsultarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tblcierremovimientos
    WHERE idCierre=?
      AND estadoCierre='CONFIRMADO'
    FOR UPDATE";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public object VentasMetPrepararTrasladoConsultarTrasladocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tbltrasladocxc
    WHERE idCierre=?";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public int VentasMetPrepararTrasladoInsertarTrasladocxc(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tbltrasladocxc(idCierre,claveIntegracion,idUsuario)
    VALUES (?,?,?)";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public int VentasMetPrepararTrasladoInsertarTrasladocxcdetalle(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tbltrasladocxcdetalle(idCierre,numeroLinea,cuenta,descripcion,debe,haber)
    VALUES (?,?,?,?,?,?)";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public DataTable VentasMetPrepararTrasladoConsultarTrasladocxc2(params object[] Parametros)
        {
            const string Consulta = @"SELECT t.*,c.idMoneda,c.fechaHasta
    FROM tbltrasladocxc t
    JOIN tblcierremovimientos c ON c.idCierre=t.idCierre
    WHERE t.idCierre=?";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public DataTable VentasMetPrepararTrasladoConsultarTrasladocxcdetalle(params object[] Parametros)
        {
            const string Consulta = @"SELECT cuenta Cuenta,descripcion Descripcion,debe Debe,haber Haber
    FROM tbltrasladocxcdetalle
    WHERE idCierre=?
    ORDER BY numeroLinea";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // Partida: consultas parametrizadas.
        public DataTable VentasMetPartidaConsultarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"SELECT m.tipoMovimiento,SUM(m.cargo) Cargos,SUM(m.abono) Abonos,p.cuentaClientes,p.cuentaContrapartida
    FROM tblcierremovimientos c
    JOIN tblfactura f ON f.idMoneda=c.idMoneda
    JOIN tblmovimientocxc m ON m.idFactura=f.idFactura
    LEFT JOIN tblcuentacontablecxc p ON p.idMoneda=c.idMoneda
      AND p.tipoMovimiento=m.tipoMovimiento
    WHERE c.idCierre=?
      AND c.estadoCierre='CONFIRMADO'
      AND m.fechaMovimiento BETWEEN c.fechaDesde
      AND c.fechaHasta
    GROUP BY m.tipoMovimiento,p.cuentaClientes,p.cuentaContrapartida
    ORDER BY m.tipoMovimiento";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // Listar: consultas parametrizadas.
        public DataTable VentasMetListarConsultarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"SELECT c.idCierre Id,c.idMoneda,c.numeroCierre Numero,m.codigoMoneda Moneda,c.tipoCierre Tipo,c.fechaDesde Desde,c.fechaHasta Hasta,c.estadoCierre Estado,COALESCE(c.observaciones,'') Observaciones
    FROM tblcierremovimientos c
    JOIN tblmonedaventas m ON m.idMoneda=c.idMoneda
    WHERE (?=0 OR c.idMoneda=?)
    ORDER BY c.fechaDesde DESC,c.idCierre DESC";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // Historial: consultas parametrizadas.
        public DataTable VentasMetHistorialConsultarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"SELECT h.idCierre,h.numeroCierre,CONCAT(h.tipoCierre,' ',DATE_FORMAT(h.fechaDesde,'%Y-%m')) Periodo,c.codigoCliente Codigo,c.nombreCliente Nombre,c.saldoAnterior Anterior,c.cargosPeriodo Cargos,c.abonosPeriodo Abonos,c.cargosAcumulados CargosAcumulados,c.abonosAcumulados AbonosAcumulados,c.saldoActual Actual,h.estadoCierre Estado
    FROM tblcierremovimientos h
    JOIN tblcierresaldocliente c ON c.idCierre=h.idCierre
    WHERE h.idMoneda=?
      AND (?=0 OR c.idCliente=?)
      AND DATE_FORMAT(h.fechaDesde,'%Y-%m') LIKE CONCAT(?,'%')
    ORDER BY h.fechaDesde DESC,h.idCierre,c.codigoCliente";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // GuardarBorrador: consultas parametrizadas.
        public DataTable VentasMetGuardarBorradorConsultarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"SELECT idMoneda,estadoCierre
    FROM tblcierremovimientos
    WHERE idCierre=?
    FOR UPDATE";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public object VentasMetGuardarBorradorConsultarCierremovimientos2(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblcierremovimientos
    WHERE idMoneda=?
      AND tipoCierre=?
      AND fechaDesde=?
      AND fechaHasta=?
      AND estadoCierre IN ('BORRADOR','CONFIRMADO')
      AND idCierre<>?";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public int VentasMetGuardarBorradorInsertarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblcierremovimientos(numeroCierre,fechaDesde,fechaHasta,fechaCorte,idMoneda,tipoCierre,estadoCierre,observaciones,idUsuarioResponsable)
    VALUES (?,?,?,?,?,?,'BORRADOR',?,?)";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public object VentasMetGuardarBorradorConsultarCierremovimientos3(params object[] Parametros)
        {
            const string Consulta = @"SELECT numeroCierre
    FROM tblcierremovimientos
    WHERE idCierre=?";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public int VentasMetGuardarBorradorActualizarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcierremovimientos
    SET tipoCierre=?,fechaDesde=?,fechaHasta=?,fechaCorte=?,observaciones=?
    WHERE idCierre=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        // EstadoTraslado: consultas parametrizadas.
        public DataTable VentasMetEstadoTrasladoConsultarTrasladocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tbltrasladocxc
    WHERE idCierre=?";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // EstadoPeriodo: consultas parametrizadas.
        public DataTable VentasMetEstadoPeriodoConsultarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tblcierremovimientos
    WHERE idMoneda=?
      AND tipoCierre=?
      AND fechaDesde=?
      AND fechaHasta=?
      AND estadoCierre='CONFIRMADO'";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // ConfirmarTraslado: consultas parametrizadas.
        public DataTable VentasMetConfirmarTrasladoConsultarTrasladocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tbltrasladocxc
    WHERE idCierre=?
    FOR UPDATE";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public int VentasMetConfirmarTrasladoActualizarTrasladocxc(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tbltrasladocxc
    SET estado='CONFIRMADO',referenciaContable=?,fechaConfirmacion=CURRENT_TIMESTAMP
    WHERE idCierre=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        // Confirmar: consultas parametrizadas.
        public object VentasMetConfirmarConsultarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblcierremovimientos
    WHERE idMoneda=?
      AND tipoCierre=?
      AND fechaDesde=?
      AND fechaHasta=?
      AND estadoCierre='CONFIRMADO'";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public object VentasMetConfirmarConsultarCierremovimientos2(params object[] Parametros)
        {
            const string Consulta = @"SELECT MIN(fechaDesde)
    FROM tblcierremovimientos
    WHERE idMoneda=?
      AND tipoCierre='MENSUAL'
      AND estadoCierre='CONFIRMADO'";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public object VentasMetConfirmarConsultarCierremovimientos3(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(DISTINCT MONTH(fechaDesde))
    FROM tblcierremovimientos
    WHERE idMoneda=?
      AND tipoCierre='MENSUAL'
      AND estadoCierre='CONFIRMADO'
      AND YEAR(fechaDesde)=?
      AND MONTH(fechaDesde)>=?";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public object VentasMetConfirmarConsultarControlperiodocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT fechaUltimoCierre
    FROM tblcontrolperiodocxc
    WHERE idMoneda=?";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public object VentasMetConfirmarConsultarMovimientocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT MIN(m.fechaMovimiento)
    FROM tblmovimientocxc m
    JOIN tblfactura f ON f.idFactura=m.idFactura
    WHERE f.idMoneda=?";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public object VentasMetConfirmarConsultarFactura(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblfactura
    WHERE idMoneda=?
      AND fechaEmision BETWEEN ?
      AND ?
      AND estadoFactura='BORRADOR'";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public object VentasMetConfirmarConsultarPago(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblpago
    WHERE idMoneda=?
      AND fechaPago BETWEEN ?
      AND ?
      AND estadoPago='BORRADOR'";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public DataTable VentasMetConfirmarConsultarCierremovimientos4(params object[] Parametros)
        {
            const string Consulta = @"SELECT idCierre,numeroCierre
    FROM tblcierremovimientos
    WHERE idMoneda=?
      AND tipoCierre=?
      AND fechaDesde=?
      AND fechaHasta=?
      AND estadoCierre='BORRADOR'
    FOR UPDATE";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public int VentasMetConfirmarActualizarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcierremovimientos
    SET documentosRevisados=1,idUsuarioResponsable=?
    WHERE idCierre=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public int VentasMetConfirmarInsertarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblcierremovimientos(numeroCierre,fechaDesde,fechaHasta,fechaCorte,idMoneda,tipoCierre,estadoCierre,documentosRevisados,pagosConciliados,idUsuarioResponsable)
    VALUES (?,?,?,?,?,?,'BORRADOR',1,0,?)";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public int VentasMetConfirmarInsertarCierresaldocliente(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblcierresaldocliente(idCierre,idCliente,codigoCliente,nombreCliente,saldoAnterior,cargosPeriodo,abonosPeriodo,cargosAcumulados,abonosAcumulados,saldoActual)
    VALUES (?,?,?,?,?,?,?,?,?,?)";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public int VentasMetConfirmarInsertarCierremovimientosdetalle(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblcierremovimientosdetalle(idCierre,idFactura,saldoInicial,cargosPeriodo,abonosPeriodo,saldoFinal) SELECT ?,m.idFactura,SUM(CASE WHEN m.fechaMovimiento<? THEN m.cargo-m.abono ELSE 0 END),SUM(CASE WHEN m.fechaMovimiento>=? THEN m.cargo ELSE 0 END),SUM(CASE WHEN m.fechaMovimiento>=? THEN m.abono ELSE 0 END),SUM(m.cargo-m.abono)
    FROM tblmovimientocxc m
    JOIN tblfactura f ON f.idFactura=m.idFactura
    WHERE f.idMoneda=?
      AND m.fechaMovimiento<=?
    GROUP BY m.idFactura";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public int VentasMetConfirmarActualizarCierremovimientos2(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcierremovimientos
    SET estadoCierre='CONFIRMADO',fechaConfirmacion=CURRENT_TIMESTAMP
    WHERE idCierre=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public int VentasMetConfirmarActualizarControlperiodocxc(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcontrolperiodocxc
    SET fechaUltimoCierre=?
    WHERE idMoneda=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        // Generales: consultas parametrizadas.
        public DataTable VentasMetGeneralesEliminarBorradorConsultarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"SELECT idMoneda
    FROM tblcierremovimientos
    WHERE idCierre=?";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public DataTable VentasMetGeneralesEliminarBorradorConsultarCierremovimientos2(params object[] Parametros)
        {
            const string Consulta = @"SELECT estadoCierre
    FROM tblcierremovimientos
    WHERE idCierre=?
    FOR UPDATE";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public int VentasMetGeneralesEliminarBorradorActualizarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcierremovimientos
    SET estadoCierre='ANULADO',observaciones=?
    WHERE idCierre=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }
    }
}
