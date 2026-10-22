/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Entidades del presupuesto: el encabezado con sus fechas y lo
 *  gastado, y las partidas asignadas a cada módulo.
 * ===================================================================
*/

using System;
using System.Collections.Generic;

namespace CapaModelo_Contabilidad.Entidades
{
    public class ClsPresupuestoContabilidad
    {
        // Número del presupuesto.
        public int IdPresupuesto { get; set; }
        // Fecha de inicio.
        public DateTime FechaInicio { get; set; }
        // Fecha de cierre, vacía si sigue abierto.
        public DateTime? FechaCierre { get; set; }
        // Total gastado hasta hoy.
        public decimal Gastado { get; set; }
        // Partidas del presupuesto.
        public List<ClsPresupuestoDetalleContabilidad> Detalle { get; set; } = new List<ClsPresupuestoDetalleContabilidad>();
    }

    public class ClsPresupuestoDetalleContabilidad
    {
        // Nombre de la partida.
        public string Nombre { get; set; }
        // Módulo al que se asigna.
        public string Modulo { get; set; }
        // Importe asignado.
        public decimal Cantidad { get; set; }
    }
}
