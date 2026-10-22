/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Entidades de la póliza: el encabezado y sus líneas de detalle.
 *  Los importes son decimal, nunca double.
 * ===================================================================
*/

using System;
using System.Collections.Generic;

namespace CapaModelo_Contabilidad.Entidades
{
    public class ClsPolizaContabilidad
    {
        // Número de la póliza.
        public int IdPoliza { get; set; }
        // Fecha de la póliza.
        public DateTime Fecha { get; set; }
        // BORRADOR, APLICADA o ANULADA.
        public string Estado { get; set; }
        // Período con formato yyyy-MM.
        public string Periodo { get; set; }
        // MANUAL, ACTIVOS_FIJOS o PRESUPUESTO.
        public string Origen { get; set; }
        // Descripción libre.
        public string Descripcion { get; set; }
        // Líneas del detalle.
        public List<ClsPolizaDetalleContabilidad> Detalle { get; set; } = new List<ClsPolizaDetalleContabilidad>();
    }

    public class ClsPolizaDetalleContabilidad
    {
        // Cuenta de la línea.
        public int IdCuenta { get; set; }
        // Código de la cuenta.
        public string Codigo { get; set; }
        // Nombre de la cuenta.
        public string Cuenta { get; set; }
        // Importe al debe.
        public decimal Debe { get; set; }
        // Importe al haber.
        public decimal Haber { get; set; }
    }
}
