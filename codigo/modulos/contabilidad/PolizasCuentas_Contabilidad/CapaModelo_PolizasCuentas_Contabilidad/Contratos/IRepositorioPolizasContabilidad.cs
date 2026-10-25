/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Oskar Cermeño
 * Carné : 0901-23-15379
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Contrato del repositorio de pólizas: el mantenimiento genérico
 *  más los catálogos que necesita la póliza.
 * ===================================================================
*/

using System.Collections.Generic;
using CapaModelo_PolizasCuentas_Contabilidad.Entidades;
using CapaModelo_PolizasCuentas_Contabilidad.Repositorios;

namespace CapaModelo_PolizasCuentas_Contabilidad.Contratos
{
    public interface IRepositorioPolizasContabilidad : CapaModelo_Contabilidad.Contratos.IRepositorioGenericoContabilidad<ClsPolizaContabilidad>
    {
        List<ClsCuentaContabilidad> ContabilidadFuncObtenerCuentas();

        string[] ContabilidadFuncObtenerEstados();

        string ContabilidadFuncObtenerNombreModulo(int IdModulo);
    }
}
