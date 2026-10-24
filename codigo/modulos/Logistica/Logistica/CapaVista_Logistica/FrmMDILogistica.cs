using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaVista_Inventario_Logistica;
using CapaVista_Inventario_Logistica.Formas;
using CapaVista_TransporteExterno_Logistica;
using CapaVista_Traslados_Logistica;
using CapaVista_MuestreoAuditoria_Logistica;
using CapaVista_CierreInventario.Forms;
using CapaVista_TransporteInterno_Logistica.Formas;

namespace CapaVista_Logistica
{
    public partial class FrmMDILogistica : Form
    {
        public FrmMDILogistica()
        {
            InitializeComponent();


        }
        public void LogisticaMetActualizarInfoUsuario(string Nombre, string Rol)
        {
            LogisticaLblUsuario.Text = Nombre;
            LogisticaLblRolUsuario.Text = Rol;
            LogisticaLbUsuarioRol.Left = LogisticaLblUsuario.Right + 30;
        }

        private void LogisticaBtnUbicaciones_Click(object sender, EventArgs e)
        {
            CapaVista_Inventario_Logistica.Formas.FrmMantenimientoSucursales frmMantenimientoSucursales = new CapaVista_Inventario_Logistica.Formas.FrmMantenimientoSucursales();
            frmMantenimientoSucursales.ShowDialog();
        }

        private void LogisticaBtnCatalogo_Click(object sender, EventArgs e)
        {
            CapaVista_Inventario_Logistica.Formas.FrmMantenimientoProducto frmMantenimientoProducto = new CapaVista_Inventario_Logistica.Formas.FrmMantenimientoProducto();
            frmMantenimientoProducto.ShowDialog();
        }

       

        private void LogisticaBtnInventario_Click(object sender, EventArgs e)
        {
            CapaVista_Inventario_Logistica.Formas.FrmMantenimientoStock frmMantenimientoStock = new CapaVista_Inventario_Logistica.Formas.FrmMantenimientoStock();
            frmMantenimientoStock.ShowDialog();
        }

        private void LogisticaBtnGestionTransporte_Click(object sender, EventArgs e)
        {
            CapaVista_TransporteInterno_Logistica.Formas.FrmSeleccionTipoTraslado frmnTransporte = new CapaVista_TransporteInterno_Logistica.Formas.FrmSeleccionTipoTraslado();
            frmnTransporte.ShowDialog();

        }

        private void LogisticaBtnTraslado_Click(object sender, EventArgs e)
        {
            CapaVista_Traslados_Logistica.Formas.FrmTraslados frmTraslado = new CapaVista_Traslados_Logistica.Formas.FrmTraslados();
            frmTraslado.ShowDialog();
        }

        private void LogisticaBtnAuditoria_Click(object sender, EventArgs e)
        {
            CapaVista_MuestreoAuditoria_Logistica.FrmAuditoriaInventario frmAuditoriaInventario = new CapaVista_MuestreoAuditoria_Logistica.FrmAuditoriaInventario();
            frmAuditoriaInventario.ShowDialog();

        }

        private void LogisticaBtnCierredeInventario_Click(object sender, EventArgs e)
        {
            CapaVista_CierreInventario.Forms.FrmCierreInventario frmCierreInventario = new CapaVista_CierreInventario.Forms.FrmCierreInventario();
            frmCierreInventario.ShowDialog();
        }
    }
}