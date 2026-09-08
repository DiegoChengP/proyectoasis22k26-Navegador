using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo_Navegador;

namespace CapaControlador_Navegador
{
    public class Controlador
    {
        private Sentencias sentencias =
            new Sentencias();

      

        public DataTable llenarDgv(
            string nombreTabla)
        {
            DataTable dtControlador =
                new DataTable();

            try
            {
                using (
                    OdbcDataAdapter daControlador =
                        sentencias.llenarTbl(
                            nombreTabla))
                {
                    daControlador.Fill(
                        dtControlador);

                    if (daControlador.SelectCommand != null &&
                        daControlador.SelectCommand.Connection != null)
                    {
                        daControlador
                            .SelectCommand
                            .Connection
                            .Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error al cargar la tabla '" +
                    nombreTabla +
                    "': " +
                    ex.Message,
                    ex);
            }

            return dtControlador;
        }


        public bool GuardarRelacionUsuarioPermiso(
            int idUsuario,
            int idAplicacion,
            int idModulo,
            int idPermiso)
        {
           
            // Instancia de la clase Sentencias de CapaModelo
            Sentencias modelo = new Sentencias();

            // 1. Validar que existan el ID de aplicaci�n e ID de m�dulo
            bool existeApp = modelo.ExisteAplicacion(idAplicacion);
            bool existeMod = modelo.ExisteModulo(idModulo);

            // 2. Si ambos existen, procede a guardar la relaci�n

            if (existeApp &&
                existeMod)
            {
                return modelo
                    .GuardarUsuarioPermisoBD(
                        idUsuario,
                        idAplicacion,
                        idModulo,
                        idPermiso);
            }

            return false;
        }


        public DataTable ConsultarEmpleados()
        {
            try
            {
                return sentencias
                    .ConsultarEmpleados();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error al consultar la tabla de empleados: " +
                    ex.Message,
                    ex);
            }
        }

        // =========================================================
        // OBTENER COLUMNAS
        // =========================================================

        public List<string> ObtenerColumnas(
            string nombreTabla)
        {
            return sentencias
                .ObtenerColumnas(
                    nombreTabla);
        }

        // =========================================================
        // EXISTE LLAVE PRIMARIA
        // =========================================================

        public bool ExisteLlavePrimaria(
            string nombreTabla,
            string[] camposPK,
            string[] valoresPK)
        {
            return sentencias
                .ExisteLlavePrimaria(
                    nombreTabla,
                    camposPK,
                    valoresPK);
        }

        // =========================================================
        // EXISTE VALOR DE CAMPO
        // =========================================================

        public bool ExisteValorCampo(
            string nombreTabla,
            string nombreCampo,
            string valor)
        {
            return sentencias
                .ExisteValorCampo(
                    nombreTabla,
                    nombreCampo,
                    valor);
        }

        // =========================================================
        // INSERTAR REGISTRO
        // =========================================================

        public bool InsertarRegistro(
            string nombreTabla,
            Dictionary<string, string> datos)
        {
            return sentencias
                .InsertarRegistro(
                    nombreTabla,
                    datos);
        }


        //autenticación de usuario Jose Torres
        public bool AutenticarUsuario(
            string usuario,
            string clave,
            out string mensaje,
            out DataRow datosUsuario)
        {
            mensaje =
                string.Empty;

            datosUsuario =
                null;

            if (string.IsNullOrWhiteSpace(
                usuario))
            {
                mensaje =
                    "Debe ingresar el usuario.";

                return false;
            }

            if (string.IsNullOrWhiteSpace(
                clave))
            {
                mensaje =
                    "Debe ingresar la contraseña.";

                return false;
            }

            DataTable dt =
                sentencias.ValidarUsuario(
                    usuario.Trim(),
                    clave.Trim());

            if (dt.Rows.Count == 0)
            {
                mensaje =
                    "Usuario o contraseña incorrectos.";

                return false;
            }

            datosUsuario =
                dt.Rows[0];

            return true;
        }

        

        public DataTable ObtenerEsquemaTabla(
            string nombreTabla)
        {
            try
            {
                return sentencias
                    .ObtenerEsquemaTabla(
                        nombreTabla);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error al obtener el esquema de la tabla '" +
                    nombreTabla +
                    "': " +
                    ex.Message,
                    ex);
            }
        }

        // =========================================================
        // VALIDAR CAMPO
        // =========================================================

        private bool ValidarCampo(
            string valor,
            string tipoDato,
            int? longitudMaxima)
        {
            if (string.IsNullOrEmpty(valor))
            {
                return true;
            }

            switch (
                tipoDato.ToLower())
            {
                case "varchar":
                case "char":
                case "text":
                case "longtext":
                case "tinytext":
                case "mediumtext":

                    if (!System.Text.RegularExpressions.Regex.IsMatch(
                        valor,
                        @"^[\p{L}\p{N}\s\-_\.]+$"))
                    {
                        return false;
                    }

                    if (longitudMaxima.HasValue &&
                        valor.Length >
                        longitudMaxima.Value)
                    {
                        return false;
                    }

                    return true;

                case "int":
                case "integer":
                case "decimal":
                case "numeric":
                case "float":
                case "double":
                case "real":

                    if (!System.Text.RegularExpressions.Regex.IsMatch(
                        valor,
                        @"^[0-9]+(\.[0-9]+)?$"))
                    {
                        return false;
                    }

                    return true;

                case "datetime":
                case "date":
                case "timestamp":

                    if (!DateTime.TryParse(
                        valor,
                        out _))
                    {
                        return false;
                    }

                    return true;

                default:

                    return true;
            }
        }

        
        public List<string> ValidarRegistro(
            Dictionary<string, string> datos,
            string nombreTabla)
        {
            List<string> errores =
                new List<string>();

            try
            {
                DataTable esquema =
                    sentencias
                        .ObtenerEsquemaTabla(
                            nombreTabla);

                foreach (
                    DataRow columna
                    in esquema.Rows)
                {
                    string nombreCampo =
                        columna["COLUMN_NAME"]
                        .ToString();

                    string tipoDato =
                        columna["DATA_TYPE"]
                        .ToString();

                    int? longitudMaxima =
                        null;

                    if (esquema.Columns.Contains(
                        "CHARACTER_MAXIMUM_LENGTH") &&
                        columna["CHARACTER_MAXIMUM_LENGTH"] !=
                        DBNull.Value)
                    {
                        try
                        {
                            longitudMaxima =
                                Convert.ToInt32(
                                    columna[
                                        "CHARACTER_MAXIMUM_LENGTH"]);
                        }
                        catch
                        {
                            longitudMaxima =
                                null;
                        }
                    }

                    string isNullable =
                        columna["IS_NULLABLE"]
                        .ToString();

                    if (!datos.ContainsKey(
                        nombreCampo))
                    {
                        continue;
                    }

                    string valor =
                        datos[nombreCampo];

                    if (isNullable == "NO" &&
                        string.IsNullOrWhiteSpace(
                            valor))
                    {
                        errores.Add(
                            "El campo '" +
                            nombreCampo +
                            "' es obligatorio.");

                        continue;
                    }

                    if (!ValidarCampo(
                        valor,
                        tipoDato,
                        longitudMaxima))
                    {
                        switch (
                            tipoDato.ToLower())
                        {
                            case "varchar":
                            case "char":
                            case "text":

                                if (longitudMaxima.HasValue &&
                                    valor.Length >
                                    longitudMaxima.Value)
                                {
                                    errores.Add(
                                        "El campo '" +
                                        nombreCampo +
                                        "' excede la longitud máxima permitida (" +
                                        longitudMaxima.Value +
                                        " caracteres).");
                                }
                                else
                                {
                                    errores.Add(
                                        "El campo '" +
                                        nombreCampo +
                                        "' contiene caracteres no permitidos.");
                                }

                                break;

                            case "int":
                            case "integer":
                            case "decimal":
                            case "float":
                            case "numeric":
                            case "double":
                            case "real":

                                errores.Add(
                                    "El campo '" +
                                    nombreCampo +
                                    "' debe ser un valor numérico.");

                                break;

                            case "datetime":
                            case "date":
                            case "timestamp":

                                errores.Add(
                                    "El campo '" +
                                    nombreCampo +
                                    "' debe ser una fecha válida.");

                                break;

                            default:

                                errores.Add(
                                    "El campo '" +
                                    nombreCampo +
                                    "' no es válido.");

                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error al validar los datos: " +
                    ex.Message,
                    ex);
            }

            return errores;
        }

        
        public bool ActualizarRegistro(
            string nombreTabla,
            Dictionary<string, string> valores,
            Dictionary<string, string> clavesPrimarias)
        {
            if (string.IsNullOrWhiteSpace(
                nombreTabla))
            {
                throw new ArgumentException(
                    "El nombre de la tabla es obligatorio.");
            }

            if (valores == null ||
                valores.Count == 0)
            {
                throw new ArgumentException(
                    "No existen datos para actualizar.");
            }

            if (clavesPrimarias == null ||
                clavesPrimarias.Count == 0)
            {
                throw new ArgumentException(
                    "No se encontró la llave primaria del registro.");
            }

            return sentencias
                .ActualizarRegistro(
                    nombreTabla,
                    valores,
                    clavesPrimarias);
        }

        
        public bool EliminarRegistro(
            string nombreTabla,
            Dictionary<string, string> clavesPrimarias)
        {
            if (string.IsNullOrWhiteSpace(
                nombreTabla))
            {
                throw new ArgumentException(
                    "El nombre de la tabla es obligatorio.");
            }

            if (clavesPrimarias == null ||
                clavesPrimarias.Count == 0)
            {
                throw new ArgumentException(
                    "No se encontró la llave primaria del registro.");
            }

            return sentencias
                .EliminarRegistro(
                    nombreTabla,
                    clavesPrimarias);
        }

        
        public void guardarDatos(
            string query)
        {
            try
            {
                sentencias.guardarDatos(
                    query);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error al guardar los datos: " +
                    ex.Message,
                    ex);
            }
        }

        
        public DataTable filtrarDgv(
            string nombreTabla,
            string columna,
            string valor)
        {
            OdbcDataAdapter daControlador =
                sentencias.filtrarTbl(
                    nombreTabla,
                    columna,
                    valor);

            DataTable dtControlador =
                new DataTable();

            try
            {
                daControlador.Fill(
                    dtControlador);
            }
            finally
            {
                if (daControlador != null &&
                    daControlador.SelectCommand != null &&
                    daControlador.SelectCommand.Connection != null)
                {
                    daControlador
                        .SelectCommand
                        .Connection
                        .Close();
                }

                if (daControlador != null)
                {
                    daControlador.Dispose();
                }
            }

            return dtControlador;
        }
    }
}
