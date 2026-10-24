using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaVista_TransporteExterno_Logistica.Formas
{
    public partial class TransporteExterno : Form
    {
        public TransporteExterno()
        {
            InitializeComponent();
        }

        private void LogisticaBtnRegistroTransporte_Click(object sender, EventArgs e)
        {
            RegistroTransporteExterno frmTransporteExterno = new RegistroTransporteExterno();
            frmTransporteExterno.ShowDialog();
        }

        private void LogisticaBtnRutaSeguimiento_Click(object sender, EventArgs e)
        {
            RutaYSeguimiento frmRutaSeguimiento = new RutaYSeguimiento();
            frmRutaSeguimiento.ShowDialog();
        }

        private void LogisticaBtnProductosCarga_Click(object sender, EventArgs e)
        {
            ProductosATrasladar frmProductoTrasladado = new ProductosATrasladar();
            frmProductoTrasladado.ShowDialog();
        }

        private void LogisticaBtnCostosPagos_Click(object sender, EventArgs e)
        {
            CostosYPagosDelServicio costosYPagosDelServicio = new CostosYPagosDelServicio();
            costosYPagosDelServicio.ShowDialog();
        }

        private void LogisticaBtnIncidenciasEHistorial_Click(object sender, EventArgs e)
        {
            IncidenciasEHistorial frmIncidenciasEHistorial = new IncidenciasEHistorial();
            frmIncidenciasEHistorial.ShowDialog();
        }

        private void LogisticaBtnListadoTransporteExterno_Click(object sender, EventArgs e)
        {
            ListadoTransporteExterno frmListadoTransporteExterno = new ListadoTransporteExterno();
            frmListadoTransporteExterno.ShowDialog();
        }
    }
}