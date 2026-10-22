/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 14/10/2026
 * ==================================================================
 * Propósito :
 *  Cuenta tal como la necesita la Vista para elegirla en una póliza.
 *  Es el tipo que expone el Controlador, de modo que la Vista no
 *  depende de las clases del Modelo.
 * ===================================================================
*/

namespace CapaControlador_Contabilidad
{
    // Cuenta que la Vista elige en una póliza.
    public class ClsCuentaPolizaContabilidad
    {
        // Id de la cuenta.
        public int IdCuenta { get; set; }
        // Código de la cuenta.
        public string Codigo { get; set; }
        // Nombre de la cuenta.
        public string Nombre { get; set; }
        // Código y nombre juntos.
        public string Descripcion { get { return Codigo + " - " + Nombre; } }

        // Se muestra la descripción.
        public override string ToString()
        {
            return Descripcion;
        }
    }
}
