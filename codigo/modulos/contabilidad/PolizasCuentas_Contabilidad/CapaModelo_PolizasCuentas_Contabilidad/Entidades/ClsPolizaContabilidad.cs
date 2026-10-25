/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Oskar Cermeño
 * Carné : 0901-23-15379
 * Fecha : 21/10/2026
 * ==================================================================
 * Propósito :
 *  Entidad del encabezado de la póliza (tblPolizaEncabezado) con su
 *  lista de líneas de detalle.
 * Reglas especificas:
 *  Los campos CodigoPoliza y PeriodoContable los agregó Oskar
 *  Cermeño. IdModulo es el módulo que origina la póliza.
 * ===================================================================
*/

using System;
using System.Collections.Generic;
using CapaModelo_PolizasCuentas_Contabilidad.Contratos;
using CapaModelo_PolizasCuentas_Contabilidad.Repositorios;

namespace CapaModelo_PolizasCuentas_Contabilidad.Entidades
{
    public class ClsPolizaContabilidad
    {
        public int IdPoliza { get; set; }
        public string CodigoPoliza { get; set; }
        public DateTime Fecha { get; set; }
        public string PeriodoContable { get; set; }
        public string Estado { get; set; } = "BORRADOR";
        public int IdModulo { get; set; } = 1;
        public string Modulo { get; set; }
        public string DocumentoOrigen { get; set; }
        public string Descripcion { get; set; }
        public List<ClsPolizaDetalleContabilidad> Detalle { get; set; } = new List<ClsPolizaDetalleContabilidad>();
    }
}
