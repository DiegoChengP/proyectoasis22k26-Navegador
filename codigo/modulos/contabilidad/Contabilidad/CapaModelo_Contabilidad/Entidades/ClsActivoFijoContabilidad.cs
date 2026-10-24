/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Entidades de activos fijos: la ficha del activo y el maestro de
 *  vida útil por tipo de activo.
 * ===================================================================
*/

using System;

namespace CapaModelo_Contabilidad.Entidades
{
    public class ClsActivoFijoContabilidad
    {
        // Número del activo.
        public int IdActivo { get; set; }
        // Descripción del activo.
        public string Descripcion { get; set; }
        // Centro de costo.
        public string Centro { get; set; }
        // Módulo dueño del activo.
        public string Modulo { get; set; }
        // Código del tipo de activo.
        public string Tipo { get; set; }
        // Fecha de adquisición.
        public DateTime Adquisicion { get; set; }
        // Fecha de inicio de uso.
        public DateTime Inicio { get; set; }
        // Valor de compra.
        public decimal Compra { get; set; }
        // Valor residual.
        public decimal Residual { get; set; }
        // Vida útil en meses.
        public int Meses { get; set; }
    }

    public class ClsVidaUtilContabilidad
    {
        // Código del tipo de activo.
        public string Tipo { get; set; }
        // Nombre del tipo.
        public string Nombre { get; set; }
        // Meses de vida útil.
        public int Meses { get; set; }
    }
}
