/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 21/10/2026
 * ==================================================================
 * Propósito :
 *  Contrato del repositorio de cuentas contables. Es de solo lectura:
 *  el mantenimiento de cuentas lo hace el control Navegador.
 * Reglas especificas:
 *  Contrato creado por Oskar Cermeño; se quitaron las operaciones de
 *  escritura y se agregó la consulta de cuentas imputables.
 * ===================================================================
*/

using System.Collections.Generic;
using CapaModelo_Contabilidad.Entidades;

namespace CapaModelo_Contabilidad.Contratos
{
    public interface IRepositorioCuentasContabilidad
    {
        // Todas las cuentas del catálogo.
        IEnumerable<ClsCuentaContabilidad> ContabilidadFuncObtenerTodos();

        // Cuentas asignadas a un módulo ERP.
        IEnumerable<ClsCuentaContabilidad> ContabilidadFuncObtenerTodos(int IdModulo);

        // Cuentas de un módulo que se pueden usar en una póliza (activas y de último nivel).
        IEnumerable<ClsCuentaContabilidad> ContabilidadFuncObtenerImputables(int IdModulo);

        // La cuenta y todos sus ancestros hasta la raíz.
        IEnumerable<ClsCuentaContabilidad> ContabilidadFuncObtenerPadres(ClsCuentaContabilidad Entidad);
    }
}
