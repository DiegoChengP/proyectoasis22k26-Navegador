/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Reglas del presupuesto: módulos, partidas, totales, restante,
 *  validación y grabación según el estado. Responsable de la
 *  pantalla: Mario.
 * Reglas especificas:
 *  Las validaciones están en los modelos con DataAnnotations. La
 *  Vista recibe DataTable para poder paginar y ordenar la tabla.
 * ===================================================================
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CapaModelo_Contabilidad.Contratos;
using CapaModelo_Contabilidad.Entidades;
using CapaModelo_Contabilidad.Repositorios;

namespace CapaControlador_Contabilidad
{
    public class ClsControladorPresupuestosContabilidad
    {
        // Repositorio de presupuestos (hoy en memoria).
        private readonly IRepositorioPresupuestosContabilidad _Repositorio = new ClsRepositorioPresupuestosContabilidad();

        // Módulos para el combo de la partida.
        public string[] ContabilidadFuncModulos()
        {
            return _Repositorio.ContabilidadFuncObtenerModulos();
        }

        // Partidas del presupuesto de muestra como tabla.
        public DataTable ContabilidadFuncPresupuesto()
        {
            DataTable Tabla = new DataTable();
            Tabla.Columns.Add("Nombre", typeof(string));
            Tabla.Columns.Add("Modulo", typeof(string));
            Tabla.Columns.Add("Cantidad", typeof(decimal));
            ClsPresupuestoContabilidad Presupuesto = _Repositorio.ContabilidadFuncObtenerTodos().FirstOrDefault();
            if (Presupuesto != null)
                foreach (ClsPresupuestoDetalleContabilidad Partida in Presupuesto.Detalle)
                    Tabla.Rows.Add(Partida.Nombre, Partida.Modulo, Partida.Cantidad);
            return Tabla;
        }

        // Errores por campo al agregar una partida.
        public Dictionary<string, string> ContabilidadFuncErroresPartida(string Nombre, string Modulo, decimal Cantidad, DataTable Detalle)
        {
            Dictionary<string, string> Errores = ClsValidacionDatosContabilidad.ContabilidadFuncErrores(
                new ClsModeloPartidaPresupuestoContabilidad { Nombre = Nombre, Modulo = Modulo, Cantidad = Cantidad });
            if (!Errores.ContainsKey("Modulo") && !ContabilidadFuncModulos().Contains(Modulo)) Errores["Modulo"] = "Seleccione un módulo válido.";
            if (!Errores.ContainsKey("Cantidad") && ContabilidadFuncTotalPresupuesto(Detalle) + Cantidad > ClsLimitesContabilidad.MaximoMonetario)
                Errores["Cantidad"] = "El total excede el límite permitido.";
            return Errores;
        }

        // Error del rango de fechas, vacío si es correcto.
        public string ContabilidadFuncErrorFechas(DateTime Inicio, DateTime Fin)
        {
            return ClsValidacionDatosContabilidad.ContabilidadFuncErrorRangoFechas(Inicio, Fin);
        }

        // Lo que queda por gastar.
        public decimal ContabilidadFuncRestante(DataTable Detalle, decimal Gastado)
        {
            return ContabilidadFuncTotalPresupuesto(Detalle) - Gastado;
        }

        // Suma de las partidas.
        public decimal ContabilidadFuncTotalPresupuesto(DataTable Detalle)
        {
            decimal Total = 0m;
            foreach (DataRow Fila in Detalle.Rows) Total += Convert.ToDecimal(Fila["Cantidad"]);
            return Total;
        }

        // Devuelve el modelo con los datos de la pantalla.
        private ClsModeloPresupuestoContabilidad ContabilidadFuncCrearModelo(DateTime Inicio, DateTime? Cierre, decimal Gastado, DataTable Detalle)
        {
            ClsModeloPresupuestoContabilidad Modelo = new ClsModeloPresupuestoContabilidad { FechaInicio = Inicio, FechaCierre = Cierre, Gastado = Gastado };
            foreach (DataRow Fila in Detalle.Rows)
                Modelo.Partidas.Add(new ClsModeloPartidaPresupuestoContabilidad
                {
                    Nombre = Convert.ToString(Fila["Nombre"]), Modulo = Convert.ToString(Fila["Modulo"]), Cantidad = Convert.ToDecimal(Fila["Cantidad"])
                });
            return Modelo;
        }

        // Devuelve "" si el presupuesto es válido; si no, el motivo.
        public string ContabilidadFuncValidarPresupuesto(DateTime Inicio, DateTime? Cierre, DataTable Detalle)
        {
            return ClsValidacionDatosContabilidad.ContabilidadFuncPrimerError(ContabilidadFuncCrearModelo(Inicio, Cierre, 0m, Detalle));
        }

        // Valida y graba, modifica o borra según el estado.
        public bool ContabilidadFuncGrabarCambios(EstadoEntidadContabilidad Estado, DateTime Inicio, DateTime? Cierre, decimal Gastado,
            DataTable Detalle, out string Mensaje)
        {
            ClsModeloPresupuestoContabilidad Modelo = ContabilidadFuncCrearModelo(Inicio, Cierre, Gastado, Detalle);
            Mensaje = ClsValidacionDatosContabilidad.ContabilidadFuncPrimerError(Modelo);
            if (Mensaje != string.Empty) return false;

            // Se pasa del modelo validado a la entidad del Modelo.
            ClsPresupuestoContabilidad Entidad = new ClsPresupuestoContabilidad
            {
                IdPresupuesto = Modelo.IdPresupuesto, FechaInicio = Modelo.FechaInicio, FechaCierre = Modelo.FechaCierre, Gastado = Modelo.Gastado,
                Detalle = Modelo.Partidas.Select(Partida => new ClsPresupuestoDetalleContabilidad
                    { Nombre = Partida.Nombre.Trim(), Modulo = Partida.Modulo, Cantidad = Partida.Cantidad }).ToList()
            };
            try
            {
                switch (Estado)
                {
                    case EstadoEntidadContabilidad.Agregar:
                        _Repositorio.ContabilidadFuncAgregar(Entidad);
                        Mensaje = "Presupuesto validado correctamente.";
                        break;
                    case EstadoEntidadContabilidad.Modificar:
                        _Repositorio.ContabilidadFuncEditar(Entidad);
                        Mensaje = "Presupuesto actualizado correctamente.";
                        break;
                    case EstadoEntidadContabilidad.Borrar:
                        _Repositorio.ContabilidadFuncEliminar(Entidad);
                        Mensaje = "Presupuesto eliminado correctamente.";
                        break;
                }
                return true;
            }
            catch (Exception Excepcion)
            {
                Mensaje = Excepcion.Message;
                return false;
            }
        }
    }
}
