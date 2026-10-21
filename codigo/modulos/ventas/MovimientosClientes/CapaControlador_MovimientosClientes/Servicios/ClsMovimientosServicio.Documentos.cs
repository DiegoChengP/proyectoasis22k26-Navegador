using System;
using System.Data;
using CapaModelo_MovimientosClientes;
using CapaControlador_MovimientosClientes.Validaciones;
using VentasValidaciones;

namespace CapaControlador_MovimientosClientes.Servicios
{
    internal sealed partial class ClsMovimientosServicio
    {
        public DataTable ListarDocumentos(int Cliente, int Moneda)
        {
            using (var Db = new ClsMovimientosRepositorio()) return Db.Tabla(ConsultaMovimientos.ListarDocumentosConsultarFactura, Cliente, Cliente, Moneda, Moneda, Cliente, Cliente, Moneda, Moneda);
        }

        public DataTable ListarRecibos(int Cliente, int Moneda)
        {
            using (var Db = new ClsMovimientosRepositorio()) return Db.Tabla(ConsultaMovimientos.ListarRecibosConsultarPago, Cliente, Cliente, Moneda, Moneda);
        }

        private static DataRow BloquearDocumento(ClsMovimientosRepositorio Db, int Id, string Clase)
        {
            ClsValidacionesVentas.Exigir(Clase == "FACTURA" || Clase == "NOTA", "Seleccione un documento interno de CxC.");
            DataTable Origen = Clase == "FACTURA"
                ? Db.Tabla(ConsultaMovimientos.BloquearDocumentoConsultarFactura, Id)
                : Db.Tabla(ConsultaMovimientos.BloquearDocumentoConsultarNotacxc, Id);
            ClsValidacionesVentas.Exigir(Origen.Rows.Count == 1, "El documento no existe.");
            Db.PeriodoAbierto(Convert.ToInt32(Origen.Rows[0]["idMoneda"]), Convert.ToDateTime(Origen.Rows[0]["Fecha"]));
            DataTable Factura = Db.Tabla(ConsultaMovimientos.BloquearDocumentoConsultarFactura2, Origen.Rows[0]["idFactura"]);
            ClsValidacionesVentas.Exigir((string)Factura.Rows[0]["estadoFactura"] == "EMITIDA", "La factura no esta emitida o ya fue anulada.");
            if (Clase == "FACTURA") return Factura.Rows[0];
            DataTable Nota = Db.Tabla(ConsultaMovimientos.BloquearDocumentoConsultarNotacxc2, Id);
            ClsValidacionesVentas.Exigir((string)Nota.Rows[0]["estadoNota"] == "APLICADA", "La nota ya fue anulada.");
            return Nota.Rows[0];
        }

        public void ModificarDocumento(int Id, string Clase, int Usuario, string Descripcion, DateTime Vencimiento)
        {
            Descripcion = ClsValidacionesVentas.Texto(Descripcion, 255, "descripcion");
            using (var Db = new ClsMovimientosRepositorio())
            {
                Db.Iniciar(); Db.Usuario(Usuario); DataRow Fila = BloquearDocumento(Db, Id, Clase);
                if (Clase == "FACTURA")
                {
                    ClsValidacionesVentas.Exigir(Vencimiento.Date >= Convert.ToDateTime(Fila["fechaEmision"]).Date, "El vencimiento no puede ser anterior a la emision.");
                    Db.Ejecutar(ConsultaMovimientos.ModificarDocumentoActualizarFactura, Descripcion, Vencimiento.Date, Id);
                }
                else Db.Ejecutar(ConsultaMovimientos.ModificarDocumentoActualizarNotacxc, Descripcion, Id);
                Db.Auditar(Clase + "_CXC", Id, "MODIFICAR", "Descripcion: " + Descripcion + "; vencimiento: " + Vencimiento.ToString("yyyy-MM-dd"), Usuario);
                Db.Confirmar();
            }
        }

        public void AnularDocumento(int Id, string Clase, int Usuario, string Motivo)
        {
            Motivo = ClsValidacionesVentas.Texto(Motivo, 180, "motivo de anulacion");
            using (var Db = new ClsMovimientosRepositorio())
            {
                Db.Iniciar(); Db.Usuario(Usuario); DataRow Fila = BloquearDocumento(Db, Id, Clase);
                int Factura = Convert.ToInt32(Fila["idFactura"]);
                DataRow F = Db.Tabla(ConsultaMovimientos.AnularDocumentoConsultarFactura, Factura).Rows[0];
                Db.PeriodoAbierto(Convert.ToInt32(F["idMoneda"]), DateTime.Today);
                if (Clase == "FACTURA")
                {
                    ClsValidacionesVentas.Exigir(Convert.ToInt32(Db.Escalar(ConsultaMovimientos.AnularDocumentoConsultarAplicacionpago, Factura)) == 0 &&
                        Convert.ToInt32(Db.Escalar(ConsultaMovimientos.AnularDocumentoConsultarNotacxc, Factura)) == 0,
                        "Anule primero los recibos y las notas aplicados a esta factura.");
                }
                DataTable Libro = Clase == "FACTURA"
                    ? Db.Tabla(ConsultaMovimientos.AnularDocumentoConsultarMovimientocxc, Factura)
                    : Db.Tabla(ConsultaMovimientos.AnularDocumentoConsultarMovimientocxc2, Id);
                ClsValidacionesVentas.Exigir(Libro.Rows.Count == 1, "El movimiento original no se puede identificar de forma unica.");
                DataRow M = Libro.Rows[0];
                ClsValidacionesVentas.Exigir(Convert.ToInt32(Db.Escalar(ConsultaMovimientos.AnularDocumentoConsultarMovimientocxc3, M["idMovimiento"])) == 0, "El documento ya tiene un reverso.");
                if ((decimal)M["cargo"] > 0) ClsValidacionesVentas.Exigir(Disponible(Db, Factura, DateTime.Today) >= (decimal)M["cargo"], "La anulacion dejaria un saldo negativo. Revise pagos y ajustes posteriores.");
                Movimiento(Db, Factura, DateTime.Today, "REVERSO", "Anulacion: " + Motivo, (decimal)M["abono"], (decimal)M["cargo"], Usuario, null, null, Convert.ToInt32(M["idMovimiento"]));
                Db.Ejecutar(Clase == "FACTURA"  ? ConsultaMovimientos.AnularDocumentoActualizarFactura : ConsultaMovimientos.AnularDocumentoActualizarNotacxc, Id);
                Db.Auditar(Clase + "_CXC", Id, "ANULAR", Motivo, Usuario); Db.Confirmar();
            }
        }

        public void ModificarRecibo(int Pago, int Usuario, string Observaciones)
        {
            Observaciones = ClsValidacionesVentas.Texto(Observaciones, 500, "observaciones", false);
            using (var Db = new ClsMovimientosRepositorio())
            {
                Db.Iniciar(); Db.Usuario(Usuario);
                DataTable Origen = Db.Tabla(ConsultaMovimientos.ModificarReciboConsultarPago, Pago);
                ClsValidacionesVentas.Exigir(Origen.Rows.Count == 1, "No existe el recibo.");
                Db.PeriodoAbierto(Convert.ToInt32(Origen.Rows[0]["idMoneda"]), Convert.ToDateTime(Origen.Rows[0]["fechaPago"]));
                DataTable Actual = Db.Tabla(ConsultaMovimientos.ModificarReciboConsultarPago2, Pago);
                ClsValidacionesVentas.Exigir((string)Actual.Rows[0][0] == "CONFIRMADO", "El recibo fue anulado.");
                ClsValidacionesVentas.Exigir(Convert.ToInt32(Db.Escalar(ConsultaMovimientos.AnularReciboConsultarDepositoventasdetalle, Pago)) == 0, "El recibo ya forma parte de un deposito; coordine el cambio con Bancos.");
                Db.Ejecutar(ConsultaMovimientos.ModificarReciboActualizarPago, Observaciones, Pago);
                Db.Auditar("RECIBO", Pago, "MODIFICAR", Observaciones, Usuario); Db.Confirmar();
            }
        }
        public DataRow LeerDocumento(int Id, string Clase)
        {
            ClsValidacionesVentas.Exigir(Id > 0 && (Clase == "FACTURA" || Clase == "NOTA"), "Seleccione un documento de CxC.");
            using (var Db = new ClsMovimientosRepositorio())
            {
                DataTable Datos=Db.Tabla(Clase == "FACTURA" ? ConsultaMovimientos.LeerFactura : ConsultaMovimientos.LeerNota, Id);
                ClsValidacionesVentas.Exigir(Datos.Rows.Count == 1, "No se encontro el documento interno.");
                return Datos.Rows[0];
            }
        }
        public DataSet LeerRecibo(int Id)
        {
            using (var Db = new ClsMovimientosRepositorio())
            {
                var Datos=new DataSet();
                Datos.Tables.Add(Db.Tabla(ConsultaMovimientos.LeerRecibo,Id));
                ClsValidacionesVentas.Exigir(Datos.Tables[0].Rows.Count == 1, "No se encontro el recibo.");
                Datos.Tables.Add(Db.Tabla(ConsultaMovimientos.LeerAplicacionesRecibo,Id));
                return Datos;
            }
        }
        public DataRow LeerMovimiento(int Id)
        {
            using(var Db=new ClsMovimientosRepositorio())
            {
                DataTable Datos=Db.Tabla(ConsultaMovimientos.LeerMovimiento,Id);
                ClsValidacionesVentas.Exigir(Datos.Rows.Count == 1, "El documento pertenece a Facturas o no admite cambios desde CxC.");
                return Datos.Rows[0];
            }
        }
    }
}
