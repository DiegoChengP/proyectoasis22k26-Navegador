/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Modelo de la póliza con sus validaciones.
 * Reglas especificas:
 *  Una póliza es válida si tiene descripción, al menos dos líneas,
 *  cada línea tiene importe solo en el debe o solo en el haber, y el
 *  total del debe es igual al del haber y mayor que cero.
 * ===================================================================
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CapaControlador_Contabilidad
{
    public class ClsModeloPolizaContabilidad : IValidatableObject
    {
        // Número de la póliza, 0 si es nueva.
        public int IdPoliza { get; set; }

        // Descripción de la póliza.
        [Required(ErrorMessage = "Escriba la descripción de la póliza.")]
        [MaxLength(255, ErrorMessage = "La descripción admite hasta 255 caracteres.")]
        public string Descripcion { get; set; }

        // Fecha de la póliza.
        public DateTime Fecha { get; set; }

        // BORRADOR, APLICADA o ANULADA.
        public string Estado { get; set; } = "BORRADOR";

        // MANUAL, ACTIVOS_FIJOS o PRESUPUESTO.
        public string Origen { get; set; } = "MANUAL";

        // Líneas del detalle.
        public IList<ClsLineaPolizaContabilidad> Lineas { get; set; } = new List<ClsLineaPolizaContabilidad>();

        // Reglas del detalle y del cuadre.
        public IEnumerable<ValidationResult> Validate(ValidationContext Contexto)
        {
            if (Lineas.Count < 2)
            {
                yield return new ValidationResult("La póliza necesita al menos dos cuentas.", new[] { "Lineas" });
                yield break;
            }
            foreach (ClsLineaPolizaContabilidad Linea in Lineas)
            {
                if (Linea.Debe < 0 || Linea.Haber < 0)
                {
                    yield return new ValidationResult("Los importes no pueden ser negativos (" + Linea.Cuenta + ").", new[] { "Lineas" });
                    yield break;
                }
                if ((Linea.Debe > 0) == (Linea.Haber > 0))
                {
                    yield return new ValidationResult("Cada cuenta debe tener importe solo en el debe o solo en el haber (" + Linea.Cuenta + ").", new[] { "Lineas" });
                    yield break;
                }
            }
            decimal Debe = Lineas.Sum(Linea => Linea.Debe);
            if (Debe <= 0 || Debe != Lineas.Sum(Linea => Linea.Haber))
                yield return new ValidationResult("La póliza no cuadra: el total del debe debe ser igual al del haber.", new[] { "Lineas" });
        }
    }
}
