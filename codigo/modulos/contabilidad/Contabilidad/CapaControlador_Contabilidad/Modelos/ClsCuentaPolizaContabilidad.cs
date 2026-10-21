using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CapaModelo_Contabilidad.Contratos;
using CapaModelo_Contabilidad.Entidades;
using CapaModelo_Contabilidad.Repositorios;

namespace CapaControlador_Contabilidad
{
    public class ClsControladorPolizaContabilidad
    {
        private readonly IRepositorioPolizasContabilidad _repositorioPolizas;

        public ClsControladorPolizaContabilidad()
        {
            _repositorioPolizas = new ClsRepositorioPolizasContabilidad();
        }

        public ClsControladorPolizaContabilidad(IRepositorioPolizasContabilidad repositorio)
        {
            _repositorioPolizas = repositorio;
        }

        // =========================================================================
        // 1. SELECT ALL (Consulta global)
        // =========================================================================

        /// <summary>
        /// Obtiene todas las pólizas con su encabezado y su lista de líneas (ClsLineaPolizaContabilidad).
        /// </summary>
        public List<ClsPolizaContabilidad> ContabilidadFuncObtenerTodasLasPolizas()
        {
            try
            {
                return _repositorioPolizas.ContabilidadFuncObtenerTodos().ToList();
            }
            catch (Exception)
            {
                return new List<ClsPolizaContabilidad>();
            }
        }

        /// <summary>
        /// Retorna el DataTable consolidado listo para enlazar directamente a un DataGridView.
        /// </summary>
        //public DataTable ContabilidadFuncObtenerTodasLasPolizasTabla()
        //{
        //    try
        //    {
        //        return _repositorioPolizas.ContabilidadFuncObtenerPolizasComoTabla();
        //    }
        //    catch (Exception)
        //    {
        //        return new DataTable();
        //    }
        //}

        // =========================================================================
        // 2. INSERT ALL (Cabecera + N Líneas en Transacción)
        // =========================================================================

        /// <summary>
        /// Valida la cabecera, la partida doble de las líneas y delega el guardado completo al repositorio.
        /// </summary>
        /// <param name="codigoPoliza">Código identificador (ej. POL-2026-0001).</param>
        /// <param name="descripcion">Descripción o concepto general.</param>
        /// <param name="lineas">Lista de líneas con cuenta, debe y haber (ClsLineaPolizaContabilidad).</param>
        /// <param name="origen">Origen de la póliza (por defecto 'MANUAL').</param>
        /// <param name="estado">Estado inicial (por defecto 'BORRADOR').</param>
        /// <returns>Cadena con mensaje de éxito o descripción del error de validación/base de datos.</returns>
        public string ContabilidadFuncInsertarPolizaCompleta(
            string codigoPoliza,
            string descripcion,
            List<ClsLineaPolizaContabilidad> lineas,
            string origen = "MANUAL",
            string estado = "BORRADOR")
        {
            try
            {
                // --- Validaciones de Reglas de Negocio ---
                if (string.IsNullOrWhiteSpace(codigoPoliza))
                    return "El código de la póliza es obligatorio.";

                if (lineas == null || lineas.Count < 2)
                    return "Una póliza contable debe contener al menos dos líneas de detalle.";

                if (lineas.Any(l => l.IdCuenta <= 0))
                    return "Todas las líneas deben tener una cuenta contable válida asignada.";

                if (lineas.Any(l => l.Debe < 0 || l.Haber < 0))
                    return "Los importes al Debe y al Haber no pueden ser negativos.";

                decimal totalDebe = lineas.Sum(l => l.Debe);
                decimal totalHaber = lineas.Sum(l => l.Haber);

                if (totalDebe <= 0m)
                    return "El importe total de la póliza debe ser mayor a cero.";

                if (totalDebe != totalHaber)
                    return $"Partida descuadrada: El total del Debe ({totalDebe:C2}) no coincide con el total del Haber ({totalHaber:C2}).";

                // --- Mapeo a las Entidades de la Capa Modelo ---
                var polizaEncabezado = new ClsPolizaContabilidad
                {
                    CodigoPoliza = codigoPoliza.Trim(),
                    Descripcion = string.IsNullOrWhiteSpace(descripcion) ? null : descripcion.Trim(),
                    Origen = string.IsNullOrWhiteSpace(origen) ? "MANUAL" : origen.Trim(),
                    Estado = string.IsNullOrWhiteSpace(estado) ? "BORRADOR" : estado.Trim(),
                    Detalle = lineas.Select(linea => new ClsPolizaDetalleContabilidad
                    {
                        IdCuenta = linea.IdCuenta,
                        Cuenta = linea.Cuenta,
                        Debe = linea.Debe,
                        Haber = linea.Haber
                    }).ToList()
                };

                // --- Ejecución en Repositorio ---
                int filasAfectadas = _repositorioPolizas.ContabilidadFuncAgregar(polizaEncabezado);

                return filasAfectadas > 0 ? "Grabacion exitosa" : "No se pudo registrar la póliza.";
            }
            catch (Exception ex)
            {
                return $"Error al registrar la póliza: {ex.Message}";
            }
        }
    }
}