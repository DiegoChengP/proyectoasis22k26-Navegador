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
    public partial class FrmMantenimientoProducto : Form
    {
        public FrmMantenimientoProducto()
        {
            InitializeComponent();
        }

        private void LogisticaProcAbrirMantenimientoCategoria()
        {
            using (FrmMantenimientoCategoria FrmCategoria = new FrmMantenimientoCategoria())
            {
                FrmCategoria.ShowDialog(this);
            }
        }

        private void LogisticaNavProducto_Load(object sender, EventArgs e)
        {
            // Se eliminó la llamada duplicada a InitializeComponent()
            LogisticaNavProducto.NavegadorMetConfigurar("tblProducto", 4, 5);
        }

        private void LogisticaBtnCategoria_Click(object sender, EventArgs e)
        {
            LogisticaProcAbrirMantenimientoCategoria();
        }


        private void LogisticaProcAbrirMantenimientoMarca()
        {
            using (FrmMantenimientoMarca FrmMarca = new FrmMantenimientoMarca())
            {
                FrmMarca.ShowDialog(this);
            }
        }

        private void LogisticaBtnMarca_Click(object sender, EventArgs e)
        {
            LogisticaProcAbrirMantenimientoMarca();
        }

        private void LogisticaProcAbrirMantenimientoLinea()
        {
            using (FrmMantenimientoLinea FrmLinea = new FrmMantenimientoLinea())
            {
                FrmLinea.ShowDialog(this);
            }
        }
        private void LogisticaBtnLinea_Click(object sender, EventArgs e)
        {
            LogisticaProcAbrirMantenimientoLinea();
        }
    }
}
