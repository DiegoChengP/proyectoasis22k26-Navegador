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

        // =========================================================
        // MEJORA: LISTAR TODAS LAS TABLAS DE LA BASE DE DATOS
        // =========================================================
        // Necesario para que "Ingresar" pueda mostrar todas las tablas
        // disponibles sin importar el motor de base de datos conectado
        // por ODBC (MySQL, SQL Server, PostgreSQL, Access, etc.).
        public List<string> ObtenerTablas()
        {
            List<string> tablas = new List<string>();
            OdbcConnection conexion = conn.conexion();

            try
            {
                DataTable dtTablas = conexion.GetSchema("Tables");

                foreach (DataRow fila in dtTablas.Rows)
                {
                    string tipo = ObtenerValorSchema(fila, "TABLE_TYPE");

                    // Solo tablas de usuario; se descartan vistas y
                    // tablas de sistema cuando el driver informa el tipo.
                    if (!string.IsNullOrWhiteSpace(tipo) &&
                        tipo.IndexOf("TABLE", StringComparison.OrdinalIgnoreCase) < 0)
                    {
                        continue;
                    }

                    if (!string.IsNullOrWhiteSpace(tipo) &&
                        tipo.IndexOf("SYSTEM", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        continue;
                    }

                    string nombre = ObtenerValorSchema(fila, "TABLE_NAME");

                    if (!string.IsNullOrWhiteSpace(nombre) &&
                        !tablas.Contains(nombre, StringComparer.OrdinalIgnoreCase))
                    {
                        tablas.Add(nombre);
                    }
                }
            }
            finally
            {
                conn.desconexion(conexion);
            }

            tablas.Sort(StringComparer.OrdinalIgnoreCase);

            return tablas;
        }

        public DataTable ConsultarTodo(string nombreTabla)
        {
            ValidarIdentificador(nombreTabla);

            string sSQL = "SELECT * FROM " + nombreTabla;
            OdbcConnection conexion = conn.conexion();
            DataTable dt = new DataTable();

            try
            {
                using (OdbcDataAdapter da = new OdbcDataAdapter(sSQL, conexion))
                {
                    da.Fill(dt);
                }
            }
            finally
            {
                conn.desconexion(conexion);
            }

            return dt;
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

        // =========================================================
        // MEJORA: DETECCIÓN ROBUSTA DE LLAVE PRIMARIA
        // =========================================================
        // Muchos drivers ODBC no soportan (o exponen de forma distinta)
        // la colección estándar "Primary_Keys". Antes, si esa colección
        // fallaba, el catch la ignoraba en silencio y NINGUNA columna
        // quedaba marcada como PK, rompiendo Modificar/Eliminar y la
        // autogeneración de IDs. Ahora se intentan varias estrategias
        // en orden, de la más estándar a la más genérica.
        private HashSet<string> ObtenerLlavesPrimarias(OdbcConnection conexion, string nombreTabla)
        {
            HashSet<string> pk = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // Estrategia 1: colección ODBC estándar.
            try
            {
                DataTable llaves = conexion.GetSchema(
                    "Primary_Keys",
                    new string[] { null, null, nombreTabla }
                );

                foreach (DataRow fila in llaves.Rows)
                {
                    string columnaPK = ObtenerValorSchema(fila, "COLUMN_NAME");

                    if (!string.IsNullOrWhiteSpace(columnaPK))
                    {
                        pk.Add(columnaPK);
                    }
                }
            }
            catch
            {
            }

            if (pk.Count > 0) return pk;

            // Estrategia 2: algunos drivers solo exponen "Indexes",
            // marcando ahí cuál es el índice primario.
            try
            {
                DataTable indices = conexion.GetSchema(
                    "Indexes",
                    new string[] { null, null, nombreTabla }
                );

                foreach (DataRow fila in indices.Rows)
                {
                    string indicador = ObtenerValorSchema(fila, "PRIMARY_KEY");

                    if (string.IsNullOrWhiteSpace(indicador))
                    {
                        indicador = ObtenerValorSchema(fila, "INDEX_NAME");
                    }

                    bool esPrimaria =
                        indicador.Equals("YES", StringComparison.OrdinalIgnoreCase) ||
                        indicador.Equals("TRUE", StringComparison.OrdinalIgnoreCase) ||
                        indicador == "1" ||
                        indicador.IndexOf("PRIMARY", StringComparison.OrdinalIgnoreCase) >= 0;

                    if (esPrimaria)
                    {
                        string columna = ObtenerValorSchema(fila, "COLUMN_NAME");

                        if (!string.IsNullOrWhiteSpace(columna))
                        {
                            pk.Add(columna);
                        }
                    }
                }
            }
            catch
            {
            }

            if (pk.Count > 0) return pk;

            // Estrategia 3: consulta ANSI a INFORMATION_SCHEMA. Funciona
            // en MySQL/MariaDB, SQL Server y PostgreSQL, que son los
            // motores ODBC más comunes.
            try
            {
                string sSQL =
                    "SELECT kcu.COLUMN_NAME " +
                    "FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc " +
                    "JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu " +
                    "  ON tc.CONSTRAINT_NAME = kcu.CONSTRAINT_NAME " +
                    " AND tc.TABLE_SCHEMA = kcu.TABLE_SCHEMA " +
                    " AND tc.TABLE_NAME = kcu.TABLE_NAME " +
                    "WHERE tc.CONSTRAINT_TYPE = 'PRIMARY KEY' " +
                    "  AND tc.TABLE_NAME = ?";

                using (OdbcCommand comando = new OdbcCommand(sSQL, conexion))
                {
                    comando.Parameters.AddWithValue("@tabla", nombreTabla);

                    using (OdbcDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            string columna = Convert.ToString(lector["COLUMN_NAME"]);

                            if (!string.IsNullOrWhiteSpace(columna))
                            {
                                pk.Add(columna);
                            }
                        }
                    }
                }
            }
            catch
            {
            }

            return pk;
        }

        // =========================================================
        // MEJORA: DETECCIÓN ROBUSTA DE LLAVES FORÁNEAS
        // =========================================================
        // Mismo problema que con la llave primaria: la colección
        // "ForeignKeys" no está soportada igual en todos los drivers.
        // Se agrega una estrategia adicional vía INFORMATION_SCHEMA.
        private Dictionary<string, Tuple<string, string>> ObtenerLlavesForaneas(
            OdbcConnection conexion,
            string nombreTabla)
        {
            Dictionary<string, Tuple<string, string>> relaciones =
                new Dictionary<string, Tuple<string, string>>(
                    StringComparer.OrdinalIgnoreCase
                );

            DataTable fks = null;

            try
            {
                fks = conexion.GetSchema(
                    "ForeignKeys",
                    new string[] { null, null, nombreTabla, null, null, null }
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

            if (fks != null)
            {
                foreach (DataRow fila in fks.Rows)
                {
                    string fkTabla = ObtenerValorSchema(fila, "FK_TABLE_NAME");
                    string fkColumna = ObtenerValorSchema(fila, "FK_COLUMN_NAME");
                    string pkTabla = ObtenerValorSchema(fila, "PK_TABLE_NAME");
                    string pkColumna = ObtenerValorSchema(fila, "PK_COLUMN_NAME");

                    if (string.Equals(fkTabla, nombreTabla, StringComparison.OrdinalIgnoreCase) &&
                        !string.IsNullOrWhiteSpace(fkColumna))
                    {
                        relaciones[fkColumna] = Tuple.Create(pkTabla, pkColumna);
                    }
                }
            }

            if (relaciones.Count > 0) return relaciones;

            // Estrategia adicional: INFORMATION_SCHEMA ANSI, uniendo
            // restricciones referenciales con las columnas involucradas.
            try
            {
                string sSQL =
                    "SELECT kcu1.COLUMN_NAME AS FK_COLUMN, " +
                    "       kcu2.TABLE_NAME AS PK_TABLE, " +
                    "       kcu2.COLUMN_NAME AS PK_COLUMN " +
                    "FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc " +
                    "JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu1 " +
                    "  ON rc.CONSTRAINT_NAME = kcu1.CONSTRAINT_NAME " +
                    " AND rc.CONSTRAINT_SCHEMA = kcu1.CONSTRAINT_SCHEMA " +
                    "JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu2 " +
                    "  ON rc.UNIQUE_CONSTRAINT_NAME = kcu2.CONSTRAINT_NAME " +
                    " AND rc.UNIQUE_CONSTRAINT_SCHEMA = kcu2.CONSTRAINT_SCHEMA " +
                    " AND kcu1.ORDINAL_POSITION = kcu2.ORDINAL_POSITION " +
                    "WHERE kcu1.TABLE_NAME = ?";

                using (OdbcCommand comando = new OdbcCommand(sSQL, conexion))
                {
                    comando.Parameters.AddWithValue("@tabla", nombreTabla);

                    using (OdbcDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            string fkColumna = Convert.ToString(lector["FK_COLUMN"]);
                            string pkTabla = Convert.ToString(lector["PK_TABLE"]);
                            string pkColumna = Convert.ToString(lector["PK_COLUMN"]);

                            if (!string.IsNullOrWhiteSpace(fkColumna))
                            {
                                relaciones[fkColumna] = Tuple.Create(pkTabla, pkColumna);
                            }
                        }
                    }
                }
            }
            catch
            {
            }

            return relaciones;
        }

        // =========================================================
        // MEJORA: TIPO .NET REAL POR COLUMNA
        // =========================================================
        // El texto de "DATA_TYPE" que reporta GetSchema("Columns") varía
        // mucho según el driver ODBC (a veces es un nombre amigable como
        // "date", a veces es un código numérico del estándar ODBC, a
        // veces es específico del motor). Eso hacía que la detección de
        // fechas/booleanos/números fallara silenciosamente con ciertos
        // drivers (los campos de fecha terminaban como texto plano).
        //
        // Para evitarlo, se ejecuta una consulta que no trae filas
        // ("WHERE 1 = 0") y se lee el tipo .NET que ADO.NET le asigna a
        // cada columna a partir del propio driver — esto es mucho más
        // confiable porque no depende de cómo el driver nombra sus tipos.
        private Dictionary<string, string> ObtenerTiposNet(OdbcConnection conexion, string nombreTabla)
        {
            Dictionary<string, string> tipos =
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            try
            {
                string sSQL = "SELECT * FROM " + nombreTabla + " WHERE 1 = 0";

                using (OdbcCommand comando = new OdbcCommand(sSQL, conexion))
                using (OdbcDataAdapter da = new OdbcDataAdapter(comando))
                {
                    DataTable vacio = new DataTable();
                    da.Fill(vacio);

                    foreach (DataColumn col in vacio.Columns)
                    {
                        tipos[col.ColumnName] = col.DataType.Name;
                    }
                }
            }
            catch
            {
                // Si el motor no admite ese predicado o falla por
                // cualquier razón, simplemente no tendremos el tipo
                // .NET real y se usará el nombre de tipo del driver.
            }

            return tipos;
        }

        // =========================================================
        // NUEVO: TEXTO REAL DE COLUMN_TYPE (MySQL/MariaDB)
        // =========================================================
        // GetSchema("Columns") por ODBC NO conserva el "display width"
        // que MySQL guarda al declarar una columna (ej. "tinyint(1)"
        // para BOOLEAN). Casi todos los drivers ODBC devuelven
        // COLUMN_SIZE = 3 (o la precisión genérica del tipo) para
        // CUALQUIER tinyint, sin importar si se declaró como (1) o no.
        // Por eso la detección de booleano basada solo en COLUMN_SIZE
        // fallaba siempre para columnas BOOLEAN reales de MySQL
        // (ej. "estado_seguro BOOLEAN DEFAULT TRUE").
        //
        // INFORMATION_SCHEMA.COLUMNS sí conserva ese texto tal cual en
        // la columna COLUMN_TYPE (p. ej. "tinyint(1)", "tinyint(3)",
        // "varchar(50)"), así que se usa como señal adicional. Si el
        // motor conectado no es MySQL/MariaDB, la consulta simplemente
        // falla y se ignora (no rompe nada, EsBooleano usa sus otros
        // criterios de respaldo).
        private Dictionary<string, string> ObtenerTiposColumnaTexto(OdbcConnection conexion, string nombreTabla)
        {
            Dictionary<string, string> tipos =
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            try
            {
                string sSQL =
                    "SELECT COLUMN_NAME, COLUMN_TYPE " +
                    "FROM INFORMATION_SCHEMA.COLUMNS " +
                    "WHERE TABLE_NAME = ?";

                using (OdbcCommand comando = new OdbcCommand(sSQL, conexion))
                {
                    comando.Parameters.AddWithValue("@tabla", nombreTabla);

                    using (OdbcDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            string nombre = Convert.ToString(lector["COLUMN_NAME"]);
                            string tipo = Convert.ToString(lector["COLUMN_TYPE"]);

                            if (!string.IsNullOrWhiteSpace(nombre))
                            {
                                tipos[nombre] = tipo ?? "";
                            }
                        }
                    }
                }
            }
            catch
            {
                // Motor sin COLUMN_TYPE en INFORMATION_SCHEMA (no es
                // MySQL/MariaDB, o la vista no está disponible): se
                // ignora y simplemente no tendremos este dato adicional.
            }

            return tipos;
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
                dtEsquema.Columns.Add("NET_TYPE", typeof(string));

                // NUEVO: texto real de COLUMN_TYPE (MySQL), conserva el
                // display width que ODBC pierde (ej. "tinyint(1)").
                // Usado por Frm_Crud.EsBooleano para detectar columnas
                // BOOLEAN de MySQL que se guardan como TINYINT(1).
                dtEsquema.Columns.Add("COLUMN_TYPE_TEXT", typeof(string));

                dtEsquema.Columns.Add("CHARACTER_MAXIMUM_LENGTH", typeof(long));
                // MEJORA: precisión/longitud numérica de la columna
                // (COLUMN_SIZE del driver ODBC). Para columnas char/text
                // suele coincidir con CHARACTER_MAXIMUM_LENGTH, pero
                // para tipos numéricos (tinyint, smallint, etc.) es la
                // única forma de saber, por ejemplo, que un "tinyint" es
                // en realidad "tinyint(1)" (la convención de MySQL para
                // BOOLEAN), y no un tinyint numérico normal. (Se
                // mantiene como respaldo para motores distintos a MySQL,
                // donde COLUMN_TYPE_TEXT no está disponible.)
                dtEsquema.Columns.Add("COLUMN_SIZE", typeof(long));
                dtEsquema.Columns.Add("IS_NULLABLE", typeof(string));
                dtEsquema.Columns.Add("IS_PRIMARY_KEY", typeof(bool));
                dtEsquema.Columns.Add("IS_FOREIGN_KEY", typeof(bool));
                dtEsquema.Columns.Add("IS_AUTOINCREMENT", typeof(bool));
                dtEsquema.Columns.Add("FK_TABLE_NAME", typeof(string));
                dtEsquema.Columns.Add("FK_COLUMN_NAME", typeof(string));

                HashSet<string> pk = ObtenerLlavesPrimarias(conexion, nombreTabla);

                Dictionary<string, Tuple<string, string>> relaciones =
                    ObtenerLlavesForaneas(conexion, nombreTabla);

                // MEJORA (fix): antes se calculaba tiposNet pero nunca se
                // usaba dentro del foreach, así que la columna NET_TYPE
                // quedaba siempre vacía y la detección de fecha/booleano
                // dependía 100% del texto crudo de DATA_TYPE del driver.
                Dictionary<string, string> tiposNet =
                    ObtenerTiposNet(conexion, nombreTabla);

                // NUEVO: texto real de COLUMN_TYPE (ver comentario arriba).
                Dictionary<string, string> tiposColumnaTexto =
                    ObtenerTiposColumnaTexto(conexion, nombreTabla);

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

                    // MEJORA: leer COLUMN_SIZE (precisión numérica).
                    // Si el driver no expone esta columna en el esquema,
                    // queda en 0 y simplemente no se usa para detectar
                    // "tinyint(1)".
                    long tamanoColumna = 0;

                    string tamanoColumnaTexto = ObtenerValorSchema(
                        fila,
                        "COLUMN_SIZE"
                    );

                    long.TryParse(
                        tamanoColumnaTexto,
                        out tamanoColumna
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

                    // MEJORA (fix): tipo .NET real de la columna, cuando
                    // se pudo determinar; se guarda en NET_TYPE.
                    string tipoNet;
                    tiposNet.TryGetValue(nombre, out tipoNet);

                    // NUEVO: texto real de COLUMN_TYPE, cuando se pudo
                    // determinar (MySQL/MariaDB); se guarda en
                    // COLUMN_TYPE_TEXT.
                    string tipoColumnaTexto;
                    tiposColumnaTexto.TryGetValue(nombre, out tipoColumnaTexto);

                    DataRow nueva = dtEsquema.NewRow();

                    nueva["COLUMN_NAME"] = nombre;
                    nueva["DATA_TYPE"] = tipo;
                    nueva["NET_TYPE"] = tipoNet ?? "";
                    nueva["COLUMN_TYPE_TEXT"] = tipoColumnaTexto ?? "";
                    nueva["CHARACTER_MAXIMUM_LENGTH"] = longitud;
                    nueva["COLUMN_SIZE"] = tamanoColumna;
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

        // =========================================================
        // MEJORA: SIGUIENTE VALOR DE LLAVE PRIMARIA (AUTOGENERACIÓN
        // INDEPENDIENTE DEL MOTOR DE BASE DE DATOS)
        // =========================================================
        // Como el CRUD debe funcionar contra cualquier BD por ODBC, no
        // podemos depender del autoincremento nativo del motor (muchos
        // drivers ODBC no lo exponen igual, o la tabla simplemente no
        // lo tiene). En su lugar calculamos MAX(columnaPK) + 1.
        //
        // Devuelve:
        //   - long con el siguiente valor si la columna es numérica.
        //   - null si la columna no es numérica (no se puede autogenerar,
        //     el usuario debe ingresarla manualmente).
        public object ObtenerSiguienteValorLlave(string nombreTabla, string columnaPK)
        {
            ValidarIdentificador(nombreTabla);
            ValidarIdentificador(columnaPK);

            string sSQL = "SELECT MAX(" + columnaPK + ") FROM " + nombreTabla;
            OdbcConnection conexion = conn.conexion();

            try
            {
                using (OdbcCommand comando = new OdbcCommand(sSQL, conexion))
                {
                    object resultado = comando.ExecuteScalar();

                    // Tabla vacía o el máximo es nulo: empezamos en 1.
                    if (resultado == null || resultado == DBNull.Value)
                    {
                        return (long)1;
                    }

                    long maximo;

                    if (long.TryParse(Convert.ToString(resultado), out maximo))
                    {
                        return maximo + 1;
                    }

                    // La llave no es numérica (ej. códigos alfanuméricos):
                    // no se puede autogenerar de forma segura.
                    return null;
                }
            }
            finally
            {
                conn.desconexion(conexion);
            }
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