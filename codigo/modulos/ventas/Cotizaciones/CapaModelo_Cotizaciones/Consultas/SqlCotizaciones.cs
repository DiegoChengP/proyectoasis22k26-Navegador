using System;

namespace CapaModelo_Cotizaciones
{
    public enum ConsultaCotizaciones
    {
        ListaConsultarClientelistaprecios,
        ProductosConsultarProductoventas,
        ConsultarConsultarCotizacion,
        ConsultarConsultarCotizaciondetalle,
        GuardarConsultarCliente,
        GuardarConsultarVendedor,
        GuardarConsultarCotizacion,
        GuardarConsultarProductoventas,
        GuardarInsertarCotizacion,
        GuardarInsertarCotizacioncontrol,
        GuardarActualizarCotizacion,
        GuardarActualizarCotizacioncontrol,
        GuardarEliminarCotizaciondetalle,
        GuardarInsertarCotizaciondetalle,
        ConvertirConsultarCotizacion,
        ConvertirConsultarCotizacion2,
        ConvertirConsultarFactura,
        ConvertirConsultarPedido,
        ConvertirInsertarPedido,
        ConvertirInsertarPedidodetalle,
        ConvertirConsultarEstablecimientoventas,
        ConvertirConsultarCliente,
        ConvertirInsertarFactura,
        ConvertirInsertarFacturadetalle,
        ConvertirActualizarCotizacioncontrol,
        ConvertirActualizarCotizacioncontrol2,
        ConvertirActualizarCotizacion,
        AnularConsultarCotizacion,
        AnularActualizarCotizacion,
        ListarConsultarCotizacion,
    }

    internal static class SqlCotizaciones
    {
        internal static string Obtener(ConsultaCotizaciones Consulta)
        {
            switch (Consulta)
            {
                case ConsultaCotizaciones.ListaConsultarClientelistaprecios: return "SELECT l.idListaPrecio,l.nombreListaPrecio FROM tblclientelistaprecios c JOIN tbllistaprecios l ON l.idListaPrecio=c.idListaPrecio WHERE c.idCliente=? AND c.idMoneda=? AND l.idMoneda=? AND l.is_active=1 AND l.vigenteDesde<=? AND (l.vigenteHasta IS NULL OR l.vigenteHasta>=?)";
                case ConsultaCotizaciones.ProductosConsultarProductoventas: return "SELECT p.idProducto Id,CONCAT(p.codigoProducto,' - ',p.nombreProducto) Nombre,p.nombreProducto Descripcion,p.tipoItem Tipo,p.porcentajeImpuesto Tasa,d.precioUnitario Precio FROM tblproductoventas p JOIN tbllistapreciosdetalle d ON d.idProducto=p.idProducto WHERE d.idListaPrecio=? AND d.is_active=1 AND p.is_active=1 ORDER BY p.nombreProducto";
                case ConsultaCotizaciones.ConsultarConsultarCotizacion: return "SELECT c.*,l.nombreListaPrecio,co.idListaPrecio,co.idPedido,co.idFactura,CASE WHEN c.estadoCotizacion IN ('EMITIDA','ACEPTADA','BORRADOR') AND c.vigenteHasta<CURRENT_DATE() THEN 'VENCIDA' ELSE c.estadoCotizacion END EstadoVisual FROM tblcotizacion c LEFT JOIN tblcotizacioncontrol co ON co.idCotizacion=c.idCotizacion LEFT JOIN tbllistaprecios l ON l.idListaPrecio=co.idListaPrecio WHERE c.numeroCotizacion=?";
                case ConsultaCotizaciones.ConsultarConsultarCotizaciondetalle: return "SELECT d.*,p.codigoProducto FROM tblcotizaciondetalle d JOIN tblproductoventas p ON p.idProducto=d.idProducto WHERE d.idCotizacion=? ORDER BY d.numeroLinea";
                case ConsultaCotizaciones.GuardarConsultarCliente: return "SELECT COUNT(*) FROM tblcliente WHERE idCliente=? AND is_active=1";
                case ConsultaCotizaciones.GuardarConsultarVendedor: return "SELECT COUNT(*) FROM tblvendedor WHERE idVendedor=? AND is_active=1";
                case ConsultaCotizaciones.GuardarConsultarCotizacion: return "SELECT estadoCotizacion,numeroCotizacion,vigenteHasta FROM tblcotizacion WHERE idCotizacion=? FOR UPDATE";
                case ConsultaCotizaciones.GuardarConsultarProductoventas: return "SELECT p.nombreProducto,p.tipoItem,p.porcentajeImpuesto,d.precioUnitario FROM tblproductoventas p JOIN tbllistapreciosdetalle d ON d.idProducto=p.idProducto WHERE p.idProducto=? AND d.idListaPrecio=? AND p.is_active=1 AND d.is_active=1";
                case ConsultaCotizaciones.GuardarInsertarCotizacion: return "INSERT INTO tblcotizacion(numeroCotizacion,idCliente,idVendedor,idMoneda,fechaEmision,vigenteHasta,tipoCambio,estadoCotizacion,subtotal,descuentoTotal,impuestoTotal,total,idUsuarioRegistro) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?)";
                case ConsultaCotizaciones.GuardarInsertarCotizacioncontrol: return "INSERT INTO tblcotizacioncontrol(idCotizacion,idListaPrecio) VALUES (?,?)";
                case ConsultaCotizaciones.GuardarActualizarCotizacion: return "UPDATE tblcotizacion SET idCliente=?,idVendedor=?,idMoneda=?,fechaEmision=?,vigenteHasta=?,tipoCambio=?,estadoCotizacion=?,subtotal=?,descuentoTotal=?,impuestoTotal=?,total=? WHERE idCotizacion=?";
                case ConsultaCotizaciones.GuardarActualizarCotizacioncontrol: return "UPDATE tblcotizacioncontrol SET idListaPrecio=? WHERE idCotizacion=?";
                case ConsultaCotizaciones.GuardarEliminarCotizaciondetalle: return "DELETE FROM tblcotizaciondetalle WHERE idCotizacion=?";
                case ConsultaCotizaciones.GuardarInsertarCotizaciondetalle: return "INSERT INTO tblcotizaciondetalle(idCotizacion,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea) VALUES (?,?,?,?,?,?,?,?,?,?,?)";
                case ConsultaCotizaciones.ConvertirConsultarCotizacion: return "SELECT idMoneda FROM tblcotizacion WHERE idCotizacion=?";
                case ConsultaCotizaciones.ConvertirConsultarCotizacion2: return "SELECT c.*,co.idPedido,co.idFactura FROM tblcotizacion c JOIN tblcotizacioncontrol co ON co.idCotizacion=c.idCotizacion WHERE c.idCotizacion=? FOR UPDATE";
                case ConsultaCotizaciones.ConvertirConsultarFactura: return "SELECT CONCAT(serieFactura,'-',numeroFactura) FROM tblfactura WHERE idFactura=?";
                case ConsultaCotizaciones.ConvertirConsultarPedido: return "SELECT numeroPedido FROM tblpedido WHERE idPedido=?";
                case ConsultaCotizaciones.ConvertirInsertarPedido: return "INSERT INTO tblpedido(numeroPedido,idCotizacion,idCliente,idVendedor,idMoneda,fechaPedido,tipoCambio,estadoPedido,subtotal,descuentoTotal,impuestoTotal,total,idUsuarioRegistro) VALUES (?,?,?,?,?,?,?,'CONFIRMADO',?,?,?,?,?)";
                case ConsultaCotizaciones.ConvertirInsertarPedidodetalle: return "INSERT INTO tblpedidodetalle(idPedido,idCotizacionDetalle,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea) SELECT ?,idCotizacionDetalle,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea FROM tblcotizaciondetalle WHERE idCotizacion=?";
                case ConsultaCotizaciones.ConvertirConsultarEstablecimientoventas: return "SELECT COUNT(*) FROM tblestablecimientoventas WHERE idEstablecimiento=? AND is_active=1";
                case ConsultaCotizaciones.ConvertirConsultarCliente: return "SELECT * FROM tblcliente WHERE idCliente=?";
                case ConsultaCotizaciones.ConvertirInsertarFactura: return "INSERT INTO tblfactura(serieFactura,numeroFactura,idEstablecimiento,idCliente,idVendedor,idMoneda,fechaEmision,fechaVencimiento,tipoDte,condicionPago,diasCreditoAplicados,tipoCambio,tipoIdentificacionReceptor,identificacionReceptor,nombreReceptor,direccionReceptor,subtotal,descuentoTotal,impuestoTotal,total,estadoFactura,observaciones,idUsuarioRegistro) VALUES ('COT',?,?,?,?,?,?,?,'PENDIENTE',?,?,?,?,?,?,?,?,?,?,?,'BORRADOR',?,?)";
                case ConsultaCotizaciones.ConvertirInsertarFacturadetalle: return "INSERT INTO tblfacturadetalle(idFactura,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea) SELECT ?,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea FROM tblcotizaciondetalle WHERE idCotizacion=?";
                case ConsultaCotizaciones.ConvertirActualizarCotizacioncontrol: return "UPDATE tblcotizacioncontrol SET idFactura=? WHERE idCotizacion=?";
                case ConsultaCotizaciones.ConvertirActualizarCotizacioncontrol2: return "UPDATE tblcotizacioncontrol SET idPedido=? WHERE idCotizacion=?";
                case ConsultaCotizaciones.ConvertirActualizarCotizacion: return "UPDATE tblcotizacion SET estadoCotizacion='CONVERTIDA' WHERE idCotizacion=?";
                case ConsultaCotizaciones.AnularConsultarCotizacion: return "SELECT estadoCotizacion FROM tblcotizacion WHERE idCotizacion=? FOR UPDATE";
                case ConsultaCotizaciones.AnularActualizarCotizacion: return "UPDATE tblcotizacion SET estadoCotizacion='ANULADA' WHERE idCotizacion=?";
                case ConsultaCotizaciones.ListarConsultarCotizacion: return "SELECT c.idCotizacion Id,c.numeroCotizacion Numero,cl.nombreCliente Cliente,m.codigoMoneda Moneda,c.fechaEmision Fecha,c.vigenteHasta Vigencia,c.total Total,CASE WHEN c.estadoCotizacion IN ('BORRADOR','EMITIDA','ACEPTADA') AND c.vigenteHasta<CURRENT_DATE() THEN 'VENCIDA' ELSE c.estadoCotizacion END Estado FROM tblcotizacion c JOIN tblcliente cl ON cl.idCliente=c.idCliente JOIN tblmonedaventas m ON m.idMoneda=c.idMoneda WHERE (?='' OR c.numeroCotizacion LIKE CONCAT('%',?,'%') OR cl.nombreCliente LIKE CONCAT('%',?,'%')) ORDER BY c.idCotizacion DESC";
                default: throw new ArgumentOutOfRangeException("Consulta");
            }
        }
    }
}
