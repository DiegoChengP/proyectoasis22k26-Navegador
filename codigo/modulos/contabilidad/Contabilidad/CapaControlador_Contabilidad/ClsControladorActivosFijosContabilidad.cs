/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Reglas de activos fijos: catálogos, depreciación por línea recta,
 *  validación y grabación según el estado. Responsable de la
 *  pantalla: Dylan.
 * Reglas especificas:
 *  La validación vive en ClsModeloActivoFijoContabilidad. La
 *  depreciación mensual se redondea a dos decimales.
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
    public class ClsControladorActivosFijosContabilidad
    {
        // Repositorio de activos fijos (hoy en memoria).
        private readonly IRepositorioActivosFijosContabilidad _Repositorio = new ClsRepositorioActivosFijosContabilidad();

        // Módulos para el combo.
        public string[] ContabilidadFuncModulos()
        {
            return _Repositorio.ContabilidadFuncObtenerModulos();
        }

        // Maestro de vida útil como tabla.
        public DataTable ContabilidadFuncVidaUtil()
        {
            DataTable Tabla = new DataTable();
            Tabla.Columns.Add("Tipo", typeof(string));
            Tabla.Columns.Add("Nombre", typeof(string));
            Tabla.Columns.Add("Meses", typeof(int));
            foreach (ClsVidaUtilContabilidad Vida in _Repositorio.ContabilidadFuncObtenerVidaUtil())
                Tabla.Rows.Add(Vida.Tipo, Vida.Nombre, Vida.Meses);
            return Tabla;
        }

        // Devuelve el modelo con los datos de la pantalla.
        private ClsModeloActivoFijoContabilidad ContabilidadFuncCrearModelo(string Descripcion, string Centro, DateTime Adquisicion,
            DateTime Inicio, decimal Compra, decimal Residual, int Meses)
        {
            return new ClsModeloActivoFijoContabilidad
            {
                Descripcion = Descripcion, Centro = Centro, Adquisicion = Adquisicion, Inicio = Inicio,
                Compra = Compra, Residual = Residual, Meses = Meses
            };
        }

        // Errores por campo de la ficha.
        public Dictionary<string, string> ContabilidadFuncErroresActivo(string Descripcion, string Centro, DateTime Adquisicion,
            DateTime Inicio, decimal Compra, decimal Residual, int Meses)
        {
            return ClsValidacionDatosContabilidad.ContabilidadFuncErrores(
                ContabilidadFuncCrearModelo(Descripcion, Centro, Adquisicion, Inicio, Compra, Residual, Meses));
        }

        // Primer error de la ficha, vacío si es válida.
        public string ContabilidadFuncValidarActivo(string Descripcion, string Centro, DateTime Adquisicion,
            DateTime Inicio, decimal Compra, decimal Residual, int Meses)
        {
            return ContabilidadFuncErroresActivo(Descripcion, Centro, Adquisicion, Inicio, Compra, Residual, Meses).Values.FirstOrDefault() ?? string.Empty;
        }

        // Compra menos residual.
        public decimal ContabilidadFuncBaseDepreciable(decimal Compra, decimal Residual)
        {
            return Compra - Residual;
        }

        // Cuota mensual de línea recta.
        public decimal ContabilidadFuncDepreciacion(decimal Compra, decimal Residual, int Meses)
        {
            if (Compra < 0m || Residual < 0m || Residual > Compra || Meses <= 0) return 0m;
            return decimal.Round((Compra - Residual) / Meses, 2, MidpointRounding.AwayFromZero);
        }

        // Valida y graba, modifica o borra según el estado.
        public bool ContabilidadFuncGrabarCambios(EstadoEntidadContabilidad Estado, string Descripcion, string Centro, string Modulo,
            string Tipo, DateTime Adquisicion, DateTime Inicio, decimal Compra, decimal Residual, int Meses, out string Mensaje)
        {
            ClsModeloActivoFijoContabilidad Modelo = ContabilidadFuncCrearModelo(Descripcion, Centro, Adquisicion, Inicio, Compra, Residual, Meses);
            Modelo.Modulo = Modulo;
            Modelo.Tipo = Tipo;
            Mensaje = ClsValidacionDatosContabilidad.ContabilidadFuncPrimerError(Modelo);
            if (Mensaje != string.Empty) return false;

            // Se pasa del modelo validado a la entidad del Modelo.
            ClsActivoFijoContabilidad Entidad = new ClsActivoFijoContabilidad
            {
                IdActivo = Modelo.IdActivo, Descripcion = Modelo.Descripcion.Trim(), Centro = Modelo.Centro.Trim(), Modulo = Modelo.Modulo,
                Tipo = Modelo.Tipo, Adquisicion = Modelo.Adquisicion, Inicio = Modelo.Inicio, Compra = Modelo.Compra,
                Residual = Modelo.Residual, Meses = Modelo.Meses
            };
            try
            {
                switch (Estado)
                {
                    case EstadoEntidadContabilidad.Agregar:
                        _Repositorio.ContabilidadFuncAgregar(Entidad);
                        Mensaje = "Activo validado correctamente.";
                        break;
                    case EstadoEntidadContabilidad.Modificar:
                        _Repositorio.ContabilidadFuncEditar(Entidad);
                        Mensaje = "Activo actualizado correctamente.";
                        break;
                    case EstadoEntidadContabilidad.Borrar:
                        _Repositorio.ContabilidadFuncEliminar(Entidad);
                        Mensaje = "Activo eliminado correctamente.";
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
