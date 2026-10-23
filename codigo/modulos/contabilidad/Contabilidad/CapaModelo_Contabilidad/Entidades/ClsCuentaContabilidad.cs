/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 21/10/2026
 * ==================================================================
 * Propósito :
 *  Entidad que coincide con la tabla tblCuenta: código, nombre,
 *  naturaleza, cuenta padre y estado.
 * Reglas especificas:
 *  Los campos de tipo, padre y estado los agregó Oskar Cermeño; aquí
 *  se ajustaron a los nombres de EST-01 (is_active como bool).
 * ===================================================================
*/

namespace CapaModelo_Contabilidad.Entidades
{
    public class ClsCuentaContabilidad
    {
        // Llave de la cuenta (Pk_Cuenta).
        public int IdCuenta { get; set; }
        // Código de la cuenta (codigoCuenta), por ejemplo 1.01.01.001.
        public string Codigo { get; set; }
        // Nombre de la cuenta.
        public string Nombre { get; set; }
        // Naturaleza: DEUDORA o ACREEDORA.
        public string Tipo { get; set; }
        // Cuenta padre; 0 si es una cuenta raíz.
        public int IdPadre { get; set; }
        // Indica si la cuenta está activa (is_active).
        public bool Activo { get; set; }

        // Se muestra el nombre en los listados.
        public override string ToString()
        {
            return Nombre;
        }
    }
}
