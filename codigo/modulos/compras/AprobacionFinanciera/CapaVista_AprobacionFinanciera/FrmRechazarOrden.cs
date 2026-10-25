using System;
using System.Windows.Forms;

namespace CapaVista_AprobacionFinanciera
{
    public partial class FrmRechazarOrden : Form
    {
        public FrmRechazarOrden()
        {
            InitializeComponent();
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasMetFijarTamanoFormulario(this);
        }
    }
}
