using System;

namespace CapaModelo_MovimientosClientes
{
    public enum ConsultaMovimientos
    {
        LeerMovimiento,
        LeerAplicacionesRecibo,
        LeerRecibo,
        LeerNota,
        LeerFactura,
        EstadoCuentaConsultarMovimientocxc,
        EstadoCuentaConsultarMovimientocxc2,
        PendientesConsultarFactura,
        MovimientoInsertarMovimientocxc,
        RegistrarDocumentoConsultarCliente,
        RegistrarDocumentoConsultarEstablecimientoventas,
        RegistrarDocumentoInsertarFactura,
        RegistrarDocumentoConsultarFactura,
        RegistrarDocumentoInsertarNotacxc,
        SaldoConsultarMovimientocxc,
        SaldoConsultarMovimientocxc2,
        DisponibleConsultarMovimientocxc,
        RegistrarReciboConsultarCliente,
        RegistrarReciboConsultarFactura,
        RegistrarReciboInsertarPago,
        RegistrarReciboInsertarAplicacionpago,
        ConsultarRecibosConsultarPago,
        AplicacionesConsultarAplicacionpago,
        DocumentoConsultarFactura,
        DocumentoConsultarNotacxc,
        DepositoConsultarDepositoventas,
        DepositoConsultarDepositoventasdetalle,
        RecibosDisponiblesConsultarPago,
        PrepararDepositoConsultarPago,
        PrepararDepositoConsultarDepositoventasdetalle,
        PrepararDepositoInsertarDepositoventas,
        PrepararDepositoInsertarDepositoventasdetalle,
        AnularReciboConsultarPago,
        AnularReciboConsultarPago2,
        AnularReciboConsultarDepositoventasdetalle,
        AnularReciboConsultarMovimientocxc,
        AnularReciboActualizarAplicacionpago,
        AnularReciboActualizarPago,
        ListarDocumentosConsultarFactura,
        ListarRecibosConsultarPago,
        BloquearDocumentoConsultarFactura,
        BloquearDocumentoConsultarNotacxc,
        BloquearDocumentoConsultarFactura2,
        BloquearDocumentoConsultarNotacxc2,
        ModificarDocumentoActualizarFactura,
        ModificarDocumentoActualizarNotacxc,
        AnularDocumentoConsultarFactura,
        AnularDocumentoConsultarAplicacionpago,
        AnularDocumentoConsultarNotacxc,
        AnularDocumentoConsultarMovimientocxc,
        AnularDocumentoConsultarMovimientocxc2,
        AnularDocumentoConsultarMovimientocxc3,
        AnularDocumentoActualizarFactura,
        AnularDocumentoActualizarNotacxc,
        ModificarReciboConsultarPago,
        ModificarReciboConsultarPago2,
        ModificarReciboActualizarPago,
    }

    internal static class SqlMovimientos
    {
        internal static string Obtener(ConsultaMovimientos Consulta)
        {
            switch (Consulta)
            {
                case ConsultaMovimientos.EstadoCuentaConsultarMovimientocxc: return "SELECT COALESCE(SUM(CASE WHEN m.fechaMovimiento<? THEN m.cargo-m.abono ELSE 0 END),0) Inicial,COALESCE(SUM(CASE WHEN m.fechaMovimiento>=? THEN m.cargo ELSE 0 END),0) Cargos,COALESCE(SUM(CASE WHEN m.fechaMovimiento>=? THEN m.abono ELSE 0 END),0) Abonos FROM tblmovimientocxc m JOIN tblfactura f ON f.idFactura=m.idFactura WHERE f.idCliente=? AND f.idMoneda=? AND m.fechaMovimiento<=?";
                case ConsultaMovimientos.EstadoCuentaConsultarMovimientocxc2: return "SELECT m.idMovimiento,m.fechaMovimiento Fecha,CONCAT(f.serieFactura,'-',f.numeroFactura) Documento,m.tipoMovimiento Tipo,m.descripcionMovimiento Descripcion,m.cargo Cargo,m.abono Abono,CASE WHEN c.fechaUltimoCierre IS NOT NULL AND m.fechaMovimiento<=c.fechaUltimoCierre THEN 'CERRADO' ELSE 'APLICADO' END Estado FROM tblmovimientocxc m JOIN tblfactura f ON f.idFactura=m.idFactura LEFT JOIN tblcontrolperiodocxc c ON c.idMoneda=f.idMoneda WHERE f.idCliente=? AND f.idMoneda=? AND m.fechaMovimiento BETWEEN ? AND ? ORDER BY m.fechaMovimiento,m.idMovimiento";
                case ConsultaMovimientos.PendientesConsultarFactura: return "SELECT f.idFactura,CONCAT(f.serieFactura,'-',f.numeroFactura) Documento,f.fechaEmision Fecha,f.fechaVencimiento Vencimiento,mo.codigoMoneda Moneda,SUM(m.cargo-m.abono) Saldo FROM tblfactura f JOIN tblmovimientocxc m ON m.idFactura=f.idFactura JOIN tblmonedaventas mo ON mo.idMoneda=f.idMoneda WHERE f.idCliente=? AND f.idMoneda=? GROUP BY f.idFactura,f.serieFactura,f.numeroFactura,f.fechaEmision,f.fechaVencimiento,mo.codigoMoneda HAVING SUM(m.cargo-m.abono)>0 ORDER BY f.fechaVencimiento,f.idFactura";
                case ConsultaMovimientos.MovimientoInsertarMovimientocxc: return "INSERT INTO tblmovimientocxc(idFactura,fechaMovimiento,tipoMovimiento,descripcionMovimiento,cargo,abono,idUsuarioRegistro,idAplicacionPago,idNota,idMovimientoOrigen) VALUES (?,?,?,?,?,?,?,?,?,?)";
                case ConsultaMovimientos.RegistrarDocumentoConsultarCliente: return "SELECT * FROM tblcliente WHERE idCliente=? AND is_active=1";
                case ConsultaMovimientos.RegistrarDocumentoConsultarEstablecimientoventas: return "SELECT COUNT(*) FROM tblestablecimientoventas WHERE idEstablecimiento=? AND is_active=1";
                case ConsultaMovimientos.RegistrarDocumentoInsertarFactura: return "INSERT INTO tblfactura(serieFactura,numeroFactura,idEstablecimiento,idCliente,idMoneda,fechaEmision,fechaVencimiento,tipoDte,condicionPago,diasCreditoAplicados,tipoIdentificacionReceptor,identificacionReceptor,nombreReceptor,direccionReceptor,subtotal,total,estadoFactura,observaciones,idUsuarioRegistro) VALUES ('CXC',?,?,?,?,?,?,'INTERNO_CXC',?,?,?,?,?,?,?,?,'EMITIDA',?,?)";
                case ConsultaMovimientos.RegistrarDocumentoConsultarFactura: return "SELECT f.idFactura,f.fechaEmision FROM tblfactura f WHERE f.idCliente=? AND f.idMoneda=? AND CONCAT(f.serieFactura,'-',f.numeroFactura)=? AND f.estadoFactura='EMITIDA' FOR UPDATE";
                case ConsultaMovimientos.RegistrarDocumentoInsertarNotacxc: return "INSERT INTO tblnotacxc(numeroNota,idFactura,tipoNota,fechaNota,monto,motivo,estadoNota,idUsuarioRegistro) VALUES (?,?,?,?,?,?,'APLICADA',?)";
                case ConsultaMovimientos.SaldoConsultarMovimientocxc: return "SELECT COALESCE(SUM(cargo-abono),0) FROM tblmovimientocxc WHERE idFactura=? AND fechaMovimiento<=?";
                case ConsultaMovimientos.SaldoConsultarMovimientocxc2: return "SELECT COALESCE(SUM(cargo-abono),0) FROM tblmovimientocxc WHERE idFactura=?";
                case ConsultaMovimientos.DisponibleConsultarMovimientocxc: return "SELECT MIN(Saldo) FROM (SELECT fechaMovimiento,SUM(cargo-abono) OVER (ORDER BY fechaMovimiento,idMovimiento ROWS UNBOUNDED PRECEDING) Saldo FROM tblmovimientocxc WHERE idFactura=?) s WHERE fechaMovimiento>?";
                case ConsultaMovimientos.RegistrarReciboConsultarCliente: return "SELECT COUNT(*) FROM tblcliente WHERE idCliente=? AND is_active=1";
                case ConsultaMovimientos.RegistrarReciboConsultarFactura: return "SELECT fechaEmision FROM tblfactura WHERE idFactura=? AND idCliente=? AND idMoneda=? AND estadoFactura='EMITIDA' FOR UPDATE";
                case ConsultaMovimientos.RegistrarReciboInsertarPago: return "INSERT INTO tblpago(numeroRecibo,idCliente,idMoneda,fechaPago,montoPago,tipoCambio,formaPago,referenciaPago,estadoPago,observaciones,idUsuarioRegistro) VALUES (?,?,?,?,?,?,?,?,'CONFIRMADO',?,?)";
                case ConsultaMovimientos.RegistrarReciboInsertarAplicacionpago: return "INSERT INTO tblaplicacionpago(idPago,idFactura,fechaAplicacion,montoAplicado,estadoAplicacion,idUsuarioRegistro) VALUES (?,?,?,?,'APLICADA',?)";
                case ConsultaMovimientos.ConsultarRecibosConsultarPago: return "SELECT p.idPago,p.numeroRecibo Recibo,p.fechaPago Fecha,c.nombreCliente Cliente,mo.codigoMoneda Moneda,p.montoPago Monto,p.estadoPago Estado,COALESCE(SUM(dd.importe),0) Depositado,COALESCE(GROUP_CONCAT(DISTINCT d.codigoDeposito ORDER BY d.codigoDeposito SEPARATOR ', '),'') Deposito,COALESCE(GROUP_CONCAT(DISTINCT d.movimientoBancario SEPARATOR ', '),'') MovimientoBancario FROM tblpago p JOIN tblcliente c ON c.idCliente=p.idCliente JOIN tblmonedaventas mo ON mo.idMoneda=p.idMoneda LEFT JOIN tbldepositoventasdetalle dd ON dd.idPago=p.idPago LEFT JOIN tbldepositoventas d ON d.idDeposito=dd.idDeposito WHERE (?='' OR p.numeroRecibo=?) AND (?='' OR EXISTS(SELECT 1 FROM tbldepositoventasdetalle x JOIN tbldepositoventas y ON y.idDeposito=x.idDeposito WHERE x.idPago=p.idPago AND y.codigoDeposito=?)) GROUP BY p.idPago,p.numeroRecibo,p.fechaPago,c.nombreCliente,mo.codigoMoneda,p.montoPago,p.estadoPago ORDER BY p.fechaPago DESC,p.idPago DESC";
                case ConsultaMovimientos.AplicacionesConsultarAplicacionpago: return "SELECT CONCAT(f.serieFactura,'-',f.numeroFactura) Factura,a.fechaAplicacion Fecha,a.montoAplicado Importe,a.estadoAplicacion Estado FROM tblaplicacionpago a JOIN tblfactura f ON f.idFactura=a.idFactura WHERE a.idPago=? ORDER BY a.idAplicacionPago";
                case ConsultaMovimientos.DocumentoConsultarFactura: return "SELECT CONCAT(serieFactura,'-',numeroFactura) Documento,fechaEmision Fecha,fechaVencimiento Vencimiento,nombreReceptor Cliente,total Importe,estadoFactura Estado,observaciones Descripcion FROM tblfactura WHERE idCliente=? AND idMoneda=? AND idEstablecimiento=? AND serieFactura='CXC' AND numeroFactura=?";
                case ConsultaMovimientos.DocumentoConsultarNotacxc: return "SELECT n.numeroNota Documento,n.fechaNota Fecha,n.tipoNota Tipo,f.nombreReceptor Cliente,n.monto Importe,n.estadoNota Estado,n.motivo Descripcion FROM tblnotacxc n JOIN tblfactura f ON f.idFactura=n.idFactura WHERE f.idCliente=? AND f.idMoneda=? AND n.numeroNota=?";
                case ConsultaMovimientos.DepositoConsultarDepositoventas: return "SELECT * FROM tbldepositoventas WHERE codigoDeposito=?";
                case ConsultaMovimientos.DepositoConsultarDepositoventasdetalle: return "SELECT p.numeroRecibo Recibo,c.nombreCliente Cliente,dd.importe Importe FROM tbldepositoventasdetalle dd JOIN tblpago p ON p.idPago=dd.idPago JOIN tblcliente c ON c.idCliente=p.idCliente WHERE dd.idDeposito=? ORDER BY p.idPago";
                case ConsultaMovimientos.RecibosDisponiblesConsultarPago: return "SELECT p.idPago,p.numeroRecibo Recibo,c.nombreCliente Cliente,mo.codigoMoneda Moneda,p.montoPago Monto,COALESCE(SUM(dd.importe),0) Depositado,p.montoPago-COALESCE(SUM(dd.importe),0) Disponible FROM tblpago p JOIN tblcliente c ON c.idCliente=p.idCliente JOIN tblmonedaventas mo ON mo.idMoneda=p.idMoneda LEFT JOIN tbldepositoventasdetalle dd ON dd.idPago=p.idPago WHERE p.idMoneda=? AND p.estadoPago='CONFIRMADO' GROUP BY p.idPago,p.numeroRecibo,c.nombreCliente,mo.codigoMoneda,p.montoPago HAVING Disponible>0 ORDER BY p.idPago";
                case ConsultaMovimientos.PrepararDepositoConsultarPago: return "SELECT montoPago,fechaPago FROM tblpago WHERE idPago=? AND idMoneda=? AND estadoPago='CONFIRMADO' FOR UPDATE";
                case ConsultaMovimientos.PrepararDepositoConsultarDepositoventasdetalle: return "SELECT COALESCE(SUM(importe),0) FROM tbldepositoventasdetalle WHERE idPago=?";
                case ConsultaMovimientos.PrepararDepositoInsertarDepositoventas: return "INSERT INTO tbldepositoventas(codigoDeposito,fechaDeposito,empresaReferencia,bancoReferencia,cuentaReferencia,conceptoReferencia,referenciaDeposito,idMoneda,total,idUsuarioRegistro) VALUES (?,?,?,?,?,?,?,?,?,?)";
                case ConsultaMovimientos.PrepararDepositoInsertarDepositoventasdetalle: return "INSERT INTO tbldepositoventasdetalle(idDeposito,idPago,importe) VALUES (?,?,?)";
                case ConsultaMovimientos.AnularReciboConsultarPago: return "SELECT idMoneda FROM tblpago WHERE idPago=?";
                case ConsultaMovimientos.AnularReciboConsultarPago2: return "SELECT * FROM tblpago WHERE idPago=? FOR UPDATE";
                case ConsultaMovimientos.AnularReciboConsultarDepositoventasdetalle: return "SELECT COUNT(*) FROM tbldepositoventasdetalle WHERE idPago=?";
                case ConsultaMovimientos.AnularReciboConsultarMovimientocxc: return "SELECT m.* FROM tblmovimientocxc m JOIN tblaplicacionpago a ON a.idAplicacionPago=m.idAplicacionPago WHERE a.idPago=? AND a.estadoAplicacion='APLICADA' ORDER BY m.idFactura FOR UPDATE";
                case ConsultaMovimientos.AnularReciboActualizarAplicacionpago: return "UPDATE tblaplicacionpago SET estadoAplicacion='REVERTIDA' WHERE idPago=?";
                case ConsultaMovimientos.AnularReciboActualizarPago: return "UPDATE tblpago SET estadoPago='ANULADO' WHERE idPago=?";
                case ConsultaMovimientos.ListarDocumentosConsultarFactura: return "SELECT f.idFactura Id,'FACTURA' Clase,CONCAT(f.serieFactura,'-',f.numeroFactura) Documento,f.fechaEmision Fecha,f.fechaVencimiento Vencimiento,f.total Importe,f.estadoFactura Estado,COALESCE(f.observaciones,'') Descripcion FROM tblfactura f WHERE f.serieFactura='CXC' AND (?=0 OR f.idCliente=?) AND (?=0 OR f.idMoneda=?) UNION ALL SELECT n.idNota,'NOTA',n.numeroNota,n.fechaNota,n.fechaNota,n.monto,n.estadoNota,n.motivo FROM tblnotacxc n JOIN tblfactura f ON f.idFactura=n.idFactura WHERE (?=0 OR f.idCliente=?) AND (?=0 OR f.idMoneda=?) ORDER BY Fecha DESC,Documento";
                case ConsultaMovimientos.ListarRecibosConsultarPago: return "SELECT p.idPago Id,p.numeroRecibo Recibo,c.nombreCliente Cliente,mo.codigoMoneda Moneda,p.fechaPago Fecha,p.montoPago Monto,p.formaPago Forma,p.referenciaPago Referencia,p.estadoPago Estado,COALESCE(p.observaciones,'') Descripcion FROM tblpago p JOIN tblcliente c ON c.idCliente=p.idCliente JOIN tblmonedaventas mo ON mo.idMoneda=p.idMoneda WHERE (?=0 OR p.idCliente=?) AND (?=0 OR p.idMoneda=?) ORDER BY p.fechaPago DESC,p.idPago DESC";
                case ConsultaMovimientos.BloquearDocumentoConsultarFactura: return "SELECT idFactura,idMoneda,fechaEmision Fecha FROM tblfactura WHERE idFactura=? AND serieFactura='CXC'";
                case ConsultaMovimientos.BloquearDocumentoConsultarNotacxc: return "SELECT f.idFactura,f.idMoneda,n.fechaNota Fecha FROM tblnotacxc n JOIN tblfactura f ON f.idFactura=n.idFactura WHERE n.idNota=?";
                case ConsultaMovimientos.BloquearDocumentoConsultarFactura2: return "SELECT * FROM tblfactura WHERE idFactura=? FOR UPDATE";
                case ConsultaMovimientos.BloquearDocumentoConsultarNotacxc2: return "SELECT * FROM tblnotacxc WHERE idNota=? FOR UPDATE";
                case ConsultaMovimientos.ModificarDocumentoActualizarFactura: return "UPDATE tblfactura SET observaciones=?,fechaVencimiento=? WHERE idFactura=?";
                case ConsultaMovimientos.ModificarDocumentoActualizarNotacxc: return "UPDATE tblnotacxc SET motivo=? WHERE idNota=?";
                case ConsultaMovimientos.AnularDocumentoConsultarFactura: return "SELECT idMoneda FROM tblfactura WHERE idFactura=?";
                case ConsultaMovimientos.AnularDocumentoConsultarAplicacionpago: return "SELECT COUNT(*) FROM tblaplicacionpago WHERE idFactura=? AND estadoAplicacion='APLICADA'";
                case ConsultaMovimientos.AnularDocumentoConsultarNotacxc: return "SELECT COUNT(*) FROM tblnotacxc WHERE idFactura=? AND estadoNota='APLICADA'";
                case ConsultaMovimientos.AnularDocumentoConsultarMovimientocxc: return "SELECT * FROM tblmovimientocxc WHERE idFactura=? AND tipoMovimiento='FACTURA' FOR UPDATE";
                case ConsultaMovimientos.AnularDocumentoConsultarMovimientocxc2: return "SELECT * FROM tblmovimientocxc WHERE idNota=? AND tipoMovimiento<>'REVERSO' FOR UPDATE";
                case ConsultaMovimientos.AnularDocumentoConsultarMovimientocxc3: return "SELECT COUNT(*) FROM tblmovimientocxc WHERE idMovimientoOrigen=?";
                case ConsultaMovimientos.AnularDocumentoActualizarFactura: return "UPDATE tblfactura SET estadoFactura='ANULADA' WHERE idFactura=?";
                case ConsultaMovimientos.AnularDocumentoActualizarNotacxc: return "UPDATE tblnotacxc SET estadoNota='ANULADA' WHERE idNota=?";
                case ConsultaMovimientos.ModificarReciboConsultarPago: return "SELECT idMoneda,fechaPago FROM tblpago WHERE idPago=?";
                case ConsultaMovimientos.ModificarReciboConsultarPago2: return "SELECT estadoPago FROM tblpago WHERE idPago=? FOR UPDATE";
                case ConsultaMovimientos.ModificarReciboActualizarPago: return "UPDATE tblpago SET observaciones=? WHERE idPago=?";
                case ConsultaMovimientos.LeerFactura: return "SELECT f.idFactura Id,'FACTURA' Clase,f.idCliente,f.idMoneda,f.idEstablecimiento,f.numeroFactura Numero,0 Tipo,f.fechaEmision Fecha,f.fechaVencimiento Vencimiento,f.total Importe,COALESCE(f.observaciones,'') Descripcion,f.estadoFactura Estado,'' Referencia FROM tblfactura f WHERE f.idFactura=? AND f.serieFactura='CXC'";
                case ConsultaMovimientos.LeerNota: return "SELECT n.idNota Id,'NOTA' Clase,f.idCliente,f.idMoneda,f.idEstablecimiento,n.numeroNota Numero,CASE m.tipoMovimiento WHEN 'AJUSTE_POSITIVO' THEN 3 WHEN 'AJUSTE_NEGATIVO' THEN 4 ELSE CASE n.tipoNota WHEN 'CREDITO' THEN 1 ELSE 2 END END Tipo,n.fechaNota Fecha,n.fechaNota Vencimiento,n.monto Importe,n.motivo Descripcion,n.estadoNota Estado,CONCAT(f.serieFactura,'-',f.numeroFactura) Referencia FROM tblnotacxc n JOIN tblfactura f ON f.idFactura=n.idFactura LEFT JOIN tblmovimientocxc m ON m.idNota=n.idNota WHERE n.idNota=?";
                case ConsultaMovimientos.LeerRecibo: return "SELECT * FROM tblpago WHERE idPago=?";
                case ConsultaMovimientos.LeerAplicacionesRecibo: return "SELECT a.idFactura,CONCAT(f.serieFactura,'-',f.numeroFactura) Documento,f.fechaEmision Fecha,f.fechaVencimiento Vencimiento,mo.codigoMoneda Moneda,(SELECT COALESCE(SUM(m.cargo-m.abono),0) FROM tblmovimientocxc m WHERE m.idFactura=f.idFactura) Saldo,a.montoAplicado Importe,a.estadoAplicacion Estado FROM tblaplicacionpago a JOIN tblfactura f ON f.idFactura=a.idFactura JOIN tblmonedaventas mo ON mo.idMoneda=f.idMoneda WHERE a.idPago=? ORDER BY a.idAplicacionPago";
                case ConsultaMovimientos.LeerMovimiento: return "SELECT CASE WHEN m.idAplicacionPago IS NOT NULL THEN a.idPago WHEN m.idNota IS NOT NULL THEN m.idNota ELSE m.idFactura END Id,CASE WHEN m.idAplicacionPago IS NOT NULL THEN 'RECIBO' WHEN m.idNota IS NOT NULL THEN 'NOTA' ELSE 'FACTURA' END Clase FROM tblmovimientocxc m JOIN tblfactura f ON f.idFactura=m.idFactura LEFT JOIN tblaplicacionpago a ON a.idAplicacionPago=m.idAplicacionPago WHERE m.idMovimiento=? AND (f.serieFactura='CXC' OR m.idNota IS NOT NULL OR m.idAplicacionPago IS NOT NULL)";
                default: throw new ArgumentOutOfRangeException("Consulta");
            }
        }
    }
}
