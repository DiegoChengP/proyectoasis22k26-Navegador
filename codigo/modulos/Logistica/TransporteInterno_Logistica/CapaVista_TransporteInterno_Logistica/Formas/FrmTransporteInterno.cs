using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaVista_TransporteInterno_Logistica.Formas
{
    public partial class FrmTransporteInterno : Form
    {
        public FrmTransporteInterno()
        {
            InitializeComponent();
        }

        private void LogisticaBtnGestionViajes_Click(object sender, EventArgs e)
        {
            FrmGestionViajes frmGestionViajes = new FrmGestionViajes();
            frmGestionViajes.ShowDialog();
        }

        private void LogisticaBtnControlTransporte_Click(object sender, EventArgs e)
        {
            FrmControlTransporte frmControlTransporte = new FrmControlTransporte();
            frmControlTransporte.ShowDialog();
        }

        private void LogisticaBtnConsultasTransporte_Click(object sender, EventArgs e)
        {
            FrmConsultaTransporte frmConsultasTransporte = new FrmConsultaTransporte();
            frmConsultasTransporte.ShowDialog();
        }
    }
}
