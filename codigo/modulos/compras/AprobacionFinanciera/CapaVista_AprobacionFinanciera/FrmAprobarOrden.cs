using System;
using System.Windows.Forms;

namespace CapaVista_AprobacionFinanciera
{
    public partial class FrmAprobarOrden : Form
    {
        public FrmAprobarOrden()
        {
            InitializeComponent();
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasMetFijarTamanoFormulario(this);
        }
    }
}
