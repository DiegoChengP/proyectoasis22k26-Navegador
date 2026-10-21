// Jose Javier Torres Martinez | 0901-23-1091 | 20/10/2026
using System;
using System.Data;
using System.Linq;
using VentasDatos;

namespace CapaModelo_CierreMovimientos
{
    public sealed class ClsCierreRepositorio
    {
        public DataTable Catalogo(string Nombre) { using (var Db = new ClsDatosVentas()) return Db.Catalogo(Nombre); }
        private static void ValidarPeriodo(string Tipo, DateTime Desde, DateTime Hasta)
        {
            bool Mensual = Tipo == "MENSUAL" && Desde.Day == 1 && Hasta.Date == Desde.Date.AddMonths(1).AddDays(-1);
            bool Anual = Tipo == "ANUAL" && Desde.Month == 1 && Desde.Day == 1 && Hasta.Date == Desde.Date.AddYears(1).AddDays(-1);
            ClsDatosVentas.Exigir(Mensual || Anual, "Seleccione un periodo mensual o anual completo.");
        }
        private static DataTable Saldos(ClsDatosVentas Db, int Moneda, DateTime Desde, DateTime Hasta)
        {
            return Db.Tabla("SELECT c.idCliente,c.codigoCliente Codigo,c.nombreCliente Nombre," +
                "SUM(CASE WHEN m.fechaMovimiento<? THEN m.cargo-m.abono ELSE 0 END) Anterior," +
                "SUM(CASE WHEN m.fechaMovimiento>=? THEN m.cargo ELSE 0 END) Cargos," +
                "SUM(CASE WHEN m.fechaMovimiento>=? THEN m.abono ELSE 0 END) Abonos," +
                "SUM(CASE WHEN m.fechaMovimiento>=? THEN m.cargo ELSE 0 END) CargosAcumulados," +
                "SUM(CASE WHEN m.fechaMovimiento>=? THEN m.abono ELSE 0 END) AbonosAcumulados,SUM(m.cargo-m.abono) Actual " +
                "FROM tblmovimientocxc m JOIN tblfactura f ON f.idFactura=m.idFactura JOIN tblcliente c ON c.idCliente=f.idCliente " +
                "WHERE f.idMoneda=? AND m.fechaMovimiento<=? GROUP BY c.idCliente,c.codigoCliente,c.nombreCliente ORDER BY c.codigoCliente",
                Desde.Date, Desde.Date, Desde.Date, new DateTime(Desde.Year, 1, 1), new DateTime(Desde.Year, 1, 1), Moneda, Hasta.Date);
        }
        public DataTable Revisar(int Moneda, string Tipo, DateTime Desde, DateTime Hasta)
        {
            ValidarPeriodo(Tipo, Desde, Hasta);
            using (var Db = new ClsDatosVentas()) return Saldos(Db, Moneda, Desde, Hasta);
        }
        public DataTable EstadoPeriodo(int Moneda, string Tipo, DateTime Desde, DateTime Hasta)
        {
            using (var Db = new ClsDatosVentas()) return Db.Tabla("SELECT * FROM tblcierremovimientos WHERE idMoneda=? AND tipoCierre=? AND fechaDesde=? AND fechaHasta=? AND estadoCierre='CONFIRMADO'", Moneda, Tipo, Desde.Date, Hasta.Date);
        }
        public string Confirmar(int Moneda, int Usuario, string Tipo, DateTime Desde, DateTime Hasta, bool Revisado)
        {
            ValidarPeriodo(Tipo, Desde, Hasta);
            ClsDatosVentas.Exigir(Revisado, "Confirme que reviso los documentos y los recibos del periodo.");
            ClsDatosVentas.Exigir(Hasta.Date < DateTime.Today, "El periodo debe haber finalizado antes de confirmarlo.");
            using (var Db = new ClsDatosVentas())
            {
                Db.Iniciar(); Db.Usuario(Usuario); Db.BloquearMoneda(Moneda);
                ClsDatosVentas.Exigir(Convert.ToInt32(Db.Escalar("SELECT COUNT(*) FROM tblcierremovimientos WHERE idMoneda=? AND tipoCierre=? AND fechaDesde=? AND fechaHasta=? AND estadoCierre='CONFIRMADO'", Moneda, Tipo, Desde.Date, Hasta.Date)) == 0, "El periodo ya fue cerrado.");
                if (Tipo == "ANUAL")
                {
                    object Primera = Db.Escalar("SELECT MIN(fechaDesde) FROM tblcierremovimientos WHERE idMoneda=? AND tipoCierre='MENSUAL' AND estadoCierre='CONFIRMADO'", Moneda);
                    int Inicio = Primera != null && Primera != DBNull.Value && Convert.ToDateTime(Primera).Year == Desde.Year ? Convert.ToDateTime(Primera).Month : 1;
                    ClsDatosVentas.Exigir(Convert.ToInt32(Db.Escalar("SELECT COUNT(DISTINCT MONTH(fechaDesde)) FROM tblcierremovimientos WHERE idMoneda=? AND tipoCierre='MENSUAL' AND estadoCierre='CONFIRMADO' AND YEAR(fechaDesde)=? AND MONTH(fechaDesde)>=?", Moneda, Desde.Year, Inicio)) == 13 - Inicio,
                        "Cierre primero todos los meses operados del ano, hasta diciembre, en esta moneda.");
                }
                else
                {
                    object Ultimo = Db.Escalar("SELECT fechaUltimoCierre FROM tblcontrolperiodocxc WHERE idMoneda=?", Moneda);
                    if (Ultimo != null && Ultimo != DBNull.Value)
                        ClsDatosVentas.Exigir(Desde.Date == Convert.ToDateTime(Ultimo).Date.AddDays(1), "Los cierres mensuales deben ser consecutivos y no pueden superponerse.");
                    else
                    {
                        object Primera = Db.Escalar("SELECT MIN(m.fechaMovimiento) FROM tblmovimientocxc m JOIN tblfactura f ON f.idFactura=m.idFactura WHERE f.idMoneda=?", Moneda);
                        if (Primera != null && Primera != DBNull.Value)
                        {
                            DateTime Fecha = Convert.ToDateTime(Primera);
                            ClsDatosVentas.Exigir(Desde.Date <= new DateTime(Fecha.Year, Fecha.Month, 1), "Hay movimientos de meses anteriores sin cerrar.");
                        }
                    }
                }
                ClsDatosVentas.Exigir(Convert.ToInt32(Db.Escalar("SELECT COUNT(*) FROM tblfactura WHERE idMoneda=? AND fechaEmision BETWEEN ? AND ? AND estadoFactura='BORRADOR'", Moneda, Desde.Date, Hasta.Date)) == 0,
                    "Hay facturas en borrador en el periodo. Emitirlas o anularlas corresponde a Facturas.");
                ClsDatosVentas.Exigir(Convert.ToInt32(Db.Escalar("SELECT COUNT(*) FROM tblpago WHERE idMoneda=? AND fechaPago BETWEEN ? AND ? AND estadoPago='BORRADOR'", Moneda, Desde.Date, Hasta.Date)) == 0, "Hay recibos en borrador en el periodo.");
                DataTable Clientes = Saldos(Db, Moneda, Desde, Hasta);
                foreach (DataRow C in Clientes.Rows)
                    foreach (string Campo in new[] { "Anterior", "Cargos", "Abonos", "CargosAcumulados", "AbonosAcumulados", "Actual" })
                        ClsDatosVentas.Exigir(Math.Abs(Convert.ToDecimal(C[Campo])) <= ClsDatosVentas.ImporteMaximo, "Un saldo supera la capacidad del historico de cierre.");
                string Numero = Db.Numero("CIE");
                Db.Ejecutar("INSERT INTO tblcierremovimientos(numeroCierre,fechaDesde,fechaHasta,fechaCorte,idMoneda,tipoCierre,estadoCierre,documentosRevisados,pagosConciliados,idUsuarioResponsable) VALUES (?,?,?,?,?,?,'BORRADOR',1,0,?)", Numero, Desde.Date, Hasta.Date, Hasta.Date, Moneda, Tipo, Usuario);
                int Id = Db.Id();
                foreach (DataRow C in Clientes.Rows) Db.Ejecutar("INSERT INTO tblcierresaldocliente(idCierre,idCliente,codigoCliente,nombreCliente,saldoAnterior,cargosPeriodo,abonosPeriodo,cargosAcumulados,abonosAcumulados,saldoActual) VALUES (?,?,?,?,?,?,?,?,?,?)",
                    Id, C["idCliente"], C["Codigo"], C["Nombre"], C["Anterior"], C["Cargos"], C["Abonos"], C["CargosAcumulados"], C["AbonosAcumulados"], C["Actual"]);
                Db.Ejecutar("INSERT INTO tblcierremovimientosdetalle(idCierre,idFactura,saldoInicial,cargosPeriodo,abonosPeriodo,saldoFinal) " +
                    "SELECT ?,m.idFactura,SUM(CASE WHEN m.fechaMovimiento<? THEN m.cargo-m.abono ELSE 0 END),SUM(CASE WHEN m.fechaMovimiento>=? THEN m.cargo ELSE 0 END),SUM(CASE WHEN m.fechaMovimiento>=? THEN m.abono ELSE 0 END),SUM(m.cargo-m.abono) " +
                    "FROM tblmovimientocxc m JOIN tblfactura f ON f.idFactura=m.idFactura WHERE f.idMoneda=? AND m.fechaMovimiento<=? GROUP BY m.idFactura", Id, Desde.Date, Desde.Date, Desde.Date, Moneda, Hasta.Date);
                Db.Ejecutar("UPDATE tblcierremovimientos SET estadoCierre='CONFIRMADO',fechaConfirmacion=CURRENT_TIMESTAMP WHERE idCierre=?", Id);
                if (Tipo == "MENSUAL") Db.Ejecutar("UPDATE tblcontrolperiodocxc SET fechaUltimoCierre=? WHERE idMoneda=?", Hasta.Date, Moneda);
                Db.Confirmar(); return Numero;
            }
        }
        public DataTable Historial(int Moneda, int Cliente, string Periodo)
        {
            Periodo = (Periodo ?? "").Trim();
            ClsDatosVentas.Exigir(Periodo.Length == 0 || System.Text.RegularExpressions.Regex.IsMatch(Periodo, @"^\d{4}(-\d{2})?$"), "Consulte el periodo como AAAA o AAAA-MM.");
            using (var Db = new ClsDatosVentas()) return Db.Tabla("SELECT h.idCierre,h.numeroCierre,CONCAT(h.tipoCierre,' ',DATE_FORMAT(h.fechaDesde,'%Y-%m')) Periodo,c.codigoCliente Codigo,c.nombreCliente Nombre," +
                "c.saldoAnterior Anterior,c.cargosPeriodo Cargos,c.abonosPeriodo Abonos,c.cargosAcumulados CargosAcumulados,c.abonosAcumulados AbonosAcumulados,c.saldoActual Actual,h.estadoCierre Estado " +
                "FROM tblcierremovimientos h JOIN tblcierresaldocliente c ON c.idCierre=h.idCierre WHERE h.idMoneda=? AND (?=0 OR c.idCliente=?) AND DATE_FORMAT(h.fechaDesde,'%Y-%m') LIKE CONCAT(?,'%') " +
                "ORDER BY h.fechaDesde DESC,h.idCierre,c.codigoCliente", Moneda, Cliente, Cliente, Periodo);
        }
        public DataTable EstadoTraslado(int IdCierre)
        { using (var Db = new ClsDatosVentas()) return Db.Tabla("SELECT * FROM tbltrasladocxc WHERE idCierre=?", IdCierre); }
        public DataSet PrepararTraslado(int IdCierre, int Usuario)
        {
            DataTable Partida = this.Partida(IdCierre);
            ClsDatosVentas.Exigir(Partida.Rows.Count > 0 && !Partida.AsEnumerable().Any(F => ((string)F["Cuenta"]).StartsWith("PENDIENTE", StringComparison.Ordinal)), "Contabilidad debe configurar las cuentas de cada tipo de movimiento antes del traslado.");
            ClsDatosVentas.Exigir(Partida.AsEnumerable().Sum(F => (decimal)F["Debe"]) == Partida.AsEnumerable().Sum(F => (decimal)F["Haber"]), "La partida no esta balanceada.");
            using (var Db = new ClsDatosVentas())
            {
                Db.Iniciar(); Db.Usuario(Usuario);
                DataTable Cierre = Db.Tabla("SELECT * FROM tblcierremovimientos WHERE idCierre=? AND estadoCierre='CONFIRMADO' FOR UPDATE", IdCierre);
                ClsDatosVentas.Exigir(Cierre.Rows.Count == 1, "El cierre debe estar confirmado antes del traslado.");
                if (Convert.ToInt32(Db.Escalar("SELECT COUNT(*) FROM tbltrasladocxc WHERE idCierre=?", IdCierre)) == 0)
                {
                    Db.Ejecutar("INSERT INTO tbltrasladocxc(idCierre,claveIntegracion,idUsuario) VALUES (?,?,?)", IdCierre, "VENTAS-CXC-CIERRE-" + IdCierre, Usuario);
                    int N = 0;
                    foreach (DataRow F in Partida.Rows) Db.Ejecutar("INSERT INTO tbltrasladocxcdetalle(idCierre,numeroLinea,cuenta,descripcion,debe,haber) VALUES (?,?,?,?,?,?)", IdCierre, ++N, F["Cuenta"], F["Descripcion"], F["Debe"], F["Haber"]);
                }
                var D = new DataSet();
                D.Tables.Add(Db.Tabla("SELECT t.*,c.idMoneda,c.fechaHasta FROM tbltrasladocxc t JOIN tblcierremovimientos c ON c.idCierre=t.idCierre WHERE t.idCierre=?", IdCierre));
                D.Tables.Add(Db.Tabla("SELECT cuenta Cuenta,descripcion Descripcion,debe Debe,haber Haber FROM tbltrasladocxcdetalle WHERE idCierre=? ORDER BY numeroLinea", IdCierre));
                Db.Confirmar(); return D;
            }
        }
        public void ConfirmarTraslado(int IdCierre, int Usuario, string Referencia)
        {
            Referencia = ClsDatosVentas.Texto(Referencia, 100, "referencia devuelta por Contabilidad");
            using (var Db = new ClsDatosVentas())
            {
                Db.Iniciar(); Db.Usuario(Usuario);
                DataTable T = Db.Tabla("SELECT * FROM tbltrasladocxc WHERE idCierre=? FOR UPDATE", IdCierre);
                ClsDatosVentas.Exigir(T.Rows.Count == 1, "La partida no fue preparada.");
                if ((string)T.Rows[0]["estado"] == "CONFIRMADO") ClsDatosVentas.Exigir((string)T.Rows[0]["referenciaContable"] == Referencia, "Contabilidad devolvio referencias diferentes para la misma clave.");
                else Db.Ejecutar("UPDATE tbltrasladocxc SET estado='CONFIRMADO',referenciaContable=?,fechaConfirmacion=CURRENT_TIMESTAMP WHERE idCierre=?", Referencia, IdCierre);
                Db.Confirmar();
            }
        }
        public DataTable Partida(int IdCierre)
        {
            var Partida = new DataTable();
            Partida.Columns.Add("Cuenta", typeof(string)); Partida.Columns.Add("Descripcion", typeof(string));
            Partida.Columns.Add("Debe", typeof(decimal)); Partida.Columns.Add("Haber", typeof(decimal));
            using (var Db = new ClsDatosVentas())
            {
                DataTable Guardada = Db.Tabla("SELECT cuenta Cuenta,descripcion Descripcion,debe Debe,haber Haber FROM tbltrasladocxcdetalle WHERE idCierre=? ORDER BY numeroLinea", IdCierre);
                if (Guardada.Rows.Count > 0) return Guardada;
                DataTable Movimientos = Db.Tabla("SELECT m.tipoMovimiento,SUM(m.cargo) Cargos,SUM(m.abono) Abonos,p.cuentaClientes,p.cuentaContrapartida " +
                    "FROM tblcierremovimientos c JOIN tblfactura f ON f.idMoneda=c.idMoneda JOIN tblmovimientocxc m ON m.idFactura=f.idFactura " +
                    "LEFT JOIN tblcuentacontablecxc p ON p.idMoneda=c.idMoneda AND p.tipoMovimiento=m.tipoMovimiento " +
                    "WHERE c.idCierre=? AND c.estadoCierre='CONFIRMADO' AND m.fechaMovimiento BETWEEN c.fechaDesde AND c.fechaHasta " +
                    "GROUP BY m.tipoMovimiento,p.cuentaClientes,p.cuentaContrapartida ORDER BY m.tipoMovimiento", IdCierre);
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
