/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Resumen del período que la pantalla de cierre muestra al usuario.
 * ===================================================================
*/

namespace CapaControlador_Contabilidad
{
    public sealed class ClsResumenCierreContabilidad
    {
        // Pólizas aplicadas.
        public int Aplicadas { get; set; }
        // Pólizas en borrador.
        public int Borradores { get; set; }
        // Total del debe aplicado.
        public decimal Debe { get; set; }
        // Total del haber aplicado.
        public decimal Haber { get; set; }
    }
}
