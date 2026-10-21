// Jose Javier Torres Martinez | 0901-23-1091 | 20/10/2026
using System;
using System.Data;
namespace CapaControlador_Cotizaciones
{
    public sealed class ClsCotizacionesControlador
    {
        private readonly Servicios.ClsCotizacionesServicio _Servicio = new Servicios.ClsCotizacionesServicio();
        public DataTable Listar(string Buscar) { return _Servicio.Listar(Buscar); }
        public DataTable Catalogo(string Nombre) { return _Servicio.Catalogo(Nombre); }
        public DataTable ListaCliente(int Cliente, int Moneda, DateTime Fecha) { return _Servicio.ListaCliente(Cliente, Moneda, Fecha); }
        public DataTable Productos(int Cliente, int Moneda, DateTime Fecha) { return _Servicio.Productos(Cliente, Moneda, Fecha); }
        public DataSet Consultar(string Numero) { return _Servicio.Consultar(Numero); }
        public string Guardar(int IdCotizacion, string Numero, int Cliente, int Vendedor, int Moneda, int Usuario, DateTime Fecha, DateTime Vigencia, decimal TipoCambio, string Estado, DataTable Lineas) { return _Servicio.Guardar(IdCotizacion, Numero, Cliente, Vendedor, Moneda, Usuario, Fecha, Vigencia, TipoCambio, Estado, Lineas); }
        public string Convertir(int IdCotizacion, bool AFactura, int Establecimiento, int Usuario) { return _Servicio.Convertir(IdCotizacion, AFactura, Establecimiento, Usuario); }
        public void Anular(int IdCotizacion, int Usuario) { _Servicio.Anular(IdCotizacion, Usuario); }
        public void ValidarCaptura(bool Condicion, string Mensaje) { VentasValidaciones.ClsValidacionesVentas.Exigir(Condicion, Mensaje); }
        public decimal ImporteCelda(object Valor) { return VentasValidaciones.ClsValidacionesVentas.ImporteCelda(Valor); }



        public bool PuedeEditar(int Id, string Estado, DateTime Vigencia) { return Validaciones.ClsReglasCotizaciones.PuedeEditar(Id,Estado,Vigencia); }
        public void ValidarEdicion(int Id, string Estado, DateTime Vigencia) { Validaciones.ClsReglasCotizaciones.ValidarEdicion(Id,Estado,Vigencia); }
        public object[] CalcularLinea(DataRow Producto, decimal Cantidad, decimal Descuento) { return Validaciones.ClsReglasCotizaciones.CalcularLinea(Producto,Cantidad,Descuento); }
    }
}
