/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Contrato del repositorio de presupuestos: el mantenimiento
 *  genérico más la lista de módulos.
 * ===================================================================
*/

using CapaModelo_Contabilidad.Entidades;

namespace CapaModelo_Contabilidad.Contratos
{
    public interface IRepositorioPresupuestosContabilidad : IRepositorioGenericoContabilidad<ClsPresupuestoContabilidad>
    {
        // Módulos del ERP a los que se asigna presupuesto.
        string[] ContabilidadFuncObtenerModulos();
    }
}
