// Jose Javier Torres Martinez | 0901-23-1091 | 23/10/2026
using System.Data;
using System;
using VentasDatos;

namespace CapaModelo_Cotizaciones
{
    public sealed class ClsConsultasCotizaciones
    {
        private readonly ClsDatosVentas _Datos;

        internal ClsConsultasCotizaciones(ClsDatosVentas Datos)
        {
            _Datos = Datos;
        }

        // Productos: consultas parametrizadas.
        public DataTable VentasFuncProductosConsultarProductoventas(params object[] Parametros)
        {
            const string Consulta = @"SELECT p.idProducto Id,CONCAT(p.codigoProducto,' - ',p.nombreProducto) Nombre,p.nombreProducto Descripcion,p.tipoItem Tipo,p.porcentajeImpuesto Tasa,d.precioUnitario Precio
    FROM tblproductoventas p
    JOIN tbllistapreciosdetalle d ON d.idProducto=p.idProducto
    WHERE d.idListaPrecio=?
      AND d.is_active=1
      AND p.is_active=1
    ORDER BY p.nombreProducto";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        // Listar: consultas parametrizadas.
        public DataTable VentasFuncListarConsultarCotizacion(params object[] Parametros)
        {
            const string Consulta = @"SELECT c.idCotizacion Id,c.numeroCotizacion Numero,cl.nombreCliente Cliente,m.codigoMoneda Moneda,c.fechaEmision Fecha,c.vigenteHasta Vigencia,c.total Total,CASE WHEN c.estadoCotizacion IN ('BORRADOR','EMITIDA','ACEPTADA')
      AND c.vigenteHasta<CURRENT_DATE() THEN 'VENCIDA' ELSE c.estadoCotizacion END Estado
    FROM tblcotizacion c
    JOIN tblcliente cl ON cl.idCliente=c.idCliente
    JOIN tblmonedaventas m ON m.idMoneda=c.idMoneda
    WHERE (?='' OR c.numeroCotizacion LIKE CONCAT('%',?,'%') OR cl.nombreCliente LIKE CONCAT('%',?,'%'))
    ORDER BY c.idCotizacion DESC";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        // Lista: consultas parametrizadas.
        public DataTable VentasFuncListaConsultarClientelistaprecios(params object[] Parametros)
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
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        // Generales: consultas parametrizadas.
        public DataTable VentasFuncGeneralesConsultarConsultarCotizacion(params object[] Parametros)
        {
            const string Consulta = @"SELECT c.*,l.nombreListaPrecio,co.idListaPrecio,co.idPedido,co.idFactura,CASE WHEN c.estadoCotizacion IN ('EMITIDA','ACEPTADA','BORRADOR')
      AND c.vigenteHasta<CURRENT_DATE() THEN 'VENCIDA' ELSE c.estadoCotizacion END EstadoVisual
    FROM tblcotizacion c
    LEFT JOIN tblcotizacioncontrol co ON co.idCotizacion=c.idCotizacion
    LEFT JOIN tbllistaprecios l ON l.idListaPrecio=co.idListaPrecio
    WHERE c.numeroCotizacion=?";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncGeneralesConsultarConsultarCotizaciondetalle(params object[] Parametros)
        {
            const string Consulta = @"SELECT d.*,p.codigoProducto
    FROM tblcotizaciondetalle d
    JOIN tblproductoventas p ON p.idProducto=d.idProducto
    WHERE d.idCotizacion=?
    ORDER BY d.numeroLinea";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }
    }
}
