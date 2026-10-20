/*
 * INICIO DEL CÓDIGO — Ventas y cuentas por cobrar
 * Asignación: Todos
 * Jose Javier Torres Martínez | Carné: 0901-23-1091
 * Roger Yankhel de Jesús Herrera Alcántara | Carné: 0901-23-2429
 * Natali Sofía Montenegro Portillo | Carné: 0901-23-10017
 * Matthew Andreé Juárez Xoy | Carné: 0901-23-4250
 * Diego Alejandro Cheng Peña | Carné: 0901-22-8091
 * Encargado: Todos
 * Fecha de actualización: 17/10/2026
 * Propósito: Mostrar únicamente el diseño estandarizado de cierre de movimientos.
 */
using System;
using System.Windows.Forms;

namespace CapaVista_CierreMovimientos
{
    public partial class FrmCierreMovimientosVentas : Form
    {
        public FrmCierreMovimientosVentas()
        {
            InitializeComponent();
            VentasCboMesCierre.SelectedIndex = DateTime.Today.Month - 1;
            VentasCboTipoCierre.SelectedIndex = 0;
        }

        private void VentasPeriodoCierre_Changed(object sender, EventArgs e)
        {
            // Sincroniza únicamente los campos del período mostrado en el prototipo.
            bool Anual = VentasCboTipoCierre.Text == "Anual";
            int Anio = (int)VentasNudAnioCierre.Value;
            int Mes = VentasCboMesCierre.SelectedIndex < 0 ? DateTime.Today.Month : VentasCboMesCierre.SelectedIndex + 1;
            DateTime Desde = new DateTime(Anio, Anual ? 1 : Mes, 1);
            DateTime Hasta = Anual ? new DateTime(Anio, 12, 31) : new DateTime(Anio, Mes, DateTime.DaysInMonth(Anio, Mes));
            VentasCboMesCierre.Enabled = !Anual;
            VentasDtpDesdeCierre.Value = Desde;
            VentasDtpHastaCierre.Value = Hasta;
            VentasDtpCorteCierre.Value = Hasta;
            string Unidad = Anual ? "año" : "mes";
            VentasClmRevisionCierreCargos.HeaderText = "Cargos del " + Unidad;
            VentasClmRevisionCierreAbonos.HeaderText = "Abonos del " + Unidad;
            VentasLblTituloCargosCierre.Text = "Cargos del " + Unidad;
            VentasLblTituloAbonosCierre.Text = "Abonos del " + Unidad;
        }
    }
}

// FIN DEL CÓDIGO — Equipo asignado al formulario | Fecha: 17/10/2026
