// Jose Javier Torres Martinez | 0901-23-1091 | 23/10/2026
using System.Data;
using System;
using VentasDatos;

namespace CapaModelo_Cotizaciones
{
    public sealed class ClsCotizacionesRepositorio : IDisposable
    {
        public ClsConversionCotizaciones Conversion { get; }
        public ClsConsultasCotizaciones Consultas { get; }

        public ClsCotizacionesRepositorio()
        {
            // Los grupos comparten la conexion y transaccion del repositorio.
            Conversion = new ClsConversionCotizaciones(_Datos);
            Consultas = new ClsConsultasCotizaciones(_Datos);
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

        // Guardar: consultas parametrizadas.
        public object VentasFuncGuardarConsultarCliente(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblcliente
    WHERE idCliente=?
      AND is_active=1";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public object VentasFuncGuardarConsultarVendedor(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblvendedor
    WHERE idVendedor=?
      AND is_active=1";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public DataTable VentasFuncGuardarConsultarCotizacion(params object[] Parametros)
        {
            const string Consulta = @"SELECT estadoCotizacion,numeroCotizacion,vigenteHasta
    FROM tblcotizacion
    WHERE idCotizacion=?
    FOR UPDATE";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncGuardarConsultarProductoventas(params object[] Parametros)
        {
            const string Consulta = @"SELECT p.nombreProducto,p.tipoItem,p.porcentajeImpuesto,d.precioUnitario
    FROM tblproductoventas p
    JOIN tbllistapreciosdetalle d ON d.idProducto=p.idProducto
    WHERE p.idProducto=?
      AND d.idListaPrecio=?
      AND p.is_active=1
      AND d.is_active=1";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public int VentasFuncGuardarInsertarCotizacion(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblcotizacion(numeroCotizacion,idCliente,idVendedor,idMoneda,fechaEmision,vigenteHasta,tipoCambio,estadoCotizacion,subtotal,descuentoTotal,impuestoTotal,total,idUsuarioRegistro)
    VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?)";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncGuardarInsertarCotizacioncontrol(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblcotizacioncontrol(idCotizacion,idListaPrecio)
    VALUES (?,?)";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncGuardarActualizarCotizacion(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcotizacion
    SET idCliente=?,idVendedor=?,idMoneda=?,fechaEmision=?,vigenteHasta=?,tipoCambio=?,estadoCotizacion=?,subtotal=?,descuentoTotal=?,impuestoTotal=?,total=?
    WHERE idCotizacion=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncGuardarActualizarCotizacioncontrol(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcotizacioncontrol
    SET idListaPrecio=?
    WHERE idCotizacion=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncGuardarEliminarCotizaciondetalle(params object[] Parametros)
        {
            const string Consulta = @"DELETE
    FROM tblcotizaciondetalle
    WHERE idCotizacion=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncGuardarInsertarCotizaciondetalle(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblcotizaciondetalle(idCotizacion,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea)
    VALUES (?,?,?,?,?,?,?,?,?,?,?)";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        // Anular: consultas parametrizadas.
        public DataTable VentasFuncAnularConsultarCotizacion(params object[] Parametros)
        {
            const string Consulta = @"SELECT estadoCotizacion
    FROM tblcotizacion
    WHERE idCotizacion=?
    FOR UPDATE";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public int VentasFuncAnularActualizarCotizacion(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcotizacion
    SET estadoCotizacion='ANULADA'
    WHERE idCotizacion=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }
    }
}
