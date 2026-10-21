// Jose Javier Torres Martinez | 0901-23-1091 | 20/10/2026
using System;
using System.Data;
namespace CapaControlador_MovimientosClientes
{
    public sealed class ClsMovimientosControlador
    {
        private readonly CapaModelo_MovimientosClientes.ClsMovimientosRepositorio _Repositorio = new CapaModelo_MovimientosClientes.ClsMovimientosRepositorio();
        public DataTable Catalogo(string Nombre) { return _Repositorio.Catalogo(Nombre); }
        public DataSet EstadoCuenta(int Cliente, int Moneda, DateTime Desde, DateTime Hasta) { return _Repositorio.EstadoCuenta(Cliente, Moneda, Desde, Hasta); }
        public DataTable Pendientes(int Cliente, int Moneda) { return _Repositorio.Pendientes(Cliente, Moneda); }
        public string RegistrarDocumento(int Cliente, int Moneda, int Usuario, int Establecimiento, string Numero, int Tipo, DateTime Fecha, DateTime Vencimiento, decimal Importe, string Referencia, string Motivo) { return _Repositorio.RegistrarDocumento(Cliente, Moneda, Usuario, Establecimiento, Numero, Tipo, Fecha, Vencimiento, Importe, Referencia, Motivo); }
        public string RegistrarRecibo(int Cliente, int Moneda, int Usuario, string Numero, DateTime Fecha, decimal Monto, decimal TipoCambio, string Forma, string Referencia, string Observaciones, DataTable Aplicaciones) { return _Repositorio.RegistrarRecibo(Cliente, Moneda, Usuario, Numero, Fecha, Monto, TipoCambio, Forma, Referencia, Observaciones, Aplicaciones); }
        public DataTable ConsultarRecibos(string Recibo, string Deposito) { return _Repositorio.ConsultarRecibos(Recibo, Deposito); }
        public DataTable Aplicaciones(int Pago) { return _Repositorio.Aplicaciones(Pago); }
        public DataTable Documento(int Cliente, int Moneda, int Establecimiento, string Numero, bool Factura) { return _Repositorio.Documento(Cliente, Moneda, Establecimiento, Numero, Factura); }
        public DataSet Deposito(string Numero) { return _Repositorio.Deposito(Numero); }
        public DataTable RecibosDisponibles(int Moneda) { return _Repositorio.RecibosDisponibles(Moneda); }
        public string PrepararDeposito(int Moneda, int Usuario, DateTime Fecha, string Numero, string Empresa, string Banco, string Cuenta, string Concepto, string Referencia, DataTable Recibos) { return _Repositorio.PrepararDeposito(Moneda, Usuario, Fecha, Numero, Empresa, Banco, Cuenta, Concepto, Referencia, Recibos); }
        public void AnularRecibo(int Pago, DateTime Fecha, int Usuario, string Motivo) { _Repositorio.AnularRecibo(Pago, Fecha, Usuario, Motivo); }
    }
}
