using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using V = VentasPresentacion.ClsVistaVentas;

namespace CapaVista_MovimientosClientes
{
    internal sealed partial class ClsMovimientosPresentador
    {
        private DataRow _DocumentoActual;
        private DataRow _ReciboActual;
        private bool _EditandoDocumento;
        private bool _EditandoRecibo;

        private void VentasMetConsultarCaptura()
        {
            if (_Vista.VentasTbcMovimientos.SelectedTab == _Vista.VentasTbpDocumento)
            {
                DataTable Datos = _Controlador.ListarDocumentos(Cliente, Moneda);
                string Numero = _Vista.VentasTxtNumeroDocumento.Text.Trim();
                DataRow F = Datos.AsEnumerable().FirstOrDefault(R => Convert.ToString(R["Documento"]) == Numero || Convert.ToString(R["Documento"]) == "CXC-" + Numero);
                if (F == null || Numero.Length == 0) F = V.Seleccionar(_Vista, "Consultar documentos de CxC", Datos);
                if (F != null) VentasMetCargarDocumento(Convert.ToInt32(F["Id"]), Convert.ToString(F["Clase"]));
            }
            else if (_Vista.VentasTbcMovimientos.SelectedTab == _Vista.VentasTbpRecibos)
            {
                DataTable Datos = _Controlador.ListarRecibos(Cliente, Moneda);
                string Numero = _Vista.VentasTxtRecibo.Text.Trim();
                DataRow F = Datos.AsEnumerable().FirstOrDefault(R => Convert.ToString(R["Recibo"]) == Numero);
                if (F == null || Numero.Length == 0) F = V.Seleccionar(_Vista, "Consultar recibos", Datos);
                if (F != null) VentasMetCargarRecibo(Convert.ToInt32(F["Id"]));
            }
            else VentasMetConsultarActual();
        }

        private void VentasMetCargarDocumento(int Id, string Clase)
        {
            DataRow F = _Controlador.LeerDocumento(Id, Clase);
            _Cargando = true;
            try
            {
                _Vista.VentasTbcMovimientos.SelectedTab = _Vista.VentasTbpDocumento;
                _Vista.VentasCboCliente.SelectedValue = F["idCliente"]; _Vista.VentasCboMoneda.SelectedValue = F["idMoneda"];
                _Vista.VentasCboEstablecimientoDocumento.SelectedValue = F["idEstablecimiento"];
                _Vista.VentasCboTipoDocumento.SelectedIndex = Convert.ToInt32(F["Tipo"]);
                _Vista.VentasTxtNumeroDocumento.Text = Convert.ToString(F["Numero"]);
                _Vista.VentasDtpFechaDocumento.Value = Convert.ToDateTime(F["Fecha"]); _Vista.VentasDtpVencimientoDocumento.Value = Convert.ToDateTime(F["Vencimiento"]);
                _Vista.VentasNudImporteDocumento.Value = Convert.ToDecimal(F["Importe"]);
                _Vista.VentasTxtFacturaReferencia.Text = Convert.ToString(F["Referencia"]); _Vista.VentasTxtDescripcionDocumento.Text = Convert.ToString(F["Descripcion"]);
                _Vista.VentasCboEstadoDocumento.Items.Clear(); _Vista.VentasCboEstadoDocumento.Items.Add(Convert.ToString(F["Estado"])); _Vista.VentasCboEstadoDocumento.SelectedIndex = 0;
                DataRow ClienteActual=V.Fila(_Vista.VentasCboCliente);
                _Vista.VentasTxtCondicion.Text=ClienteActual == null ? "" : ClienteActual["diasCredito"]+" dias; limite "+Convert.ToDecimal(ClienteActual["limiteCredito"]).ToString("N2",V.Cultura);
                _DocumentoActual = F; _EditandoDocumento = false; _Guardados[1] = true;
            }
            finally { _Cargando = false; }
            VentasMetTipoDocumentoCambio(); VentasMetRefrescarBotones();
            _Vista.VentasLblEtapaBancos.Text = "Documento consultado. Modificar permite cambiar la descripcion y el vencimiento de una factura.";
        }

        private void VentasMetCargarRecibo(int Id)
        {
            DataSet Datos = _Controlador.LeerRecibo(Id); DataRow F = Datos.Tables[0].Rows[0];
            _Cargando = true;
            try
            {
                _Vista.VentasTbcMovimientos.SelectedTab = _Vista.VentasTbpRecibos;
                _Vista.VentasCboCliente.SelectedValue = F["idCliente"]; _Vista.VentasCboMoneda.SelectedValue = F["idMoneda"];
                _Vista.VentasTxtRecibo.Text = Convert.ToString(F["numeroRecibo"]); _Vista.VentasDtpFechaPago.Value = Convert.ToDateTime(F["fechaPago"]);
                _Vista.VentasNudMontoPago.Value = Convert.ToDecimal(F["montoPago"]); _Vista.VentasNudTipoCambioPago.Value = Convert.ToDecimal(F["tipoCambio"]);
                string[] Formas = { "EFECTIVO", "TRANSFERENCIA", "DEPOSITO", "CHEQUE", "TARJETA" };
                _Vista.VentasCboFormaPago.SelectedIndex = Array.IndexOf(Formas, Convert.ToString(F["formaPago"]));
                _Vista.VentasTxtReferenciaPago.Text = Convert.ToString(F["referenciaPago"]); _Vista.VentasTxtObservacionesPago.Text = Convert.ToString(F["observaciones"]);
                _Vista.VentasDgvDocumentos.Rows.Clear();
                foreach (DataRow A in Datos.Tables[1].Rows)
                {
                    int I = _Vista.VentasDgvDocumentos.Rows.Add(true, A["Documento"], A["Fecha"], A["Vencimiento"], A["Moneda"], A["Saldo"], A["Importe"]);
                    _Vista.VentasDgvDocumentos.Rows[I].Tag = A;
                }
                DataRow ClienteActual=V.Fila(_Vista.VentasCboCliente);
                _Vista.VentasTxtCondicion.Text=ClienteActual == null ? "" : ClienteActual["diasCredito"]+" dias; limite "+Convert.ToDecimal(ClienteActual["limiteCredito"]).ToString("N2",V.Cultura);
                _ReciboActual = F; _EditandoRecibo = false; _Guardados[2] = true;
            }
            finally { _Cargando = false; }
            VentasMetTotalesPago(); VentasMetRefrescarBotones();
            _Vista.VentasLblEtapaBancos.Text = "Recibo " + F["numeroRecibo"] + " / " + F["estadoPago"] + ". Modificar permite actualizar sus observaciones.";
        }

        private void VentasMetAbrirSeleccionActual()
        {
            if (_Vista.VentasTbcMovimientos.SelectedTab == _Vista.VentasTbpCuenta)
            {
                DataRow F = _Vista.VentasDgvMovimientos.CurrentRow == null ? null : _Vista.VentasDgvMovimientos.CurrentRow.Tag as DataRow;
                _Controlador.ValidarCaptura(F != null, "Seleccione un movimiento de la cuenta.");
                DataRow R = _Controlador.LeerMovimiento(Convert.ToInt32(F["idMovimiento"]));
                if (Convert.ToString(R["Clase"]) == "RECIBO") VentasMetCargarRecibo(Convert.ToInt32(R["Id"]));
                else VentasMetCargarDocumento(Convert.ToInt32(R["Id"]), Convert.ToString(R["Clase"]));
            }
            else if (_Vista.VentasTbcMovimientos.SelectedTab == _Vista.VentasTbpConsultaRecibos)
            {
                DataRow F = _Vista.VentasDgvConsultaRecibos.CurrentRow == null ? null : _Vista.VentasDgvConsultaRecibos.CurrentRow.Tag as DataRow;
                _Controlador.ValidarCaptura(F != null, "Seleccione un recibo."); VentasMetCargarRecibo(Convert.ToInt32(F["idPago"]));
            }
            else if ((_Vista.VentasTbcMovimientos.SelectedTab == _Vista.VentasTbpDocumento && _DocumentoActual == null) ||
                     (_Vista.VentasTbcMovimientos.SelectedTab == _Vista.VentasTbpRecibos && _ReciboActual == null)) VentasMetConsultarCaptura();
        }

        private void VentasMetModificar()
        {
            VentasMetAbrirSeleccionActual();
            if (_Vista.VentasTbcMovimientos.SelectedTab == _Vista.VentasTbpDocumento)
            {
                _Controlador.ValidarCaptura(_DocumentoActual != null && Convert.ToString(_DocumentoActual["Estado"]) != "ANULADA", "Consulte un documento aplicado antes de modificarlo.");
                _EditandoDocumento = true; VentasMetRefrescarBotones(); _Vista.VentasTxtDescripcionDocumento.Focus();
            }
            else if (_Vista.VentasTbcMovimientos.SelectedTab == _Vista.VentasTbpRecibos)
            {
                _Controlador.ValidarCaptura(_ReciboActual != null && Convert.ToString(_ReciboActual["estadoPago"]) == "CONFIRMADO", "Consulte un recibo confirmado antes de modificarlo.");
                _EditandoRecibo = true; VentasMetRefrescarBotones(); _Vista.VentasTxtObservacionesPago.Focus();
            }
        }

        private void VentasMetAnular()
        {
            VentasMetAbrirSeleccionActual();
            bool Recibo = _Vista.VentasTbcMovimientos.SelectedTab == _Vista.VentasTbpRecibos;
            _Controlador.ValidarCaptura(Recibo ? _ReciboActual != null : _DocumentoActual != null, "Consulte el registro a anular.");
            string Numero = Recibo ? Convert.ToString(_ReciboActual["numeroRecibo"]) : Convert.ToString(_DocumentoActual["Numero"]);
            string Motivo = V.Motivo(_Vista, "Anular " + Numero); if (Motivo == null) return;
            if (Recibo)
            {
                int Id = Convert.ToInt32(_ReciboActual["idPago"]); _Controlador.AnularRecibo(Id, DateTime.Today, VentasIdUsuario, Motivo); VentasMetCargarRecibo(Id);
            }
            else
            {
                int Id = Convert.ToInt32(_DocumentoActual["Id"]); string Clase = Convert.ToString(_DocumentoActual["Clase"]);
                _Controlador.AnularDocumento(Id, Clase, VentasIdUsuario, Motivo); VentasMetCargarDocumento(Id, Clase);
            }
            V.Informar(_Vista, "Registro anulado. Se conservaron los movimientos originales y su reverso.");
        }

        private bool VentasMetGuardarModificacion()
        {
            if (_Vista.VentasTbcMovimientos.SelectedTab == _Vista.VentasTbpDocumento && _EditandoDocumento)
            {
                int Id = Convert.ToInt32(_DocumentoActual["Id"]); string Clase = Convert.ToString(_DocumentoActual["Clase"]);
                _Controlador.ModificarDocumento(Id, Clase, VentasIdUsuario, _Vista.VentasTxtDescripcionDocumento.Text, _Vista.VentasDtpVencimientoDocumento.Value);
                VentasMetCargarDocumento(Id, Clase); V.Informar(_Vista, "Documento actualizado."); return true;
            }
            if (_Vista.VentasTbcMovimientos.SelectedTab == _Vista.VentasTbpRecibos && _EditandoRecibo)
            {
                int Id = Convert.ToInt32(_ReciboActual["idPago"]); _Controlador.ModificarRecibo(Id, VentasIdUsuario, _Vista.VentasTxtObservacionesPago.Text);
                VentasMetCargarRecibo(Id); V.Informar(_Vista, "Observaciones del recibo actualizadas."); return true;
            }
            return false;
        }

        private void VentasMetActualizarAcciones()
        {
            int Tab = _Vista.VentasTbcMovimientos.SelectedIndex;
            bool Documento = Tab == 1 && _DocumentoActual != null, Recibo = Tab == 2 && _ReciboActual != null;
            _Vista.VentasPnlCliente.Enabled = Tab < 3 && !Documento && !Recibo;
            foreach (Control C in new Control[] { _Vista.VentasCboTipoDocumento, _Vista.VentasTxtNumeroDocumento, _Vista.VentasDtpFechaDocumento, _Vista.VentasNudImporteDocumento }) C.Enabled = !Documento;
            _Vista.VentasCboEstablecimientoDocumento.Enabled = !Documento && _Vista.VentasCboTipoDocumento.SelectedIndex == 0;
            _Vista.VentasTxtFacturaReferencia.Enabled = !Documento && _Vista.VentasCboTipoDocumento.SelectedIndex > 0;
            _SeleccionarFactura.Enabled = _Vista.VentasTxtFacturaReferencia.Enabled;
            _Vista.VentasTxtDescripcionDocumento.ReadOnly = Documento && !_EditandoDocumento;
            _Vista.VentasDtpVencimientoDocumento.Enabled = !Documento && _Vista.VentasCboTipoDocumento.SelectedIndex == 0 || _EditandoDocumento && Convert.ToString(_DocumentoActual["Clase"]) == "FACTURA";
            foreach (Control C in new Control[] { _Vista.VentasTxtRecibo, _Vista.VentasDtpFechaPago, _Vista.VentasNudMontoPago, _Vista.VentasNudTipoCambioPago, _Vista.VentasCboFormaPago, _Vista.VentasTxtReferenciaPago }) C.Enabled = !Recibo;
            _Vista.VentasDgvRecibosDeposito.ReadOnly = _Guardados[3];
            _Vista.VentasDgvDocumentos.ReadOnly = Recibo; _Vista.VentasTxtObservacionesPago.ReadOnly = Recibo && !_EditandoRecibo;
            _Vista.VentasBtnGuardar.Enabled = _Inicializado && Tab > 0 && Tab < 4 && (!_Guardados[Tab] || Tab == 1 && _EditandoDocumento || Tab == 2 && _EditandoRecibo);
            _Vista.VentasBtnModificar.Enabled = _Vista.VentasBtnEliminar.Enabled = _Inicializado && Tab != 3;
        }
        private void VentasMetConsultarDeposito()
        {
            DataSet Datos=_Controlador.Deposito(_Vista.VentasTxtCodigoDeposito.Text.Trim()); DataRow F=Datos.Tables[0].Rows[0];
            _Cargando=true;
            try
            {
                _Vista.VentasCboMonedaDeposito.SelectedValue=F["idMoneda"];
                _Vista.VentasDtpFechaDeposito.Value=Convert.ToDateTime(F["fechaDeposito"]);
                _Vista.VentasCboEmpresaDeposito.Text=Convert.ToString(F["empresaReferencia"]); _Vista.VentasCboBancoDestino.Text=Convert.ToString(F["bancoReferencia"]);
                _Vista.VentasCboCuentaDestino.Text=Convert.ToString(F["cuentaReferencia"]); _Vista.VentasCboConceptoBancario.Text=Convert.ToString(F["conceptoReferencia"]);
                _Vista.VentasTxtReferenciaDeposito.Text=Convert.ToString(F["referenciaDeposito"]);
                _Vista.VentasDgvRecibosDeposito.Rows.Clear();
                foreach(DataRow D in Datos.Tables[1].Rows) _Vista.VentasDgvRecibosDeposito.Rows.Add(true,D["Recibo"],D["Cliente"],_Vista.VentasCboMonedaDeposito.Text,D["Importe"],D["Importe"],0m,D["Importe"]);
                _Vista.VentasTxtTotalDeposito.Text=Convert.ToDecimal(F["total"]).ToString("N2",V.Cultura);
                _Vista.VentasTxtEstadoEnvio.Text=Convert.ToString(F["estadoDeposito"]);
                _Guardados[3]=true;
            }
            finally{_Cargando=false;}
            VentasMetRefrescarBotones();
            _Vista.VentasLblEtapaBancos.Text="Deposito preparado localmente. Su confirmacion bancaria corresponde al modulo de Bancos.";
        }
    }
}
