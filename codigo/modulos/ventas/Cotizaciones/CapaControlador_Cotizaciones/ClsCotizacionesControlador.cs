// Jose Javier Torres Martinez | 0901-23-1091 | 20/10/2026
using System;
using System.Data;
namespace CapaControlador_Cotizaciones
{
    public sealed class ClsCotizacionesControlador
    {
        private readonly CapaModelo_Cotizaciones.ClsCotizacionesRepositorio _Repositorio = new CapaModelo_Cotizaciones.ClsCotizacionesRepositorio();
        public DataTable Catalogo(string Nombre) { return _Repositorio.Catalogo(Nombre); }
        public DataTable ListaCliente(int Cliente, int Moneda, DateTime Fecha) { return _Repositorio.ListaCliente(Cliente, Moneda, Fecha); }
        public DataTable Productos(int Cliente, int Moneda, DateTime Fecha) { return _Repositorio.Productos(Cliente, Moneda, Fecha); }
        public DataSet Consultar(string Numero) { return _Repositorio.Consultar(Numero); }
        public string Guardar(int IdCotizacion, string Numero, int Cliente, int Vendedor, int Moneda, int Usuario, DateTime Fecha, DateTime Vigencia, decimal TipoCambio, string Estado, DataTable Lineas) { return _Repositorio.Guardar(IdCotizacion, Numero, Cliente, Vendedor, Moneda, Usuario, Fecha, Vigencia, TipoCambio, Estado, Lineas); }
        public string Convertir(int IdCotizacion, bool AFactura, int Establecimiento, int Usuario) { return _Repositorio.Convertir(IdCotizacion, AFactura, Establecimiento, Usuario); }
        public void Anular(int IdCotizacion, int Usuario) { _Repositorio.Anular(IdCotizacion, Usuario); }
    }
}
