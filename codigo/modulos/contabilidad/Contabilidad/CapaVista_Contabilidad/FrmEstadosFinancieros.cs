/* Área: Contabilidad. Autor: Gabriel André Guillén Pocón. Carné: 0901-23-1998.
 * Fecha: 14/10/2026. Propósito: informe de ejemplo de solo lectura sin conexión a base. */
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaControlador_Contabilidad;
using CapaVista_Contabilidad.Componentes;
using Diseno = CapaVista_Contabilidad.Componentes.ClsDisenoContabilidad;

namespace CapaVista_Contabilidad
{
    // Ventana del estado de resultados.
    public partial class FrmEstadosFinancieros : Form
    {
        // Controlador de estados financieros.
        private readonly ClsControladorEstadosFinancierosContabilidad _Controlador = new ClsControladorEstadosFinancierosContabilidad();
        // Resultado del informe.
        private DataTable _Resultado;
        // Paginación del informe.
        private readonly ClsPaginacionContabilidad _Paginacion;
        // Errores por campo.
        private readonly ClsValidacionContabilidad _Validacion = new ClsValidacionContabilidad();

        // Constructor para el diseñador y las pruebas: todos los permisos.
        public FrmEstadosFinancieros() : this(ClsPermisosPantallaContabilidad.Total)
        {
        }

        // Abre la ventana con los permisos que dio Seguridad.
        public FrmEstadosFinancieros(ClsPermisosPantallaContabilidad Permisos)
        {
            InitializeComponent();
            ContabilidadMetAplicarPermisos(Permisos);
            ClsPresentacionContabilidad.ContabilidadMetPrepararVentana(this, "Estados financieros", new Size(1180, 688));
            _Paginacion = new ClsPaginacionContabilidad(ContabilidadDgvResultado);
            _Validacion.ContabilidadMetObligatorio(ContabilidadDtpDesde);
            _Validacion.ContabilidadMetObligatorio(ContabilidadDtpHasta);
            Disposed += (Remitente, Argumentos) => { _Paginacion.Dispose(); _Validacion.Dispose(); };
            ContabilidadDtpDesde.Value = new DateTime(2026, 10, 1);
            ContabilidadDtpHasta.Value = new DateTime(2026, 10, 31);
            ContabilidadBtnConsultar.Click += (Remitente, Argumentos) => ContabilidadMetConsultar();
            ContabilidadDtpDesde.ValueChanged += (Remitente, Argumentos) => ContabilidadMetInvalidar();
            ContabilidadDtpHasta.ValueChanged += (Remitente, Argumentos) => ContabilidadMetInvalidar();
            ContabilidadMetConsultar();
        }

        // Deshabilita los botones que el usuario no puede usar.
        private void ContabilidadMetAplicarPermisos(ClsPermisosPantallaContabilidad Permisos)
        {
            // Solo consulta: el acceso ya lo valida el MDI.
        }

        // Retira el informe cuando cambian los filtros.
        private void ContabilidadMetInvalidar()
        {
            _Paginacion.ContabilidadMetLimpiar();
            if (_Resultado != null) { _Resultado.Dispose(); _Resultado = null; }
            ContabilidadLblPeriodo.Text = "Filtros modificados · Pulse Ver resultados para actualizar el informe.";
            ContabilidadLblEstado.Text = string.Empty;
            ContabilidadLblResumen.Text = string.Empty;
        }

        // Genera el informe del rango.
        private void ContabilidadMetConsultar()
        {
            string ErrorFechas = _Controlador.ContabilidadFuncErrorFechas(ContabilidadDtpDesde.Value, ContabilidadDtpHasta.Value);
            if (!_Validacion.ContabilidadFuncValidar(ContabilidadDtpHasta, ErrorFechas))
            {
                ContabilidadMetInvalidar();
                ContabilidadLblEstado.Text = ErrorFechas;
                return;
            }
            DataTable Anterior = _Resultado;
            try
            {
                _Resultado = _Controlador.ContabilidadFuncResultados(ContabilidadDtpDesde.Value, ContabilidadDtpHasta.Value);
            }
            catch (Exception Excepcion)
            {
                // Sin base de datos no hay movimientos que mostrar.
                _Resultado = null;
                ContabilidadMetInvalidar();
                ClsDialogosContabilidad.ContabilidadMetError(this, "No se pudo consultar el estado de resultados: " + Excepcion.Message);
                return;
            }
            _Paginacion.ContabilidadMetCargar(_Resultado);
            if (Anterior != null) Anterior.Dispose();
            ContabilidadLblPeriodo.Text = "Estado de resultados del " + ContabilidadDtpDesde.Value.ToString("dd/MM/yyyy")
                + " al " + ContabilidadDtpHasta.Value.ToString("dd/MM/yyyy");
            decimal Ingresos = _Controlador.ContabilidadFuncTotalRubro(_Resultado, "INGRESOS");
            decimal Costos = _Controlador.ContabilidadFuncTotalRubro(_Resultado, "COSTOS");
            decimal Gastos = _Controlador.ContabilidadFuncTotalRubro(_Resultado, "GASTOS");
            ContabilidadLblEstado.Text = _Resultado.Rows.Count == 0 ? "Sin movimientos en el rango seleccionado."
                : "Ingresos: Haber − Debe. Costos y gastos: Debe − Haber.\r\nSe excluyen borradores, anuladas y cuentas de balance.";
            ContabilidadLblResumen.Text = "Ingresos: " + Diseno.ContabilidadFuncMoneda(Ingresos) + "     Costos: " + Diseno.ContabilidadFuncMoneda(Costos)
                + "     Gastos: " + Diseno.ContabilidadFuncMoneda(Gastos) + "     Resultado: " + Diseno.ContabilidadFuncMoneda(_Controlador.ContabilidadFuncResultadoNeto(_Resultado));
        }
    }
}
