/* Área: Contabilidad. Autor: Gabriel André Guillén Pocón. Carné: 0901-23-1998.
 * Fecha: 14/10/2026. Propósito: revisión de cierre sin cambiar períodos. Responsable: Oskar. */
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaControlador_Contabilidad;
using CapaVista_Contabilidad.Componentes;
using Diseno = CapaVista_Contabilidad.Componentes.ClsDisenoContabilidad;

namespace CapaVista_Contabilidad
{
    // Ventana del cierre contable.
    public partial class FrmCierreContable : Form
    {
        // Controlador del cierre.
        private readonly ClsControladorCierreContabilidad _Controlador = new ClsControladorCierreContabilidad();
        // Pólizas del período.
        private DataTable _Polizas;
        // Paginación de las pólizas.
        private readonly ClsPaginacionContabilidad _Paginacion;

        // Constructor para el diseñador y las pruebas: todos los permisos.
        public FrmCierreContable() : this(ClsPermisosPantallaContabilidad.Total)
        {
        }

        // Abre la ventana con los permisos que dio Seguridad.
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

        // Deshabilita los botones que el usuario no puede usar.
        private void ContabilidadMetAplicarPermisos(ClsPermisosPantallaContabilidad Permisos)
        {
            ClsPermisosMenuContabilidad.ContabilidadMetRestringir(ContabilidadBtnSimular, Permisos.PuedeEditar);
        }

        // Revisa el período y muestra el diagnóstico.
        private void ContabilidadMetRevisar()
        {
            DataTable Anterior = _Polizas;
            try
            {
                _Polizas = _Controlador.ContabilidadFuncPolizas(ContabilidadDtpPeriodo.Value);
            }
            catch (Exception Excepcion)
            {
                // Sin base de datos no hay pólizas que revisar.
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
