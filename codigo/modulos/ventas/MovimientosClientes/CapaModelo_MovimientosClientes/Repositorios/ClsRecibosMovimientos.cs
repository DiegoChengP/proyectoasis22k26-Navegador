// Jose Javier Torres Martinez | 0901-23-1091 | 23/10/2026
using System.Data;
using System;
using VentasDatos;

namespace CapaModelo_MovimientosClientes
{
    public sealed class ClsRecibosMovimientos
    {
        private readonly ClsDatosVentas _Datos;

        internal ClsRecibosMovimientos(ClsDatosVentas Datos)
        {
            _Datos = Datos;
        }

        public object VentasFuncRegistrarReciboConsultarCliente(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblcliente
    WHERE idCliente=?
      AND is_active=1";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public DataTable VentasFuncRegistrarReciboConsultarFactura(params object[] Parametros)
        {
            const string Consulta = @"SELECT fechaEmision
    FROM tblfactura
    WHERE idFactura=?
      AND idCliente=?
      AND idMoneda=?
      AND estadoFactura='EMITIDA'
    FOR UPDATE";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public int VentasFuncRegistrarReciboInsertarPago(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblpago(numeroRecibo,idCliente,idMoneda,fechaPago,montoPago,tipoCambio,formaPago,referenciaPago,estadoPago,observaciones,idUsuarioRegistro)
    VALUES (?,?,?,?,?,?,?,?,'CONFIRMADO',?,?)";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncRegistrarReciboInsertarAplicacionpago(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblaplicacionpago(idPago,idFactura,fechaAplicacion,montoAplicado,estadoAplicacion,idUsuarioRegistro)
    VALUES (?,?,?,?,'APLICADA',?)";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public DataTable VentasFuncPrepararDepositoConsultarPago(params object[] Parametros)
        {
            const string Consulta = @"SELECT montoPago,fechaPago
    FROM tblpago
    WHERE idPago=?
      AND idMoneda=?
      AND estadoPago='CONFIRMADO'
    FOR UPDATE";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public object VentasFuncPrepararDepositoConsultarDepositoventasdetalle(params object[] Parametros)
        {
            const string Consulta = @"SELECT COALESCE(SUM(importe),0)
    FROM tbldepositoventasdetalle
    WHERE idPago=?";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public int VentasFuncPrepararDepositoInsertarDepositoventas(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tbldepositoventas(codigoDeposito,fechaDeposito,empresaReferencia,bancoReferencia,cuentaReferencia,conceptoReferencia,referenciaDeposito,idMoneda,total,idUsuarioRegistro)
    VALUES (?,?,?,?,?,?,?,?,?,?)";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncPrepararDepositoInsertarDepositoventasdetalle(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tbldepositoventasdetalle(idDeposito,idPago,importe)
    VALUES (?,?,?)";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public DataTable VentasFuncModificarReciboConsultarPago(params object[] Parametros)
        {
            const string Consulta = @"SELECT idMoneda,fechaPago
    FROM tblpago
    WHERE idPago=?";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncModificarReciboConsultarPago2(params object[] Parametros)
        {
            const string Consulta = @"SELECT estadoPago
    FROM tblpago
    WHERE idPago=?
    FOR UPDATE";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public int VentasFuncModificarReciboActualizarPago(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblpago
    SET observaciones=?
    WHERE idPago=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public object VentasFuncAnularReciboConsultarPago(params object[] Parametros)
        {
            const string Consulta = @"SELECT idMoneda
    FROM tblpago
    WHERE idPago=?";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public DataTable VentasFuncAnularReciboConsultarPago2(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tblpago
    WHERE idPago=?
    FOR UPDATE";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public object VentasFuncAnularReciboConsultarDepositoventasdetalle(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tbldepositoventasdetalle
    WHERE idPago=?";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public DataTable VentasFuncAnularReciboConsultarMovimientocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT m.*
    FROM tblmovimientocxc m
    JOIN tblaplicacionpago a ON a.idAplicacionPago=m.idAplicacionPago
    WHERE a.idPago=?
      AND a.estadoAplicacion='APLICADA'
    ORDER BY m.idFactura
    FOR UPDATE";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public int VentasFuncAnularReciboActualizarAplicacionpago(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblaplicacionpago
    SET estadoAplicacion='REVERTIDA'
    WHERE idPago=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncAnularReciboActualizarPago(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblpago
    SET estadoPago='ANULADO'
    WHERE idPago=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }
    }
}
