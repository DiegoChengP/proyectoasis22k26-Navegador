/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 21/10/2026
 * ==================================================================
 * Propósito :
 *  Entidad del encabezado de la póliza (tblPolizaEncabezado) con su
 *  lista de líneas de detalle.
 * Reglas especificas:
 *  Los campos CodigoPoliza y PeriodoContable los agregó Oskar
 *  Cermeño. El código lo genera la base con fnGenerarCodigoPoliza.
 * ===================================================================
*/

using System;
using System.Collections.Generic;

namespace CapaModelo_Contabilidad.Entidades
{
    public class ClsPolizaContabilidad
    {
        // Número de la póliza (Pk_PolizaEncabezado).
        public int IdPoliza { get; set; }
        // Correlativo POL-AAAA-MM-#### (lo genera la base).
        public string CodigoPoliza { get; set; }
        // Fecha de la póliza.
        public DateTime Fecha { get; set; }
        // Período con formato yyyy-MM.
        public string PeriodoContable { get; set; }
        // BORRADOR, APLICADA o ANULADA.
        public string Estado { get; set; } = "BORRADOR";
        // MANUAL, BANCOS, ACTIVOS_FIJOS, PRESUPUESTO, NOMINA, VENTAS o COMPRAS.
        public string Origen { get; set; } = "MANUAL";
        // Documento del módulo origen (evita traslados duplicados); vacío si es manual.
        public string DocumentoOrigen { get; set; }
        // Descripción libre.
        public string Descripcion { get; set; }
        // Líneas del detalle.
        public List<ClsPolizaDetalleContabilidad> Detalle { get; set; } = new List<ClsPolizaDetalleContabilidad>();
    }
}
