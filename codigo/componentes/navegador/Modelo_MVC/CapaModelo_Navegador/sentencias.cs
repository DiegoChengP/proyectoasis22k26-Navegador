using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo_Navegador
{
    public class Sentencias
    {
        conexionBD conn = new conexionBD();

        public OdbcDataAdapter llenarTbl(string nombreTabla)
        {
            ValidarIdentificador(nombreTabla);

            string sSQL = "SELECT * FROM " + nombreTabla;
            OdbcConnection conexion = conn.conexion();
            OdbcDataAdapter daSentencias = new OdbcDataAdapter(sSQL, conexion);
            return daSentencias;
        }

        public DataTable ConsultarEmpleados()
        {
            string sSQL = "SELECT * FROM tbl_empleados";
            OdbcConnection conexion = conn.conexion();
            DataTable dtEmpleados = new DataTable();

            try
            {
                using (OdbcDataAdapter da = new OdbcDataAdapter(sSQL, conexion))
                {
                    da.Fill(dtEmpleados);
                }
            }
            finally
            {
                conn.desconexion(conexion);
            }

            return dtEmpleados;
        }
        // Dentro de la clase Sentencias en sentencias.cs
        public bool ExisteAplicacion(int idAplicacion)
        {
            // TODO: Consulta SQL a la BD cuando esté lista
            return idAplicacion > 0;
        }

        public bool ExisteModulo(int idModulo)
        {
            // TODO: Consulta SQL a la BD cuando esté lista
            return idModulo > 0;
        }

        public bool GuardarUsuarioPermisoBD(int idUsuario, int idAplicacion, int idModulo, int idPermiso)
        {
            // TODO: INSERT SQL a la BD cuando esté lista
            return true;
        }

        public List<string> ObtenerColumnas(
            string nombreTabla)
        {
            List<string> columnas = new List<string>();
            OdbcConnection conexion = conn.conexion();

            try
            {
                DataTable dtColumnas = conexion.GetSchema(
                    "Columns",
                    new string[] { null, null, nombreTabla, null }
                );

                foreach (DataRow fila in dtColumnas.Rows)
                {
                    string nombreColumna = fila["COLUMN_NAME"].ToString();

                    if (!columnas.Contains(nombreColumna))
                    {
                        columnas.Add(nombreColumna);
                    }
                }
            }
            finally
            {
                conn.desconexion(conexion);
            }

            return columnas;
        }

        public bool ExisteLlavePrimaria(string nombreTabla, string[] camposPK, string[] valoresPK)
        {
            if (camposPK == null || camposPK.Length == 0) return false;
            if (valoresPK == null || valoresPK.Length != camposPK.Length) return false;

            ValidarIdentificador(nombreTabla);

            string condiciones = "";
            for (int i = 0; i < camposPK.Length; i++)
            {
                ValidarIdentificador(camposPK[i]);

                if (i > 0) condiciones += " AND ";
                condiciones += camposPK[i] + " = ?";
            }

            string sSQL = "SELECT COUNT(*) FROM " + nombreTabla + " WHERE " + condiciones;
            OdbcConnection conexion = conn.conexion();

            try
            {
                using (OdbcCommand comando = new OdbcCommand(sSQL, conexion))
                {
                    for (int i = 0; i < valoresPK.Length; i++)
                    {
                        comando.Parameters.AddWithValue("@p" + i, valoresPK[i]);
                    }

                    int cantidad = Convert.ToInt32(comando.ExecuteScalar());
                    return cantidad > 0;
                }
            }
            finally
            {
                conn.desconexion(conexion);
            }
        }

        public bool ExisteValorCampo(string nombreTabla, string nombreCampo, string valor)
        {
            ValidarIdentificador(nombreTabla);
            ValidarIdentificador(nombreCampo);

            string sSQL = "SELECT COUNT(*) FROM " + nombreTabla + " WHERE " + nombreCampo + " = ?";
            OdbcConnection conexion = conn.conexion();

            try
            {
                using (OdbcCommand comando = new OdbcCommand(sSQL, conexion))
                {
                    comando.Parameters.AddWithValue("@valor", valor);
                    int cantidad = Convert.ToInt32(comando.ExecuteScalar());
                    return cantidad > 0;
                }
            }
            finally
            {
                conn.desconexion(conexion);
            }
        }

        public bool InsertarRegistro(string nombreTabla, Dictionary<string, string> datos)
        {
            if (datos == null || datos.Count == 0) return false;

            ValidarIdentificador(nombreTabla);

            string columnas = "";
            string valores = "";
            int contador = 0;

            foreach (KeyValuePair<string, string> dato in datos)
            {
                ValidarIdentificador(dato.Key);

                if (contador > 0)
                {
                    columnas += ", ";
                    valores += ", ";
                }

                columnas += dato.Key;
                valores += "?";
                contador++;
            }

            string sSQL =
                "INSERT INTO " +
                nombreTabla +
                " (" +
                columnas +
                ") VALUES (" +
                valores +
                ")";

            OdbcConnection conexion = conn.conexion();

            try
            {
                using (OdbcCommand comando =
                    new OdbcCommand(sSQL, conexion))
                {
                    int posicion = 0;

                    foreach (
                        KeyValuePair<string, string> dato
                        in datos)
                    {
                        comando.Parameters.AddWithValue(
                            "@p" + posicion,
                            dato.Value);

                        posicion++;
                    }

                    int resultado =
                        comando.ExecuteNonQuery();

                    return resultado > 0;
                }
            }
            finally
            {
                conn.desconexion(conexion);
            }
        }

        public DataTable ObtenerEsquemaTabla(string nombreTabla)
        {
            ValidarIdentificador(nombreTabla);

            OdbcConnection conexion = conn.conexion();
            DataTable dtEsquema = new DataTable();

            try
            {
                // Obtener las columnas de la tabla
                DataTable columnas = conexion.GetSchema(
                    "Columns",
                    new string[] { null, null, nombreTabla, null }
                );

                // Tabla donde guardaremos la información del esquema
                dtEsquema.Columns.Add("COLUMN_NAME", typeof(string));
                dtEsquema.Columns.Add("DATA_TYPE", typeof(string));
                dtEsquema.Columns.Add("CHARACTER_MAXIMUM_LENGTH", typeof(long));
                dtEsquema.Columns.Add("IS_NULLABLE", typeof(string));
                dtEsquema.Columns.Add("IS_PRIMARY_KEY", typeof(bool));
                dtEsquema.Columns.Add("IS_FOREIGN_KEY", typeof(bool));
                dtEsquema.Columns.Add("IS_AUTOINCREMENT", typeof(bool));
                dtEsquema.Columns.Add("FK_TABLE_NAME", typeof(string));
                dtEsquema.Columns.Add("FK_COLUMN_NAME", typeof(string));

                HashSet<string> pk = new HashSet<string>(
                    StringComparer.OrdinalIgnoreCase
                );

                try
                {
                    DataTable llaves = conexion.GetSchema(
                        "Primary_Keys",
                        new string[] { null, null, nombreTabla }
                    );

                    foreach (DataRow fila in llaves.Rows)
                    {
                        string columnaPK = ObtenerValorSchema(
                            fila,
                            "COLUMN_NAME"
                        );

                        if (!string.IsNullOrWhiteSpace(columnaPK))
                        {
                            pk.Add(columnaPK);
                        }
                    }
                }
                catch
                {

                }


                DataTable fks = null;

                try
                {
                    fks = conexion.GetSchema(
                        "ForeignKeys",
                        new string[] {
                    null,
                    null,
                    nombreTabla,
                    null,
                    null,
                    null
                        }
                    );
                }
                catch
                {
                    try
                    {
                        fks = conexion.GetSchema("ForeignKeys");
                    }
                    catch
                    {
                        fks = null;
                    }
                }

                Dictionary<string, Tuple<string, string>> relaciones =
                    new Dictionary<string, Tuple<string, string>>(
                        StringComparer.OrdinalIgnoreCase
                    );

                if (fks != null)
                {
                    foreach (DataRow fila in fks.Rows)
                    {
                        string fkTabla = ObtenerValorSchema(
                            fila,
                            "FK_TABLE_NAME"
                        );

                        string fkColumna = ObtenerValorSchema(
                            fila,
                            "FK_COLUMN_NAME"
                        );

                        string pkTabla = ObtenerValorSchema(
                            fila,
                            "PK_TABLE_NAME"
                        );

                        string pkColumna = ObtenerValorSchema(
                            fila,
                            "PK_COLUMN_NAME"
                        );

                        if (
                            string.Equals(
                                fkTabla,
                                nombreTabla,
                                StringComparison.OrdinalIgnoreCase
                            )
                            &&
                            !string.IsNullOrWhiteSpace(fkColumna)
                        )
                        {
                            relaciones[fkColumna] =
                                Tuple.Create(pkTabla, pkColumna);
                        }
                    }
                }

                // =========================================================
                // RECORRER COLUMNAS
                // =========================================================
                foreach (DataRow fila in columnas.Rows)
                {
                    string nombre = ObtenerValorSchema(
                        fila,
                        "COLUMN_NAME"
                    );

                    if (string.IsNullOrWhiteSpace(nombre))
                        continue;

                    string tipo = ObtenerValorSchema(
                        fila,
                        "DATA_TYPE"
                    );

                    string nullable = ObtenerValorSchema(
                        fila,
                        "IS_NULLABLE"
                    );

                    long longitud = 0;

                    string longitudTexto = ObtenerValorSchema(
                        fila,
                        "CHARACTER_MAXIMUM_LENGTH"
                    );

                    long.TryParse(
                        longitudTexto,
                        out longitud
                    );


                    string autoTexto = ObtenerValorSchema(
                        fila,
                        "IS_AUTOINCREMENT"
                    );

                    if (string.IsNullOrWhiteSpace(autoTexto))
                    {
                        autoTexto = ObtenerValorSchema(
                            fila,
                            "IS_GENERATEDCOLUMN"
                        );
                    }

                    bool esAuto =
                        autoTexto.Equals(
                            "YES",
                            StringComparison.OrdinalIgnoreCase
                        )
                        ||
                        autoTexto.Equals(
                            "TRUE",
                            StringComparison.OrdinalIgnoreCase
                        )
                        ||
                        autoTexto.Equals(
                            "1",
                            StringComparison.OrdinalIgnoreCase
                        );

                    bool esPK = pk.Contains(nombre);


                    Tuple<string, string> relacion;

                    bool esFK = relaciones.TryGetValue(
                        nombre,
                        out relacion
                    );


                    DataRow nueva = dtEsquema.NewRow();

                    nueva["COLUMN_NAME"] = nombre;
                    nueva["DATA_TYPE"] = tipo;
                    nueva["CHARACTER_MAXIMUM_LENGTH"] = longitud;
                    nueva["IS_NULLABLE"] = nullable;
                    nueva["IS_PRIMARY_KEY"] = esPK;
                    nueva["IS_FOREIGN_KEY"] = esFK;
                    nueva["IS_AUTOINCREMENT"] = esAuto;

                    if (esFK && relacion != null)
                    {
                        nueva["FK_TABLE_NAME"] =
                            relacion.Item1;

                        nueva["FK_COLUMN_NAME"] =
                            relacion.Item2;
                    }
                    else
                    {
                        nueva["FK_TABLE_NAME"] = "";
                        nueva["FK_COLUMN_NAME"] = "";
                    }

                    dtEsquema.Rows.Add(nueva);
                }
            }
            finally
            {
                conn.desconexion(conexion);
            }

            return dtEsquema;
        }

        private string ObtenerValorSchema(DataRow fila, string columna)
        {
            if (!fila.Table.Columns.Contains(columna) || fila[columna] == DBNull.Value)
                return "";
            return Convert.ToString(fila[columna]);
        }

        private void ValidarIdentificador(string identificador)
        {
            if (string.IsNullOrWhiteSpace(identificador) ||
                !System.Text.RegularExpressions.Regex.IsMatch(identificador, @"^[A-Za-z0-9_$.]+$"))
                throw new ArgumentException("El nombre de tabla o columna no es válido.");
        }

        public bool ActualizarRegistro(string nombreTabla, Dictionary<string, string> valores, Dictionary<string, string> clavesPrimarias)
        {
            if (valores == null ||
                valores.Count == 0 ||
                clavesPrimarias == null ||
                clavesPrimarias.Count == 0)
            {
                return false;
            }

            ValidarIdentificador(nombreTabla);

            Dictionary<string, string> valoresActualizar =
                new Dictionary<string, string>(
                    StringComparer.OrdinalIgnoreCase);

            foreach (
                KeyValuePair<string, string> dato
                in valores)
            {
                ValidarIdentificador(dato.Key);

                bool esPK =
                    clavesPrimarias.ContainsKey(dato.Key);

                if (!esPK)
                {
                    valoresActualizar[dato.Key] =
                        dato.Value;
                }
            }

            if (valoresActualizar.Count == 0)
            {
                return false;
            }

            StringBuilder sql =
                new StringBuilder(
                    "UPDATE " +
                    nombreTabla +
                    " SET ");

            int i = 0;

            foreach (
                KeyValuePair<string, string> dato
                in valoresActualizar)
            {
                if (i > 0)
                {
                    sql.Append(", ");
                }

                sql.Append(
                    dato.Key +
                    " = ?");

                i++;
            }

            sql.Append(" WHERE ");

            i = 0;

            foreach (
                KeyValuePair<string, string> clave
                in clavesPrimarias)
            {
                ValidarIdentificador(clave.Key);

                if (i > 0)
                {
                    sql.Append(" AND ");
                }

                sql.Append(
                    clave.Key +
                    " = ?");

                i++;
            }

            OdbcConnection conexion =
                conn.conexion();

            try
            {
                using (OdbcCommand comando =
                    new OdbcCommand(
                        sql.ToString(),
                        conexion))
                {
                    foreach (
                        KeyValuePair<string, string> dato
                        in valoresActualizar)
                    {
                        comando.Parameters.AddWithValue(
                            "@valor_" + dato.Key,
                            dato.Value);
                    }

                    foreach (
                        KeyValuePair<string, string> clave
                        in clavesPrimarias)
                    {
                        comando.Parameters.AddWithValue(
                            "@pk_" + clave.Key,
                            clave.Value);
                    }

                    int resultado =
                        comando.ExecuteNonQuery();

                    return resultado > 0;
                }
            }
            finally
            {
                conn.desconexion(conexion);
            }
        }

        public bool EliminarRegistro(string nombreTabla, Dictionary<string, string> clavesPrimarias)
        {
            if (clavesPrimarias == null ||
                clavesPrimarias.Count == 0)
            {
                return false;
            }

            ValidarIdentificador(nombreTabla);

            StringBuilder sql =
                new StringBuilder(
                    "DELETE FROM " +
                    nombreTabla +
                    " WHERE ");

            int i = 0;

            foreach (
                KeyValuePair<string, string> clave
                in clavesPrimarias)
            {
                ValidarIdentificador(clave.Key);

                if (i > 0)
                {
                    sql.Append(" AND ");
                }

                sql.Append(
                    clave.Key +
                    " = ?");

                i++;
            }

            OdbcConnection conexion =
                conn.conexion();

            try
            {
                using (OdbcCommand comando =
                    new OdbcCommand(
                        sql.ToString(),
                        conexion))
                {
                    foreach (
                        KeyValuePair<string, string> clave
                        in clavesPrimarias)
                    {
                        comando.Parameters.AddWithValue(
                            "@pk_" + clave.Key,
                            clave.Value);
                    }

                    int resultado =
                        comando.ExecuteNonQuery();

                    return resultado > 0;
                }
            }
            finally
            {
                conn.desconexion(conexion);
            }
        }

        public void ejecutarSql(string sql)
        {
            OdbcConnection conexion = conn.conexion();
            try
            {
                using (OdbcCommand cmd = new OdbcCommand(sql, conexion))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                conn.desconexion(conexion);
            }
        }
        // Validacion de Usario Jose Torres
        public DataTable ValidarUsuario(string usuario, string clave)
        {
            string sSQL = "SELECT id_usuario, nombre_usuario, id_rol FROM tbl_usuarios " +
                           "WHERE nombre_usuario = ? AND contrasena = ? AND estado_usuario = 1";

            OdbcConnection conexion = conn.conexion();
            DataTable dt = new DataTable();

            try
            {
                using (OdbcCommand comando = new OdbcCommand(sSQL, conexion))
                {
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    comando.Parameters.AddWithValue("@clave", clave);

                    using (OdbcDataAdapter da = new OdbcDataAdapter(comando))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la validación: " + ex.Message);
            }
            finally
            {
                conn.desconexion(conexion); // Solo la desconexión dentro del finally
            }

            return dt; // El return va afuera
        }

        public void guardarDatos(string query)
        {
            try
            {
                using (OdbcConnection conexion = conn.conexion())
                {
                    using (OdbcCommand cmd = new OdbcCommand(query, conexion))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar la sentencia en la base de datos: " + ex.Message, ex);
            }
        }

        public OdbcDataAdapter filtrarTbl(string nombreTabla, string columna, string valor)
        {
            ValidarIdentificador(nombreTabla);
            ValidarIdentificador(columna);

            string sSQL =
                "SELECT * FROM " +
                nombreTabla +
                " WHERE " +
                columna +
                " LIKE ?";

            OdbcConnection conexion =
                conn.conexion();

            OdbcCommand comando =
                new OdbcCommand(
                    sSQL,
                    conexion);

            comando.Parameters.AddWithValue(
                "@valor",
                "%" + valor + "%");

            OdbcDataAdapter daSentencias =
                new OdbcDataAdapter(
                    comando);

            return daSentencias;
        }
    }
}