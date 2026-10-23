// Jose Javier Torres Martinez | 0901-23-1091 | 23/10/2026
using System.Data;
using System;
using VentasDatos;

namespace CapaModelo_MovimientosClientes
{
    public sealed class ClsConsultasMovimientos
    {
        private readonly ClsDatosVentas _Datos;

        internal ClsConsultasMovimientos(ClsDatosVentas Datos)
        {
            _Datos = Datos;
        }

        public DataTable VentasFuncRecibosDisponiblesConsultarPago(params object[] Parametros)
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
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncPendientesConsultarFactura(params object[] Parametros)
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
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncGeneralesConsultarRecibosConsultarPago(params object[] Parametros)
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
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncListarRecibosConsultarPago(params object[] Parametros)
        {
            const string Consulta = @"SELECT p.idPago Id,p.numeroRecibo Recibo,c.nombreCliente Cliente,mo.codigoMoneda Moneda,p.fechaPago Fecha,p.montoPago Monto,p.formaPago Forma,p.referenciaPago Referencia,p.estadoPago Estado,COALESCE(p.observaciones,'') Descripcion
    FROM tblpago p
    JOIN tblcliente c ON c.idCliente=p.idCliente
    JOIN tblmonedaventas mo ON mo.idMoneda=p.idMoneda
    WHERE (?=0 OR p.idCliente=?)
      AND (?=0 OR p.idMoneda=?)
    ORDER BY p.fechaPago DESC,p.idPago DESC";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncListarDocumentosConsultarFactura(params object[] Parametros)
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
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncLecturaConsultarFactura(params object[] Parametros)
        {
            const string Consulta = @"SELECT f.idFactura Id,'FACTURA' Clase,f.idCliente,f.idMoneda,f.idEstablecimiento,f.numeroFactura Numero,0 Tipo,f.fechaEmision Fecha,f.fechaVencimiento Vencimiento,f.total Importe,COALESCE(f.observaciones,'') Descripcion,f.estadoFactura Estado,'' Referencia
    FROM tblfactura f
    WHERE f.idFactura=?
      AND f.serieFactura='CXC'";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncLecturaConsultarNota(params object[] Parametros)
        {
            const string Consulta = @"SELECT n.idNota Id,'NOTA' Clase,f.idCliente,f.idMoneda,f.idEstablecimiento,n.numeroNota Numero,CASE m.tipoMovimiento WHEN 'AJUSTE_POSITIVO' THEN 3 WHEN 'AJUSTE_NEGATIVO' THEN 4 ELSE CASE n.tipoNota WHEN 'CREDITO' THEN 1 ELSE 2 END END Tipo,n.fechaNota Fecha,n.fechaNota Vencimiento,n.monto Importe,n.motivo Descripcion,n.estadoNota Estado,CONCAT(f.serieFactura,'-',f.numeroFactura) Referencia
    FROM tblnotacxc n
    JOIN tblfactura f ON f.idFactura=n.idFactura
    LEFT JOIN tblmovimientocxc m ON m.idNota=n.idNota
    WHERE n.idNota=?";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncLecturaConsultarRecibo(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tblpago
    WHERE idPago=?";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncLecturaConsultarAplicacionesRecibo(params object[] Parametros)
        {
            const string Consulta = @"SELECT a.idFactura,CONCAT(f.serieFactura,'-',f.numeroFactura) Documento,f.fechaEmision Fecha,f.fechaVencimiento Vencimiento,mo.codigoMoneda Moneda,(SELECT COALESCE(SUM(m.cargo-m.abono),0)
    FROM tblmovimientocxc m
    WHERE m.idFactura=f.idFactura) Saldo,a.montoAplicado Importe,a.estadoAplicacion Estado
    FROM tblaplicacionpago a
    JOIN tblfactura f ON f.idFactura=a.idFactura
    JOIN tblmonedaventas mo ON mo.idMoneda=f.idMoneda
    WHERE a.idPago=?
    ORDER BY a.idAplicacionPago";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncLecturaConsultarMovimiento(params object[] Parametros)
        {
            const string Consulta = @"SELECT CASE WHEN m.idAplicacionPago IS NOT NULL THEN a.idPago WHEN m.idNota IS NOT NULL THEN m.idNota ELSE m.idFactura END Id,CASE WHEN m.idAplicacionPago IS NOT NULL THEN 'RECIBO' WHEN m.idNota IS NOT NULL THEN 'NOTA' ELSE 'FACTURA' END Clase
    FROM tblmovimientocxc m
    JOIN tblfactura f ON f.idFactura=m.idFactura
    LEFT JOIN tblaplicacionpago a ON a.idAplicacionPago=m.idAplicacionPago
    WHERE m.idMovimiento=?
      AND (f.serieFactura='CXC' OR m.idNota IS NOT NULL OR m.idAplicacionPago IS NOT NULL)";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncEstadoCuentaConsultarMovimientocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT COALESCE(SUM(CASE WHEN m.fechaMovimiento<? THEN m.cargo-m.abono ELSE 0 END),0) Inicial,COALESCE(SUM(CASE WHEN m.fechaMovimiento>=? THEN m.cargo ELSE 0 END),0) Cargos,COALESCE(SUM(CASE WHEN m.fechaMovimiento>=? THEN m.abono ELSE 0 END),0) Abonos
    FROM tblmovimientocxc m
    JOIN tblfactura f ON f.idFactura=m.idFactura
    WHERE f.idCliente=?
      AND f.idMoneda=?
      AND m.fechaMovimiento<=?";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncEstadoCuentaConsultarMovimientocxc2(params object[] Parametros)
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
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncDocumentoConsultarFactura(params object[] Parametros)
        {
            const string Consulta = @"SELECT CONCAT(serieFactura,'-',numeroFactura) Documento,fechaEmision Fecha,fechaVencimiento Vencimiento,nombreReceptor Cliente,total Importe,estadoFactura Estado,observaciones Descripcion
    FROM tblfactura
    WHERE idCliente=?
      AND idMoneda=?
      AND idEstablecimiento=?
      AND serieFactura='CXC'
      AND numeroFactura=?";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncDocumentoConsultarNotacxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT n.numeroNota Documento,n.fechaNota Fecha,n.tipoNota Tipo,f.nombreReceptor Cliente,n.monto Importe,n.estadoNota Estado,n.motivo Descripcion
    FROM tblnotacxc n
    JOIN tblfactura f ON f.idFactura=n.idFactura
    WHERE f.idCliente=?
      AND f.idMoneda=?
      AND n.numeroNota=?";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncDepositoConsultarDepositoventas(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tbldepositoventas
    WHERE codigoDeposito=?";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncDepositoConsultarDepositoventasdetalle(params object[] Parametros)
        {
            const string Consulta = @"SELECT p.numeroRecibo Recibo,c.nombreCliente Cliente,dd.importe Importe
    FROM tbldepositoventasdetalle dd
    JOIN tblpago p ON p.idPago=dd.idPago
    JOIN tblcliente c ON c.idCliente=p.idCliente
    WHERE dd.idDeposito=?
    ORDER BY p.idPago";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncAplicacionesConsultarAplicacionpago(params object[] Parametros)
        {
            const string Consulta = @"SELECT CONCAT(f.serieFactura,'-',f.numeroFactura) Factura,a.fechaAplicacion Fecha,a.montoAplicado Importe,a.estadoAplicacion Estado
    FROM tblaplicacionpago a
    JOIN tblfactura f ON f.idFactura=a.idFactura
    WHERE a.idPago=?
    ORDER BY a.idAplicacionPago";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }
    }
}
