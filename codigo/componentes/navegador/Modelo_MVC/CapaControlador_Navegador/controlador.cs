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

        // Dentro de la clase Controlador en controlador.cs
        public bool GuardarRelacionUsuarioPermiso(int idUsuario, int idAplicacion, int idModulo, int idPermiso)
        {
            // Instancia de la clase Sentencias de CapaModelo
            Sentencias modelo = new Sentencias();

            // 1. Validar que existan el ID de aplicación e ID de módulo
            bool existeApp = modelo.ExisteAplicacion(idAplicacion);
            bool existeMod = modelo.ExisteModulo(idModulo);

            // 2. Si ambos existen, procede a guardar la relación
            if (existeApp && existeMod)
            {
                return modelo.GuardarUsuarioPermisoBD(idUsuario, idAplicacion, idModulo, idPermiso);
            }

            // Si alguno no existe, rechaza la operación
            return false;
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
    }
}