/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 14/10/2026
 * ==================================================================
 * Propósito :
 *  Una línea del detalle de una póliza (poliza_detalle): cuenta, debe
 *  y haber. Los importes son decimal, nunca double.
 * ===================================================================
*/

namespace CapaControlador_Contabilidad
{
    // Línea del detalle de la póliza.
    public class ClsLineaPolizaContabilidad
    {
        // Id de la cuenta.
        public int IdCuenta { get; set; }
        // Nombre de la cuenta.
        public string Cuenta { get; set; }
        // Importe al debe.
        public decimal Debe { get; set; }
        // Importe al haber.
        public decimal Haber { get; set; }
    }
}
