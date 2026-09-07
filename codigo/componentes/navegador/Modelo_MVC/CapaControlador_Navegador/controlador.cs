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


        //autenticación de usuario Jose Torres
        public bool AutenticarUsuario(
            string usuario,
            string clave,
            out string mensaje,
            out DataRow datosUsuario)
        {
            mensaje = string.Empty;
            datosUsuario = null;

            if (string.IsNullOrWhiteSpace(usuario))
            {
                mensaje = "Debe ingresar el usuario.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(clave))
            {
                mensaje = "Debe ingresar la contraseña.";
                return false;
            }
            DataTable dt = sentencias.ValidarUsuario(usuario.Trim(), clave.Trim());
            if (dt.Rows.Count == 0)
            {
                mensaje = "Usuario o contraseña incorrectos.";
                return false;
            }
            datosUsuario = dt.Rows[0];
            return true;
        }
    }
}