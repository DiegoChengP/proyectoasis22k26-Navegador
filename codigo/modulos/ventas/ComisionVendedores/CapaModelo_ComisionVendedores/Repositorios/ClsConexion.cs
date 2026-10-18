using System.Data.Odbc;

namespace CapaModelo_ComisionVendedores.Repositorios
{
    public abstract class ClsConexion
    {
        protected readonly string _ConnectionString;

        protected ClsConexion()
        {
            _ConnectionString = "Dsn=EmbutidosS.A";
        }

        protected OdbcConnection ComisionVendedoresMetObtenerConexion()
        {
            return new OdbcConnection(_ConnectionString);
        }

    }
}
