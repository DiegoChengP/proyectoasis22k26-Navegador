using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaVista_TransporteExterno_Logistica.Formas;

namespace CapaVista_TransporteInterno_Logistica.Formas
{
    public partial class FrmSeleccionTipoTraslado : Form
    {
        public FrmSeleccionTipoTraslado()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Validar si el RadioButton Interno está seleccionado
            if (rdbInterno.Checked)
            {
               FrmTransporteInterno transporteInterno = new FrmTransporteInterno();
                transporteInterno.ShowDialog(); // Usa .ShowDialog() si quieres abrirlo como ventana modal
            }
            // Validar si el RadioButton Externo está seleccionado
            else if (rdbExterno.Checked)
            {
                CapaVista_TransporteExterno_Logistica.Formas.TransporteExterno transporteExterno = new CapaVista_TransporteExterno_Logistica.Formas.TransporteExterno();
                transporteExterno.ShowDialog(); // Usa .ShowDialog() si quieres abrirlo como ventana modalB
            }
        }
    }
}
