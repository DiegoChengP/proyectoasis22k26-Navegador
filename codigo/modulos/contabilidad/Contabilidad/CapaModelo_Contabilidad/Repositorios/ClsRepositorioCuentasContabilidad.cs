using CapaModelo_Contabilidad.Contratos;
using CapaModelo_Contabilidad.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo_Contabilidad.Repositorios
{
    public class ClsRepositorioCuentasContabilidad : ClsRepositorioMaestroContabilidad, IRepositorioCuentasContabilidad
    {
        private string selectAll;
        private string selectAllWithModulo
        private string insert;
        private string update;
        private string delete;
        public ClsRepositorioCuentasContabilidad()
        {selectAll = "SELECT * FROM cuenta";
            selectAllWithModulo = "SELECT c.* FROM modulo_cuenta mc INNER JOIN modulo m ON mc.id_modulo = m.id_modulo INNER JOIN cuenta c ON mc.id_cuenta = c.id_cuenta INNER JOIN pais p   ON mc.id_pais = p.id_pais WHERE m.id_modulo = ? ORDER BY m.id_modulo, c.id_cuenta";
        insert = "INSERT INTO cuenta value (NULL, ?, ?, ?, ?)";
        update = "UPDATE empleados SET idNumero=?, Nombre=?, Correo=?, Cumpleaños=? WHERE IdPK=?";
        delete = "DELETE FROM empleados WHERE IdPK=?";}
        public int ContabilidadFuncAgregar(ClsCuentaContabilidad entidad, int idModulo)
        {
            throw new NotImplementedException();
        }

        public int ContabilidadFuncEditar(ClsCuentaContabilidad entidad, int idModulo)
        {
            throw new NotImplementedException();
        }

        public int ContabilidadFuncEliminar(ClsCuentaContabilidad entidad, int idModulo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClsCuentaContabilidad> ContabilidadFuncObtenerTodos(int idModulo)
        {
            throw new NotImplementedException();
        }
    }
}
