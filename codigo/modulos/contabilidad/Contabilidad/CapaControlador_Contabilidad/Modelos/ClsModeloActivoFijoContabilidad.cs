/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Modelo del activo fijo con sus validaciones. Los atributos
 *  validan cada campo y Validate valida las reglas entre campos.
 * ===================================================================
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapaControlador_Contabilidad
{
    public class ClsModeloActivoFijoContabilidad : IValidatableObject
    {
        // Número del activo, 0 si es nuevo.
        public int IdActivo { get; set; }

        // Descripción del activo.
        [Required(ErrorMessage = "Ingrese una descripción.")]
        [MaxLength(255, ErrorMessage = "La descripción admite hasta 255 caracteres.")]
        public string Descripcion { get; set; }

        // Centro de costo.
        [Required(ErrorMessage = "Ingrese el centro de costo.")]
        [MaxLength(20, ErrorMessage = "El centro de costo admite hasta 20 caracteres.")]
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
        [Range(1, int.MaxValue, ErrorMessage = "La vida útil debe ser mayor que cero.")]
        public int Meses { get; set; }

        // Reglas entre campos: fechas e importes.
        public IEnumerable<ValidationResult> Validate(ValidationContext Contexto)
        {
            if (Inicio.Date < Adquisicion.Date)
                yield return new ValidationResult("No puede ser anterior a la adquisición.", new[] { "Inicio" });
            if (Compra <= 0m || Compra > ClsLimitesContabilidad.MaximoMonetario)
                yield return new ValidationResult("Ingrese un valor de compra válido mayor que cero.", new[] { "Compra" });
            if (Residual < 0m || Residual > Compra)
                yield return new ValidationResult("Debe estar entre cero y el valor de compra.", new[] { "Residual" });
        }
    }
}
