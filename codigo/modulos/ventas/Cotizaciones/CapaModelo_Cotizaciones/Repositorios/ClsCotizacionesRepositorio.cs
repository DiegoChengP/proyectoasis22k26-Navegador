// Jose Javier Torres Martinez | 0901-23-1091 | 22/10/2026
using System.Data;
using System;
using VentasDatos;

namespace CapaModelo_Cotizaciones
{
    public sealed class ClsCotizacionesRepositorio : IDisposable
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

        // Productos: consultas parametrizadas.
        public DataTable VentasMetProductosConsultarProductoventas(params object[] Parametros)
        {
            const string Consulta = @"SELECT p.idProducto Id,CONCAT(p.codigoProducto,' - ',p.nombreProducto) Nombre,p.nombreProducto Descripcion,p.tipoItem Tipo,p.porcentajeImpuesto Tasa,d.precioUnitario Precio
    FROM tblproductoventas p
    JOIN tbllistapreciosdetalle d ON d.idProducto=p.idProducto
    WHERE d.idListaPrecio=?
      AND d.is_active=1
      AND p.is_active=1
    ORDER BY p.nombreProducto";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // Listar: consultas parametrizadas.
        public DataTable VentasMetListarConsultarCotizacion(params object[] Parametros)
        {
            const string Consulta = @"SELECT c.idCotizacion Id,c.numeroCotizacion Numero,cl.nombreCliente Cliente,m.codigoMoneda Moneda,c.fechaEmision Fecha,c.vigenteHasta Vigencia,c.total Total,CASE WHEN c.estadoCotizacion IN ('BORRADOR','EMITIDA','ACEPTADA')
      AND c.vigenteHasta<CURRENT_DATE() THEN 'VENCIDA' ELSE c.estadoCotizacion END Estado
    FROM tblcotizacion c
    JOIN tblcliente cl ON cl.idCliente=c.idCliente
    JOIN tblmonedaventas m ON m.idMoneda=c.idMoneda
    WHERE (?='' OR c.numeroCotizacion LIKE CONCAT('%',?,'%') OR cl.nombreCliente LIKE CONCAT('%',?,'%'))
    ORDER BY c.idCotizacion DESC";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // Lista: consultas parametrizadas.
        public DataTable VentasMetListaConsultarClientelistaprecios(params object[] Parametros)
        {
            const string Consulta = @"SELECT l.idListaPrecio,l.nombreListaPrecio
    FROM tblclientelistaprecios c
    JOIN tbllistaprecios l ON l.idListaPrecio=c.idListaPrecio
    WHERE c.idCliente=?
      AND c.idMoneda=?
      AND l.idMoneda=?
      AND l.is_active=1
      AND l.vigenteDesde<=?
      AND (l.vigenteHasta IS NULL OR l.vigenteHasta>=?)";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // Guardar: consultas parametrizadas.
        public object VentasMetGuardarConsultarCliente(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblcliente
    WHERE idCliente=?
      AND is_active=1";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public object VentasMetGuardarConsultarVendedor(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblvendedor
    WHERE idVendedor=?
      AND is_active=1";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public DataTable VentasMetGuardarConsultarCotizacion(params object[] Parametros)
        {
            const string Consulta = @"SELECT estadoCotizacion,numeroCotizacion,vigenteHasta
    FROM tblcotizacion
    WHERE idCotizacion=?
    FOR UPDATE";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public DataTable VentasMetGuardarConsultarProductoventas(params object[] Parametros)
        {
            const string Consulta = @"SELECT p.nombreProducto,p.tipoItem,p.porcentajeImpuesto,d.precioUnitario
    FROM tblproductoventas p
    JOIN tbllistapreciosdetalle d ON d.idProducto=p.idProducto
    WHERE p.idProducto=?
      AND d.idListaPrecio=?
      AND p.is_active=1
      AND d.is_active=1";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public int VentasMetGuardarInsertarCotizacion(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblcotizacion(numeroCotizacion,idCliente,idVendedor,idMoneda,fechaEmision,vigenteHasta,tipoCambio,estadoCotizacion,subtotal,descuentoTotal,impuestoTotal,total,idUsuarioRegistro)
    VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?)";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public int VentasMetGuardarInsertarCotizacioncontrol(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblcotizacioncontrol(idCotizacion,idListaPrecio)
    VALUES (?,?)";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public int VentasMetGuardarActualizarCotizacion(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcotizacion
    SET idCliente=?,idVendedor=?,idMoneda=?,fechaEmision=?,vigenteHasta=?,tipoCambio=?,estadoCotizacion=?,subtotal=?,descuentoTotal=?,impuestoTotal=?,total=?
    WHERE idCotizacion=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public int VentasMetGuardarActualizarCotizacioncontrol(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcotizacioncontrol
    SET idListaPrecio=?
    WHERE idCotizacion=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public int VentasMetGuardarEliminarCotizaciondetalle(params object[] Parametros)
        {
            const string Consulta = @"DELETE
    FROM tblcotizaciondetalle
    WHERE idCotizacion=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public int VentasMetGuardarInsertarCotizaciondetalle(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblcotizaciondetalle(idCotizacion,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea)
    VALUES (?,?,?,?,?,?,?,?,?,?,?)";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        // Generales: consultas parametrizadas.
        public DataTable VentasMetGeneralesConsultarConsultarCotizacion(params object[] Parametros)
        {
            const string Consulta = @"SELECT c.*,l.nombreListaPrecio,co.idListaPrecio,co.idPedido,co.idFactura,CASE WHEN c.estadoCotizacion IN ('EMITIDA','ACEPTADA','BORRADOR')
      AND c.vigenteHasta<CURRENT_DATE() THEN 'VENCIDA' ELSE c.estadoCotizacion END EstadoVisual
    FROM tblcotizacion c
    LEFT JOIN tblcotizacioncontrol co ON co.idCotizacion=c.idCotizacion
    LEFT JOIN tbllistaprecios l ON l.idListaPrecio=co.idListaPrecio
    WHERE c.numeroCotizacion=?";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public DataTable VentasMetGeneralesConsultarConsultarCotizaciondetalle(params object[] Parametros)
        {
            const string Consulta = @"SELECT d.*,p.codigoProducto
    FROM tblcotizaciondetalle d
    JOIN tblproductoventas p ON p.idProducto=d.idProducto
    WHERE d.idCotizacion=?
    ORDER BY d.numeroLinea";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        // Convertir: consultas parametrizadas.
        public object VentasMetConvertirConsultarCotizacion(params object[] Parametros)
        {
            const string Consulta = @"SELECT idMoneda
    FROM tblcotizacion
    WHERE idCotizacion=?";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public DataTable VentasMetConvertirConsultarCotizacion2(params object[] Parametros)
        {
            const string Consulta = @"SELECT c.*,co.idPedido,co.idFactura
    FROM tblcotizacion c
    JOIN tblcotizacioncontrol co ON co.idCotizacion=c.idCotizacion
    WHERE c.idCotizacion=?
    FOR UPDATE";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public object VentasMetConvertirConsultarFactura(params object[] Parametros)
        {
            const string Consulta = @"SELECT CONCAT(serieFactura,'-',numeroFactura)
    FROM tblfactura
    WHERE idFactura=?";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public object VentasMetConvertirConsultarPedido(params object[] Parametros)
        {
            const string Consulta = @"SELECT numeroPedido
    FROM tblpedido
    WHERE idPedido=?";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public int VentasMetConvertirInsertarPedido(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblpedido(numeroPedido,idCotizacion,idCliente,idVendedor,idMoneda,fechaPedido,tipoCambio,estadoPedido,subtotal,descuentoTotal,impuestoTotal,total,idUsuarioRegistro)
    VALUES (?,?,?,?,?,?,?,'CONFIRMADO',?,?,?,?,?)";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public int VentasMetConvertirInsertarPedidodetalle(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblpedidodetalle(idPedido,idCotizacionDetalle,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea) SELECT ?,idCotizacionDetalle,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea
    FROM tblcotizaciondetalle
    WHERE idCotizacion=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public object VentasMetConvertirConsultarEstablecimientoventas(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblestablecimientoventas
    WHERE idEstablecimiento=?
      AND is_active=1";
            return _Datos.VentasMetEscalar(Consulta, Parametros);
        }

        public DataTable VentasMetConvertirConsultarCliente(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tblcliente
    WHERE idCliente=?";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public int VentasMetConvertirInsertarFactura(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblfactura(serieFactura,numeroFactura,idEstablecimiento,idCliente,idVendedor,idMoneda,fechaEmision,fechaVencimiento,tipoDte,condicionPago,diasCreditoAplicados,tipoCambio,tipoIdentificacionReceptor,identificacionReceptor,nombreReceptor,direccionReceptor,subtotal,descuentoTotal,impuestoTotal,total,estadoFactura,observaciones,idUsuarioRegistro)
    VALUES ('COT',?,?,?,?,?,?,?,'PENDIENTE',?,?,?,?,?,?,?,?,?,?,?,'BORRADOR',?,?)";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public int VentasMetConvertirInsertarFacturadetalle(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblfacturadetalle(idFactura,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea) SELECT ?,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea
    FROM tblcotizaciondetalle
    WHERE idCotizacion=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public int VentasMetConvertirActualizarCotizacioncontrol(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcotizacioncontrol
    SET idFactura=?
    WHERE idCotizacion=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public int VentasMetConvertirActualizarCotizacioncontrol2(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcotizacioncontrol
    SET idPedido=?
    WHERE idCotizacion=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        public int VentasMetConvertirActualizarCotizacion(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcotizacion
    SET estadoCotizacion='CONVERTIDA'
    WHERE idCotizacion=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }

        // Anular: consultas parametrizadas.
        public DataTable VentasMetAnularConsultarCotizacion(params object[] Parametros)
        {
            const string Consulta = @"SELECT estadoCotizacion
    FROM tblcotizacion
    WHERE idCotizacion=?
    FOR UPDATE";
            return _Datos.VentasMetTabla(Consulta, Parametros);
        }

        public int VentasMetAnularActualizarCotizacion(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcotizacion
    SET estadoCotizacion='ANULADA'
    WHERE idCotizacion=?";
            return _Datos.VentasMetEjecutar(Consulta, Parametros);
        }
    }
}
