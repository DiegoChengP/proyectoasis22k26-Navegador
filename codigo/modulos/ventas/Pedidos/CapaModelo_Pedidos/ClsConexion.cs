using System.Data.Odbc;

namespace CapaModelo_Pedidos
{
    public abstract class ClsConexion
    {
        protected readonly string _ConnectionString;

        public ClsConexion()
        {
            _ConnectionString = "Dsn=EmbutidosS.A";
        }

        protected OdbcConnection VentasMetObtenerConexion()
        {
            return new OdbcConnection(_ConnectionString);
        }
    }
}
