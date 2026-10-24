/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 21/10/2026
 * ==================================================================
 * Propósito :
 *  Entidad de una línea de la póliza (tblPolizaDetalle). Los importes
 *  son decimal, nunca double.
 * Reglas especificas:
 *  Entidad creada por Oskar Cermeño; se ajustó a EST-01 y se agregaron
 *  la línea y la descripción del detalle.
 * ===================================================================
*/

namespace CapaModelo_Contabilidad.Entidades
{
    public class ClsPolizaDetalleContabilidad
    {
        // Llave de la línea (Pk_PolizaDetalle).
        public int IdPolizaDetalle { get; set; }
        // Póliza a la que pertenece (Fk_PolizaEncabezado).
        public int IdPoliza { get; set; }
        // Cuenta de la línea (Fk_Cuenta).
        public int IdCuenta { get; set; }
        // Orden de la línea dentro de la póliza.
        public int Linea { get; set; }
        // Importe al debe.
        public decimal Debe { get; set; }
        // Importe al haber.
        public decimal Haber { get; set; }
        // Descripción de la línea.
        public string DescripcionDetalle { get; set; }
        // Código de la cuenta (viene del JOIN con tblCuenta).
        public string Codigo { get; set; }
        // Nombre de la cuenta (viene del JOIN con tblCuenta).
        public string Cuenta { get; set; }
    }
}
