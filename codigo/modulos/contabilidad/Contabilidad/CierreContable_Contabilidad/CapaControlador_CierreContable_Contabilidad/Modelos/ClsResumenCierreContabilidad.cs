/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Donald Osorio
 * Carné : 0901-23-17982
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Resumen del período que la pantalla de cierre muestra al usuario.
 * ===================================================================
*/

using CapaControlador_Contabilidad;
using CapaControlador_PolizasCuentas_Contabilidad;
using CapaModelo_Contabilidad.Contratos;
using CapaModelo_Contabilidad.Repositorios;
using CapaModelo_PolizasCuentas_Contabilidad.Contratos;
using CapaModelo_PolizasCuentas_Contabilidad.Entidades;
using CapaModelo_PolizasCuentas_Contabilidad.Repositorios;
namespace CapaControlador_CierreContable_Contabilidad
{
    public sealed class ClsResumenCierreContabilidad
    {
        public int Aplicadas { get; set; }
        public int Borradores { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
    }
}
