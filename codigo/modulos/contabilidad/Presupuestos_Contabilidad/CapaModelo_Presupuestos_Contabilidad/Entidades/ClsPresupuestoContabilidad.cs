/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Mario Taracena
 * Carné : 0901-23-9335
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Entidades del presupuesto: el encabezado con sus fechas y lo
 *  gastado, y las partidas asignadas a cada módulo.
 * ===================================================================
*/

using System;
using System.Collections.Generic;
using CapaModelo_Contabilidad.Contratos;
using CapaModelo_Contabilidad.Repositorios;
using CapaModelo_Presupuestos_Contabilidad.Contratos;
using CapaModelo_Presupuestos_Contabilidad.Repositorios;

namespace CapaModelo_Presupuestos_Contabilidad.Entidades
{
    public class ClsPresupuestoContabilidad
    {
        public int IdPresupuesto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaCierre { get; set; }
        public decimal Gastado { get; set; }
        public List<ClsPresupuestoDetalleContabilidad> Detalle { get; set; } = new List<ClsPresupuestoDetalleContabilidad>();
    }

    public class ClsPresupuestoDetalleContabilidad
    {
        public string Nombre { get; set; }
        public string Modulo { get; set; }
        public decimal Cantidad { get; set; }
    }
}
