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
    public partial class FrmMantenimientoStock : Form
    {
        public FrmMantenimientoStock()
        {
            InitializeComponent();
        }

        private void LogisticaProcAbrirMantenimientoMovimientoInventario()
        {
            using (FrmMantenimientoMovimientoInventario FrmMovimientoInventario = new FrmMantenimientoMovimientoInventario())
            {
                FrmMovimientoInventario.ShowDialog(this);
            }
        }
        private void LogisticaBtnMovimientoInventario_Click(object sender, EventArgs e)
        {
            LogisticaProcAbrirMantenimientoMovimientoInventario();

        }
    }
}
