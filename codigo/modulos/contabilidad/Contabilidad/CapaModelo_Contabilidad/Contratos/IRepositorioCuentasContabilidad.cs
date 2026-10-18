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
        int ContabilidadFuncAgregar(ClsCuentaContabilidad entidad, int idModulo);
        int ContabilidadFuncEditar(ClsCuentaContabilidad entidad, int idModulo);
        int ContabilidadFuncEliminar(ClsCuentaContabilidad entidad, int idModulo);
        IEnumerable<ClsCuentaContabilidad> ContabilidadFuncObtenerTodos(int idModulo);
    }
}
