using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo_Contabilidad.Entidades
{
    public class ClsPolizaDetalleContabilidad
    {
        // Cuenta de la línea.
        public int IdCuenta { get; set; }
        // Código de la cuenta.
        public string Codigo { get; set; }
        // Nombre de la cuenta.
        public string Cuenta { get; set; }
        // Importe al debe.
        public decimal Debe { get; set; }
        // Importe al haber.
        public decimal Haber { get; set; }
    }
}
