// Jose Javier Torres Martinez | 0901-23-1091 | 23/10/2026
using System.Data;
using System;
using VentasDatos;

namespace CapaModelo_MovimientosClientes
{
    public sealed class ClsMovimientosRepositorio : IDisposable
    {
        public ClsRecibosMovimientos Recibos { get; }
        public ClsConsultasMovimientos Consultas { get; }

        public ClsMovimientosRepositorio()
        {
            // Todos los grupos usan la misma conexion y transaccion.
            Recibos = new ClsRecibosMovimientos(_Datos);
            Consultas = new ClsConsultasMovimientos(_Datos);
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

        public object VentasFuncSaldoConsultarMovimientocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT COALESCE(SUM(cargo-abono),0)
    FROM tblmovimientocxc
    WHERE idFactura=?
      AND fechaMovimiento<=?";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public object VentasFuncSaldoConsultarMovimientocxc2(params object[] Parametros)
        {
            const string Consulta = @"SELECT COALESCE(SUM(cargo-abono),0)
    FROM tblmovimientocxc
    WHERE idFactura=?";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public DataTable VentasFuncRegistrarDocumentoConsultarCliente(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tblcliente
    WHERE idCliente=?
      AND is_active=1";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public object VentasFuncRegistrarDocumentoConsultarEstablecimientoventas(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblestablecimientoventas
    WHERE idEstablecimiento=?
      AND is_active=1";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public int VentasFuncRegistrarDocumentoInsertarFactura(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblfactura(serieFactura,numeroFactura,idEstablecimiento,idCliente,idMoneda,fechaEmision,fechaVencimiento,tipoDte,condicionPago,diasCreditoAplicados,tipoIdentificacionReceptor,identificacionReceptor,nombreReceptor,direccionReceptor,subtotal,total,estadoFactura,observaciones,idUsuarioRegistro)
    VALUES ('CXC',?,?,?,?,?,?,'INTERNO_CXC',?,?,?,?,?,?,?,?,'EMITIDA',?,?)";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public DataTable VentasFuncRegistrarDocumentoConsultarFactura(params object[] Parametros)
        {
            const string Consulta = @"SELECT f.idFactura,f.fechaEmision
    FROM tblfactura f
    WHERE f.idCliente=?
      AND f.idMoneda=?
      AND CONCAT(f.serieFactura,'-',f.numeroFactura)=?
      AND f.estadoFactura='EMITIDA'
    FOR UPDATE";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public int VentasFuncRegistrarDocumentoInsertarNotacxc(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblnotacxc(numeroNota,idFactura,tipoNota,fechaNota,monto,motivo,estadoNota,idUsuarioRegistro)
    VALUES (?,?,?,?,?,?,'APLICADA',?)";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncMovimientoInsertarMovimientocxc(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblmovimientocxc(idFactura,fechaMovimiento,tipoMovimiento,descripcionMovimiento,cargo,abono,idUsuarioRegistro,idAplicacionPago,idNota,idMovimientoOrigen)
    VALUES (?,?,?,?,?,?,?,?,?,?)";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncModificarDocumentoActualizarFactura(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblfactura
    SET observaciones=?,fechaVencimiento=?
    WHERE idFactura=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncModificarDocumentoActualizarNotacxc(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblnotacxc
    SET motivo=?
    WHERE idNota=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public object VentasFuncDisponibleConsultarMovimientocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT MIN(Saldo)
    FROM (SELECT fechaMovimiento,SUM(cargo-abono) OVER (ORDER BY fechaMovimiento,idMovimiento ROWS UNBOUNDED PRECEDING) Saldo
    FROM tblmovimientocxc
    WHERE idFactura=?) s
    WHERE fechaMovimiento>?";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public DataTable VentasFuncBloquearDocumentoConsultarFactura(params object[] Parametros)
        {
            const string Consulta = @"SELECT idFactura,idMoneda,fechaEmision Fecha
    FROM tblfactura
    WHERE idFactura=?
      AND serieFactura='CXC'";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncBloquearDocumentoConsultarNotacxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT f.idFactura,f.idMoneda,n.fechaNota Fecha
    FROM tblnotacxc n
    JOIN tblfactura f ON f.idFactura=n.idFactura
    WHERE n.idNota=?";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncBloquearDocumentoConsultarFactura2(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tblfactura
    WHERE idFactura=?
    FOR UPDATE";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncBloquearDocumentoConsultarNotacxc2(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tblnotacxc
    WHERE idNota=?
    FOR UPDATE";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncAnularDocumentoConsultarFactura(params object[] Parametros)
        {
            const string Consulta = @"SELECT idMoneda
    FROM tblfactura
    WHERE idFactura=?";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public object VentasFuncAnularDocumentoConsultarAplicacionpago(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblaplicacionpago
    WHERE idFactura=?
      AND estadoAplicacion='APLICADA'";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public object VentasFuncAnularDocumentoConsultarNotacxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblnotacxc
    WHERE idFactura=?
      AND estadoNota='APLICADA'";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public DataTable VentasFuncAnularDocumentoConsultarMovimientocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tblmovimientocxc
    WHERE idFactura=?
      AND tipoMovimiento='FACTURA'
    FOR UPDATE";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncAnularDocumentoConsultarMovimientocxc2(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tblmovimientocxc
    WHERE idNota=?
      AND tipoMovimiento<>'REVERSO'
    FOR UPDATE";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public object VentasFuncAnularDocumentoConsultarMovimientocxc3(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblmovimientocxc
    WHERE idMovimientoOrigen=?";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public int VentasFuncAnularDocumentoActualizarFactura(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblfactura
    SET estadoFactura='ANULADA'
    WHERE idFactura=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncAnularDocumentoActualizarNotacxc(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblnotacxc
    SET estadoNota='ANULADA'
    WHERE idNota=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }
    }
}
