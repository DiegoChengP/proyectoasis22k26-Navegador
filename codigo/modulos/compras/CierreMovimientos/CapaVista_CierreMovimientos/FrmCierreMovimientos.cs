using System;
using System.Windows.Forms;
using CapaControlador_CierreMovimientos;

namespace CapaVista_CierreMovimientos
{

    public partial class FrmCierreMovimientos : System.Windows.Forms.Form
    {
        private readonly CapaControlador_CierreMovimientos.ClsCierreMovimientos _Controlador = new CapaControlador_CierreMovimientos.ClsCierreMovimientos();

        public FrmCierreMovimientos()
        {
            InitializeComponent();
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasMetFijarTamanoFormulario(this);
            ComprasProcConfigurarTablas();
            Load += ComprasMetCargar;
            ComprasBtnValidar.Click += ComprasMetValidar;
            ComprasBtnEjecutarCierre.Click += ComprasMetEjecutarCierre;
            ComprasBtnImprimir.Click += ComprasMetImprimir;
            ComprasBtnAyuda.Click += ComprasMetAyuda;
            ComprasBtnSalir.Click += ComprasMetSalir;
        }

        private void ComprasProcConfigurarTablas()
        {
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasProcAgregarColumna(ComprasUsrValidaciones, "cmp_estado", "Estado", 25, false);
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasProcAgregarColumna(ComprasUsrValidaciones, "cmp_validacion", "Validación", 45, false);
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasProcAgregarColumna(ComprasUsrValidaciones, "cmp_origen", "Origen", 30, false);

            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasProcAgregarColumna(ComprasUsrSaldos, "cmp_proveedor", "Proveedor", 32, false);
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasProcAgregarColumna(ComprasUsrSaldos, "cmp_saldo_inicial", "Saldo inicial", 17, true);
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasProcAgregarColumna(ComprasUsrSaldos, "cmp_total_cargos", "Cargos", 17, true);
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasProcAgregarColumna(ComprasUsrSaldos, "cmp_total_abonos", "Abonos", 17, true);
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasProcAgregarColumna(ComprasUsrSaldos, "cmp_saldo_final", "Saldo final", 17, true);
        }

        private void ComprasMetCargar(object Remitente, EventArgs Evento)
        {
            ComprasUsrPeriodo.DisplayMember = "cmp_descripcion";
            ComprasUsrPeriodo.ValueMember = "cmp_id_periodo";
            ComprasUsrPeriodo.DataSource = _Controlador.ComprasFuncObtenerPeriodosCerrables();

            ComprasBtnEjecutarCierre.Enabled = false;
            ComprasBtnImprimir.Enabled = false;
        }

        private void ComprasMetValidar(object Remitente, EventArgs Evento)
        {
            if (ComprasUsrPeriodo.SelectedValue == null)
            {
                MessageBox.Show("Seleccione el período a cerrar.", "Cierre de Movimientos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int IdPeriodo = Convert.ToInt32(ComprasUsrPeriodo.SelectedValue);
            ClsResumenCierre Resumen = _Controlador.ComprasFuncPrepararCierre(IdPeriodo);

            ComprasUsrValidaciones.DataSource = Resumen.Validaciones;
            ComprasUsrSaldos.DataSource = Resumen.SaldosProveedor;
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasProcMostrarMonto(ComprasUsrTotalCargos, Resumen.TotalCargos);
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasProcMostrarMonto(ComprasUsrTotalAbonos, Resumen.TotalAbonos);
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasProcMostrarMonto(ComprasUsrSaldoFinal, Resumen.SaldoFinal);

            ComprasBtnEjecutarCierre.Enabled = Resumen.PuedeCerrar;
            ComprasBtnImprimir.Enabled = true;
        }

        private void ComprasMetEjecutarCierre(object Remitente, EventArgs Evento)
        {
            DialogResult Respuesta = MessageBox.Show(
                "El cierre es definitivo: los movimientos del período quedarán bloqueados.\n¿Desea continuar?",
                "Confirmar cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (Respuesta != DialogResult.Yes) return;

            int IdPeriodo = Convert.ToInt32(ComprasUsrPeriodo.SelectedValue);
            string Mensaje = _Controlador.ComprasFuncEjecutarCierre(IdPeriodo);
            MessageBox.Show(Mensaje, "Cierre de Movimientos", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ComprasBtnEjecutarCierre.Enabled = false;
        }

        private void ComprasMetImprimir(object Remitente, EventArgs Evento)
        {
            MessageBox.Show("El reporte de cierre se conectará con el componente Reporteador.",
                "Imprimir reporte", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ComprasMetAyuda(object Remitente, EventArgs Evento)
        {
            MessageBox.Show("La ayuda del módulo se integrará en la siguiente fase.",
                "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void ComprasMetSalir(object Remitente, EventArgs Evento)
        {
            Close();
        }
    }
}
