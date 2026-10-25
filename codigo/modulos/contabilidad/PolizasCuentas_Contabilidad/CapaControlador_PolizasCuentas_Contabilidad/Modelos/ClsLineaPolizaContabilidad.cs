/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Oskar Cermeño
 * Carné : 0901-23-15379
 * Fecha : 14/10/2026
 * ==================================================================
 * Propósito :
 *  Una línea del detalle de una póliza (poliza_detalle): cuenta, debe
 *  y haber. Los importes son decimal, nunca double.
 * ===================================================================
*/

using CapaModelo_PolizasCuentas_Contabilidad.Contratos;
using CapaModelo_PolizasCuentas_Contabilidad.Entidades;
using CapaModelo_PolizasCuentas_Contabilidad.Repositorios;
namespace CapaControlador_PolizasCuentas_Contabilidad
{
    public class ClsLineaPolizaContabilidad
    {
        public int IdCuenta { get; set; }
        public string Cuenta { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
    }
}
