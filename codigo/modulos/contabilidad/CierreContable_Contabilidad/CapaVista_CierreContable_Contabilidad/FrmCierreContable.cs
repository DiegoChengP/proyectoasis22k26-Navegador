/* Área: Contabilidad. Autor: Donald Osorio. Carné: 0901-23-17982.
 * Fecha: 14/10/2026. Propósito: revisión de cierre sin cambiar períodos. Responsable: Donald. */
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Diseno = CapaVista_CierreContable_Contabilidad.Componentes.ClsDisenoContabilidad;
using CapaControlador_CierreContable_Contabilidad;
using CapaControlador_Contabilidad;
using CapaVista_CierreContable_Contabilidad.Componentes;
using CapaVista_CierreContable_Contabilidad.Properties;

namespace CapaVista_CierreContable_Contabilidad
{
    public partial class FrmCierreContable : Form
    {
        private readonly ClsControladorCierreContabilidad _Controlador = new ClsControladorCierreContabilidad();
        private DataTable _Polizas;
        private readonly ClsPaginacionContabilidad _Paginacion;

        public FrmCierreContable() : this(ClsPermisosPantallaContabilidad.Total)
        {
        }

        public FrmCierreContable(ClsPermisosPantallaContabilidad Permisos)
        {
            InitializeComponent();
            ContabilidadMetAplicarPermisos(Permisos);
            ClsPresentacionContabilidad.ContabilidadMetPrepararVentana(this, "Cierre contable", new Size(1180, 688));
            ContabilidadDgvPolizas.RowTemplate.Height = 28;
            ContabilidadDgvPolizas.ColumnHeadersHeight = 34;
            _Paginacion = new ClsPaginacionContabilidad(ContabilidadDgvPolizas);
            Disposed += (Remitente, Argumentos) => _Paginacion.Dispose();
            ContabilidadDtpPeriodo.Value = new DateTime(2026, 10, 1);
            ContabilidadDtpPeriodo.ValueChanged += (Remitente, Argumentos) => ContabilidadMetRevisar();
            ContabilidadBtnRevisar.Click += (Remitente, Argumentos) => ContabilidadMetRevisar();
            ContabilidadBtnSimular.Click += (Remitente, Argumentos) =>
            {
                if (ClsDialogosContabilidad.ContabilidadFuncConfirmar(this, "¿Desea continuar con el cierre del período seleccionado?", "Sí, continuar"))
                    Diseno.ContabilidadMetAviso(this, _Controlador.ContabilidadFuncValidarCierre(_Polizas), "Período validado para cierre.");
            };
            ContabilidadMetRevisar();
        }

        private void ContabilidadMetAplicarPermisos(ClsPermisosPantallaContabilidad Permisos)
        {
            ClsPermisosMenuContabilidad.ContabilidadMetRestringir(ContabilidadBtnSimular, Permisos.PuedeEditar);
        }

        private void ContabilidadMetRevisar()
        {
            DataTable Anterior = _Polizas;
            try
            {
                _Polizas = _Controlador.ContabilidadFuncPolizas(ContabilidadDtpPeriodo.Value);
            }
            catch (Exception Excepcion)
            {
                _Polizas = new DataTable();
                ClsDialogosContabilidad.ContabilidadMetError(this, "No se pudieron consultar las pólizas: " + Excepcion.Message);
            }
            _Paginacion.ContabilidadMetCargar(_Polizas);
            if (Anterior != null) Anterior.Dispose();
            string Error = _Controlador.ContabilidadFuncValidarCierre(_Polizas);
            ContabilidadLblValidacion.Text = string.IsNullOrEmpty(Error)
                ? "Revisión completada. El período está listo para el cierre."
                : "Cierre pendiente: " + Error;
            ContabilidadLblValidacion.ForeColor = string.IsNullOrEmpty(Error) ? Color.FromArgb(70, 104, 125) : Color.FromArgb(197, 143, 62);
            ContabilidadBtnSimular.Enabled = string.IsNullOrEmpty(Error);
            ClsResumenCierreContabilidad Resumen = _Controlador.ContabilidadFuncResumenCierre(_Polizas);
            ContabilidadLblResumen.Text = "Aplicadas: " + Resumen.Aplicadas + "     Borradores: " + Resumen.Borradores
                + "     Debe aplicado: " + Diseno.ContabilidadFuncMoneda(Resumen.Debe) + "     Haber aplicado: " + Diseno.ContabilidadFuncMoneda(Resumen.Haber);
        }
    }
}
