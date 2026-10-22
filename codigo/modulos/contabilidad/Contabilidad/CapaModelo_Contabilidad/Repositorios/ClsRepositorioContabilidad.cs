/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Clase base abstracta que sabe conectarse a la base de datos por
 *  ODBC usando el DSN EmbutidosS.A. De aquí heredan los repositorios.
 * ===================================================================
*/

using System;
using System.Data.Odbc;

namespace CapaModelo_Contabilidad.Repositorios
{
    public abstract class ClsRepositorioContabilidad
    {
        // Nombre del DSN configurado en ODBC.
        protected readonly string _CadenaConexion;

        // Asigna el DSN estándar del proyecto.
        protected ClsRepositorioContabilidad()
        {
            _CadenaConexion = "Dsn=EmbutidosS.A";
        }

        // Crea la conexión sin abrirla.
        protected OdbcConnection ContabilidadFuncObtenerConexion()
        {
            return new OdbcConnection(_CadenaConexion);
        }

        // Cierra la conexión recibida.
        public void ContabilidadMetDesconexion(OdbcConnection ConexionOdbc)
        {
            try
            {
                ConexionOdbc.Close();
            }
            catch (OdbcException)
            {
                Console.WriteLine("No se desconectó");
            }
        }
    }
}
