using CapaVista_Navegador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaVista_Inventario_Logistica.Formas
{
    public partial class FrmMantenimientoSucursales : Form
    {
        public FrmMantenimientoSucursales()
        {
            InitializeComponent();
        }

        private void LogisticaProcAbrirMantenimientoBodegas()
        {
            using (FrmMantenimientoBodegas FrmBodega = new FrmMantenimientoBodegas())
            {
                FrmBodega.ShowDialog(this);
            }
        }

        private void LogisticaBtnBodega_Click(object sender, EventArgs e)
        {
               LogisticaProcAbrirMantenimientoBodegas();
        }
    }
}