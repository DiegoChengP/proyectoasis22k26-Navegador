// Jose Javier Torres Martinez | 0901-23-1091 | 23/10/2026
using System.Data;
using System;
using VentasDatos;

namespace CapaModelo_Cotizaciones
{
    public sealed class ClsConversionCotizaciones
    {
        private readonly ClsDatosVentas _Datos;

        internal ClsConversionCotizaciones(ClsDatosVentas Datos)
        {
            _Datos = Datos;
        }

        // Convertir: consultas parametrizadas.
        public object VentasFuncConvertirConsultarCotizacion(params object[] Parametros)
        {
            const string Consulta = @"SELECT idMoneda
    FROM tblcotizacion
    WHERE idCotizacion=?";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public DataTable VentasFuncConvertirConsultarCotizacion2(params object[] Parametros)
        {
            const string Consulta = @"SELECT c.*,co.idPedido,co.idFactura
    FROM tblcotizacion c
    JOIN tblcotizacioncontrol co ON co.idCotizacion=c.idCotizacion
    WHERE c.idCotizacion=?
    FOR UPDATE";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public object VentasFuncConvertirConsultarFactura(params object[] Parametros)
        {
            const string Consulta = @"SELECT CONCAT(serieFactura,'-',numeroFactura)
    FROM tblfactura
    WHERE idFactura=?";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public object VentasFuncConvertirConsultarPedido(params object[] Parametros)
        {
            const string Consulta = @"SELECT numeroPedido
    FROM tblpedido
    WHERE idPedido=?";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public int VentasFuncConvertirInsertarPedido(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblpedido(numeroPedido,idCotizacion,idCliente,idVendedor,idMoneda,fechaPedido,tipoCambio,estadoPedido,subtotal,descuentoTotal,impuestoTotal,total,idUsuarioRegistro)
    VALUES (?,?,?,?,?,?,?,'CONFIRMADO',?,?,?,?,?)";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncConvertirInsertarPedidodetalle(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblpedidodetalle(idPedido,idCotizacionDetalle,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea) SELECT ?,idCotizacionDetalle,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea
    FROM tblcotizaciondetalle
    WHERE idCotizacion=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public object VentasFuncConvertirConsultarEstablecimientoventas(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tblestablecimientoventas
    WHERE idEstablecimiento=?
      AND is_active=1";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public DataTable VentasFuncConvertirConsultarCliente(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tblcliente
    WHERE idCliente=?";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public int VentasFuncConvertirInsertarFactura(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblfactura(serieFactura,numeroFactura,idEstablecimiento,idCliente,idVendedor,idMoneda,fechaEmision,fechaVencimiento,tipoDte,condicionPago,diasCreditoAplicados,tipoCambio,tipoIdentificacionReceptor,identificacionReceptor,nombreReceptor,direccionReceptor,subtotal,descuentoTotal,impuestoTotal,total,estadoFactura,observaciones,idUsuarioRegistro)
    VALUES ('COT',?,?,?,?,?,?,?,'PENDIENTE',?,?,?,?,?,?,?,?,?,?,?,'BORRADOR',?,?)";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncConvertirInsertarFacturadetalle(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tblfacturadetalle(idFactura,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea) SELECT ?,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea
    FROM tblcotizaciondetalle
    WHERE idCotizacion=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncConvertirActualizarCotizacioncontrol(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcotizacioncontrol
    SET idFactura=?
    WHERE idCotizacion=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncConvertirActualizarCotizacioncontrol2(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcotizacioncontrol
    SET idPedido=?
    WHERE idCotizacion=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncConvertirActualizarCotizacion(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tblcotizacion
    SET estadoCotizacion='CONVERTIDA'
    WHERE idCotizacion=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }
    }
}
