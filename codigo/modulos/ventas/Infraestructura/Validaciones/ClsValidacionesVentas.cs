using System;
using System.Globalization;

namespace VentasValidaciones
{
    internal static class ClsValidacionesVentas
    {
        internal const decimal ImporteMaximo = 9999999999999999.99m;
        internal static void Exigir(bool Condicion, string Mensaje)
        { if (!Condicion) throw new InvalidOperationException(Mensaje); }
        internal static decimal Decimal(object Valor)
        { return Valor == null || Valor == DBNull.Value ? 0m : Convert.ToDecimal(Valor); }
        internal static decimal Redondear(decimal Valor)
        { return Math.Round(Valor, 2, MidpointRounding.AwayFromZero); }
        internal static string Texto(string Valor, int Maximo, string Campo, bool Requerido = true)
        {
            Valor = (Valor ?? "").Trim();
            Exigir(!Requerido || Valor.Length > 0, "Ingrese " + Campo + ".");
            Exigir(Valor.Length <= Maximo, Campo + " supera " + Maximo + " caracteres.");
            return Valor;
        }
        internal static decimal ImporteCelda(object Valor)
        {
            if (Valor == null || Valor == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(Valor))) return 0;
            decimal Resultado;
            Exigir(decimal.TryParse(Convert.ToString(Valor), NumberStyles.Number, CultureInfo.CurrentCulture, out Resultado), "Ingrese un importe numerico valido.");
            return Resultado;
        }
    }
}
