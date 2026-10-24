using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaVista_CierreInventario.Forms
{
    public partial class FrmConsultaCierres : Form
    {
        public FrmConsultaCierres()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 1. Creamos una instancia del formulario al que queremos ir
            FrmConsultaCierres formularioConsulta = new FrmConsultaCierres();

            // 2. Lo mostramos en pantalla
            formularioConsulta.Show();
        }
    }
}


