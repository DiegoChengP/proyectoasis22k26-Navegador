using CapaVista_Componentes.Componentes;
using System;
using System.Windows.Forms;

namespace CapaVista_AprobacionFinanciera
{
    public partial class FrmEvaluarOrden : System.Windows.Forms.Form
    {
        public FrmEvaluarOrden()
        {
            InitializeComponent();
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasMetFijarTamanoFormulario(this);
        }
    }
}
