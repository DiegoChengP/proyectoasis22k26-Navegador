/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Modelos del presupuesto y de su partida con sus validaciones.
 * ===================================================================
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CapaControlador_Contabilidad
{
    public class ClsModeloPartidaPresupuestoContabilidad : IValidatableObject
    {
        // Nombre de la partida.
        [Required(ErrorMessage = "Ingrese el nombre de la partida.")]
        [MaxLength(150, ErrorMessage = "El nombre admite hasta 150 caracteres.")]
        public string Nombre { get; set; }

        // Módulo al que se asigna.
        [Required(ErrorMessage = "Seleccione un módulo válido.")]
        public string Modulo { get; set; }

        // Importe asignado.
        public decimal Cantidad { get; set; }

        // Regla del importe.
        public IEnumerable<ValidationResult> Validate(ValidationContext Contexto)
        {
            if (Cantidad <= 0m || Cantidad > ClsLimitesContabilidad.MaximoMonetario)
                yield return new ValidationResult("La asignación debe ser mayor que cero.", new[] { "Cantidad" });
        }
    }

    public class ClsModeloPresupuestoContabilidad : IValidatableObject
    {
        // Número del presupuesto, 0 si es nuevo.
        public int IdPresupuesto { get; set; }

        // Fecha de inicio.
        public DateTime FechaInicio { get; set; }

        // Fecha de cierre, vacía si sigue abierto.
        public DateTime? FechaCierre { get; set; }

        // Total gastado.
        public decimal Gastado { get; set; }

        // Partidas del presupuesto.
        public List<ClsModeloPartidaPresupuestoContabilidad> Partidas { get; set; } = new List<ClsModeloPartidaPresupuestoContabilidad>();

        // Reglas entre campos y del detalle.
        public IEnumerable<ValidationResult> Validate(ValidationContext Contexto)
        {
            if (FechaCierre.HasValue && FechaCierre.Value.Date < FechaInicio.Date)
                yield return new ValidationResult("La fecha de cierre debe ser igual o posterior al inicio.", new[] { "FechaCierre" });
            if (Gastado < 0m || Gastado > ClsLimitesContabilidad.MaximoMonetario)
                yield return new ValidationResult("Lo gastado debe estar dentro del límite permitido.", new[] { "Gastado" });
            if (Partidas.Count == 0)
            {
                yield return new ValidationResult("Agregue al menos una partida al presupuesto.", new[] { "Partidas" });
                yield break;
            }
            foreach (ClsModeloPartidaPresupuestoContabilidad Partida in Partidas)
            {
                if (string.IsNullOrWhiteSpace(Partida.Nombre))
                    yield return new ValidationResult("Cada partida necesita un nombre.", new[] { "Partidas" });
                else if (Partida.Cantidad <= 0m || Partida.Cantidad > ClsLimitesContabilidad.MaximoMonetario)
                    yield return new ValidationResult("Las partidas deben tener importes positivos dentro del límite permitido.", new[] { "Partidas" });
            }
            if (Partidas.Sum(Partida => Partida.Cantidad) > ClsLimitesContabilidad.MaximoMonetario)
                yield return new ValidationResult("El total excede el límite permitido.", new[] { "Partidas" });
        }
    }
}
