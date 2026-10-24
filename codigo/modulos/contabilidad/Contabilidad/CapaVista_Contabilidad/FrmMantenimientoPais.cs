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
using CapaVista_Contabilidad.Properties;

namespace CapaVista_Contabilidad
{
    public partial class FrmMantenimientoPais : Form
    {
        public FrmMantenimientoPais() : this(ClsPermisosPantallaContabilidad.Total)
        {
        }

        public FrmMantenimientoPais(ClsPermisosPantallaContabilidad Permisos)
        {
            InitializeComponent();
            ClsPresentacionContabilidad.ContabilidadMetPrepararVentana(this, "Mantenimiento de países", new Size(1180, 688));
            ContabilidadUsrNavegador.NavegadorMetConfigurar("tblpais", Permisos.IdModulo, Permisos.IdAplicacion);
        }
    }
}
