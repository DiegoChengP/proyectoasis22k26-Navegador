/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 21/10/2026
 * ==================================================================
 * Propósito :
 *  Mantenimiento del catálogo de países (tblPais).
 *  Usa el control Navegador: la grilla, los botones y los permisos los maneja el
 *  componente con los IDs de módulo y aplicación registrados en Seguridad.
 * ===================================================================
*/

using System.Drawing;
using System.Windows.Forms;
using CapaControlador_Contabilidad;

namespace CapaVista_Contabilidad
{
    // Ventana de mantenimiento con el control Navegador.
    public partial class FrmMantenimientoPais : Form
    {
        // Constructor para el diseñador y las pruebas: todos los permisos.
        public FrmMantenimientoPais() : this(ClsPermisosPantallaContabilidad.Total)
        {
        }

        // Abre la ventana con los IDs y permisos que dio Seguridad.
        public FrmMantenimientoPais(ClsPermisosPantallaContabilidad Permisos)
        {
            InitializeComponent();
            ClsPresentacionContabilidad.ContabilidadMetPrepararVentana(this, "Mantenimiento de países", new Size(1180, 688));
            ContabilidadUsrNavegador.NavegadorMetConfigurar("tblpais", Permisos.IdModulo, Permisos.IdAplicacion);
        }
    }
}
