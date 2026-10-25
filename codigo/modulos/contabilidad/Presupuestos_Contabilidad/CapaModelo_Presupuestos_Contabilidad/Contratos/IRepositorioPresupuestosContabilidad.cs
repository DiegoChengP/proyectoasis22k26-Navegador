/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Mario Taracena
 * Carné : 0901-23-9335
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Contrato del repositorio de presupuestos: el mantenimiento
 *  genérico más la lista de módulos.
 * ===================================================================
*/


using CapaModelo_Contabilidad.Contratos;
using CapaModelo_Contabilidad.Repositorios;
using CapaModelo_Presupuestos_Contabilidad.Entidades;
using CapaModelo_Presupuestos_Contabilidad.Repositorios;
namespace CapaModelo_Presupuestos_Contabilidad.Contratos
{
    public interface IRepositorioPresupuestosContabilidad : IRepositorioGenericoContabilidad<ClsPresupuestoContabilidad>
    {
        string[] ContabilidadFuncObtenerModulos();
    }
}
