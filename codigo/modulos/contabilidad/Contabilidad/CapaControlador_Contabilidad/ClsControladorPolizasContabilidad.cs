/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
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
using CapaModelo_Contabilidad.Contratos;
using CapaModelo_Contabilidad.Entidades;
using CapaModelo_Contabilidad.Repositorios;

namespace CapaControlador_Contabilidad
{
    public class ClsControladorPolizasContabilidad
    {
        // Repositorio de pólizas (base de datos por ODBC).
        private readonly IRepositorioPolizasContabilidad _Repositorio;

        public ClsControladorPolizasContabilidad() : this(new ClsRepositorioPolizasContabilidad())
        {
        }

        // Permite inyectar otro repositorio (pruebas).
        public ClsControladorPolizasContabilidad(IRepositorioPolizasContabilidad Repositorio)
        {
            _Repositorio = Repositorio ?? throw new ArgumentNullException(nameof(Repositorio));
        }

        // Cuentas del catálogo. Si la cuenta trae código se valida el formato C.GG.CC.SSS.
        public List<ClsCuentaPolizaContabilidad> ContabilidadFuncObtenerCuentas()
        {
            List<ClsCuentaPolizaContabilidad> Cuentas = _Repositorio.ContabilidadFuncObtenerCuentas()
                .Select(Cuenta => new ClsCuentaPolizaContabilidad { IdCuenta = Cuenta.IdCuenta, Codigo = Cuenta.Codigo, Nombre = Cuenta.Nombre })
                .ToList();
            List<ClsCuentaPolizaContabilidad> ConCodigo = Cuentas.Where(Cuenta => !string.IsNullOrWhiteSpace(Cuenta.Codigo)).ToList();
            if (ConCodigo.Any(Cuenta => !ClsCodigoCuentaContabilidad.ContabilidadFuncEsValido(Cuenta.Codigo)) ||
                ConCodigo.GroupBy(Cuenta => Cuenta.Codigo).Any(Grupo => Grupo.Count() > 1))
                throw new InvalidOperationException("El catálogo contiene códigos contables inválidos o duplicados.");
            return Cuentas;
        }

        // Estados posibles.
        public string[] ContabilidadFuncObtenerEstados()
        {
            return _Repositorio.ContabilidadFuncObtenerEstados();
        }

        // Orígenes posibles.
        public string[] ContabilidadFuncObtenerOrigenes()
        {
            return _Repositorio.ContabilidadFuncObtenerOrigenes();
        }

        // Período con formato yyyy-MM.
        public string ContabilidadFuncObtenerPeriodo(DateTime Fecha)
        {
            return Fecha.ToString("yyyy-MM");
        }

        // Código automático cuando la Vista no envía uno: POL-yyyyMMdd-HHmmssfff (24 caracteres).
        public string ContabilidadFuncGenerarCodigo(DateTime Fecha)
        {
            return "POL-" + Fecha.ToString("yyyyMMdd") + "-" + DateTime.Now.ToString("HHmmssfff");
        }

        // Suma del debe.
        public decimal ContabilidadFuncTotalDebe(IEnumerable<ClsLineaPolizaContabilidad> Lineas)
        {
            return Lineas.Sum(Linea => Linea.Debe);
        }

        // Suma del haber.
        public decimal ContabilidadFuncTotalHaber(IEnumerable<ClsLineaPolizaContabilidad> Lineas)
        {
            return Lineas.Sum(Linea => Linea.Haber);
        }

        // Cuadra si el debe es mayor que cero e igual al haber.
        public bool ContabilidadFuncEstaCuadrada(IEnumerable<ClsLineaPolizaContabilidad> Lineas)
        {
            decimal Debe = ContabilidadFuncTotalDebe(Lineas);
            return Debe > 0 && Debe == ContabilidadFuncTotalHaber(Lineas);
        }

        // Devuelve "" si la póliza es válida; si no, el motivo en español.
        public string ContabilidadFuncValidar(string Descripcion, IList<ClsLineaPolizaContabilidad> Lineas)
        {
            return ClsValidacionDatosContabilidad.ContabilidadFuncPrimerError(
                new ClsModeloPolizaContabilidad { Descripcion = Descripcion, Lineas = Lineas });
        }

        // Todas las pólizas existentes con su detalle, como modelos del Controlador.
        public List<ClsModeloPolizaContabilidad> ContabilidadFuncObtenerPolizas()
        {
            return _Repositorio.ContabilidadFuncObtenerTodos()
                .Select(Poliza => new ClsModeloPolizaContabilidad
                {
                    IdPoliza = Poliza.IdPoliza, CodigoPoliza = Poliza.CodigoPoliza, Fecha = Poliza.Fecha,
                    Periodo = Poliza.PeriodoContable, Estado = Poliza.Estado, Origen = Poliza.Origen, Descripcion = Poliza.Descripcion,
                    Lineas = Poliza.Detalle.Select(Detalle => new ClsLineaPolizaContabilidad
                        { IdCuenta = Detalle.IdCuenta, Cuenta = Detalle.Cuenta, Debe = Detalle.Debe, Haber = Detalle.Haber }).ToList()
                })
                .ToList();
        }

        // Valida el modelo y lo graba, modifica o borra según el estado.
        public bool ContabilidadFuncGrabarCambios(EstadoEntidadContabilidad Estado, ClsModeloPolizaContabilidad Modelo, out string Mensaje)
        {
            if (Modelo == null)
            {
                Mensaje = "No se recibió la póliza.";
                return false;
            }

            Mensaje = ClsValidacionDatosContabilidad.ContabilidadFuncPrimerError(Modelo);
            if (Mensaje != string.Empty) return false;

            // Estado y origen deben existir en los catálogos.
            string EstadoPoliza = string.IsNullOrWhiteSpace(Modelo.Estado) ? "BORRADOR" : Modelo.Estado.Trim().ToUpperInvariant();
            string OrigenPoliza = string.IsNullOrWhiteSpace(Modelo.Origen) ? "MANUAL" : Modelo.Origen.Trim().ToUpperInvariant();
            if (!ContabilidadFuncObtenerEstados().Contains(EstadoPoliza))
            {
                Mensaje = "El estado '" + Modelo.Estado + "' no es válido.";
                return false;
            }
            if (!ContabilidadFuncObtenerOrigenes().Contains(OrigenPoliza))
            {
                Mensaje = "El origen '" + Modelo.Origen + "' no es válido.";
                return false;
            }

            // Una cuenta solo puede aparecer una vez en el detalle.
            if (Modelo.Lineas.GroupBy(Linea => Linea.IdCuenta).Any(Grupo => Grupo.Count() > 1))
            {
                Mensaje = "Una cuenta no puede repetirse en el detalle de la póliza.";
                return false;
            }

            // Se pasa del modelo validado a la entidad del Modelo.
            ClsPolizaContabilidad Entidad = new ClsPolizaContabilidad
            {
                IdPoliza = Modelo.IdPoliza,
                CodigoPoliza = string.IsNullOrWhiteSpace(Modelo.CodigoPoliza) ? ContabilidadFuncGenerarCodigo(Modelo.Fecha) : Modelo.CodigoPoliza.Trim(),
                Fecha = Modelo.Fecha.Date, Estado = EstadoPoliza, Origen = OrigenPoliza,
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
                        // Se devuelven al modelo los datos generados al grabar.
                        Modelo.IdPoliza = Entidad.IdPoliza;
                        Modelo.CodigoPoliza = Entidad.CodigoPoliza;
                        Modelo.Periodo = Entidad.PeriodoContable;
                        Mensaje = "Póliza " + Entidad.CodigoPoliza + " registrada correctamente.";
                        break;
                    case EstadoEntidadContabilidad.Modificar:
                        _Repositorio.ContabilidadFuncEditar(Entidad);
                        Mensaje = "Póliza actualizada correctamente.";
                        break;
                    case EstadoEntidadContabilidad.Borrar:
                        _Repositorio.ContabilidadFuncEliminar(Entidad);
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
