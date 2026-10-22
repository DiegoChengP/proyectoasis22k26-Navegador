// Jose Javier Torres Martinez | 0901-23-1091 | 22/10/2026
using System.Data;
using System;

namespace CapaControlador_Cotizaciones
{
    public sealed class ClsCotizacionesControlador
    {
        private readonly Servicios.ClsCotizacionesServicio _Servicio = new Servicios.ClsCotizacionesServicio();
        public DataTable VentasMetListar(string Buscar)
        {
            return _Servicio.VentasMetListar(Buscar);
        }

        public DataTable VentasMetCatalogo(string Nombre)
        {
            return _Servicio.VentasMetCatalogo(Nombre);
        }

        public DataTable VentasMetListaCliente(int Cliente, int Moneda, DateTime Fecha)
        {
            return _Servicio.VentasMetListaCliente(Cliente, Moneda, Fecha);
        }

        public DataTable VentasMetProductos(int Cliente, int Moneda, DateTime Fecha)
        {
            return _Servicio.VentasMetProductos(Cliente, Moneda, Fecha);
        }

        public DataSet VentasMetConsultar(string Numero)
        {
            return _Servicio.VentasMetConsultar(Numero);
        }

        public string VentasMetGuardar(int IdCotizacion, string Numero, int Cliente, int Vendedor, int Moneda, int Usuario, DateTime Fecha, DateTime Vigencia, decimal TipoCambio, string Estado, DataTable Lineas)
        {
            return _Servicio.VentasMetGuardar(IdCotizacion, Numero, Cliente, Vendedor, Moneda, Usuario, Fecha, Vigencia, TipoCambio, Estado, Lineas);
        }

        public string VentasMetConvertir(int IdCotizacion, bool AFactura, int Establecimiento, int Usuario)
        {
            return _Servicio.VentasMetConvertir(IdCotizacion, AFactura, Establecimiento, Usuario);
        }

        public void VentasMetAnular(int IdCotizacion, int Usuario)
        {
            _Servicio.VentasMetAnular(IdCotizacion, Usuario);
        }

        public void VentasMetValidarCaptura(bool Condicion, string Mensaje)
        {
            VentasValidaciones.ClsValidacionesVentas.VentasMetExigir(Condicion, Mensaje);
        }

        public decimal VentasFuncImporteCelda(object Valor)
        {
            return VentasValidaciones.ClsValidacionesVentas.VentasFuncImporteCelda(Valor);
        }

        public bool VentasMetPuedeEditar(int Id, string Estado, DateTime Vigencia)
        {
            return Validaciones.ClsReglasCotizaciones.VentasMetPuedeEditar(Id, Estado, Vigencia);
        }

        public void VentasMetValidarEdicion(int Id, string Estado, DateTime Vigencia)
        {
            Validaciones.ClsReglasCotizaciones.VentasMetValidarEdicion(Id, Estado, Vigencia);
        }

        public object[] VentasMetCalcularLinea(DataRow Producto, decimal Cantidad, decimal Descuento)
        {
            return Validaciones.ClsReglasCotizaciones.VentasMetCalcularLinea(Producto, Cantidad, Descuento);
        }
    }
}
