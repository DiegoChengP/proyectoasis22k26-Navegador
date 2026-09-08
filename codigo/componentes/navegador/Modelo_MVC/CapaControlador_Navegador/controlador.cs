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
        Sentencias sentencias = new Sentencias();

        public DataTable llenarDgv(string nombreTabla)
        {
            OdbcDataAdapter daControlador =
                sentencias.llenarTbl(nombreTabla);

            DataTable dtControlador = new DataTable();

            daControlador.Fill(dtControlador);

            return dtControlador;
        }

        public List<string> ObtenerColumnas(string nombreTabla)
        {
            return sentencias.ObtenerColumnas(nombreTabla);
        }

        public bool ExisteLlavePrimaria(
            string nombreTabla,
            string[] camposPK,
            string[] valoresPK)
        {
            return sentencias.ExisteLlavePrimaria(
                nombreTabla,
                camposPK,
                valoresPK
            );
        }

        public bool ExisteValorCampo(
            string nombreTabla,
            string nombreCampo,
            string valor)
        {
            return sentencias.ExisteValorCampo(
                nombreTabla,
                nombreCampo,
                valor
            );
        }

        public bool InsertarRegistro(
            string nombreTabla,
            Dictionary<string, string> datos)
        {
            return sentencias.InsertarRegistro(
                nombreTabla,
                datos
            );
        }

        public DataTable ObtenerEsquemaTabla(string nombreTabla)
        {
            return sentencias.ObtenerEsquemaTabla(nombreTabla);
        }

        private bool ValidarCampo(string valor, string tipoDato, int? longitudMaxima)
        {
            if (string.IsNullOrEmpty(valor))
                return true;

            switch (tipoDato.ToLower())
            {
                case "varchar":
                case "char":
                case "text":
                case "longtext":
                case "tinytext":
                case "mediumtext":
                    if (!System.Text.RegularExpressions.Regex.IsMatch(valor, @"^[\p{L}\p{N}\s\-_\.]+$"))
                        return false;
                    if (longitudMaxima.HasValue && valor.Length > longitudMaxima.Value)
                        return false;
                    return true;

                case "int":
                case "integer":
                case "decimal":
                case "numeric":
                case "float":
                case "double":
                case "real":
                    if (!System.Text.RegularExpressions.Regex.IsMatch(valor, @"^[0-9]+(\.[0-9]+)?$"))
                        return false;
                    return true;

                case "datetime":
                case "date":
                case "timestamp":
                    if (!DateTime.TryParse(valor, out _))
                        return false;
                    return true;

                default:
                    return true;
            }
        }

        public List<string> ValidarRegistro(Dictionary<string, string> datos, string nombreTabla)
        {
            List<string> errores = new List<string>();

            try
            {
                DataTable esquema = sentencias.ObtenerEsquemaTabla(nombreTabla);

                foreach (DataRow columna in esquema.Rows)
                {
                    string nombreCampo = columna["COLUMN_NAME"].ToString();
                    string tipoDato = columna["DATA_TYPE"].ToString();
                    int? longitudMaxima = columna["CHARACTER_MAXIMUM_LENGTH"] as int?;
                    string isNullable = columna["IS_NULLABLE"].ToString();

                    if (datos.ContainsKey(nombreCampo))
                    {
                        string valor = datos[nombreCampo];

                        if (isNullable == "NO" && string.IsNullOrWhiteSpace(valor))
                        {
                            errores.Add($"El campo '{nombreCampo}' es obligatorio.");
                            continue;
                        }

                        if (!ValidarCampo(valor, tipoDato, longitudMaxima))
                        {
                            switch (tipoDato.ToLower())
                            {
                                case "varchar":
                                case "char":
                                case "text":
                                    if (longitudMaxima.HasValue && valor.Length > longitudMaxima.Value)
                                        errores.Add($"El campo '{nombreCampo}' excede la longitud máxima permitida ({longitudMaxima.Value} caracteres).");
                                    else
                                        errores.Add($"El campo '{nombreCampo}' contiene caracteres no permitidos.");
                                    break;

                                case "int":
                                case "decimal":
                                case "float":
                                case "numeric":
                                    errores.Add($"El campo '{nombreCampo}' debe ser un valor numérico.");
                                    break;

                                case "datetime":
                                case "date":
                                    errores.Add($"El campo '{nombreCampo}' debe ser una fecha válida.");
                                    break;

                                default:
                                    errores.Add($"El campo '{nombreCampo}' no es válido.");
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar los datos: " + ex.Message, ex);
            }

            return errores;
        }
        public void guardarDatos(string query)
        {
            sentencias.ejecutarSql(query);
        }

    }
}