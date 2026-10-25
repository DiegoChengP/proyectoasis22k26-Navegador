/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Oskar Cermeño
 * Carné : 0901-23-15379
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Reglas de la póliza que no dependen de la pantalla: catálogos,
 *  período contable a partir de la fecha, totales del debe y del
 *  haber, cuadre, validación y grabación según el estado.
 * Reglas especificas:
 *  La validación vive en ClsModeloPolizaContabilidad (DataAnnotations).
 *  La Vista solo habla con esta clase, nunca con el Modelo.
 * ===================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using CapaModelo_PolizasCuentas_Contabilidad.Contratos;
using CapaModelo_PolizasCuentas_Contabilidad.Entidades;
using CapaModelo_PolizasCuentas_Contabilidad.Repositorios;
using EstadoEntidadContabilidad = CapaControlador_Contabilidad.EstadoEntidadContabilidad;
using ClsValidacionDatosContabilidad = CapaControlador_Contabilidad.ClsValidacionDatosContabilidad;

namespace CapaControlador_PolizasCuentas_Contabilidad
{
    public class ClsControladorPolizasContabilidad
    {
        private readonly IRepositorioPolizasContabilidad _Repositorio;

        public ClsControladorPolizasContabilidad() : this(new ClsRepositorioPolizasContabilidad())
        {
        }

        public ClsControladorPolizasContabilidad(IRepositorioPolizasContabilidad Repositorio)
        {
            _Repositorio = Repositorio ?? throw new ArgumentNullException(nameof(Repositorio));
        }

        public List<ClsCuentaPolizaContabilidad> ContabilidadFuncObtenerCuentas(int IdModulo)
        {
            return new ClsModeloCuentaContabilidad().ContabilidadFuncObtenerImputables(IdModulo)
                .Select(Cuenta => new ClsCuentaPolizaContabilidad { IdCuenta = Cuenta.IdCuenta, Codigo = Cuenta.Codigo, Nombre = Cuenta.Nombre })
                .ToList();
        }

        public string ContabilidadFuncObtenerNombreModulo(int IdModulo)
        {
            string Nombre = _Repositorio.ContabilidadFuncObtenerNombreModulo(IdModulo);
            return string.IsNullOrWhiteSpace(Nombre) ? "Encabezado" : Nombre;
        }

        public string[] ContabilidadFuncObtenerEstados()
        {
            return _Repositorio.ContabilidadFuncObtenerEstados();
        }

        public string ContabilidadFuncObtenerPeriodo(DateTime Fecha)
        {
            return Fecha.ToString("yyyy-MM");
        }

        public decimal ContabilidadFuncTotalDebe(IEnumerable<ClsLineaPolizaContabilidad> Lineas)
        {
            return Lineas.Sum(Linea => Linea.Debe);
        }

        public decimal ContabilidadFuncTotalHaber(IEnumerable<ClsLineaPolizaContabilidad> Lineas)
        {
            return Lineas.Sum(Linea => Linea.Haber);
        }

        public bool ContabilidadFuncEstaCuadrada(IEnumerable<ClsLineaPolizaContabilidad> Lineas)
        {
            decimal Debe = ContabilidadFuncTotalDebe(Lineas);
            return Debe > 0 && Debe == ContabilidadFuncTotalHaber(Lineas);
        }

        public string ContabilidadFuncValidar(string Descripcion, IList<ClsLineaPolizaContabilidad> Lineas)
        {
            return ClsValidacionDatosContabilidad.ContabilidadFuncPrimerError(
                new ClsModeloPolizaContabilidad { Descripcion = Descripcion, Lineas = Lineas });
        }

        public List<ClsModeloPolizaContabilidad> ContabilidadFuncObtenerPolizas()
        {
            return _Repositorio.ContabilidadFuncObtenerTodos()
                .Select(Poliza => new ClsModeloPolizaContabilidad
                {
                    IdPoliza = Poliza.IdPoliza, CodigoPoliza = Poliza.CodigoPoliza, Fecha = Poliza.Fecha,
                    Periodo = Poliza.PeriodoContable, Estado = Poliza.Estado, IdModulo = Poliza.IdModulo, Modulo = Poliza.Modulo, Descripcion = Poliza.Descripcion,
                    Lineas = Poliza.Detalle.Select(Detalle => new ClsLineaPolizaContabilidad
                        { IdCuenta = Detalle.IdCuenta, Cuenta = Detalle.Cuenta, Debe = Detalle.Debe, Haber = Detalle.Haber }).ToList()
                })
                .ToList();
        }

        public bool ContabilidadFuncGrabarCambios(EstadoEntidadContabilidad Estado, ClsModeloPolizaContabilidad Modelo, out string Mensaje)
        {
            if (Modelo == null)
            {
                Mensaje = "No se recibió la póliza.";
                return false;
            }

            Mensaje = ClsValidacionDatosContabilidad.ContabilidadFuncPrimerError(Modelo);
            if (Mensaje != string.Empty) return false;

            string EstadoPoliza = string.IsNullOrWhiteSpace(Modelo.Estado) ? "BORRADOR" : Modelo.Estado.Trim().ToUpperInvariant();
            if (!ContabilidadFuncObtenerEstados().Contains(EstadoPoliza))
            {
                Mensaje = "El estado '" + Modelo.Estado + "' no es válido.";
                return false;
            }
            if (Modelo.IdModulo <= 0)
            {
                Mensaje = "La póliza debe indicar el módulo que la origina.";
                return false;
            }

            if (Modelo.Lineas.GroupBy(Linea => Linea.IdCuenta).Any(Grupo => Grupo.Count() > 1))
            {
                Mensaje = "Una cuenta no puede repetirse en el detalle de la póliza.";
                return false;
            }

            ClsPolizaContabilidad Entidad = new ClsPolizaContabilidad
            {
                IdPoliza = Modelo.IdPoliza, Fecha = Modelo.Fecha.Date, Estado = EstadoPoliza, IdModulo = Modelo.IdModulo,
                PeriodoContable = ContabilidadFuncObtenerPeriodo(Modelo.Fecha), Descripcion = Modelo.Descripcion.Trim(),
                Detalle = Modelo.Lineas.Select(Linea => new ClsPolizaDetalleContabilidad
                    { IdCuenta = Linea.IdCuenta, Cuenta = Linea.Cuenta, Debe = Linea.Debe, Haber = Linea.Haber }).ToList()
            };
            try
            {
                switch (Estado)
                {
                    case EstadoEntidadContabilidad.Agregar:
                        if (_Repositorio.ContabilidadFuncAgregar(Entidad) <= 0)
                        {
                            Mensaje = "No se pudo registrar la póliza.";
                            return false;
                        }
                        Modelo.IdPoliza = Entidad.IdPoliza;
                        Modelo.CodigoPoliza = Entidad.CodigoPoliza;
                        Modelo.Periodo = Entidad.PeriodoContable;
                        Mensaje = "Póliza " + Entidad.CodigoPoliza + " registrada correctamente.";
                        break;
                    case EstadoEntidadContabilidad.Modificar:
                        if (_Repositorio.ContabilidadFuncEditar(Entidad) <= 0)
                        {
                            Mensaje = "Solo se puede modificar una póliza en borrador.";
                            return false;
                        }
                        Mensaje = "Póliza actualizada correctamente.";
                        break;
                    case EstadoEntidadContabilidad.Borrar:
                        if (_Repositorio.ContabilidadFuncEliminar(Entidad) <= 0)
                        {
                            Mensaje = "Una póliza aplicada no se puede anular.";
                            return false;
                        }
                        Mensaje = "Póliza eliminada correctamente.";
                        break;
                }
                return true;
            }
            catch (Exception Excepcion)
            {
                Mensaje = "Error al grabar la póliza: " + Excepcion.Message;
                return false;
            }
        }
    }
}
