/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Oskar Cermeño
 * Carné : 0901-23-15379
 * Fecha : 14/10/2026
 * ==================================================================
 * Propósito :
 *  Cuenta tal como la necesita la Vista para elegirla en una póliza.
 *  Es el tipo que expone el Controlador, de modo que la Vista no
 *  depende de las clases del Modelo.
 * ===================================================================
*/

using CapaModelo_PolizasCuentas_Contabilidad.Contratos;
using CapaModelo_PolizasCuentas_Contabilidad.Entidades;
using CapaModelo_PolizasCuentas_Contabilidad.Repositorios;
namespace CapaControlador_PolizasCuentas_Contabilidad
{
    public class ClsCuentaPolizaContabilidad
    {
        public int IdCuenta { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get { return Codigo + " - " + Nombre; } }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}
