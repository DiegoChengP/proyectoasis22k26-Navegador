using CapaVista_Componentes.Componentes;
using System;
using System.Windows.Forms;

namespace CapaVista_AprobacionFinanciera
{
    public partial class FrmAprobacionFinanciera : System.Windows.Forms.Form
    {
        public FrmAprobacionFinanciera()
        {
            InitializeComponent();
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasMetFijarTamanoFormulario(this);
        }


    }
}
