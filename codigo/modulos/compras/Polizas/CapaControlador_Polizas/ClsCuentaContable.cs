using System.Text.RegularExpressions;

namespace CapaControlador_Polizas
{
    public static class ClsCuentaContable
    {
        public static bool ComprasFuncFormatoValido(string CodigoCuenta)
        {
            return CodigoCuenta != null && Regex.IsMatch(CodigoCuenta,
                @"\A[1-6]\.[0-9]{2}\.[0-9]{2}\.[0-9]{3}\z", RegexOptions.CultureInvariant);
        }
    }
}
