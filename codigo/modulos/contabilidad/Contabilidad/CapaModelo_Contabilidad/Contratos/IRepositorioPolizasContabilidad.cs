/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Contrato del repositorio de pólizas: el mantenimiento genérico
 *  más los catálogos que necesita la póliza.
 * ===================================================================
*/

using System.Collections.Generic;
using CapaModelo_Contabilidad.Entidades;

namespace CapaModelo_Contabilidad.Contratos
{
    public interface IRepositorioPolizasContabilidad : IRepositorioGenericoContabilidad<ClsPolizaContabilidad>
    {
        // Catálogo de cuentas.
        List<ClsCuentaContabilidad> ContabilidadFuncObtenerCuentas();

        // Estados posibles de una póliza.
        string[] ContabilidadFuncObtenerEstados();

        // Orígenes posibles de una póliza.
        string[] ContabilidadFuncObtenerOrigenes();

        // Nombre de un módulo ERP según el catálogo (vacío si no existe).
        string ContabilidadFuncObtenerNombreModulo(int IdModulo);
    }
}
