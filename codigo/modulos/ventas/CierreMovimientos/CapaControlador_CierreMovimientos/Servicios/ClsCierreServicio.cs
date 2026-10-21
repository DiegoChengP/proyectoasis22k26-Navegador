// Jose Javier Torres Martinez | 0901-23-1091 | 20/10/2026
using System;
using System.Data;
using System.Linq;
using CapaModelo_CierreMovimientos;
using CapaControlador_CierreMovimientos.Validaciones;
using VentasValidaciones;

namespace CapaControlador_CierreMovimientos.Servicios
{
    internal sealed partial class ClsCierreServicio
    {
        public DataTable Catalogo(string Nombre) { using (var Db = new ClsCierreRepositorio()) return Db.Catalogo(Nombre); }

        private static DataTable Saldos(ClsCierreRepositorio Db, int Moneda, DateTime Desde, DateTime Hasta)
        {
            return Db.Tabla(ConsultaCierre.SaldosConsultarMovimientocxc,
                Desde.Date, Desde.Date, Desde.Date, new DateTime(Desde.Year, 1, 1), new DateTime(Desde.Year, 1, 1), Moneda, Hasta.Date);
        }
        public DataTable Revisar(int Moneda, string Tipo, DateTime Desde, DateTime Hasta)
        {
            ClsReglasCierre.ValidarPeriodo(Tipo, Desde, Hasta);
            using (var Db = new ClsCierreRepositorio()) return Saldos(Db, Moneda, Desde, Hasta);
        }
        public DataTable EstadoPeriodo(int Moneda, string Tipo, DateTime Desde, DateTime Hasta)
        {
            using (var Db = new ClsCierreRepositorio()) return Db.Tabla(ConsultaCierre.EstadoPeriodoConsultarCierremovimientos, Moneda, Tipo, Desde.Date, Hasta.Date);
        }
        public string Confirmar(int Moneda, int Usuario, string Tipo, DateTime Desde, DateTime Hasta, bool Revisado)
        {
            ClsReglasCierre.ValidarPeriodo(Tipo, Desde, Hasta);
            ClsValidacionesVentas.Exigir(Revisado, "Confirme que reviso los documentos y los recibos del periodo.");
            ClsValidacionesVentas.Exigir(Hasta.Date < DateTime.Today, "El periodo debe haber finalizado antes de confirmarlo.");
            using (var Db = new ClsCierreRepositorio())
            {
                Db.Iniciar(); Db.Usuario(Usuario); Db.BloquearMoneda(Moneda);
                ClsValidacionesVentas.Exigir(Convert.ToInt32(Db.Escalar(ConsultaCierre.ConfirmarConsultarCierremovimientos, Moneda, Tipo, Desde.Date, Hasta.Date)) == 0, "El periodo ya fue cerrado.");
                if (Tipo == "ANUAL")
                {
                    object Primera = Db.Escalar(ConsultaCierre.ConfirmarConsultarCierremovimientos2, Moneda);
                    int Inicio = Primera != null && Primera != DBNull.Value && Convert.ToDateTime(Primera).Year == Desde.Year ? Convert.ToDateTime(Primera).Month : 1;
                    ClsValidacionesVentas.Exigir(Convert.ToInt32(Db.Escalar(ConsultaCierre.ConfirmarConsultarCierremovimientos3, Moneda, Desde.Year, Inicio)) == 13 - Inicio,
                        "Cierre primero todos los meses operados del ano, hasta diciembre, en esta moneda.");
                }
                else
                {
                    object Ultimo = Db.Escalar(ConsultaCierre.ConfirmarConsultarControlperiodocxc, Moneda);
                    if (Ultimo != null && Ultimo != DBNull.Value)
                        ClsValidacionesVentas.Exigir(Desde.Date == Convert.ToDateTime(Ultimo).Date.AddDays(1), "Los cierres mensuales deben ser consecutivos y no pueden superponerse.");
                    else
                    {
                        object Primera = Db.Escalar(ConsultaCierre.ConfirmarConsultarMovimientocxc, Moneda);
                        if (Primera != null && Primera != DBNull.Value)
                        {
                            DateTime Fecha = Convert.ToDateTime(Primera);
                            ClsValidacionesVentas.Exigir(Desde.Date <= new DateTime(Fecha.Year, Fecha.Month, 1), "Hay movimientos de meses anteriores sin cerrar.");
                        }
                    }
                }
                ClsValidacionesVentas.Exigir(Convert.ToInt32(Db.Escalar(ConsultaCierre.ConfirmarConsultarFactura, Moneda, Desde.Date, Hasta.Date)) == 0,
                    "Hay facturas en borrador en el periodo. Emitirlas o anularlas corresponde a Facturas.");
                ClsValidacionesVentas.Exigir(Convert.ToInt32(Db.Escalar(ConsultaCierre.ConfirmarConsultarPago, Moneda, Desde.Date, Hasta.Date)) == 0, "Hay recibos en borrador en el periodo.");
                DataTable Clientes = Saldos(Db, Moneda, Desde, Hasta);
                foreach (DataRow C in Clientes.Rows)
                    foreach (string Campo in new[] { "Anterior", "Cargos", "Abonos", "CargosAcumulados", "AbonosAcumulados", "Actual" })
                        ClsValidacionesVentas.Exigir(Math.Abs(Convert.ToDecimal(C[Campo])) <= ClsValidacionesVentas.ImporteMaximo, "Un saldo supera la capacidad del historico de cierre.");
                DataTable Borrador = Db.Tabla(ConsultaCierre.ConfirmarConsultarCierremovimientos4, Moneda, Tipo, Desde.Date, Hasta.Date);
                ClsValidacionesVentas.Exigir(Borrador.Rows.Count <= 1, "Hay varios borradores del mismo periodo; revise los registros antes de confirmar.");
                string Numero; int Id;
                if (Borrador.Rows.Count == 1)
                {
                    Id = Convert.ToInt32(Borrador.Rows[0]["idCierre"]); Numero = Convert.ToString(Borrador.Rows[0]["numeroCierre"]);
                    Db.Ejecutar(ConsultaCierre.ConfirmarActualizarCierremovimientos, Usuario, Id);
                }
                else
                {
                    Numero = Db.Numero("CIE");
                    Db.Ejecutar(ConsultaCierre.ConfirmarInsertarCierremovimientos, Numero, Desde.Date, Hasta.Date, Hasta.Date, Moneda, Tipo, Usuario);
                    Id = Db.Id();
                }
                foreach (DataRow C in Clientes.Rows) Db.Ejecutar(ConsultaCierre.ConfirmarInsertarCierresaldocliente,
                    Id, C["idCliente"], C["Codigo"], C["Nombre"], C["Anterior"], C["Cargos"], C["Abonos"], C["CargosAcumulados"], C["AbonosAcumulados"], C["Actual"]);
                Db.Ejecutar(ConsultaCierre.ConfirmarInsertarCierremovimientosdetalle, Id, Desde.Date, Desde.Date, Desde.Date, Moneda, Hasta.Date);
                Db.Ejecutar(ConsultaCierre.ConfirmarActualizarCierremovimientos2, Id);
                if (Tipo == "MENSUAL") Db.Ejecutar(ConsultaCierre.ConfirmarActualizarControlperiodocxc, Hasta.Date, Moneda);
                Db.Auditar("CIERRE", Id, "CONFIRMAR", Numero, Usuario);
                Db.Confirmar(); return Numero;
            }
        }
        public DataTable Historial(int Moneda, int Cliente, string Periodo)
        {
            Periodo = (Periodo ?? "").Trim();
            ClsValidacionesVentas.Exigir(Periodo.Length == 0 || System.Text.RegularExpressions.Regex.IsMatch(Periodo, @"^\d{4}(-\d{2})?$"), "Consulte el periodo como AAAA o AAAA-MM.");
            using (var Db = new ClsCierreRepositorio()) return Db.Tabla(ConsultaCierre.HistorialConsultarCierremovimientos, Moneda, Cliente, Cliente, Periodo);
        }
        public DataTable EstadoTraslado(int IdCierre)
        { using (var Db = new ClsCierreRepositorio()) return Db.Tabla(ConsultaCierre.EstadoTrasladoConsultarTrasladocxc, IdCierre); }
        public DataSet PrepararTraslado(int IdCierre, int Usuario)
        {
            DataTable Partida = this.Partida(IdCierre);
            ClsValidacionesVentas.Exigir(Partida.Rows.Count > 0 && !Partida.AsEnumerable().Any(F => ((string)F["Cuenta"]).StartsWith("PENDIENTE", StringComparison.Ordinal)), "Contabilidad debe configurar las cuentas de cada tipo de movimiento antes del traslado.");
            ClsValidacionesVentas.Exigir(Partida.AsEnumerable().Sum(F => (decimal)F["Debe"]) == Partida.AsEnumerable().Sum(F => (decimal)F["Haber"]), "La partida no esta balanceada.");
            using (var Db = new ClsCierreRepositorio())
            {
                Db.Iniciar(); Db.Usuario(Usuario);
                DataTable Cierre = Db.Tabla(ConsultaCierre.PrepararTrasladoConsultarCierremovimientos, IdCierre);
                ClsValidacionesVentas.Exigir(Cierre.Rows.Count == 1, "El cierre debe estar confirmado antes del traslado.");
                if (Convert.ToInt32(Db.Escalar(ConsultaCierre.PrepararTrasladoConsultarTrasladocxc, IdCierre)) == 0)
                {
                    Db.Ejecutar(ConsultaCierre.PrepararTrasladoInsertarTrasladocxc, IdCierre, "VENTAS-CXC-CIERRE-" + IdCierre, Usuario);
                    int N = 0;
                    foreach (DataRow F in Partida.Rows) Db.Ejecutar(ConsultaCierre.PrepararTrasladoInsertarTrasladocxcdetalle, IdCierre, ++N, F["Cuenta"], F["Descripcion"], F["Debe"], F["Haber"]);
                }
                var D = new DataSet();
                D.Tables.Add(Db.Tabla(ConsultaCierre.PrepararTrasladoConsultarTrasladocxc2, IdCierre));
                D.Tables.Add(Db.Tabla(ConsultaCierre.PrepararTrasladoConsultarTrasladocxcdetalle, IdCierre));
                Db.Confirmar(); return D;
            }
        }
        public void ConfirmarTraslado(int IdCierre, int Usuario, string Referencia)
        {
            Referencia = ClsValidacionesVentas.Texto(Referencia, 100, "referencia devuelta por Contabilidad");
            using (var Db = new ClsCierreRepositorio())
            {
                Db.Iniciar(); Db.Usuario(Usuario);
                DataTable T = Db.Tabla(ConsultaCierre.ConfirmarTrasladoConsultarTrasladocxc, IdCierre);
                ClsValidacionesVentas.Exigir(T.Rows.Count == 1, "La partida no fue preparada.");
                if ((string)T.Rows[0]["estado"] == "CONFIRMADO") ClsValidacionesVentas.Exigir((string)T.Rows[0]["referenciaContable"] == Referencia, "Contabilidad devolvio referencias diferentes para la misma clave.");
                else Db.Ejecutar(ConsultaCierre.ConfirmarTrasladoActualizarTrasladocxc, Referencia, IdCierre);
                Db.Confirmar();
            }
        }
        public DataTable Partida(int IdCierre)
        {
            var Partida = new DataTable();
            Partida.Columns.Add("Cuenta", typeof(string)); Partida.Columns.Add("Descripcion", typeof(string));
            Partida.Columns.Add("Debe", typeof(decimal)); Partida.Columns.Add("Haber", typeof(decimal));
            using (var Db = new ClsCierreRepositorio())
            {
                DataTable Guardada = Db.Tabla(ConsultaCierre.PrepararTrasladoConsultarTrasladocxcdetalle, IdCierre);
                if (Guardada.Rows.Count > 0) return Guardada;
                DataTable Movimientos = Db.Tabla(ConsultaCierre.PartidaConsultarCierremovimientos, IdCierre);
                foreach (DataRow M in Movimientos.Rows)
                {
                    string Clientes = M["cuentaClientes"] == DBNull.Value ? "PENDIENTE: CxC" : (string)M["cuentaClientes"];
                    string Contra = M["cuentaContrapartida"] == DBNull.Value ? "PENDIENTE: contrapartida" : (string)M["cuentaContrapartida"];
                    decimal Cargos = (decimal)M["Cargos"], Abonos = (decimal)M["Abonos"];
                    if (Cargos > 0) { Partida.Rows.Add(Clientes, M["tipoMovimiento"], Cargos, 0m); Partida.Rows.Add(Contra, M["tipoMovimiento"], 0m, Cargos); }
                    if (Abonos > 0) { Partida.Rows.Add(Contra, M["tipoMovimiento"], Abonos, 0m); Partida.Rows.Add(Clientes, M["tipoMovimiento"], 0m, Abonos); }
                }
            }
            return Partida;
        }
    }
}
