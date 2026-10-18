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
 * Propósito: Acceso a recepción de pagos dentro del formulario unificado de CxC.
 */
using System.Windows.Forms;

namespace CapaVista_Ventas
{
    public partial class FrmRecepcionPagosVentas : FrmMovimientosClientesVentas
    {
        public FrmRecepcionPagosVentas()
        {
            InitializeComponent();
            VentasMetMostrarRecepcionPagos();
        }
    }
}

// FIN DEL CÓDIGO — Equipo asignado al formulario | Fecha: 17/10/2026
