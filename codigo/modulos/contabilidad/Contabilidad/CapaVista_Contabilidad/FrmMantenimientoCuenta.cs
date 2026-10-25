/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Donald Osorio
 * Carné : 0901-23-17982
 * Fecha : 21/10/2026
 * ==================================================================
 * Propósito :
 *  Mantenimiento del catálogo de cuentas contables (tblCuenta).
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
    public partial class FrmMantenimientoCuenta : Form
    {
        public FrmMantenimientoCuenta() : this(ClsPermisosPantallaContabilidad.Total)
        {
        }

        public FrmMantenimientoCuenta(ClsPermisosPantallaContabilidad Permisos)
        {
            InitializeComponent();
            ClsPresentacionContabilidad.ContabilidadMetPrepararVentana(this, "Mantenimiento de cuentas", new Size(1180, 688));
            ContabilidadUsrNavegador.NavegadorMetConfigurar("tblcuenta", Permisos.IdModulo, Permisos.IdAplicacion);
        }
    }
}
