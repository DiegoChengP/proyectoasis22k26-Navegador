// Jose Javier Torres Martinez | 0901-23-1091 | 20/10/2026
using System;
using System.Data;
namespace CapaControlador_MovimientosClientes
{
    public sealed class ClsMovimientosControlador
    {
        private readonly Servicios.ClsMovimientosServicio _Servicio = new Servicios.ClsMovimientosServicio();
        public DataTable ListarDocumentos(int Cliente, int Moneda) { return _Servicio.ListarDocumentos(Cliente, Moneda); }
        public DataTable ListarRecibos(int Cliente, int Moneda) { return _Servicio.ListarRecibos(Cliente, Moneda); }
        public void ModificarDocumento(int Id, string Clase, int Usuario, string Descripcion, DateTime Vencimiento) { _Servicio.ModificarDocumento(Id, Clase, Usuario, Descripcion, Vencimiento); }
        public void AnularDocumento(int Id, string Clase, int Usuario, string Motivo) { _Servicio.AnularDocumento(Id, Clase, Usuario, Motivo); }
        public void ModificarRecibo(int Id, int Usuario, string Observaciones) { _Servicio.ModificarRecibo(Id, Usuario, Observaciones); }
        public DataTable Catalogo(string Nombre) { return _Servicio.Catalogo(Nombre); }
        public DataSet EstadoCuenta(int Cliente, int Moneda, DateTime Desde, DateTime Hasta) { return _Servicio.EstadoCuenta(Cliente, Moneda, Desde, Hasta); }
        public DataTable Pendientes(int Cliente, int Moneda) { return _Servicio.Pendientes(Cliente, Moneda); }
        public string RegistrarDocumento(int Cliente, int Moneda, int Usuario, int Establecimiento, string Numero, int Tipo, DateTime Fecha, DateTime Vencimiento, decimal Importe, string Referencia, string Motivo) { return _Servicio.RegistrarDocumento(Cliente, Moneda, Usuario, Establecimiento, Numero, Tipo, Fecha, Vencimiento, Importe, Referencia, Motivo); }
        public string RegistrarRecibo(int Cliente, int Moneda, int Usuario, string Numero, DateTime Fecha, decimal Monto, decimal TipoCambio, string Forma, string Referencia, string Observaciones, DataTable Aplicaciones) { return _Servicio.RegistrarRecibo(Cliente, Moneda, Usuario, Numero, Fecha, Monto, TipoCambio, Forma, Referencia, Observaciones, Aplicaciones); }
        public DataTable ConsultarRecibos(string Recibo, string Deposito) { return _Servicio.ConsultarRecibos(Recibo, Deposito); }
        public DataTable Aplicaciones(int Pago) { return _Servicio.Aplicaciones(Pago); }
        public DataTable Documento(int Cliente, int Moneda, int Establecimiento, string Numero, bool Factura) { return _Servicio.Documento(Cliente, Moneda, Establecimiento, Numero, Factura); }
        public DataSet Deposito(string Numero) { return _Servicio.Deposito(Numero); }
        public DataTable RecibosDisponibles(int Moneda) { return _Servicio.RecibosDisponibles(Moneda); }
        public string PrepararDeposito(int Moneda, int Usuario, DateTime Fecha, string Numero, string Empresa, string Banco, string Cuenta, string Concepto, string Referencia, DataTable Recibos) { return _Servicio.PrepararDeposito(Moneda, Usuario, Fecha, Numero, Empresa, Banco, Cuenta, Concepto, Referencia, Recibos); }
        public void AnularRecibo(int Pago, DateTime Fecha, int Usuario, string Motivo) { _Servicio.AnularRecibo(Pago, Fecha, Usuario, Motivo); }
        public void ValidarCaptura(bool Condicion, string Mensaje) { VentasValidaciones.ClsValidacionesVentas.Exigir(Condicion, Mensaje); }
        public decimal ImporteCelda(object Valor) { return VentasValidaciones.ClsValidacionesVentas.ImporteCelda(Valor); }
        public DataRow LeerDocumento(int Id, string Clase) { return _Servicio.LeerDocumento(Id,Clase); }
        public DataSet LeerRecibo(int Id) { return _Servicio.LeerRecibo(Id); }
        public DataRow LeerMovimiento(int Id) { return _Servicio.LeerMovimiento(Id); }
    }
}
