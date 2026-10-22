// Jose Javier Torres Martinez | 0901-23-1091 | 22/10/2026
using System.Data;
using System;

namespace CapaControlador_MovimientosClientes
{
    public sealed class ClsMovimientosControlador
    {
        private readonly Servicios.ClsMovimientosServicio _Servicio = new Servicios.ClsMovimientosServicio();
        public DataTable VentasMetListarDocumentos(int Cliente, int Moneda)
        {
            return _Servicio.VentasMetListarDocumentos(Cliente, Moneda);
        }

        public DataTable VentasMetListarRecibos(int Cliente, int Moneda)
        {
            return _Servicio.VentasMetListarRecibos(Cliente, Moneda);
        }

        public void VentasMetModificarDocumento(int Id, string Clase, int Usuario, string Descripcion, DateTime Vencimiento)
        {
            _Servicio.VentasMetModificarDocumento(Id, Clase, Usuario, Descripcion, Vencimiento);
        }

        public void VentasMetAnularDocumento(int Id, string Clase, int Usuario, string Motivo)
        {
            _Servicio.VentasMetAnularDocumento(Id, Clase, Usuario, Motivo);
        }

        public void VentasMetModificarRecibo(int Id, int Usuario, string Observaciones)
        {
            _Servicio.VentasMetModificarRecibo(Id, Usuario, Observaciones);
        }

        public DataTable VentasMetCatalogo(string Nombre)
        {
            return _Servicio.VentasMetCatalogo(Nombre);
        }

        public DataSet VentasMetEstadoCuenta(int Cliente, int Moneda, DateTime Desde, DateTime Hasta)
        {
            return _Servicio.VentasMetEstadoCuenta(Cliente, Moneda, Desde, Hasta);
        }

        public DataTable VentasMetPendientes(int Cliente, int Moneda)
        {
            return _Servicio.VentasMetPendientes(Cliente, Moneda);
        }

        public string VentasMetRegistrarDocumento(int Cliente, int Moneda, int Usuario, int Establecimiento, string Numero, int Tipo, DateTime Fecha, DateTime Vencimiento, decimal Importe, string Referencia, string Motivo)
        {
            return _Servicio.VentasMetRegistrarDocumento(Cliente, Moneda, Usuario, Establecimiento, Numero, Tipo, Fecha, Vencimiento, Importe, Referencia, Motivo);
        }

        public string VentasMetRegistrarRecibo(int Cliente, int Moneda, int Usuario, string Numero, DateTime Fecha, decimal Monto, decimal TipoCambio, string Forma, string Referencia, string Observaciones, DataTable Aplicaciones)
        {
            return _Servicio.VentasMetRegistrarRecibo(Cliente, Moneda, Usuario, Numero, Fecha, Monto, TipoCambio, Forma, Referencia, Observaciones, Aplicaciones);
        }

        public DataTable VentasMetConsultarRecibos(string Recibo, string Deposito)
        {
            return _Servicio.VentasMetConsultarRecibos(Recibo, Deposito);
        }

        public DataTable VentasMetAplicaciones(int Pago)
        {
            return _Servicio.VentasMetAplicaciones(Pago);
        }

        public DataTable VentasMetDocumento(int Cliente, int Moneda, int Establecimiento, string Numero, bool Factura)
        {
            return _Servicio.VentasMetDocumento(Cliente, Moneda, Establecimiento, Numero, Factura);
        }

        public DataSet VentasMetDeposito(string Numero)
        {
            return _Servicio.VentasMetDeposito(Numero);
        }

        public DataTable VentasMetRecibosDisponibles(int Moneda)
        {
            return _Servicio.VentasMetRecibosDisponibles(Moneda);
        }

        public string VentasMetPrepararDeposito(int Moneda, int Usuario, DateTime Fecha, string Numero, string Empresa, string Banco, string Cuenta, string Concepto, string Referencia, DataTable Recibos)
        {
            return _Servicio.VentasMetPrepararDeposito(Moneda, Usuario, Fecha, Numero, Empresa, Banco, Cuenta, Concepto, Referencia, Recibos);
        }

        public void VentasMetAnularRecibo(int Pago, DateTime Fecha, int Usuario, string Motivo)
        {
            _Servicio.VentasMetAnularRecibo(Pago, Fecha, Usuario, Motivo);
        }

        public void VentasMetValidarCaptura(bool Condicion, string Mensaje)
        {
            VentasValidaciones.ClsValidacionesVentas.VentasMetExigir(Condicion, Mensaje);
        }

        public decimal VentasFuncImporteCelda(object Valor)
        {
            return VentasValidaciones.ClsValidacionesVentas.VentasFuncImporteCelda(Valor);
        }

        public DataRow VentasMetLeerDocumento(int Id, string Clase)
        {
            return _Servicio.VentasMetLeerDocumento(Id, Clase);
        }

        public DataSet VentasMetLeerRecibo(int Id)
        {
            return _Servicio.VentasMetLeerRecibo(Id);
        }

        public DataRow VentasMetLeerMovimiento(int Id)
        {
            return _Servicio.VentasMetLeerMovimiento(Id);
        }
    }
}
