// Jose Javier Torres Martinez | 0901-23-1091 | 22/10/2026
using System;
using System.Data;
using System.Data.Odbc;
using System.Globalization;

namespace VentasDatos
{
    internal sealed class ClsCorrelativosVentas
    {
        private readonly ClsDatosVentas _Datos;
        internal ClsCorrelativosVentas(ClsDatosVentas Datos)
        {
            _Datos = Datos;
        }

        internal string VentasMetNumero(string Prefijo)
        {
            // Se llama dentro de la transaccion; la fila serializa los correlativos por prefijo.
            _Datos.VentasMetEjecutar("INSERT INTO tblcorrelativoventas(prefijo,ultimoNumero) VALUES (?,0) ON DUPLICATE KEY UPDATE prefijo=VALUES(prefijo)", Prefijo);
            long N = Convert.ToInt64(_Datos.VentasMetEscalar("SELECT ultimoNumero FROM tblcorrelativoventas WHERE prefijo=? FOR UPDATE", Prefijo));
            string Buscar;
            switch (Prefijo)
            {
                case "COT":
                    Buscar = "SELECT COUNT(*) FROM tblcotizacion WHERE numeroCotizacion=?";
                    break;
                case "REC":
                    Buscar = "SELECT COUNT(*) FROM tblpago WHERE numeroRecibo=?";
                    break;
                case "CXC":
                    Buscar = "SELECT COUNT(*) FROM tblfactura WHERE serieFactura='CXC' AND numeroFactura=?";
                    break;
                case "NOT":
                    Buscar = "SELECT COUNT(*) FROM tblnotacxc WHERE numeroNota=?";
                    break;
                case "DEP":
                    Buscar = "SELECT COUNT(*) FROM tbldepositoventas WHERE codigoDeposito=?";
                    break;
                case "CIE":
                    Buscar = "SELECT COUNT(*) FROM tblcierremovimientos WHERE numeroCierre=?";
                    break;
                case "PCOT":
                    Buscar = "SELECT COUNT(*) FROM tblpedido WHERE numeroPedido=?";
                    break;
                case "FCOT":
                    Buscar = "SELECT COUNT(*) FROM tblfactura WHERE serieFactura='COT' AND numeroFactura=?";
                    break;
                default:
                    throw new ArgumentException("Correlativo no reconocido.");
            }

            string Numero;
            do
            {
                Numero = Prefijo + "-" + (++N).ToString("D8", CultureInfo.InvariantCulture);
            }
            while (Convert.ToInt32(_Datos.VentasMetEscalar(Buscar, Numero)) > 0);
            _Datos.VentasMetEjecutar("UPDATE tblcorrelativoventas SET ultimoNumero=? WHERE prefijo=?", N, Prefijo);
            return Numero;
        }
    }
}
