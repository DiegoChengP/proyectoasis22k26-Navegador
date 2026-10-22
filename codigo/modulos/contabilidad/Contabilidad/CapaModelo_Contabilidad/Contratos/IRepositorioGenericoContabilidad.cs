/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Contrato base de todo repositorio con mantenimiento: agregar,
 *  editar, eliminar y obtener todos. Solo define las firmas.
 * ===================================================================
*/

using System.Collections.Generic;

namespace CapaModelo_Contabilidad.Contratos
{
    public interface IRepositorioGenericoContabilidad<TEntidad> where TEntidad : class
    {
        // Agrega la entidad y devuelve las filas afectadas.
        int ContabilidadFuncAgregar(TEntidad Entidad);

        // Modifica la entidad y devuelve las filas afectadas.
        int ContabilidadFuncEditar(TEntidad Entidad);

        // Elimina la entidad y devuelve las filas afectadas.
        int ContabilidadFuncEliminar(TEntidad Entidad);

        // Devuelve todas las entidades.
        IEnumerable<TEntidad> ContabilidadFuncObtenerTodos();
    }
}
