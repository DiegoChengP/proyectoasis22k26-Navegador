/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Oskar Cermeño
 * Carné : 0901-23-15379
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
using CapaModelo_PolizasCuentas_Contabilidad.Entidades;
using CapaModelo_PolizasCuentas_Contabilidad.Repositorios;

namespace CapaModelo_PolizasCuentas_Contabilidad.Contratos
{
    public interface IRepositorioCuentasContabilidad
    {
        IEnumerable<ClsCuentaContabilidad> ContabilidadFuncObtenerTodos();

        IEnumerable<ClsCuentaContabilidad> ContabilidadFuncObtenerTodos(int IdModulo);

        IEnumerable<ClsCuentaContabilidad> ContabilidadFuncObtenerImputables(int IdModulo);

        IEnumerable<ClsCuentaContabilidad> ContabilidadFuncObtenerPadres(ClsCuentaContabilidad Entidad);
    }
}
