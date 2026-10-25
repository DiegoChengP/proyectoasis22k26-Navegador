using System;
using System.Data.Odbc;


namespace CapaModelo_Ventas
{
    public abstract class ClsConexion
    {
        protected readonly string _ConnectionString;

        public ClsConexion()
        {
            _ConnectionString = "Dsn=EmbutidosS.A";
        }

        protected OdbcConnection SeguridadMetObtenerConexion()
        {
            return new OdbcConnection(_ConnectionString);
        }

        public void SeguridadMetDesconexion(OdbcConnection ConexionOdbc)
        {
            try
            {
                ConexionOdbc.Close();
            }
            catch (OdbcException)
            {
                Console.WriteLine("No se desconecto");
            }
        }
    }
}