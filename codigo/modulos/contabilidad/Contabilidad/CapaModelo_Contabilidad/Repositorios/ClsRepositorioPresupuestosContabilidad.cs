/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Repositorio de presupuestos. Por ahora trabaja en memoria con una
 *  muestra; no consulta la base de datos. Al pasar a MySQL solo
 *  cambia esta clase.
 * ===================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using CapaModelo_Contabilidad.Contratos;
using CapaModelo_Contabilidad.Entidades;

namespace CapaModelo_Contabilidad.Repositorios
{
    public class ClsRepositorioPresupuestosContabilidad : IRepositorioPresupuestosContabilidad
    {
        // Presupuestos en memoria.
        private readonly List<ClsPresupuestoContabilidad> _Presupuestos = new List<ClsPresupuestoContabilidad>();

        // Carga un presupuesto de octubre como muestra.
        public ClsRepositorioPresupuestosContabilidad()
        {
            ClsPresupuestoContabilidad Muestra = new ClsPresupuestoContabilidad
            {
                IdPresupuesto = 1, FechaInicio = new DateTime(2026, 10, 1), FechaCierre = new DateTime(2026, 10, 31), Gastado = 21500m
            };
            Muestra.Detalle.Add(new ClsPresupuestoDetalleContabilidad { Nombre = "Insumos de producción", Modulo = "Compras", Cantidad = 45000m });
            Muestra.Detalle.Add(new ClsPresupuestoDetalleContabilidad { Nombre = "Distribución de producto", Modulo = "Logística", Cantidad = 18000m });
            Muestra.Detalle.Add(new ClsPresupuestoDetalleContabilidad { Nombre = "Promoción comercial", Modulo = "Ventas", Cantidad = 12000m });
            _Presupuestos.Add(Muestra);
        }

        // Agrega el presupuesto con el siguiente número.
        public int ContabilidadFuncAgregar(ClsPresupuestoContabilidad Entidad)
        {
            Entidad.IdPresupuesto = _Presupuestos.Count == 0 ? 1 : _Presupuestos.Max(Presupuesto => Presupuesto.IdPresupuesto) + 1;
            _Presupuestos.Add(Entidad);
            return 1;
        }

        // Reemplaza el presupuesto que tenga el mismo número.
        public int ContabilidadFuncEditar(ClsPresupuestoContabilidad Entidad)
        {
            int Indice = _Presupuestos.FindIndex(Presupuesto => Presupuesto.IdPresupuesto == Entidad.IdPresupuesto);
            if (Indice < 0) return 0;
            _Presupuestos[Indice] = Entidad;
            return 1;
        }

        // Quita el presupuesto (con MySQL será baja lógica con is_active).
        public int ContabilidadFuncEliminar(ClsPresupuestoContabilidad Entidad)
        {
            return _Presupuestos.RemoveAll(Presupuesto => Presupuesto.IdPresupuesto == Entidad.IdPresupuesto);
        }

        // Devuelve una copia de la lista.
        public IEnumerable<ClsPresupuestoContabilidad> ContabilidadFuncObtenerTodos()
        {
            return _Presupuestos.ToList();
        }

        // Módulos del ERP.
        public string[] ContabilidadFuncObtenerModulos()
        {
            return new[] { "Contabilidad", "Logística", "Compras", "Ventas", "Recursos Humanos", "Bancos" };
        }
    }
}
