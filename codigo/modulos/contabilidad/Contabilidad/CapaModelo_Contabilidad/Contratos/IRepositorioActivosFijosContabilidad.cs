/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Contrato del repositorio de activos fijos: el mantenimiento
 *  genérico más módulos y maestro de vida útil.
 * ===================================================================
*/

using System.Collections.Generic;
using CapaModelo_Contabilidad.Entidades;

namespace CapaModelo_Contabilidad.Contratos
{
    public interface IRepositorioActivosFijosContabilidad : IRepositorioGenericoContabilidad<ClsActivoFijoContabilidad>
    {
        // Módulos del ERP dueños de activos.
        string[] ContabilidadFuncObtenerModulos();

        // Maestro de vida útil por tipo de activo.
        List<ClsVidaUtilContabilidad> ContabilidadFuncObtenerVidaUtil();
    }
}
