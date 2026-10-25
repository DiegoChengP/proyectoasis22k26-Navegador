using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaVista_EnlaceB.Formularios;
using CapaVista_MovimientosBancarios.Formularios;
using CapaVista_AutorizacionOrdenes;
using CapaVista_AutorizacionOrdenes.Formularios;
using CapaVista_DisponibilidadDiaria.Formularios;
using CapaVista_TipoCambio.Formularios;
using CapaVista_ConciliacionBancaria.Formularios;
using CapaVista_GeneracionPagos.Formularios;
using CapaVista_CierreBancario.Formularios;


namespace CapaVista_Bancos.Formularios
{
    public partial class FrmMDIBancos : Form
    {
        public FrmMDIBancos()
        {
            InitializeComponent();
        }

        private void BancosBtnBurger_Click(object sender, EventArgs e)
        {
            if (BancosPnlNavegador.Width == 270)
            {
                BancosPnlNavegador.Width = 67;
                BancosPnlDashboard.Location = new Point(200, 52);
                BancosBtnBurger.Location = new Point(220, 13);
                BancosLblUsuario.Location = new Point(285, 14);
                BancosLblUsuarioRol.Location = new Point(524, 14);
            }
            else
            {
                BancosPnlNavegador.Width = 270;
                BancosPnlDashboard.Location = new Point(307, 52);
                BancosBtnBurger.Location = new Point(323, 13);
                BancosLblUsuario.Location = new Point(385, 14);
                BancosLblUsuarioRol.Location = new Point(624, 14);
            }
        }
        private void BancosBtnMovBan_Click(object sender, EventArgs e)
        {
            FrmMovimientosBancarios MovimientosBancarios = new FrmMovimientosBancarios();
            MovimientosBancarios.ShowDialog();
        }
        public void BancosMetActualizarInfoUsuario(string Nombre, string Rol)
        {
            BancosLblUsuario.Text = $"Usuario: {Nombre}";
            BancosLblUsuarioRol.Text = $"Rol: {Rol}";
            BancosLblUsuarioRol.Left = BancosLblUsuario.Right + 30;
        }

        private void BancosBtnAutoCompra_Click(object sender, EventArgs e)
        {
            FrmAutorizacionOrdenes AutorizacionOrdenes = new FrmAutorizacionOrdenes();
            AutorizacionOrdenes.ShowDialog();
        }

        private void BancosBtnEnlace_Click(object sender, EventArgs e)
        {
            FrmEnlaceOtrosModulos Enlace = new FrmEnlaceOtrosModulos();
            Enlace.ShowDialog();
        }

        private void BancosBtnDisp_Click(object sender, EventArgs e)
        {
            FrmDisponibilidadDiaria Enlace = new FrmDisponibilidadDiaria();
            Enlace.ShowDialog();
        }

        private void BancosBtnCambio_Click(object sender, EventArgs e)
        {
            FrmTipoCambio Enlace = new FrmTipoCambio();
            Enlace.ShowDialog();
        }

        private void BancosBtnConBan_Click(object sender, EventArgs e)
        {
            FrmConciliacion Conciliacion = new FrmConciliacion();
            Conciliacion.ShowDialog();
        }

        private void BancosBtnGenPagos_Click(object sender, EventArgs e)
        {
            FrmGeneracionPagos GeneracionPagos = new FrmGeneracionPagos();
            GeneracionPagos.ShowDialog();
        }

        private void BancosBtnCierreBan_Click(object sender, EventArgs e)
        {
            FrmCierreBancario CierreBancario = new FrmCierreBancario();
            CierreBancario.ShowDialog();
        }
    }
}
