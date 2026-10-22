/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Repositorio de activos fijos y del maestro de vida útil. Por ahora
 *  trabaja en memoria; no consulta la base de datos. Al pasar a MySQL
 *  solo cambia esta clase.
 * ===================================================================
*/

using System.Collections.Generic;
using System.Linq;
using CapaModelo_Contabilidad.Contratos;
using CapaModelo_Contabilidad.Entidades;

namespace CapaModelo_Contabilidad.Repositorios
{
    public class ClsRepositorioActivosFijosContabilidad : IRepositorioActivosFijosContabilidad
    {
        // Activos en memoria.
        private readonly List<ClsActivoFijoContabilidad> _Activos = new List<ClsActivoFijoContabilidad>();

        // Agrega el activo con el siguiente número.
        public int ContabilidadFuncAgregar(ClsActivoFijoContabilidad Entidad)
        {
            Entidad.IdActivo = _Activos.Count == 0 ? 1 : _Activos.Max(Activo => Activo.IdActivo) + 1;
            _Activos.Add(Entidad);
            return 1;
        }

        // Reemplaza el activo que tenga el mismo número.
        public int ContabilidadFuncEditar(ClsActivoFijoContabilidad Entidad)
        {
            int Indice = _Activos.FindIndex(Activo => Activo.IdActivo == Entidad.IdActivo);
            if (Indice < 0) return 0;
            _Activos[Indice] = Entidad;
            return 1;
        }

        // Quita el activo (con MySQL será baja lógica con is_active).
        public int ContabilidadFuncEliminar(ClsActivoFijoContabilidad Entidad)
        {
            return _Activos.RemoveAll(Activo => Activo.IdActivo == Entidad.IdActivo);
        }

        // Devuelve una copia de la lista.
        public IEnumerable<ClsActivoFijoContabilidad> ContabilidadFuncObtenerTodos()
        {
            return _Activos.ToList();
        }

        // Módulos del ERP.
        public string[] ContabilidadFuncObtenerModulos()
        {
            return new[] { "Contabilidad", "Logística", "Compras", "Ventas", "Recursos Humanos", "Bancos" };
        }

        // Vida útil por tipo de activo.
        public List<ClsVidaUtilContabilidad> ContabilidadFuncObtenerVidaUtil()
        {
            return new List<ClsVidaUtilContabilidad>
            {
                new ClsVidaUtilContabilidad { Tipo = "VEHICULO", Nombre = "Vehículo de distribución", Meses = 60 },
                new ClsVidaUtilContabilidad { Tipo = "MAQUINARIA", Nombre = "Maquinaria de producción", Meses = 120 },
                new ClsVidaUtilContabilidad { Tipo = "EQ_COMPUTO", Nombre = "Equipo de cómputo", Meses = 36 },
                new ClsVidaUtilContabilidad { Tipo = "MOBILIARIO", Nombre = "Mobiliario y equipo", Meses = 60 }
            };
        }
    }
}
