// Jose Javier Torres Martinez | 0901-23-1091 | 22/10/2026
using System.Data;
using System;
using VentasDatos;

namespace CapaModelo_MovimientosClientes
{
    public sealed class ClsMovimientosRepositorio : IDisposable
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

        // Saldo: consultas parametrizadas.
        public object VentasMetSaldoConsultarMovimientocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT COALESCE(SUM(cargo-abono),0)
    FROM tblmovimientocxc
    WHERE idFactura=?
      AND fechaMovimiento<=?";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public object VentasMetSaldoConsultarMovimientocxc2(params object[] Parametros)
        {
            const string Consulta = @"SELECT COALESCE(SUM(cargo-abono),0)
    FROM tblmovimientocxc
    WHERE idFactura=?";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        // RegistrarRecibo: consultas parametrizadas.
        public object VentasMetRegistrarReciboConsultarCliente(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblcliente
    WHERE idCliente=?
      AND is_active=1";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public DataTable VentasMetRegistrarReciboConsultarFactura(params object[] Parametros)
        {
            const string Consulta = @"SELECT fechaEmision
    FROM tblfactura
    WHERE idFactura=?
      AND idCliente=?
      AND idMoneda=?
      AND estadoFactura='EMITIDA'
    FOR UPDATE";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public int VentasMetRegistrarReciboInsertarPago(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblpago(numeroRecibo,idCliente,idMoneda,fechaPago,montoPago,tipoCambio,formaPago,referenciaPago,estadoPago,observaciones,idUsuarioRegistro)
    VALUES (?,?,?,?,?,?,?,?,'CONFIRMADO',?,?)";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public int VentasMetRegistrarReciboInsertarAplicacionpago(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblaplicacionpago(idPago,idFactura,fechaAplicacion,montoAplicado,estadoAplicacion,idUsuarioRegistro)
    VALUES (?,?,?,?,'APLICADA',?)";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        // RegistrarDocumento: consultas parametrizadas.
        public DataTable VentasMetRegistrarDocumentoConsultarCliente(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tblcliente
    WHERE idCliente=?
      AND is_active=1";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public object VentasMetRegistrarDocumentoConsultarEstablecimientoventas(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblestablecimientoventas
    WHERE idEstablecimiento=?
      AND is_active=1";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public int VentasMetRegistrarDocumentoInsertarFactura(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblfactura(serieFactura,numeroFactura,idEstablecimiento,idCliente,idMoneda,fechaEmision,fechaVencimiento,tipoDte,condicionPago,diasCreditoAplicados,tipoIdentificacionReceptor,identificacionReceptor,nombreReceptor,direccionReceptor,subtotal,total,estadoFactura,observaciones,idUsuarioRegistro)
    VALUES ('CXC',?,?,?,?,?,?,'INTERNO_CXC',?,?,?,?,?,?,?,?,'EMITIDA',?,?)";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public DataTable VentasMetRegistrarDocumentoConsultarFactura(params object[] Parametros)
        {
            const string Consulta = @"SELECT f.idFactura,f.fechaEmision
    FROM tblfactura f
    WHERE f.idCliente=?
      AND f.idMoneda=?
      AND CONCAT(f.serieFactura,'-',f.numeroFactura)=?
      AND f.estadoFactura='EMITIDA'
    FOR UPDATE";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public int VentasMetRegistrarDocumentoInsertarNotacxc(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblnotacxc(numeroNota,idFactura,tipoNota,fechaNota,monto,motivo,estadoNota,idUsuarioRegistro)
    VALUES (?,?,?,?,?,?,'APLICADA',?)";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        // RecibosDisponibles: consultas parametrizadas.
        public DataTable VentasMetRecibosDisponiblesConsultarPago(params object[] Parametros)
        {
            const string Consulta = @"SELECT p.idPago,p.numeroRecibo Recibo,c.nombreCliente Cliente,mo.codigoMoneda Moneda,p.montoPago Monto,COALESCE(SUM(dd.importe),0) Depositado,p.montoPago-COALESCE(SUM(dd.importe),0) Disponible
    FROM tblpago p
    JOIN tblcliente c ON c.idCliente=p.idCliente
    JOIN tblmonedaventas mo ON mo.idMoneda=p.idMoneda
    LEFT JOIN tbldepositoventasdetalle dd ON dd.idPago=p.idPago
    WHERE p.idMoneda=?
      AND p.estadoPago='CONFIRMADO'
    GROUP BY p.idPago,p.numeroRecibo,c.nombreCliente,mo.codigoMoneda,p.montoPago
    HAVING Disponible>0
    ORDER BY p.idPago";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // PrepararDeposito: consultas parametrizadas.
        public DataTable VentasMetPrepararDepositoConsultarPago(params object[] Parametros)
        {
            const string Consulta = @"SELECT montoPago,fechaPago
    FROM tblpago
    WHERE idPago=?
      AND idMoneda=?
      AND estadoPago='CONFIRMADO'
    FOR UPDATE";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public object VentasMetPrepararDepositoConsultarDepositoventasdetalle(params object[] Parametros)
        {
            const string Consulta = @"SELECT COALESCE(SUM(importe),0)
    FROM tbldepositoventasdetalle
    WHERE idPago=?";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public int VentasMetPrepararDepositoInsertarDepositoventas(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tbldepositoventas(codigoDeposito,fechaDeposito,empresaReferencia,bancoReferencia,cuentaReferencia,conceptoReferencia,referenciaDeposito,idMoneda,total,idUsuarioRegistro)
    VALUES (?,?,?,?,?,?,?,?,?,?)";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public int VentasMetPrepararDepositoInsertarDepositoventasdetalle(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tbldepositoventasdetalle(idDeposito,idPago,importe)
    VALUES (?,?,?)";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        // Pendientes: consultas parametrizadas.
        public DataTable VentasMetPendientesConsultarFactura(params object[] Parametros)
        {
            const string Consulta = @"SELECT f.idFactura,CONCAT(f.serieFactura,'-',f.numeroFactura) Documento,f.fechaEmision Fecha,f.fechaVencimiento Vencimiento,mo.codigoMoneda Moneda,SUM(m.cargo-m.abono) Saldo
    FROM tblfactura f
    JOIN tblmovimientocxc m ON m.idFactura=f.idFactura
    JOIN tblmonedaventas mo ON mo.idMoneda=f.idMoneda
    WHERE f.idCliente=?
      AND f.idMoneda=?
    GROUP BY f.idFactura,f.serieFactura,f.numeroFactura,f.fechaEmision,f.fechaVencimiento,mo.codigoMoneda
    HAVING SUM(m.cargo-m.abono)>0
    ORDER BY f.fechaVencimiento,f.idFactura";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // Generales: consultas parametrizadas.
        public DataTable VentasMetGeneralesConsultarRecibosConsultarPago(params object[] Parametros)
        {
            const string Consulta = @"SELECT p.idPago,p.numeroRecibo Recibo,p.fechaPago Fecha,c.nombreCliente Cliente,mo.codigoMoneda Moneda,p.montoPago Monto,p.estadoPago Estado,COALESCE(SUM(dd.importe),0) Depositado,COALESCE(GROUP_CONCAT(DISTINCT d.codigoDeposito
    ORDER BY d.codigoDeposito SEPARATOR ', '),'') Deposito,COALESCE(GROUP_CONCAT(DISTINCT d.movimientoBancario SEPARATOR ', '),'') MovimientoBancario
    FROM tblpago p
    JOIN tblcliente c ON c.idCliente=p.idCliente
    JOIN tblmonedaventas mo ON mo.idMoneda=p.idMoneda
    LEFT JOIN tbldepositoventasdetalle dd ON dd.idPago=p.idPago
    LEFT JOIN tbldepositoventas d ON d.idDeposito=dd.idDeposito
    WHERE (?='' OR p.numeroRecibo=?)
      AND (?='' OR EXISTS(SELECT 1
    FROM tbldepositoventasdetalle x
    JOIN tbldepositoventas y ON y.idDeposito=x.idDeposito
    WHERE x.idPago=p.idPago
      AND y.codigoDeposito=?))
    GROUP BY p.idPago,p.numeroRecibo,p.fechaPago,c.nombreCliente,mo.codigoMoneda,p.montoPago,p.estadoPago
    ORDER BY p.fechaPago DESC,p.idPago DESC";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // Movimiento: consultas parametrizadas.
        public int VentasMetMovimientoInsertarMovimientocxc(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblmovimientocxc(idFactura,fechaMovimiento,tipoMovimiento,descripcionMovimiento,cargo,abono,idUsuarioRegistro,idAplicacionPago,idNota,idMovimientoOrigen)
    VALUES (?,?,?,?,?,?,?,?,?,?)";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        // ModificarRecibo: consultas parametrizadas.
        public DataTable VentasMetModificarReciboConsultarPago(params object[] Parametros)
        {
            const string Consulta = @"SELECT idMoneda,fechaPago
    FROM tblpago
    WHERE idPago=?";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public DataTable VentasMetModificarReciboConsultarPago2(params object[] Parametros)
        {
            const string Consulta = @"SELECT estadoPago
    FROM tblpago
    WHERE idPago=?
    FOR UPDATE";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public int VentasMetModificarReciboActualizarPago(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblpago
    SET observaciones=?
    WHERE idPago=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        // ModificarDocumento: consultas parametrizadas.
        public int VentasMetModificarDocumentoActualizarFactura(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblfactura
    SET observaciones=?,fechaVencimiento=?
    WHERE idFactura=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public int VentasMetModificarDocumentoActualizarNotacxc(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblnotacxc
    SET motivo=?
    WHERE idNota=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        // ListarRecibos: consultas parametrizadas.
        public DataTable VentasMetListarRecibosConsultarPago(params object[] Parametros)
        {
            const string Consulta = @"SELECT p.idPago Id,p.numeroRecibo Recibo,c.nombreCliente Cliente,mo.codigoMoneda Moneda,p.fechaPago Fecha,p.montoPago Monto,p.formaPago Forma,p.referenciaPago Referencia,p.estadoPago Estado,COALESCE(p.observaciones,'') Descripcion
    FROM tblpago p
    JOIN tblcliente c ON c.idCliente=p.idCliente
    JOIN tblmonedaventas mo ON mo.idMoneda=p.idMoneda
    WHERE (?=0 OR p.idCliente=?)
      AND (?=0 OR p.idMoneda=?)
    ORDER BY p.fechaPago DESC,p.idPago DESC";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // ListarDocumentos: consultas parametrizadas.
        public DataTable VentasMetListarDocumentosConsultarFactura(params object[] Parametros)
        {
            const string Consulta = @"SELECT f.idFactura Id,'FACTURA' Clase,CONCAT(f.serieFactura,'-',f.numeroFactura) Documento,f.fechaEmision Fecha,f.fechaVencimiento Vencimiento,f.total Importe,f.estadoFactura Estado,COALESCE(f.observaciones,'') Descripcion
    FROM tblfactura f
    WHERE f.serieFactura='CXC'
      AND (?=0 OR f.idCliente=?)
      AND (?=0 OR f.idMoneda=?)
    UNION ALL SELECT n.idNota,'NOTA',n.numeroNota,n.fechaNota,n.fechaNota,n.monto,n.estadoNota,n.motivo
    FROM tblnotacxc n
    JOIN tblfactura f ON f.idFactura=n.idFactura
    WHERE (?=0 OR f.idCliente=?)
      AND (?=0 OR f.idMoneda=?)
    ORDER BY Fecha DESC,Documento";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // Lectura: consultas parametrizadas.
        public DataTable VentasMetLecturaConsultarFactura(params object[] Parametros)
        {
            const string Consulta = @"SELECT f.idFactura Id,'FACTURA' Clase,f.idCliente,f.idMoneda,f.idEstablecimiento,f.numeroFactura Numero,0 Tipo,f.fechaEmision Fecha,f.fechaVencimiento Vencimiento,f.total Importe,COALESCE(f.observaciones,'') Descripcion,f.estadoFactura Estado,'' Referencia
    FROM tblfactura f
    WHERE f.idFactura=?
      AND f.serieFactura='CXC'";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public DataTable VentasMetLecturaConsultarNota(params object[] Parametros)
        {
            const string Consulta = @"SELECT n.idNota Id,'NOTA' Clase,f.idCliente,f.idMoneda,f.idEstablecimiento,n.numeroNota Numero,CASE m.tipoMovimiento WHEN 'AJUSTE_POSITIVO' THEN 3 WHEN 'AJUSTE_NEGATIVO' THEN 4 ELSE CASE n.tipoNota WHEN 'CREDITO' THEN 1 ELSE 2 END END Tipo,n.fechaNota Fecha,n.fechaNota Vencimiento,n.monto Importe,n.motivo Descripcion,n.estadoNota Estado,CONCAT(f.serieFactura,'-',f.numeroFactura) Referencia
    FROM tblnotacxc n
    JOIN tblfactura f ON f.idFactura=n.idFactura
    LEFT JOIN tblmovimientocxc m ON m.idNota=n.idNota
    WHERE n.idNota=?";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public DataTable VentasMetLecturaConsultarRecibo(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tblpago
    WHERE idPago=?";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public DataTable VentasMetLecturaConsultarAplicacionesRecibo(params object[] Parametros)
        {
            const string Consulta = @"SELECT a.idFactura,CONCAT(f.serieFactura,'-',f.numeroFactura) Documento,f.fechaEmision Fecha,f.fechaVencimiento Vencimiento,mo.codigoMoneda Moneda,(SELECT COALESCE(SUM(m.cargo-m.abono),0)
    FROM tblmovimientocxc m
    WHERE m.idFactura=f.idFactura) Saldo,a.montoAplicado Importe,a.estadoAplicacion Estado
    FROM tblaplicacionpago a
    JOIN tblfactura f ON f.idFactura=a.idFactura
    JOIN tblmonedaventas mo ON mo.idMoneda=f.idMoneda
    WHERE a.idPago=?
    ORDER BY a.idAplicacionPago";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public DataTable VentasMetLecturaConsultarMovimiento(params object[] Parametros)
        {
            const string Consulta = @"SELECT CASE WHEN m.idAplicacionPago IS NOT NULL THEN a.idPago WHEN m.idNota IS NOT NULL THEN m.idNota ELSE m.idFactura END Id,CASE WHEN m.idAplicacionPago IS NOT NULL THEN 'RECIBO' WHEN m.idNota IS NOT NULL THEN 'NOTA' ELSE 'FACTURA' END Clase
    FROM tblmovimientocxc m
    JOIN tblfactura f ON f.idFactura=m.idFactura
    LEFT JOIN tblaplicacionpago a ON a.idAplicacionPago=m.idAplicacionPago
    WHERE m.idMovimiento=?
      AND (f.serieFactura='CXC' OR m.idNota IS NOT NULL OR m.idAplicacionPago IS NOT NULL)";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // EstadoCuenta: consultas parametrizadas.
        public DataTable VentasMetEstadoCuentaConsultarMovimientocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT COALESCE(SUM(CASE WHEN m.fechaMovimiento<? THEN m.cargo-m.abono ELSE 0 END),0) Inicial,COALESCE(SUM(CASE WHEN m.fechaMovimiento>=? THEN m.cargo ELSE 0 END),0) Cargos,COALESCE(SUM(CASE WHEN m.fechaMovimiento>=? THEN m.abono ELSE 0 END),0) Abonos
    FROM tblmovimientocxc m
    JOIN tblfactura f ON f.idFactura=m.idFactura
    WHERE f.idCliente=?
      AND f.idMoneda=?
      AND m.fechaMovimiento<=?";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public DataTable VentasMetEstadoCuentaConsultarMovimientocxc2(params object[] Parametros)
        {
            const string Consulta = @"SELECT m.idMovimiento,m.fechaMovimiento Fecha,CONCAT(f.serieFactura,'-',f.numeroFactura) Documento,m.tipoMovimiento Tipo,m.descripcionMovimiento Descripcion,m.cargo Cargo,m.abono Abono,CASE WHEN c.fechaUltimoCierre IS NOT NULL
      AND m.fechaMovimiento<=c.fechaUltimoCierre THEN 'CERRADO' ELSE 'APLICADO' END Estado
    FROM tblmovimientocxc m
    JOIN tblfactura f ON f.idFactura=m.idFactura
    LEFT JOIN tblcontrolperiodocxc c ON c.idMoneda=f.idMoneda
    WHERE f.idCliente=?
      AND f.idMoneda=?
      AND m.fechaMovimiento BETWEEN ?
      AND ?
    ORDER BY m.fechaMovimiento,m.idMovimiento";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // Documento: consultas parametrizadas.
        public DataTable VentasMetDocumentoConsultarFactura(params object[] Parametros)
        {
            const string Consulta = @"SELECT CONCAT(serieFactura,'-',numeroFactura) Documento,fechaEmision Fecha,fechaVencimiento Vencimiento,nombreReceptor Cliente,total Importe,estadoFactura Estado,observaciones Descripcion
    FROM tblfactura
    WHERE idCliente=?
      AND idMoneda=?
      AND idEstablecimiento=?
      AND serieFactura='CXC'
      AND numeroFactura=?";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public DataTable VentasMetDocumentoConsultarNotacxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT n.numeroNota Documento,n.fechaNota Fecha,n.tipoNota Tipo,f.nombreReceptor Cliente,n.monto Importe,n.estadoNota Estado,n.motivo Descripcion
    FROM tblnotacxc n
    JOIN tblfactura f ON f.idFactura=n.idFactura
    WHERE f.idCliente=?
      AND f.idMoneda=?
      AND n.numeroNota=?";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // Disponible: consultas parametrizadas.
        public object VentasMetDisponibleConsultarMovimientocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT MIN(Saldo)
    FROM (SELECT fechaMovimiento,SUM(cargo-abono) OVER (ORDER BY fechaMovimiento,idMovimiento ROWS UNBOUNDED PRECEDING) Saldo
    FROM tblmovimientocxc
    WHERE idFactura=?) s
    WHERE fechaMovimiento>?";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        // Deposito: consultas parametrizadas.
        public DataTable VentasMetDepositoConsultarDepositoventas(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tbldepositoventas
    WHERE codigoDeposito=?";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public DataTable VentasMetDepositoConsultarDepositoventasdetalle(params object[] Parametros)
        {
            const string Consulta = @"SELECT p.numeroRecibo Recibo,c.nombreCliente Cliente,dd.importe Importe
    FROM tbldepositoventasdetalle dd
    JOIN tblpago p ON p.idPago=dd.idPago
    JOIN tblcliente c ON c.idCliente=p.idCliente
    WHERE dd.idDeposito=?
    ORDER BY p.idPago";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // BloquearDocumento: consultas parametrizadas.
        public DataTable VentasMetBloquearDocumentoConsultarFactura(params object[] Parametros)
        {
            const string Consulta = @"SELECT idFactura,idMoneda,fechaEmision Fecha
    FROM tblfactura
    WHERE idFactura=?
      AND serieFactura='CXC'";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public DataTable VentasMetBloquearDocumentoConsultarNotacxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT f.idFactura,f.idMoneda,n.fechaNota Fecha
    FROM tblnotacxc n
    JOIN tblfactura f ON f.idFactura=n.idFactura
    WHERE n.idNota=?";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public DataTable VentasMetBloquearDocumentoConsultarFactura2(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tblfactura
    WHERE idFactura=?
    FOR UPDATE";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public DataTable VentasMetBloquearDocumentoConsultarNotacxc2(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tblnotacxc
    WHERE idNota=?
    FOR UPDATE";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // Aplicaciones: consultas parametrizadas.
        public DataTable VentasMetAplicacionesConsultarAplicacionpago(params object[] Parametros)
        {
            const string Consulta = @"SELECT CONCAT(f.serieFactura,'-',f.numeroFactura) Factura,a.fechaAplicacion Fecha,a.montoAplicado Importe,a.estadoAplicacion Estado
    FROM tblaplicacionpago a
    JOIN tblfactura f ON f.idFactura=a.idFactura
    WHERE a.idPago=?
    ORDER BY a.idAplicacionPago";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // AnularRecibo: consultas parametrizadas.
        public object VentasMetAnularReciboConsultarPago(params object[] Parametros)
        {
            const string Consulta = @"SELECT idMoneda
    FROM tblpago
    WHERE idPago=?";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public DataTable VentasMetAnularReciboConsultarPago2(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tblpago
    WHERE idPago=?
    FOR UPDATE";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public object VentasMetAnularReciboConsultarDepositoventasdetalle(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tbldepositoventasdetalle
    WHERE idPago=?";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public DataTable VentasMetAnularReciboConsultarMovimientocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT m.*
    FROM tblmovimientocxc m
    JOIN tblaplicacionpago a ON a.idAplicacionPago=m.idAplicacionPago
    WHERE a.idPago=?
      AND a.estadoAplicacion='APLICADA'
    ORDER BY m.idFactura
    FOR UPDATE";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public int VentasMetAnularReciboActualizarAplicacionpago(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblaplicacionpago
    SET estadoAplicacion='REVERTIDA'
    WHERE idPago=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public int VentasMetAnularReciboActualizarPago(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblpago
    SET estadoPago='ANULADO'
    WHERE idPago=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        // AnularDocumento: consultas parametrizadas.
        public DataTable VentasMetAnularDocumentoConsultarFactura(params object[] Parametros)
        {
            const string Consulta = @"SELECT idMoneda
    FROM tblfactura
    WHERE idFactura=?";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public object VentasMetAnularDocumentoConsultarAplicacionpago(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblaplicacionpago
    WHERE idFactura=?
      AND estadoAplicacion='APLICADA'";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public object VentasMetAnularDocumentoConsultarNotacxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblnotacxc
    WHERE idFactura=?
      AND estadoNota='APLICADA'";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public DataTable VentasMetAnularDocumentoConsultarMovimientocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tblmovimientocxc
    WHERE idFactura=?
      AND tipoMovimiento='FACTURA'
    FOR UPDATE";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public DataTable VentasMetAnularDocumentoConsultarMovimientocxc2(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tblmovimientocxc
    WHERE idNota=?
      AND tipoMovimiento<>'REVERSO'
    FOR UPDATE";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public object VentasMetAnularDocumentoConsultarMovimientocxc3(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblmovimientocxc
    WHERE idMovimientoOrigen=?";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public int VentasMetAnularDocumentoActualizarFactura(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblfactura
    SET estadoFactura='ANULADA'
    WHERE idFactura=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public int VentasMetAnularDocumentoActualizarNotacxc(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblnotacxc
    SET estadoNota='ANULADA'
    WHERE idNota=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }
    }
}
