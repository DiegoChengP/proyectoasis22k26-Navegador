using System;

namespace CapaModelo_CierreMovimientos
{
    public enum ConsultaCierre
    {
        SaldosConsultarMovimientocxc,
        EstadoPeriodoConsultarCierremovimientos,
        ConfirmarConsultarCierremovimientos,
        ConfirmarConsultarCierremovimientos2,
        ConfirmarConsultarCierremovimientos3,
        ConfirmarConsultarControlperiodocxc,
        ConfirmarConsultarMovimientocxc,
        ConfirmarConsultarFactura,
        ConfirmarConsultarPago,
        ConfirmarConsultarCierremovimientos4,
        ConfirmarActualizarCierremovimientos,
        ConfirmarInsertarCierremovimientos,
        ConfirmarInsertarCierresaldocliente,
        ConfirmarInsertarCierremovimientosdetalle,
        ConfirmarActualizarCierremovimientos2,
        ConfirmarActualizarControlperiodocxc,
        HistorialConsultarCierremovimientos,
        EstadoTrasladoConsultarTrasladocxc,
        PrepararTrasladoConsultarCierremovimientos,
        PrepararTrasladoConsultarTrasladocxc,
        PrepararTrasladoInsertarTrasladocxc,
        PrepararTrasladoInsertarTrasladocxcdetalle,
        PrepararTrasladoConsultarTrasladocxc2,
        PrepararTrasladoConsultarTrasladocxcdetalle,
        ConfirmarTrasladoConsultarTrasladocxc,
        ConfirmarTrasladoActualizarTrasladocxc,
        PartidaConsultarCierremovimientos,
        ListarConsultarCierremovimientos,
        GuardarBorradorConsultarCierremovimientos,
        GuardarBorradorConsultarCierremovimientos2,
        GuardarBorradorInsertarCierremovimientos,
        GuardarBorradorConsultarCierremovimientos3,
        GuardarBorradorActualizarCierremovimientos,
        EliminarBorradorConsultarCierremovimientos,
        EliminarBorradorConsultarCierremovimientos2,
        EliminarBorradorActualizarCierremovimientos,
    }

    internal static class SqlCierre
    {
        internal static string Obtener(ConsultaCierre Consulta)
        {
            switch (Consulta)
            {
                case ConsultaCierre.SaldosConsultarMovimientocxc: return "SELECT c.idCliente,c.codigoCliente Codigo,c.nombreCliente Nombre,SUM(CASE WHEN m.fechaMovimiento<? THEN m.cargo-m.abono ELSE 0 END) Anterior,SUM(CASE WHEN m.fechaMovimiento>=? THEN m.cargo ELSE 0 END) Cargos,SUM(CASE WHEN m.fechaMovimiento>=? THEN m.abono ELSE 0 END) Abonos,SUM(CASE WHEN m.fechaMovimiento>=? THEN m.cargo ELSE 0 END) CargosAcumulados,SUM(CASE WHEN m.fechaMovimiento>=? THEN m.abono ELSE 0 END) AbonosAcumulados,SUM(m.cargo-m.abono) Actual FROM tblmovimientocxc m JOIN tblfactura f ON f.idFactura=m.idFactura JOIN tblcliente c ON c.idCliente=f.idCliente WHERE f.idMoneda=? AND m.fechaMovimiento<=? GROUP BY c.idCliente,c.codigoCliente,c.nombreCliente ORDER BY c.codigoCliente";
                case ConsultaCierre.EstadoPeriodoConsultarCierremovimientos: return "SELECT * FROM tblcierremovimientos WHERE idMoneda=? AND tipoCierre=? AND fechaDesde=? AND fechaHasta=? AND estadoCierre='CONFIRMADO'";
                case ConsultaCierre.ConfirmarConsultarCierremovimientos: return "SELECT COUNT(*) FROM tblcierremovimientos WHERE idMoneda=? AND tipoCierre=? AND fechaDesde=? AND fechaHasta=? AND estadoCierre='CONFIRMADO'";
                case ConsultaCierre.ConfirmarConsultarCierremovimientos2: return "SELECT MIN(fechaDesde) FROM tblcierremovimientos WHERE idMoneda=? AND tipoCierre='MENSUAL' AND estadoCierre='CONFIRMADO'";
                case ConsultaCierre.ConfirmarConsultarCierremovimientos3: return "SELECT COUNT(DISTINCT MONTH(fechaDesde)) FROM tblcierremovimientos WHERE idMoneda=? AND tipoCierre='MENSUAL' AND estadoCierre='CONFIRMADO' AND YEAR(fechaDesde)=? AND MONTH(fechaDesde)>=?";
                case ConsultaCierre.ConfirmarConsultarControlperiodocxc: return "SELECT fechaUltimoCierre FROM tblcontrolperiodocxc WHERE idMoneda=?";
                case ConsultaCierre.ConfirmarConsultarMovimientocxc: return "SELECT MIN(m.fechaMovimiento) FROM tblmovimientocxc m JOIN tblfactura f ON f.idFactura=m.idFactura WHERE f.idMoneda=?";
                case ConsultaCierre.ConfirmarConsultarFactura: return "SELECT COUNT(*) FROM tblfactura WHERE idMoneda=? AND fechaEmision BETWEEN ? AND ? AND estadoFactura='BORRADOR'";
                case ConsultaCierre.ConfirmarConsultarPago: return "SELECT COUNT(*) FROM tblpago WHERE idMoneda=? AND fechaPago BETWEEN ? AND ? AND estadoPago='BORRADOR'";
                case ConsultaCierre.ConfirmarConsultarCierremovimientos4: return "SELECT idCierre,numeroCierre FROM tblcierremovimientos WHERE idMoneda=? AND tipoCierre=? AND fechaDesde=? AND fechaHasta=? AND estadoCierre='BORRADOR' FOR UPDATE";
                case ConsultaCierre.ConfirmarActualizarCierremovimientos: return "UPDATE tblcierremovimientos SET documentosRevisados=1,idUsuarioResponsable=? WHERE idCierre=?";
                case ConsultaCierre.ConfirmarInsertarCierremovimientos: return "INSERT INTO tblcierremovimientos(numeroCierre,fechaDesde,fechaHasta,fechaCorte,idMoneda,tipoCierre,estadoCierre,documentosRevisados,pagosConciliados,idUsuarioResponsable) VALUES (?,?,?,?,?,?,'BORRADOR',1,0,?)";
                case ConsultaCierre.ConfirmarInsertarCierresaldocliente: return "INSERT INTO tblcierresaldocliente(idCierre,idCliente,codigoCliente,nombreCliente,saldoAnterior,cargosPeriodo,abonosPeriodo,cargosAcumulados,abonosAcumulados,saldoActual) VALUES (?,?,?,?,?,?,?,?,?,?)";
                case ConsultaCierre.ConfirmarInsertarCierremovimientosdetalle: return "INSERT INTO tblcierremovimientosdetalle(idCierre,idFactura,saldoInicial,cargosPeriodo,abonosPeriodo,saldoFinal) SELECT ?,m.idFactura,SUM(CASE WHEN m.fechaMovimiento<? THEN m.cargo-m.abono ELSE 0 END),SUM(CASE WHEN m.fechaMovimiento>=? THEN m.cargo ELSE 0 END),SUM(CASE WHEN m.fechaMovimiento>=? THEN m.abono ELSE 0 END),SUM(m.cargo-m.abono) FROM tblmovimientocxc m JOIN tblfactura f ON f.idFactura=m.idFactura WHERE f.idMoneda=? AND m.fechaMovimiento<=? GROUP BY m.idFactura";
                case ConsultaCierre.ConfirmarActualizarCierremovimientos2: return "UPDATE tblcierremovimientos SET estadoCierre='CONFIRMADO',fechaConfirmacion=CURRENT_TIMESTAMP WHERE idCierre=?";
                case ConsultaCierre.ConfirmarActualizarControlperiodocxc: return "UPDATE tblcontrolperiodocxc SET fechaUltimoCierre=? WHERE idMoneda=?";
                case ConsultaCierre.HistorialConsultarCierremovimientos: return "SELECT h.idCierre,h.numeroCierre,CONCAT(h.tipoCierre,' ',DATE_FORMAT(h.fechaDesde,'%Y-%m')) Periodo,c.codigoCliente Codigo,c.nombreCliente Nombre,c.saldoAnterior Anterior,c.cargosPeriodo Cargos,c.abonosPeriodo Abonos,c.cargosAcumulados CargosAcumulados,c.abonosAcumulados AbonosAcumulados,c.saldoActual Actual,h.estadoCierre Estado FROM tblcierremovimientos h JOIN tblcierresaldocliente c ON c.idCierre=h.idCierre WHERE h.idMoneda=? AND (?=0 OR c.idCliente=?) AND DATE_FORMAT(h.fechaDesde,'%Y-%m') LIKE CONCAT(?,'%') ORDER BY h.fechaDesde DESC,h.idCierre,c.codigoCliente";
                case ConsultaCierre.EstadoTrasladoConsultarTrasladocxc: return "SELECT * FROM tbltrasladocxc WHERE idCierre=?";
                case ConsultaCierre.PrepararTrasladoConsultarCierremovimientos: return "SELECT * FROM tblcierremovimientos WHERE idCierre=? AND estadoCierre='CONFIRMADO' FOR UPDATE";
                case ConsultaCierre.PrepararTrasladoConsultarTrasladocxc: return "SELECT COUNT(*) FROM tbltrasladocxc WHERE idCierre=?";
                case ConsultaCierre.PrepararTrasladoInsertarTrasladocxc: return "INSERT INTO tbltrasladocxc(idCierre,claveIntegracion,idUsuario) VALUES (?,?,?)";
                case ConsultaCierre.PrepararTrasladoInsertarTrasladocxcdetalle: return "INSERT INTO tbltrasladocxcdetalle(idCierre,numeroLinea,cuenta,descripcion,debe,haber) VALUES (?,?,?,?,?,?)";
                case ConsultaCierre.PrepararTrasladoConsultarTrasladocxc2: return "SELECT t.*,c.idMoneda,c.fechaHasta FROM tbltrasladocxc t JOIN tblcierremovimientos c ON c.idCierre=t.idCierre WHERE t.idCierre=?";
                case ConsultaCierre.PrepararTrasladoConsultarTrasladocxcdetalle: return "SELECT cuenta Cuenta,descripcion Descripcion,debe Debe,haber Haber FROM tbltrasladocxcdetalle WHERE idCierre=? ORDER BY numeroLinea";
                case ConsultaCierre.ConfirmarTrasladoConsultarTrasladocxc: return "SELECT * FROM tbltrasladocxc WHERE idCierre=? FOR UPDATE";
                case ConsultaCierre.ConfirmarTrasladoActualizarTrasladocxc: return "UPDATE tbltrasladocxc SET estado='CONFIRMADO',referenciaContable=?,fechaConfirmacion=CURRENT_TIMESTAMP WHERE idCierre=?";
                case ConsultaCierre.PartidaConsultarCierremovimientos: return "SELECT m.tipoMovimiento,SUM(m.cargo) Cargos,SUM(m.abono) Abonos,p.cuentaClientes,p.cuentaContrapartida FROM tblcierremovimientos c JOIN tblfactura f ON f.idMoneda=c.idMoneda JOIN tblmovimientocxc m ON m.idFactura=f.idFactura LEFT JOIN tblcuentacontablecxc p ON p.idMoneda=c.idMoneda AND p.tipoMovimiento=m.tipoMovimiento WHERE c.idCierre=? AND c.estadoCierre='CONFIRMADO' AND m.fechaMovimiento BETWEEN c.fechaDesde AND c.fechaHasta GROUP BY m.tipoMovimiento,p.cuentaClientes,p.cuentaContrapartida ORDER BY m.tipoMovimiento";
                case ConsultaCierre.ListarConsultarCierremovimientos: return "SELECT c.idCierre Id,c.idMoneda,c.numeroCierre Numero,m.codigoMoneda Moneda,c.tipoCierre Tipo,c.fechaDesde Desde,c.fechaHasta Hasta,c.estadoCierre Estado,COALESCE(c.observaciones,'') Observaciones FROM tblcierremovimientos c JOIN tblmonedaventas m ON m.idMoneda=c.idMoneda WHERE (?=0 OR c.idMoneda=?) ORDER BY c.fechaDesde DESC,c.idCierre DESC";
                case ConsultaCierre.GuardarBorradorConsultarCierremovimientos: return "SELECT idMoneda,estadoCierre FROM tblcierremovimientos WHERE idCierre=? FOR UPDATE";
                case ConsultaCierre.GuardarBorradorConsultarCierremovimientos2: return "SELECT COUNT(*) FROM tblcierremovimientos WHERE idMoneda=? AND tipoCierre=? AND fechaDesde=? AND fechaHasta=? AND estadoCierre IN ('BORRADOR','CONFIRMADO') AND idCierre<>?";
                case ConsultaCierre.GuardarBorradorInsertarCierremovimientos: return "INSERT INTO tblcierremovimientos(numeroCierre,fechaDesde,fechaHasta,fechaCorte,idMoneda,tipoCierre,estadoCierre,observaciones,idUsuarioResponsable) VALUES (?,?,?,?,?,?,'BORRADOR',?,?)";
                case ConsultaCierre.GuardarBorradorConsultarCierremovimientos3: return "SELECT numeroCierre FROM tblcierremovimientos WHERE idCierre=?";
                case ConsultaCierre.GuardarBorradorActualizarCierremovimientos: return "UPDATE tblcierremovimientos SET tipoCierre=?,fechaDesde=?,fechaHasta=?,fechaCorte=?,observaciones=? WHERE idCierre=?";
                case ConsultaCierre.EliminarBorradorConsultarCierremovimientos: return "SELECT idMoneda FROM tblcierremovimientos WHERE idCierre=?";
                case ConsultaCierre.EliminarBorradorConsultarCierremovimientos2: return "SELECT estadoCierre FROM tblcierremovimientos WHERE idCierre=? FOR UPDATE";
                case ConsultaCierre.EliminarBorradorActualizarCierremovimientos: return "UPDATE tblcierremovimientos SET estadoCierre='ANULADO',observaciones=? WHERE idCierre=?";
                default: throw new ArgumentOutOfRangeException("Consulta");
            }
        }
    }
}
