// Jose Javier Torres Martinez | 0901-23-1091 | 20/10/2026
// Fuente enlazada en cada Modelo: no agrega una DLL compartida.
using System;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using System.Globalization;

namespace VentasDatos
{
    internal sealed class ClsDatosVentas : IDisposable
    {
        internal const decimal ImporteMaximo = 9999999999999999.99m;
        internal readonly OdbcConnection Conexion;
        internal OdbcTransaction Transaccion;
        internal ClsDatosVentas()
        {
            string Cadena = Environment.GetEnvironmentVariable("VENTAS_ODBC");
            if (string.IsNullOrWhiteSpace(Cadena)) Cadena = ConfigurationManager.AppSettings["VentasConexion"];
            Conexion = new OdbcConnection(string.IsNullOrWhiteSpace(Cadena) ? "DSN=EmbutidosS.A;Connection Timeout=5" : Cadena);
            try { Conexion.Open(); } catch { Conexion.Dispose(); throw; }
        }
        internal void Iniciar() { Transaccion = Conexion.BeginTransaction(IsolationLevel.ReadCommitted); }
        internal void Confirmar() { Transaccion.Commit(); Transaccion.Dispose(); Transaccion = null; }
        private OdbcCommand Comando(string Sql, object[] Valores)
        {
            Exigir(Sql.Split('?').Length - 1 == Valores.Length, "Cantidad de parametros SQL incorrecta.");
            var Cmd = new OdbcCommand(Sql, Conexion, Transaccion) { CommandTimeout = 20 };
            foreach (object Valor in Valores)
            {
                var Parametro = new OdbcParameter();
                // ODBC 8 ANSI falla al enlazar SQL_C_NUMERIC despues de varios parametros.
                // Representacion decimal exacta e invariante; MySQL la guarda en columnas DECIMAL.
                // No convertir a double ni interpolar importes en el SQL.
                if (Valor is decimal)
                {
                    Parametro.OdbcType = OdbcType.VarChar; Parametro.Size = 64;
                    Parametro.Value = ((decimal)Valor).ToString(CultureInfo.InvariantCulture);
                    Cmd.Parameters.Add(Parametro); continue;
                }
                if (Valor is DateTime) Parametro.OdbcType = OdbcType.DateTime;
                Parametro.Value = Valor ?? DBNull.Value;
                Cmd.Parameters.Add(Parametro);
            }
            return Cmd;
        }
        internal DataTable Tabla(string Sql, params object[] Valores)
        {
            using (var Cmd = Comando(Sql, Valores))
            using (var Adaptador = new OdbcDataAdapter(Cmd)) { var Tabla = new DataTable(); Adaptador.Fill(Tabla); return Tabla; }
        }
        internal object Escalar(string Sql, params object[] Valores)
        { using (var Cmd = Comando(Sql, Valores)) return Cmd.ExecuteScalar(); }
        internal int Ejecutar(string Sql, params object[] Valores)
        { using (var Cmd = Comando(Sql, Valores)) return Cmd.ExecuteNonQuery(); }
        internal int Id() { return Convert.ToInt32(Escalar("SELECT LAST_INSERT_ID()")); }
        internal DataTable Catalogo(string Nombre)
        {
            switch (Nombre)
            {
                case "Clientes": return Tabla("SELECT idCliente Id,CONCAT(codigoCliente,' - ',nombreCliente) Nombre,numeroIdentificacion,direccionCliente,contactoCliente,diasCredito,limiteCredito,idMonedaCredito FROM tblcliente WHERE is_active=1 ORDER BY nombreCliente");
                case "Monedas": return Tabla("SELECT idMoneda Id,CONCAT(codigoMoneda,' - ',nombreMoneda) Nombre,codigoMoneda,simboloMoneda FROM tblmonedaventas WHERE is_active=1 ORDER BY codigoMoneda");
                case "Usuarios": return Tabla("SELECT idUsuario Id,nombreUsuario Nombre FROM tblusuario WHERE is_active=1 ORDER BY nombreUsuario");
                case "Vendedores": return Tabla("SELECT v.idVendedor Id,CONCAT(v.codigoVendedor,' - ',e.nombresEmpleado,' ',e.apellidosEmpleado) Nombre FROM tblvendedor v JOIN tblempleado e ON e.idEmpleado=v.idEmpleado WHERE v.is_active=1 ORDER BY v.codigoVendedor");
                case "Establecimientos": return Tabla("SELECT idEstablecimiento Id,CONCAT(codigoEstablecimiento,' - ',nombreEstablecimiento) Nombre FROM tblestablecimientoventas WHERE is_active=1 ORDER BY nombreEstablecimiento");
                default: throw new ArgumentException("Catalogo no reconocido.");
            }
        }
        internal void Usuario(int IdUsuario)
        {
            Exigir(IdUsuario > 0 && Convert.ToInt32(Escalar("SELECT COUNT(*) FROM tblusuario WHERE idUsuario=? AND is_active=1", IdUsuario)) == 1,
                "Seleccione un responsable activo de Seguridad.");
        }
        internal void BloquearMoneda(int Moneda)
        {
            Exigir(Transaccion != null, "La operacion requiere una transaccion.");
            Exigir(Convert.ToInt32(Escalar("SELECT COUNT(*) FROM tblmonedaventas WHERE idMoneda=? AND is_active=1", Moneda)) == 1, "Seleccione una moneda activa.");
            Ejecutar("INSERT INTO tblcontrolperiodocxc(idMoneda) VALUES (?) ON DUPLICATE KEY UPDATE idMoneda=VALUES(idMoneda)", Moneda);
            Tabla("SELECT idMoneda FROM tblcontrolperiodocxc WHERE idMoneda=? FOR UPDATE", Moneda);
        }
        internal void PeriodoAbierto(int Moneda, DateTime Fecha)
        {
            BloquearMoneda(Moneda);
            object Ultimo = Escalar("SELECT fechaUltimoCierre FROM tblcontrolperiodocxc WHERE idMoneda=?", Moneda);
            Exigir(Ultimo == null || Ultimo == DBNull.Value || Fecha.Date > Convert.ToDateTime(Ultimo).Date,
                "La fecha pertenece a un periodo cerrado. Registre el movimiento en un periodo abierto.");
        }
        internal string Numero(string Prefijo)
        {
            // Se llama dentro de la transaccion; la fila serializa los correlativos por prefijo.
            Ejecutar("INSERT INTO tblcorrelativoventas(prefijo,ultimoNumero) VALUES (?,0) ON DUPLICATE KEY UPDATE prefijo=VALUES(prefijo)", Prefijo);
            long N = Convert.ToInt64(Escalar("SELECT ultimoNumero FROM tblcorrelativoventas WHERE prefijo=? FOR UPDATE", Prefijo));
            string Buscar;
            switch (Prefijo)
            {
                case "COT": Buscar = "SELECT COUNT(*) FROM tblcotizacion WHERE numeroCotizacion=?"; break;
                case "REC": Buscar = "SELECT COUNT(*) FROM tblpago WHERE numeroRecibo=?"; break;
                case "CXC": Buscar = "SELECT COUNT(*) FROM tblfactura WHERE serieFactura='CXC' AND numeroFactura=?"; break;
                case "NOT": Buscar = "SELECT COUNT(*) FROM tblnotacxc WHERE numeroNota=?"; break;
                case "DEP": Buscar = "SELECT COUNT(*) FROM tbldepositoventas WHERE codigoDeposito=?"; break;
                case "CIE": Buscar = "SELECT COUNT(*) FROM tblcierremovimientos WHERE numeroCierre=?"; break;
                case "PCOT": Buscar = "SELECT COUNT(*) FROM tblpedido WHERE numeroPedido=?"; break;
                case "FCOT": Buscar = "SELECT COUNT(*) FROM tblfactura WHERE serieFactura='COT' AND numeroFactura=?"; break;
                default: throw new ArgumentException("Correlativo no reconocido.");
            }
            string Numero;
            do { Numero = Prefijo + "-" + (++N).ToString("D8", CultureInfo.InvariantCulture); }
            while (Convert.ToInt32(Escalar(Buscar, Numero)) > 0);
            Ejecutar("UPDATE tblcorrelativoventas SET ultimoNumero=? WHERE prefijo=?", N, Prefijo);
            return Numero;
        }
        internal static void Exigir(bool Condicion, string Mensaje) { if (!Condicion) throw new InvalidOperationException(Mensaje); }
        internal static decimal Decimal(object Valor) { return Valor == null || Valor == DBNull.Value ? 0m : Convert.ToDecimal(Valor); }
        internal static decimal Redondear(decimal Valor) { return Math.Round(Valor, 2, MidpointRounding.AwayFromZero); }
        internal static string Texto(string Valor, int Maximo, string Campo, bool Requerido = true)
        {
            Valor = (Valor ?? "").Trim();
            Exigir(!Requerido || Valor.Length > 0, "Ingrese " + Campo + ".");
            Exigir(Valor.Length <= Maximo, Campo + " supera " + Maximo + " caracteres.");
            return Valor;
        }
        public void Dispose()
        {
            if (Transaccion != null) { try { Transaccion.Rollback(); } catch (OdbcException) { } Transaccion.Dispose(); }
            Conexion.Dispose();
        }
    }
}
