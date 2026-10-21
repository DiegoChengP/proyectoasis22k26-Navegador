using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CapaControlador_Cotizaciones;
using V = VentasPresentacion.ClsVistaVentas;

namespace CapaVista_Cotizaciones
{
    internal sealed partial class ClsCotizacionesPresentador
    {
        private readonly FrmCotizacionVentas _Vista;
        internal ClsCotizacionesPresentador(FrmCotizacionVentas Vista)
        {
            _Vista = Vista;
            VentasMetConectarEventos();
            _Vista.VentasCboCotEstado.SelectedIndex = 0;
            VentasMetMostrarVigencia();
        }

        private readonly ClsCotizacionesControlador _Controlador = new ClsCotizacionesControlador();
        private readonly DataTable _Lineas = VentasMetCrearDetalle();
        private bool _Cargando = true;
        private int _IdCotizacion;
        private int _UsuarioInicial;
        private string _EstadoGuardado = "";
        private string _NumeroGuardado = "";
        public int VentasIdUsuario
        {
            get { return _UsuarioInicial > 0 ? _UsuarioInicial : V.Id(_Vista.VentasCboUsuarioRegistro); }
            set
            {
                _UsuarioInicial = value;
                _Vista.VentasCboUsuarioRegistro.Enabled = value <= 0;
                if (_Vista.VentasCboUsuarioRegistro.DataSource != null) _Vista.VentasCboUsuarioRegistro.SelectedValue = value;
            }
        }
        private static DataTable VentasMetCrearDetalle()
        {
            var T = new DataTable(); T.Columns.Add("idProducto", typeof(int)); T.Columns.Add("Descripcion", typeof(string)); T.Columns.Add("Tipo", typeof(string));
            foreach (string C in new[] { "Cantidad", "Precio", "Descuento", "Tasa", "Base", "Impuesto", "Total" }) T.Columns.Add(C, typeof(decimal));
            return T;
        }
        private void VentasMetConectarEventos()
        {
            _Vista.Load += (s, e) => V.Ejecutar(_Vista, VentasMetCargar);
            _Vista.VentasBtnCotIngresar.Click += (s, e) => V.Ejecutar(_Vista, VentasMetNuevo);
            _Vista.VentasBtnCotModificar.Click += (s,e) => V.Ejecutar(_Vista,VentasMetModificar);
            _Vista.VentasBtnCotAnular.Click += (s,e) => V.Ejecutar(_Vista,VentasMetAnular);
            _Vista.VentasBtnCotRefrescar.Click += (s,e) => V.Ejecutar(_Vista,VentasMetRefrescar);
            _Vista.VentasDgvCotProductos.CellDoubleClick += (s,e) => { if(e.RowIndex>=0) V.Ejecutar(_Vista,VentasMetEditarLinea); };
            _Vista.VentasBtnCotAgregar.Click += (s, e) => V.Ejecutar(_Vista, VentasMetAgregar);
            _Vista.VentasBtnCotGuardar.Click += (s, e) => V.Ejecutar(_Vista, VentasMetGuardar);
            _Vista.VentasBtnCotLimpiar.Click += (s, e) => V.Ejecutar(_Vista, VentasMetNuevo);
            _Vista.VentasBtnCotConsultar.Click += (s, e) => V.Ejecutar(_Vista, VentasMetBuscar);
            _Vista.VentasTxtCotNumero.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; V.Ejecutar(_Vista, VentasMetBuscar); } };
            _Vista.VentasBtnCotPedido.Click += (s, e) => V.Ejecutar(_Vista, () => VentasMetConvertir(false));
            _Vista.VentasBtnCotFactura.Click += (s, e) => V.Ejecutar(_Vista, () => VentasMetConvertir(true));
            _Vista.VentasBtnCotEliminar.Click += (s, e) => V.Ejecutar(_Vista, () => { if (_Vista.VentasDgvCotProductos.CurrentRow != null && VentasMetEditable()) { _Lineas.Rows.RemoveAt(_Vista.VentasDgvCotProductos.CurrentRow.Index); VentasMetRestablecerLinea(); VentasMetMostrarLineas(); } });
            _Vista.VentasBtnCotCerrar.Click += (s, e) => _Vista.Close();
            _Vista.VentasBtnCotImprimir.Click += (s, e) => V.Ejecutar(_Vista, () =>
            {
                _Controlador.ValidarCaptura(!(_IdCotizacion <= 0), "Guarde la cotizacion antes de imprimir.");
                V.Imprimir(_Vista, "Cotizacion " + _NumeroGuardado, _Vista.VentasCboCotCliente.Text + " / " + _Vista.VentasCboCotMoneda.Text + " / " + _Vista.VentasCboCotEstado.Text + " / Vigencia " + _Vista.VentasDtpCotVigencia.Value.ToString("dd/MM/yyyy"), _Lineas);
            });
            _Vista.VentasCboCotCliente.SelectedIndexChanged += (s, e) => { if (!_Cargando) V.Ejecutar(_Vista, VentasMetCliente); };
            _Vista.VentasCboCotMoneda.SelectedIndexChanged += (s, e) => { if (!_Cargando) V.Ejecutar(_Vista, VentasMetPrecios); };
            _Vista.VentasDtpCotFecha.ValueChanged += (s, e) => { if (!_Cargando) V.Ejecutar(_Vista, VentasMetPrecios); };
            _Vista.VentasCboCotProducto.SelectedIndexChanged += (s, e) =>
            { DataRow P = V.Fila(_Vista.VentasCboCotProducto); _Vista.VentasTxtCotPrecio.Text = P == null ? "" : Convert.ToDecimal(P["Precio"]).ToString("N4", V.Cultura); };
            _Vista.VentasTxtCotNit.ReadOnly = true; _Vista.VentasTxtCotContacto.ReadOnly = true; _Vista.VentasTxtCotDireccion.ReadOnly = true;
            _Vista.VentasNudCotCantidad.DecimalPlaces = 4; _Vista.VentasNudCotCantidad.Minimum = 0.0001m; _Vista.VentasNudCotCantidad.Value = 1;
            _Vista.VentasTtpAcciones.SetToolTip(_Vista.VentasNudCotDescuento, "Descuento como importe de la linea, en la moneda de la cotizacion.");
            _Vista.VentasTtpAcciones.SetToolTip(_Vista.VentasTxtCotNumero, "Numero o vacio para generar correlativo. Enter para consultar una cotizacion guardada.");
        }
        private void VentasMetCargar()
        {
            _Cargando = true;
            try
            {
                V.Combo(_Vista.VentasCboUsuarioRegistro, _Controlador.Catalogo("Usuarios"), _UsuarioInicial);
                V.Combo(_Vista.VentasCboCotCliente, _Controlador.Catalogo("Clientes"));
                V.Combo(_Vista.VentasCboCotMoneda, _Controlador.Catalogo("Monedas"));
                V.Combo(_Vista.VentasCboCotVendedor, _Controlador.Catalogo("Vendedores"));
                V.Combo(_Vista.VentasCboCotEstablecimiento, _Controlador.Catalogo("Establecimientos"));
            }
            finally { _Cargando = false; }
            VentasMetNuevo();
        }
        private bool VentasMetEditable() { return _EditandoCotizacion && _Controlador.PuedeEditar(_IdCotizacion,_EstadoGuardado,_Vista.VentasDtpCotVigencia.Value); }
        private void VentasMetActualizarEdicion()
        {
            bool Editable = VentasMetEditable();
            _Vista.VentasBtnCotModificar.Enabled = _IdCotizacion > 0 && _Controlador.PuedeEditar(_IdCotizacion,_EstadoGuardado,_Vista.VentasDtpCotVigencia.Value);
            _Vista.VentasBtnCotAnular.Enabled = _IdCotizacion > 0 && (_EstadoGuardado == "BORRADOR" || _EstadoGuardado == "EMITIDA" || _EstadoGuardado == "ACEPTADA");
            _Vista.VentasBtnCotGuardar.Enabled = Editable; _Vista.VentasBtnCotAgregar.Enabled = Editable; _Vista.VentasBtnCotEliminar.Enabled = Editable;
            _Vista.VentasPnlCotProducto.Enabled = Editable;
            _Vista.VentasCboCotCliente.Enabled = Editable && _Lineas.Rows.Count == 0; _Vista.VentasCboCotMoneda.Enabled = Editable && _Lineas.Rows.Count == 0;
            _Vista.VentasCboCotVendedor.Enabled = Editable; _Vista.VentasCboCotEstado.Enabled = Editable;
            _Vista.VentasDtpCotFecha.Enabled = Editable && _Lineas.Rows.Count == 0; _Vista.VentasDtpCotVigencia.Enabled = Editable;
            _Vista.VentasNudCotTipoCambio.Enabled = Editable;
            bool Vigente = _IdCotizacion > 0 && (_EstadoGuardado == "EMITIDA" || _EstadoGuardado == "ACEPTADA") && _Vista.VentasDtpCotFecha.Value.Date <= DateTime.Today && _Vista.VentasDtpCotVigencia.Value.Date >= DateTime.Today;
            _Vista.VentasBtnCotPedido.Enabled = Vigente; _Vista.VentasBtnCotFactura.Enabled = Vigente;
        }
        private void VentasMetCliente()
        {
            DataRow C = V.Fila(_Vista.VentasCboCotCliente);
            _Vista.VentasTxtCotNit.Text = C == null ? "" : Convert.ToString(C["numeroIdentificacion"]);
            _Vista.VentasTxtCotDireccion.Text = C == null ? "" : Convert.ToString(C["direccionCliente"]);
            _Vista.VentasTxtCotContacto.Text = C == null ? "" : Convert.ToString(C["contactoCliente"]);
            if (C != null) _Vista.VentasCboCotMoneda.SelectedValue = Convert.ToInt32(C["idMonedaCredito"]);
            VentasMetPrecios();
        }
        private void VentasMetPrecios()
        {
            if (_Cargando || !VentasMetEditable()) return;
            int Cliente = V.Id(_Vista.VentasCboCotCliente), Moneda = V.Id(_Vista.VentasCboCotMoneda);
            if (Cliente <= 0 || Moneda <= 0) { _Vista.VentasTxtCotListaPrecios.Clear(); V.Combo(_Vista.VentasCboCotProducto, new DataTable()); return; }
            DataTable Lista = _Controlador.ListaCliente(Cliente, Moneda, _Vista.VentasDtpCotFecha.Value);
            _Vista.VentasTxtCotListaPrecios.Text = Lista.Rows.Count == 1 ? Convert.ToString(Lista.Rows[0]["nombreListaPrecio"]) : "Sin lista vigente asignada para esta moneda";
            V.Combo(_Vista.VentasCboCotProducto, _Controlador.Productos(Cliente, Moneda, _Vista.VentasDtpCotFecha.Value));
        }
        private void VentasMetAgregar()
        {
            _Controlador.ValidarCaptura(!(!VentasMetEditable()), "La cotizacion emitida conserva su detalle.");
            object[] Valores=_Controlador.CalcularLinea(V.Fila(_Vista.VentasCboCotProducto),_Vista.VentasNudCotCantidad.Value,_Vista.VentasNudCotDescuento.Value);
            if (_IndiceLinea >= 0) _Lineas.Rows[_IndiceLinea].ItemArray=Valores;
            else _Lineas.Rows.Add(Valores);
            VentasMetRestablecerLinea(); VentasMetMostrarLineas(); _Vista.VentasNudCotDescuento.Value=0;
        }
        private void VentasMetMostrarLineas()
        {
            _Vista.VentasDgvCotProductos.Rows.Clear();
            foreach (DataRow F in _Lineas.Rows) _Vista.VentasDgvCotProductos.Rows.Add(F["Descripcion"], F["Cantidad"], F["Precio"], F["Descuento"], F["Base"], F["Impuesto"], F["Total"]);
            string Moneda = V.Fila(_Vista.VentasCboCotMoneda) == null ? "" : Convert.ToString(V.Fila(_Vista.VentasCboCotMoneda)["codigoMoneda"]);
            _Vista.VentasLblCotSubtotal.Text = V.Dinero(_Lineas.AsEnumerable().Sum(F => (decimal)F["Base"]), Moneda);
            _Vista.VentasLblCotDescuento.Text = V.Dinero(_Lineas.AsEnumerable().Sum(F => (decimal)F["Descuento"]), Moneda);
            _Vista.VentasLblCotImpuesto.Text = V.Dinero(_Lineas.AsEnumerable().Sum(F => (decimal)F["Impuesto"]), Moneda);
            _Vista.VentasLblCotTotal.Text = V.Dinero(_Lineas.AsEnumerable().Sum(F => (decimal)F["Total"]), Moneda);
            VentasMetActualizarEdicion();
        }
        private void VentasMetGuardar()
        {
            string Estado = _Vista.VentasCboCotEstado.Text == "Borrador" ? "BORRADOR" : _Vista.VentasCboCotEstado.Text == "Vigente" ? "EMITIDA" : "";
            string Numero = _Controlador.Guardar(_IdCotizacion, _Vista.VentasTxtCotNumero.Text, V.Id(_Vista.VentasCboCotCliente), V.Id(_Vista.VentasCboCotVendedor), V.Id(_Vista.VentasCboCotMoneda), VentasIdUsuario,
                _Vista.VentasDtpCotFecha.Value, _Vista.VentasDtpCotVigencia.Value, _Vista.VentasNudCotTipoCambio.Value, Estado, _Lineas.Copy());
            VentasMetConsultar(Numero); V.Informar(_Vista, "Cotizacion guardada: " + Numero);
        }
        private void VentasMetConsultar(string Numero)
        {
            _Controlador.ValidarCaptura(!(string.IsNullOrWhiteSpace(Numero)), "Ingrese el numero de cotizacion a consultar.");
            DataSet D = _Controlador.Consultar(Numero); DataRow C = D.Tables[0].Rows[0]; _Cargando = true;
            try
            {
                _IdCotizacion = Convert.ToInt32(C["idCotizacion"]); _EstadoGuardado = Convert.ToString(C["estadoCotizacion"]); _NumeroGuardado = Convert.ToString(C["numeroCotizacion"]);
                _Vista.VentasTxtCotNumero.Text = _NumeroGuardado; _Vista.VentasDtpCotFecha.Value = Convert.ToDateTime(C["fechaEmision"]); _Vista.VentasDtpCotVigencia.Value = Convert.ToDateTime(C["vigenteHasta"]);
                _Vista.VentasCboCotCliente.SelectedValue = Convert.ToInt32(C["idCliente"]); _Vista.VentasCboCotMoneda.SelectedValue = Convert.ToInt32(C["idMoneda"]); _Vista.VentasCboCotVendedor.SelectedValue = Convert.ToInt32(C["idVendedor"]);
                DataRow Cliente = V.Fila(_Vista.VentasCboCotCliente);
                _Vista.VentasTxtCotNit.Text = Cliente == null ? "" : Convert.ToString(Cliente["numeroIdentificacion"]);
                _Vista.VentasTxtCotDireccion.Text = Cliente == null ? "" : Convert.ToString(Cliente["direccionCliente"]);
                _Vista.VentasTxtCotContacto.Text = Cliente == null ? "" : Convert.ToString(Cliente["contactoCliente"]);
                _Vista.VentasNudCotTipoCambio.Value = Convert.ToDecimal(C["tipoCambio"]); _Vista.VentasTxtCotListaPrecios.Text = Convert.ToString(C["nombreListaPrecio"]);
                string Visual = Convert.ToString(C["EstadoVisual"]);
                _Vista.VentasCboCotEstado.SelectedIndex = Visual == "BORRADOR" ? 0 : Visual == "EMITIDA" || Visual == "ACEPTADA" ? 1 : Visual == "VENCIDA" ? 2 : Visual == "CONVERTIDA" ? 3 : 4;
                _Lineas.Rows.Clear();
                foreach (DataRow F in D.Tables[1].Rows) _Lineas.Rows.Add(F["idProducto"], F["descripcionItem"], F["tipoItem"], F["cantidad"], F["precioUnitario"], F["descuentoMonto"], F["porcentajeImpuesto"], Math.Round((decimal)F["cantidad"] * (decimal)F["precioUnitario"], 2, MidpointRounding.AwayFromZero), F["impuestoMonto"], F["totalLinea"]);
            }
            finally { _Cargando = false; }
            _EditandoCotizacion=false; VentasMetRestablecerLinea();
            VentasMetMostrarLineas(); VentasMetMostrarVigencia();
            if (VentasMetEditable()) VentasMetPrecios();
        }
        private void VentasMetNuevo()
        {
            _Cargando = true;
            try
            {
                _EditandoCotizacion=true; VentasMetRestablecerLinea();
                _IdCotizacion = 0; _EstadoGuardado = ""; _NumeroGuardado = ""; _Lineas.Rows.Clear();
                _Vista.VentasTxtCotNumero.Clear(); _Vista.VentasTxtCotNit.Clear(); _Vista.VentasTxtCotDireccion.Clear(); _Vista.VentasTxtCotContacto.Clear(); _Vista.VentasTxtCotListaPrecios.Clear(); _Vista.VentasTxtCotPrecio.Clear();
                _Vista.VentasCboCotCliente.SelectedIndex = -1; _Vista.VentasCboCotMoneda.SelectedIndex = -1; _Vista.VentasCboCotVendedor.SelectedIndex = -1;
                _Vista.VentasDtpCotFecha.Value = DateTime.Today; _Vista.VentasDtpCotVigencia.Value = DateTime.Today.AddDays(30); _Vista.VentasCboCotEstado.SelectedIndex = 0;
                _Vista.VentasNudCotCantidad.Value = 1; _Vista.VentasNudCotDescuento.Value = 0;
                _Vista.VentasNudCotTipoCambio.Value = 1; V.Combo(_Vista.VentasCboCotProducto, new DataTable());
            }
            finally { _Cargando = false; }
            VentasMetMostrarLineas(); VentasMetMostrarVigencia();
            _Vista.VentasCboCotCliente.Focus();
            if (_Vista.VentasCboCotCliente.Items.Count == 0) _Vista.VentasLblCotVigencia.Text = "Configure clientes, vendedores, productos y la lista asignada al cliente para registrar cotizaciones.";
        }
        private void VentasMetConvertir(bool AFactura)
        {
            string Numero = _Controlador.Convertir(_IdCotizacion, AFactura, V.Id(_Vista.VentasCboCotEstablecimiento), VentasIdUsuario);
            VentasMetConsultar(_NumeroGuardado);
            V.Informar(_Vista, AFactura ? "Factura borrador preparada con los precios originales: " + Numero + ". Su emision e inventarios corresponden a Facturas." : "Pedido creado con los precios originales: " + Numero);
        }
        internal void VentasMetMostrarVigencia()
        {
            // Las reglas definitivas se validan en el Controlador al guardar o convertir.
            bool FechaValida = _Vista.VentasDtpCotVigencia.Value.Date >= DateTime.Today &&
                _Vista.VentasDtpCotVigencia.Value.Date >= _Vista.VentasDtpCotFecha.Value.Date;
            bool Vigente = FechaValida && _Vista.VentasCboCotEstado.Text == "Vigente";
            _Vista.VentasBtnCotPedido.Enabled = Vigente;
            _Vista.VentasBtnCotFactura.Enabled = Vigente;
            _Vista.VentasLblCotVigencia.Text = !FechaValida || _Vista.VentasCboCotEstado.Text == "Vencida"
                ? "Cotización vencida o fechas no válidas: genere una nueva con los precios actuales."
                : "Una cotización vigente conserva sus precios. Si vence, se genera una nueva con los precios actuales.";
            VentasMetActualizarEdicion();
                }
    }
}
