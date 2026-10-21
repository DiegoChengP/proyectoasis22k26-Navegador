// Jose Javier Torres Martinez | 0901-23-1091 | 20/10/2026
using System;
using System.Data;
using System.Linq;
using CapaModelo_MovimientosClientes;
using CapaControlador_MovimientosClientes.Validaciones;
using VentasValidaciones;

namespace CapaControlador_MovimientosClientes.Servicios
{
    internal sealed partial class ClsMovimientosServicio
    {
        public DataTable Catalogo(string Nombre) { using (var Db = new ClsMovimientosRepositorio()) return Db.Catalogo(Nombre); }


        public DataSet EstadoCuenta(int Cliente, int Moneda, DateTime Desde, DateTime Hasta)
        {
            ClsValidacionesVentas.Exigir(Cliente > 0 && Moneda > 0, "Seleccione cliente y moneda.");
            ClsValidacionesVentas.Exigir(Desde.Date <= Hasta.Date, "La fecha inicial supera la fecha final.");
            using (var Db = new ClsMovimientosRepositorio())
            {
                var Datos = new DataSet();
                Datos.Tables.Add(Db.Tabla(ConsultaMovimientos.EstadoCuentaConsultarMovimientocxc,
                    Desde.Date, Desde.Date, Desde.Date, Cliente, Moneda, Hasta.Date));
                DataTable Libro = Db.Tabla(ConsultaMovimientos.EstadoCuentaConsultarMovimientocxc2, Cliente, Moneda, Desde.Date, Hasta.Date);
                Libro.Columns.Add("Saldo", typeof(decimal));
                decimal Saldo = ClsValidacionesVentas.Decimal(Datos.Tables[0].Rows[0]["Inicial"]);
                foreach (DataRow Fila in Libro.Rows) { Saldo += (decimal)Fila["Cargo"] - (decimal)Fila["Abono"]; Fila["Saldo"] = Saldo; }
                Datos.Tables.Add(Libro);
                return Datos;
            }
        }

        public DataTable Pendientes(int Cliente, int Moneda)
        {
            using (var Db = new ClsMovimientosRepositorio()) return Db.Tabla(ConsultaMovimientos.PendientesConsultarFactura, Cliente, Moneda);
        }

        private static void Movimiento(ClsMovimientosRepositorio Db, int Factura, DateTime Fecha, string Tipo, string Descripcion, decimal Cargo, decimal Abono, int Usuario, int? Aplicacion = null, int? Nota = null, int? Origen = null)
        {
            Db.Ejecutar(ConsultaMovimientos.MovimientoInsertarMovimientocxc,
                Factura, Fecha.Date, Tipo, Descripcion, Cargo, Abono, Usuario, Aplicacion, Nota, Origen);
        }

        public string RegistrarDocumento(int Cliente, int Moneda, int Usuario, int Establecimiento, string Numero, int Tipo, DateTime Fecha, DateTime Vencimiento, decimal Importe, string Referencia, string Motivo)
        {
            ClsValidacionesVentas.Exigir(Tipo >= 0 && Tipo <= 4, "Seleccione el tipo de documento.");
            ClsValidacionesVentas.Exigir(Importe > 0 && Importe <= ClsValidacionesVentas.ImporteMaximo && Importe == ClsValidacionesVentas.Redondear(Importe), "El importe debe ser positivo, estar dentro del rango y tener hasta dos decimales.");
            Numero = ClsValidacionesVentas.Texto(Numero, 40, "numero de documento", false);
            Motivo = ClsValidacionesVentas.Texto(Motivo, 255, "descripcion / motivo");
            using (var Db = new ClsMovimientosRepositorio())
            {
                Db.Iniciar(); Db.Usuario(Usuario); Db.PeriodoAbierto(Moneda, Fecha);
                DataTable Clientes = Db.Tabla(ConsultaMovimientos.RegistrarDocumentoConsultarCliente, Cliente);
                ClsValidacionesVentas.Exigir(Clientes.Rows.Count == 1, "Seleccione un cliente activo.");
                if (Numero.Length == 0) Numero = Db.Numero(Tipo == 0 ? "CXC" : "NOT");
                int Factura;
                if (Tipo == 0)
                {
                    ClsValidacionesVentas.Exigir(Vencimiento.Date >= Fecha.Date, "El vencimiento es anterior a la emision.");
                    ClsValidacionesVentas.Exigir(Convert.ToInt32(Db.Escalar(ConsultaMovimientos.RegistrarDocumentoConsultarEstablecimientoventas, Establecimiento)) == 1, "Seleccione un establecimiento emisor activo.");
                    DataRow C = Clientes.Rows[0];
                    Db.Ejecutar(ConsultaMovimientos.RegistrarDocumentoInsertarFactura, Numero, Establecimiento, Cliente, Moneda, Fecha.Date, Vencimiento.Date,
                        Vencimiento.Date == Fecha.Date ? "CONTADO" : "CREDITO", (Vencimiento.Date - Fecha.Date).Days,
                        C["tipoIdentificacion"], C["numeroIdentificacion"] == DBNull.Value ? "CF" : C["numeroIdentificacion"], C["nombreCliente"], C["direccionCliente"], Importe, Importe, Motivo, Usuario);
                    Factura = Db.Id();
                    Movimiento(Db, Factura, Fecha, "FACTURA", Motivo, Importe, 0, Usuario);
                }
                else
                {
                    Referencia = ClsValidacionesVentas.Texto(Referencia, 80, "factura relacionada (serie-numero)");
                    DataTable Facturas = Db.Tabla(ConsultaMovimientos.RegistrarDocumentoConsultarFactura, Cliente, Moneda, Referencia);
                    ClsValidacionesVentas.Exigir(Facturas.Rows.Count == 1, "La referencia debe identificar una sola factura emitida del cliente y moneda seleccionados.");
                    Factura = Convert.ToInt32(Facturas.Rows[0]["idFactura"]);
                    ClsValidacionesVentas.Exigir(Fecha.Date >= Convert.ToDateTime(Facturas.Rows[0]["fechaEmision"]).Date, "El ajuste es anterior a la factura.");
                    bool Abono = Tipo == 1 || Tipo == 4;
                    if (Abono) ClsValidacionesVentas.Exigir(Importe <= Disponible(Db, Factura, Fecha), "El abono supera el saldo disponible de la factura.");
                    Db.Ejecutar(ConsultaMovimientos.RegistrarDocumentoInsertarNotacxc, Numero, Factura, Abono ? "CREDITO" : "DEBITO", Fecha.Date, Importe, Motivo, Usuario);
                    int Nota = Db.Id();
                    string[] Tipos = { "FACTURA", "NOTA_CREDITO", "NOTA_DEBITO", "AJUSTE_POSITIVO", "AJUSTE_NEGATIVO" };
                    Movimiento(Db, Factura, Fecha, Tipos[Tipo], Motivo, Abono ? 0 : Importe, Abono ? Importe : 0, Usuario, null, Nota);
                }
                Db.Confirmar(); return Numero;
            }
        }

        private static decimal Saldo(ClsMovimientosRepositorio Db, int Factura, DateTime? Fecha)
        {
            return ClsValidacionesVentas.Decimal(Fecha.HasValue
                ? Db.Escalar(ConsultaMovimientos.SaldoConsultarMovimientocxc, Factura, Fecha.Value.Date)
                : Db.Escalar(ConsultaMovimientos.SaldoConsultarMovimientocxc2, Factura));
        }
        private static decimal Disponible(ClsMovimientosRepositorio Db, int Factura, DateTime Fecha)
        {
            decimal AlDia = Saldo(Db, Factura, Fecha);
            // Un pago retroactivo tampoco puede dejar negativos los saldos de fechas posteriores.
            object Minimo = Db.Escalar(ConsultaMovimientos.DisponibleConsultarMovimientocxc, Factura, Fecha.Date);
            return Minimo == null || Minimo == DBNull.Value ? AlDia : Math.Min(AlDia, Convert.ToDecimal(Minimo));
        }

        public string RegistrarRecibo(int Cliente, int Moneda, int Usuario, string Numero, DateTime Fecha, decimal Monto, decimal TipoCambio, string Forma, string Referencia, string Observaciones, DataTable Aplicaciones)
        {
            ClsValidacionesVentas.Exigir(Monto > 0 && Monto <= ClsValidacionesVentas.ImporteMaximo && Monto == ClsValidacionesVentas.Redondear(Monto) && TipoCambio > 0 && TipoCambio <= 999999999999.999999m && TipoCambio == Math.Round(TipoCambio, 6), "Monto o tipo de cambio invalido.");
            ClsValidacionesVentas.Exigir(new[] { "EFECTIVO", "TRANSFERENCIA", "DEPOSITO", "CHEQUE", "TARJETA" }.Contains(Forma), "Seleccione la forma de pago.");
            Referencia = ClsValidacionesVentas.Texto(Referencia, 100, "referencia", Forma != "EFECTIVO");
            Numero = ClsValidacionesVentas.Texto(Numero, 40, "numero de recibo", false);
            Observaciones = ClsValidacionesVentas.Texto(Observaciones, 500, "observaciones", false);
            ClsValidacionesVentas.Exigir(Aplicaciones != null && Aplicaciones.Rows.Count > 0, "Seleccione las facturas a pagar.");
            var Filas = Aplicaciones.AsEnumerable().OrderBy(F => Convert.ToInt32(F["idFactura"])).ToList();
            ClsValidacionesVentas.Exigir(Filas.Select(F => Convert.ToInt32(F["idFactura"])).Distinct().Count() == Filas.Count, "Una factura se repite en las aplicaciones.");
            ClsValidacionesVentas.Exigir(Filas.Sum(F => Convert.ToDecimal(F["Importe"])) == Monto, "Aplique el monto completo del recibo a las facturas; no se admiten anticipos en esta etapa.");
            using (var Db = new ClsMovimientosRepositorio())
            {
                Db.Iniciar(); Db.Usuario(Usuario); Db.PeriodoAbierto(Moneda, Fecha);
                ClsValidacionesVentas.Exigir(Convert.ToInt32(Db.Escalar(ConsultaMovimientos.RegistrarReciboConsultarCliente, Cliente)) == 1, "Seleccione un cliente activo.");
                if (Numero.Length == 0) Numero = Db.Numero("REC");
                // Validar y bloquear todas las facturas antes de registrar el encabezado.
                foreach (DataRow Fila in Filas)
                {
                    int Factura = Convert.ToInt32(Fila["idFactura"]); decimal Importe = Convert.ToDecimal(Fila["Importe"]);
                    DataTable Datos = Db.Tabla(ConsultaMovimientos.RegistrarReciboConsultarFactura, Factura, Cliente, Moneda);
                    ClsValidacionesVentas.Exigir(Datos.Rows.Count == 1, "Una factura no pertenece al cliente/moneda o no esta emitida.");
                    ClsValidacionesVentas.Exigir(Fecha.Date >= Convert.ToDateTime(Datos.Rows[0][0]).Date, "El pago es anterior a una factura.");
                    ClsValidacionesVentas.Exigir(Importe > 0 && Importe == ClsValidacionesVentas.Redondear(Importe), "Cada aplicacion debe ser positiva y tener hasta dos decimales.");
                    ClsValidacionesVentas.Exigir(Importe <= Disponible(Db, Factura, Fecha), "El saldo de una factura cambio o el importe lo supera. Consulte nuevamente.");
                }
                Db.Ejecutar(ConsultaMovimientos.RegistrarReciboInsertarPago, Numero, Cliente, Moneda, Fecha.Date, Monto, TipoCambio, Forma, Referencia, Observaciones, Usuario);
                int Pago = Db.Id();
                foreach (DataRow Fila in Filas)
                {
                    int Factura = Convert.ToInt32(Fila["idFactura"]); decimal Importe = Convert.ToDecimal(Fila["Importe"]);
                    Db.Ejecutar(ConsultaMovimientos.RegistrarReciboInsertarAplicacionpago, Pago, Factura, Fecha.Date, Importe, Usuario);
                    Movimiento(Db, Factura, Fecha, "PAGO", "Recibo " + Numero, 0, Importe, Usuario, Db.Id());
                }
                Db.Confirmar(); return Numero;
            }
        }

        public DataTable ConsultarRecibos(string Recibo, string Deposito)
        {
            using (var Db = new ClsMovimientosRepositorio()) return Db.Tabla(ConsultaMovimientos.ConsultarRecibosConsultarPago, Recibo ?? "", Recibo ?? "", Deposito ?? "", Deposito ?? "");
        }
        public DataTable Aplicaciones(int Pago)
        {
            using (var Db = new ClsMovimientosRepositorio()) return Db.Tabla(ConsultaMovimientos.AplicacionesConsultarAplicacionpago, Pago);
        }
        public DataTable Documento(int Cliente, int Moneda, int Establecimiento, string Numero, bool Factura)
        {
            using (var Db = new ClsMovimientosRepositorio())
                return Factura
                    ? Db.Tabla(ConsultaMovimientos.DocumentoConsultarFactura, Cliente, Moneda, Establecimiento, Numero)
                    : Db.Tabla(ConsultaMovimientos.DocumentoConsultarNotacxc, Cliente, Moneda, Numero);
        }
        public DataSet Deposito(string Numero)
        {
            using (var Db = new ClsMovimientosRepositorio())
            {
                var D = new DataSet();
                D.Tables.Add(Db.Tabla(ConsultaMovimientos.DepositoConsultarDepositoventas, Numero));
                ClsValidacionesVentas.Exigir(D.Tables[0].Rows.Count == 1, "Consulte el codigo de un deposito preparado.");
                D.Tables.Add(Db.Tabla(ConsultaMovimientos.DepositoConsultarDepositoventasdetalle, D.Tables[0].Rows[0]["idDeposito"]));
                return D;
            }
        }
        public DataTable RecibosDisponibles(int Moneda)
        {
            using (var Db = new ClsMovimientosRepositorio()) return Db.Tabla(ConsultaMovimientos.RecibosDisponiblesConsultarPago, Moneda);
        }
        public string PrepararDeposito(int Moneda, int Usuario, DateTime Fecha, string Numero, string Empresa, string Banco, string Cuenta, string Concepto, string Referencia, DataTable Recibos)
        {
            Empresa = ClsValidacionesVentas.Texto(Empresa, 100, "empresa suministrada por Bancos");
            Banco = ClsValidacionesVentas.Texto(Banco, 100, "banco destino"); Cuenta = ClsValidacionesVentas.Texto(Cuenta, 100, "cuenta de la empresa");
            Concepto = ClsValidacionesVentas.Texto(Concepto, 100, "concepto bancario"); Referencia = ClsValidacionesVentas.Texto(Referencia, 100, "referencia", false);
            Numero = ClsValidacionesVentas.Texto(Numero, 40, "codigo de deposito", false);
            ClsValidacionesVentas.Exigir(Recibos != null && Recibos.Rows.Count > 0, "Seleccione recibos para preparar el deposito.");
            var Filas = Recibos.AsEnumerable().OrderBy(F => Convert.ToInt32(F["idPago"])).ToList();
            ClsValidacionesVentas.Exigir(Filas.Select(F => Convert.ToInt32(F["idPago"])).Distinct().Count() == Filas.Count, "No repita un recibo.");
            using (var Db = new ClsMovimientosRepositorio())
            {
                Db.Iniciar(); Db.Usuario(Usuario); Db.PeriodoAbierto(Moneda, Fecha);
                decimal Total = 0;
                foreach (DataRow Fila in Filas)
                {
                    DataTable P = Db.Tabla(ConsultaMovimientos.PrepararDepositoConsultarPago, Fila["idPago"], Moneda);
                    ClsValidacionesVentas.Exigir(P.Rows.Count == 1 && Fecha.Date >= Convert.ToDateTime(P.Rows[0]["fechaPago"]).Date, "Recibo no confirmado, moneda o fecha incorrecta.");
                    decimal Importe = Convert.ToDecimal(Fila["Importe"]);
                    decimal Usado = ClsValidacionesVentas.Decimal(Db.Escalar(ConsultaMovimientos.PrepararDepositoConsultarDepositoventasdetalle, Fila["idPago"]));
                    ClsValidacionesVentas.Exigir(Importe > 0 && Importe == ClsValidacionesVentas.Redondear(Importe) && Importe <= (decimal)P.Rows[0]["montoPago"] - Usado, "Importe de deposito invalido o superior al disponible."); Total += Importe;
                }
                if (Numero.Length == 0) Numero = Db.Numero("DEP");
                ClsValidacionesVentas.Exigir(Total <= ClsValidacionesVentas.ImporteMaximo, "El deposito supera la capacidad del documento.");
                Db.Ejecutar(ConsultaMovimientos.PrepararDepositoInsertarDepositoventas, Numero, Fecha.Date, Empresa, Banco, Cuenta, Concepto, Referencia, Moneda, Total, Usuario);
                int Id = Db.Id();
                foreach (DataRow Fila in Filas) Db.Ejecutar(ConsultaMovimientos.PrepararDepositoInsertarDepositoventasdetalle, Id, Fila["idPago"], Fila["Importe"]);
                Db.Confirmar(); return Numero;
            }
        }
        public void AnularRecibo(int Pago, DateTime Fecha, int Usuario, string Motivo)
        {
            Motivo = ClsValidacionesVentas.Texto(Motivo, 180, "motivo de anulacion");
            using (var Db = new ClsMovimientosRepositorio())
            {
                Db.Iniciar(); Db.Usuario(Usuario);
                object Moneda = Db.Escalar(ConsultaMovimientos.AnularReciboConsultarPago, Pago);
                ClsValidacionesVentas.Exigir(Moneda != null, "No existe el recibo."); Db.PeriodoAbierto(Convert.ToInt32(Moneda), Fecha);
                DataTable P = Db.Tabla(ConsultaMovimientos.AnularReciboConsultarPago2, Pago);
                ClsValidacionesVentas.Exigir((string)P.Rows[0]["estadoPago"] == "CONFIRMADO" && Fecha.Date >= Convert.ToDateTime(P.Rows[0]["fechaPago"]).Date, "El recibo ya esta anulado o la fecha es anterior al pago.");
                ClsValidacionesVentas.Exigir(Convert.ToInt32(Db.Escalar(ConsultaMovimientos.AnularReciboConsultarDepositoventasdetalle, Pago)) == 0, "El recibo pertenece a un deposito; coordine su reverso con Bancos.");
                DataTable Movimientos = Db.Tabla(ConsultaMovimientos.AnularReciboConsultarMovimientocxc, Pago);
                foreach (DataRow M in Movimientos.Rows) Movimiento(Db, Convert.ToInt32(M["idFactura"]), Fecha, "REVERSO", "Anulacion: " + Motivo, (decimal)M["abono"], (decimal)M["cargo"], Usuario, null, null, Convert.ToInt32(M["idMovimiento"]));
                Db.Ejecutar(ConsultaMovimientos.AnularReciboActualizarAplicacionpago, Pago);
                Db.Ejecutar(ConsultaMovimientos.AnularReciboActualizarPago, Pago);
                Db.Auditar("RECIBO", Pago, "ANULAR", Motivo, Usuario);
                Db.Confirmar();
            }
        }
    }
}
