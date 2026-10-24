/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 22/10/2026
 * ==================================================================
 * Propósito :
 *  MDI del prototipo no funcional de Contabilidad. Solo diseño: los botones
 *  del menú abren las cinco pantallas y el botón de menú contrae el panel lateral.
 * ===================================================================
*/

using System;
using System.Windows.Forms;

namespace CapaVista_Contabilidad
{
    // Ventana principal del módulo.
    public partial class FrmMDIContabilidad : Form
    {
        // Ancho del menú expandido y contraído, y margen del panel lateral.
        private const int _AnchoMenuExpandido = 270;
        private const int _AnchoMenuContraido = 64;
        private const int _MargenMenu = 32;

        // Crea los controles del diseño.
        public FrmMDIContabilidad()
        {
            InitializeComponent();
        }

        // Contrae o expande el menú lateral.
        private void ContabilidadMetAlternarMenu(object Remitente, EventArgs Evento)
        {
            bool Expandido = ContabilidadPnlNavegador.Width == _AnchoMenuExpandido;
            int NuevoAncho = Expandido ? _AnchoMenuContraido : _AnchoMenuExpandido;
            ContabilidadPnlNavegador.Width = NuevoAncho;
            ContabilidadPnlLateral.Width = NuevoAncho + _MargenMenu;
        }

        // Abre Pólizas locales.
        private void ContabilidadMetAbrirPolizasLocales(object Remitente, EventArgs Evento)
        {
            using (FrmPolizasLocales Formulario = new FrmPolizasLocales())
            {
                Formulario.ShowDialog(this);
            }
        }

        // Abre Presupuestos.
        private void ContabilidadMetAbrirPresupuestos(object Remitente, EventArgs Evento)
        {
            using (FrmPresupuestos Formulario = new FrmPresupuestos())
            {
                Formulario.ShowDialog(this);
            }
        }

        // Abre Activos fijos.
        private void ContabilidadMetAbrirActivosFijos(object Remitente, EventArgs Evento)
        {
            using (FrmActivosFijos Formulario = new FrmActivosFijos())
            {
                Formulario.ShowDialog(this);
            }
        }

        // Abre Cierre contable.
        private void ContabilidadMetAbrirCierreContable(object Remitente, EventArgs Evento)
        {
            using (FrmCierreContable Formulario = new FrmCierreContable())
            {
                Formulario.ShowDialog(this);
            }
        }

        // Abre Estados financieros.
        private void ContabilidadMetAbrirEstadosFinancieros(object Remitente, EventArgs Evento)
        {
            using (FrmEstadosFinancieros Formulario = new FrmEstadosFinancieros())
            {
                Formulario.ShowDialog(this);
            }
        }
    }
}
