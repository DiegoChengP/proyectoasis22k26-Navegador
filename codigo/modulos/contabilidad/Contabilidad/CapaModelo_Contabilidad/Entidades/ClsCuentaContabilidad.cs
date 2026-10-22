/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Entidad que coincide con la tabla de cuentas contables: id, código
 *  con formato C.GG.CC.SSS y nombre.
 * ===================================================================
*/

namespace CapaModelo_Contabilidad.Entidades
{
    public class ClsCuentaContabilidad
    {
        // Llave de la cuenta.
        public int IdCuenta { get; set; }
        // Código con formato C.GG.CC.SSS.
        public string Codigo { get; set; }
        // Nombre de la cuenta.
        public string Nombre { get; set; }

        // Se muestra el nombre en los listados.
        public override string ToString()
        {
            return Nombre;
        }
    }
}
