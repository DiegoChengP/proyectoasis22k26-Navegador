using System;
using System.Data;
using System.Windows.Forms;
using V = VentasPresentacion.ClsVistaVentas;

namespace CapaVista_Cotizaciones
{
    internal sealed partial class ClsCotizacionesPresentador
    {
        private bool _EditandoCotizacion = true;
        private int _IndiceLinea = -1;
        private void VentasMetBuscar()
        {
            DataTable Datos = _Controlador.Listar(_Vista.VentasTxtCotNumero.Text.Trim());
            DataRow F = Datos.Rows.Count == 1 ? Datos.Rows[0] : V.Seleccionar(_Vista, "Consultar cotizaciones", Datos);
            if (F != null) VentasMetConsultar(Convert.ToString(F["Numero"]));
        }
        private void VentasMetModificar()
        {
            if (_IdCotizacion == 0) VentasMetBuscar();
            _Controlador.ValidarCaptura(_IdCotizacion > 0, "Seleccione una cotizacion guardada.");
            _Controlador.ValidarEdicion(_IdCotizacion, _EstadoGuardado, _Vista.VentasDtpCotVigencia.Value);
            _EditandoCotizacion = true; VentasMetPrecios(); VentasMetActualizarEdicion(); _Vista.VentasCboCotProducto.Focus();
        }
        private void VentasMetAnular()
        {
            if (_IdCotizacion == 0) VentasMetBuscar();
            _Controlador.ValidarCaptura(_IdCotizacion > 0, "Consulte la cotizacion a anular.");
            if (MessageBox.Show(_Vista, "¿Desea anular la cotizacion " + _NumeroGuardado + "?", "Anular cotizacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            _Controlador.Anular(_IdCotizacion, VentasIdUsuario); VentasMetConsultar(_NumeroGuardado);
            V.Informar(_Vista, "Cotizacion anulada.");
        }
        private void VentasMetEditarLinea()
        {
            _Controlador.ValidarEdicion(_IdCotizacion, _EstadoGuardado, _Vista.VentasDtpCotVigencia.Value);
            if (!_EditandoCotizacion) VentasMetModificar();
            if (_Vista.VentasDgvCotProductos.CurrentRow == null) return;
            _IndiceLinea = _Vista.VentasDgvCotProductos.CurrentRow.Index;
            DataRow F = _Lineas.Rows[_IndiceLinea];
            _Vista.VentasCboCotProducto.SelectedValue = F["idProducto"];
            _Vista.VentasNudCotCantidad.Value = Convert.ToDecimal(F["Cantidad"]); _Vista.VentasNudCotDescuento.Value = Convert.ToDecimal(F["Descuento"]);
            _Vista.VentasBtnCotAgregar.BackgroundImage = Properties.Resources.BotonOriginalGuardarVentas;
            _Vista.VentasTtpAcciones.SetToolTip(_Vista.VentasBtnCotAgregar, "Guardar cambios del producto seleccionado");
        }
        private void VentasMetRestablecerLinea()
        {
            _IndiceLinea = -1; _Vista.VentasBtnCotAgregar.BackgroundImage = Properties.Resources.BotonOriginalIngresarVentas;
            _Vista.VentasTtpAcciones.SetToolTip(_Vista.VentasBtnCotAgregar, "Agregar producto con precio de la lista del cliente");
        }
        private void VentasMetRefrescar()
        {
            if (_IdCotizacion > 0) VentasMetConsultar(_NumeroGuardado);
            else VentasMetCargar();
        }
    }
}
