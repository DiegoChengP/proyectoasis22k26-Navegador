using System;
using System.Data.Odbc;

namespace CapaModelo_Compras
{
    public static class ClsModuloCompras
    {
        public static int ComprasFuncObtenerIdModulo()
        {
            try { return ComprasFuncConsultarModulo(); }
            catch (OdbcException Error)
            {
                throw new InvalidOperationException("No se pudo consultar la configuración de Seguridad.", Error);
            }
        }

        private static int ComprasFuncConsultarModulo()
        {
            using (var Conexion = new OdbcConnection("Dsn=EmbutidosS.A"))
            using (var Comando = new OdbcCommand("SELECT idModulo FROM tblmodulo WHERE nombreModulo = ? AND is_active = 1", Conexion))
            {
                Comando.Parameters.Add("NombreModulo", OdbcType.VarChar, 100).Value = "Compras y Cuentas por Pagar";
                Comando.CommandTimeout = 5;
                Conexion.Open();
                using (var Lector = Comando.ExecuteReader())
                {
                    if (!Lector.Read()) throw new InvalidOperationException("El módulo de Compras no está registrado en Seguridad.");
                    int Codigo = Convert.ToInt32(Lector[0]);
                    if (Lector.Read()) throw new InvalidOperationException("El registro del módulo de Compras está duplicado.");
                    return Codigo;
                }
            }
        }
    }
}
