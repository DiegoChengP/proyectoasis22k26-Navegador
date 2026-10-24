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
    public partial class FrmCierreInventario : Form
    {
        public FrmCierreInventario()
        {
            InitializeComponent();
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            FrmConsultaCierres frmConsultas = new FrmConsultaCierres();
            frmConsultas.ShowDialog();
        }
    }
    
}
