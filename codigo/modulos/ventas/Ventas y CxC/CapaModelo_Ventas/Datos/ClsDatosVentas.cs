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

        private OdbcCommand VentasMetComando(string Sql, object[] Valores)
        {
            if (Sql.Split('?').Length - 1 != Valores.Length)
                throw new ArgumentException("Cantidad de parametros SQL incorrecta.");
            var Cmd = new OdbcCommand(Sql, _Conexion, _Transaccion)
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
                    Cmd.Parameters.Add(Parametro);
                    continue;
                }

                if (Valor is DateTime)
                    Parametro.OdbcType = OdbcType.DateTime;
                Parametro.Value = Valor ?? DBNull.Value;
                Cmd.Parameters.Add(Parametro);
            }

            return Cmd;
        }

        internal DataTable VentasMetTabla(string Sql, params object[] Valores)
        {
            using (var Cmd = VentasMetComando(Sql, Valores))
            using (var Adaptador = new OdbcDataAdapter(Cmd))
            {
                var Tabla = new DataTable();
                Adaptador.Fill(Tabla);
                return Tabla;
            }
        }

        internal object VentasMetEscalar(string Sql, params object[] Valores)
        {
            using (var Cmd = VentasMetComando(Sql, Valores))
                return Cmd.ExecuteScalar();
        }

        internal int VentasMetEjecutar(string Sql, params object[] Valores)
        {
            using (var Cmd = VentasMetComando(Sql, Valores))
                return Cmd.ExecuteNonQuery();
        }

        internal int VentasFuncObtenerId()
        {
            return Convert.ToInt32(VentasMetEscalar("SELECT LAST_INSERT_ID()"));
        }

        internal DataTable VentasMetCatalogo(string Nombre)
        {
            return new ClsCatalogosVentas(this).VentasMetCatalogo(Nombre);
        }

        internal string VentasMetNumero(string Prefijo)
        {
            return new ClsCorrelativosVentas(this).VentasMetNumero(Prefijo);
        }

        internal void VentasMetAuditar(string Entidad, int Id, string Accion, string Detalle, int Usuario)
        {
            if (_Transaccion == null)
                throw new InvalidOperationException("La auditoria requiere una transaccion.");
            VentasMetEjecutar("INSERT INTO tblbitacoracrudventas(entidad,idRegistro,accion,detalle,idUsuario) VALUES (?,?,?,?,?)", Entidad, Id, Accion, Detalle ?? "", Usuario);
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
