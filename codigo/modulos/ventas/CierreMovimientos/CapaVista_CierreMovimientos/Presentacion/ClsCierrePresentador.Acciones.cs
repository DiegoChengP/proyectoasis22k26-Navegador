using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using V = VentasPresentacion.ClsVistaVentas;

namespace CapaVista_CierreMovimientos
{
    internal sealed partial class ClsCierrePresentador
    {
        private int _IdBorrador;
        private bool _EditandoBorrador = true;
        private void VentasMetConsultarCierre()
        {
            if (_Vista.VentasTbcCierre.SelectedTab != _Vista.VentasTbpPreparar) { VentasMetRevisar(); return; }
            DataRow F = V.Seleccionar(_Vista, "Consultar cierres y borradores", _Controlador.Listar(Moneda));
            if (F == null) return;
            _Cargando = true;
            try
            {
                _Vista.VentasCboMonedaCierre.SelectedValue = F["idMoneda"];
                DateTime Desde = Convert.ToDateTime(F["Desde"]);
                _Vista.VentasCboTipoCierre.SelectedIndex = Convert.ToString(F["Tipo"]) == "ANUAL" ? 1 : 0;
                _Vista.VentasNudAnioCierre.Value = Desde.Year; _Vista.VentasCboMesCierre.SelectedIndex = Desde.Month - 1;
                VentasMetPeriodoCambio();
                _IdBorrador = Convert.ToString(F["Estado"]) == "BORRADOR" ? Convert.ToInt32(F["Id"]) : 0;
                _Vista.VentasTxtObservacionesCierre.Text = Convert.ToString(F["Observaciones"]); _EditandoBorrador = false;
            }
            finally { _Cargando = false; }
            if (Convert.ToString(F["Estado"]) == "ANULADO") { VentasMetLimpiarRevision(); _Vista.VentasTxtEstadoCierre.Text = "Anulado: " + F["Numero"]; }
            else VentasMetRevisar();
            VentasMetActualizarAccionesCierre();
        }
        private void VentasMetModificarBorrador()
        {
            if (_IdBorrador == 0) VentasMetConsultarCierre();
            _Controlador.ValidarCaptura(_IdBorrador > 0, "Seleccione un cierre en borrador. Un cierre confirmado conserva su historico.");
            _EditandoBorrador = true; VentasMetActualizarAccionesCierre(); _Vista.VentasTxtObservacionesCierre.Focus();
        }
        private void VentasMetGuardarBorrador()
        {
            VentasMetPersistirBorrador(); VentasMetRevisar();
            V.Informar(_Vista, "Borrador guardado. Revise los saldos antes de confirmar el cierre.");
        }
        private void VentasMetPersistirBorrador()
        {
            string Numero = _Controlador.GuardarBorrador(_IdBorrador, Moneda, VentasIdUsuario, Tipo,
                _Vista.VentasDtpDesdeCierre.Value, _Vista.VentasDtpHastaCierre.Value, _Vista.VentasTxtObservacionesCierre.Text);
            _IdBorrador = Convert.ToInt32(_Controlador.Listar(Moneda).AsEnumerable().Single(F => Convert.ToString(F["Numero"]) == Numero)["Id"]);
            _EditandoBorrador = false; VentasMetActualizarAccionesCierre();
        }
        private void VentasMetAnularBorrador()
        {
            if (_IdBorrador == 0) VentasMetConsultarCierre();
            _Controlador.ValidarCaptura(_IdBorrador > 0, "Seleccione un borrador para anular. Los cierres confirmados se conservan.");
            string Motivo = V.Motivo(_Vista, "Anular borrador de cierre"); if (Motivo == null) return;
            _Controlador.EliminarBorrador(_IdBorrador, VentasIdUsuario, Motivo);
            _IdBorrador = 0; VentasMetNuevo(); V.Informar(_Vista, "Borrador anulado.");
        }
        private void VentasMetActualizarAccionesCierre()
        {
            bool Preparar = _Vista.VentasTbcCierre.SelectedTab == _Vista.VentasTbpPreparar;
            _Vista.VentasBtnGuardarBorrador.Enabled = Preparar && Moneda > 0 && _IdCierre == 0 && _EditandoBorrador;
            _Vista.VentasBtnModificar.Enabled = _Vista.VentasBtnEliminar.Enabled = Preparar && _IdBorrador > 0 && _IdCierre == 0;
            _Vista.VentasTxtObservacionesCierre.ReadOnly = !_EditandoBorrador || _IdCierre > 0;
        }
        private void VentasMetImprimirCierre()
        {
            DataTable Datos=_Revision; string Titulo="Cierre "+_NumeroCierre;
            if(_Vista.VentasTbcCierre.SelectedTab == _Vista.VentasTbpHistorial)
            { Datos=_Controlador.Historial(Moneda,V.Id(_Vista.VentasCboClienteHistorial),_Vista.VentasTxtPeriodoHistorial.Text); Titulo="Historial de cierres"; }
            else if(_Vista.VentasTbcCierre.SelectedTab == _Vista.VentasTbpContabilidad) { Datos=_Partida; Titulo="Partida del cierre "+_NumeroCierre; }
            V.Imprimir(_Vista,Titulo,_Vista.VentasCboMonedaCierre.Text,Datos);
        }
    }
}
