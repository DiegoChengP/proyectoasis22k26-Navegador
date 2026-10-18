using CapaModelo_Contabilidad.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo_Contabilidad.Contratos
{
    interface IRepositorioCuentasContabilidad
    {

        int ContabilidadFuncAgregar(ClsCuentaContabilidad entidad);
        int ContabilidadFuncEditar(ClsCuentaContabilidad entidad);
        int ContabilidadFuncEliminar(ClsCuentaContabilidad entidad);

        IEnumerable<ClsCuentaContabilidad> ContabilidadFuncObtenerPadres (ClsCuentaContabilidad entidad);

        IEnumerable<ClsCuentaContabilidad> ContabilidadFuncObtenerTodos();
        IEnumerable<ClsCuentaContabilidad> ContabilidadFuncObtenerTodos(int idModulo);
    }
}
