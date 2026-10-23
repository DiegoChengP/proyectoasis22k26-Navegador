// Jose Javier Torres Martinez | 0901-23-1091 | 20/10/2026
// Fuente enlazada en cada Modelo: no agrega una DLL compartida.
using System;
using System.Data;
using System.Data.Odbc;
using System.Globalization;

namespace VentasDatos
{
    internal sealed class ClsDatosVentas : IDisposable
    {
        private readonly OdbcConnection _Conexion;
        private OdbcTransaction _Transaccion;
        internal bool EnTransaccion
        {
            get
            {
                return _Transaccion != null;
            }
        }

        internal ClsDatosVentas()
        {
            _Conexion = new OdbcConnection(CapaModelo_Ventas.ClsConexion.CadenaConexion);
            try
            {
                _Conexion.Open();
            }
            catch
            {
                _Conexion.Dispose();
                throw;
            }
        }

        internal void VentasMetIniciar()
        {
            _Transaccion = _Conexion.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        internal void VentasMetConfirmar()
        {
            _Transaccion.Commit();
            _Transaccion.Dispose();
            _Transaccion = null;
        }

        private OdbcCommand VentasFuncComando(string SentenciaSql, object[] Valores)
        {
            if (SentenciaSql.Split('?').Length - 1 != Valores.Length)
                throw new ArgumentException("Cantidad de parametros SQL incorrecta.");
            var Comando = new OdbcCommand(SentenciaSql, _Conexion, _Transaccion)
            {
                CommandTimeout = 20
            };
            foreach (object Valor in Valores)
            {
                var Parametro = new OdbcParameter();
                // ODBC 8 ANSI falla al enlazar SQL_C_NUMERIC despues de varios parametros.
                // Representacion decimal exacta e invariante; MySQL la guarda en columnas DECIMAL.
                // No convertir a double ni interpolar importes en el SQL.
                if (Valor is decimal)
                {
                    Parametro.OdbcType = OdbcType.VarChar;
                    Parametro.Size = 64;
                    Parametro.Value = ((decimal)Valor).ToString(CultureInfo.InvariantCulture);
                    Comando.Parameters.Add(Parametro);
                    continue;
                }

                if (Valor is DateTime)
                    Parametro.OdbcType = OdbcType.DateTime;
                Parametro.Value = Valor ?? DBNull.Value;
                Comando.Parameters.Add(Parametro);
            }

            return Comando;
        }

        internal DataTable VentasFuncTabla(string SentenciaSql, params object[] Valores)
        {
            using (var Comando = VentasFuncComando(SentenciaSql, Valores))
            using (var Adaptador = new OdbcDataAdapter(Comando))
            {
                var Tabla = new DataTable();
                Adaptador.Fill(Tabla);
                return Tabla;
            }
        }

        internal object VentasFuncEscalar(string SentenciaSql, params object[] Valores)
        {
            using (var Comando = VentasFuncComando(SentenciaSql, Valores))
                return Comando.ExecuteScalar();
        }

        internal int VentasFuncEjecutar(string SentenciaSql, params object[] Valores)
        {
            using (var Comando = VentasFuncComando(SentenciaSql, Valores))
                return Comando.ExecuteNonQuery();
        }

        internal int VentasFuncObtenerId()
        {
            return Convert.ToInt32(VentasFuncEscalar("SELECT LAST_INSERT_ID()"));
        }

        internal DataTable VentasFuncCatalogo(string Nombre)
        {
            switch (Nombre)
            {
                case "Clientes":
                    return this.VentasFuncTabla("SELECT idCliente Id,CONCAT(codigoCliente,' - ',nombreCliente) Nombre,numeroIdentificacion,direccionCliente,contactoCliente,diasCredito,limiteCredito,idMonedaCredito FROM tblcliente WHERE is_active=1 ORDER BY nombreCliente");
                case "Monedas":
                    return this.VentasFuncTabla("SELECT idMoneda Id,CONCAT(codigoMoneda,' - ',nombreMoneda) Nombre,codigoMoneda,simboloMoneda FROM tblmonedaventas WHERE is_active=1 ORDER BY codigoMoneda");
                case "Usuarios":
                    return this.VentasFuncTabla("SELECT idUsuario Id,nombreUsuario Nombre FROM tblusuario WHERE is_active=1 ORDER BY nombreUsuario");
                case "Vendedores":
                    return this.VentasFuncTabla("SELECT v.idVendedor Id,CONCAT(v.codigoVendedor,' - ',e.nombresEmpleado,' ',e.apellidosEmpleado) Nombre FROM tblvendedor v JOIN tblempleado e ON e.idEmpleado=v.idEmpleado WHERE v.is_active=1 ORDER BY v.codigoVendedor");
                case "Establecimientos":
                    return this.VentasFuncTabla("SELECT idEstablecimiento Id,CONCAT(codigoEstablecimiento,' - ',nombreEstablecimiento) Nombre FROM tblestablecimientoventas WHERE is_active=1 ORDER BY nombreEstablecimiento");
                default:
                    throw new ArgumentException("Catalogo no reconocido.");
            }
        }

        internal string VentasFuncNumero(string Prefijo)
        {
            // Se llama dentro de la transaccion; la fila serializa los correlativos por prefijo.
            this.VentasFuncEjecutar("INSERT INTO tblcorrelativoventas(prefijo,ultimoNumero) VALUES (?,0) ON DUPLICATE KEY UPDATE prefijo=VALUES(prefijo)", Prefijo);
            long UltimoNumero = Convert.ToInt64(this.VentasFuncEscalar("SELECT ultimoNumero FROM tblcorrelativoventas WHERE prefijo=? FOR UPDATE", Prefijo));
            string Buscar;
            switch (Prefijo)
            {
                case "COT":
                    Buscar = "SELECT COUNT(*) FROM tblcotizacion WHERE numeroCotizacion=?";
                    break;
                case "REC":
                    Buscar = "SELECT COUNT(*) FROM tblpago WHERE numeroRecibo=?";
                    break;
                case "CXC":
                    Buscar = "SELECT COUNT(*) FROM tblfactura WHERE serieFactura='CXC' AND numeroFactura=?";
                    break;
                case "NOT":
                    Buscar = "SELECT COUNT(*) FROM tblnotacxc WHERE numeroNota=?";
                    break;
                case "DEP":
                    Buscar = "SELECT COUNT(*) FROM tbldepositoventas WHERE codigoDeposito=?";
                    break;
                case "CIE":
                    Buscar = "SELECT COUNT(*) FROM tblcierremovimientos WHERE numeroCierre=?";
                    break;
                case "PCOT":
                    Buscar = "SELECT COUNT(*) FROM tblpedido WHERE numeroPedido=?";
                    break;
                case "FCOT":
                    Buscar = "SELECT COUNT(*) FROM tblfactura WHERE serieFactura='COT' AND numeroFactura=?";
                    break;
                default:
                    throw new ArgumentException("Correlativo no reconocido.");
            }

            string Numero;
            do
            {
                Numero = Prefijo + "-" + (++UltimoNumero).ToString("D8", CultureInfo.InvariantCulture);
            }
            while (Convert.ToInt32(this.VentasFuncEscalar(Buscar, Numero)) > 0);
            this.VentasFuncEjecutar("UPDATE tblcorrelativoventas SET ultimoNumero=? WHERE prefijo=?", UltimoNumero, Prefijo);
            return Numero;
        }

        internal void VentasMetAuditar(string Entidad, int Id, string Accion, string Detalle, int Usuario)
        {
            if (_Transaccion == null)
                throw new InvalidOperationException("La auditoria requiere una transaccion.");
            VentasFuncEjecutar("INSERT INTO tblbitacoracrudventas(entidad,idRegistro,accion,detalle,idUsuario) VALUES (?,?,?,?,?)", Entidad, Id, Accion, Detalle ?? "", Usuario);
        }

        public void Dispose()
        {
            if (_Transaccion != null)
            {
                try
                {
                    _Transaccion.Rollback();
                }
                catch (OdbcException)
                {
                }

                _Transaccion.Dispose();
            }

            _Conexion.Dispose();
        }
    }
}
