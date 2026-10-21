using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CapaControlador_Cotizaciones;
using V = VentasPresentacion.ClsVistaVentas;

namespace CapaVista_Cotizaciones
{
    public partial class FrmCotizacionVentas
    {
        private readonly ClsCotizacionesControlador _Controlador = new ClsCotizacionesControlador();
        private readonly DataTable _Lineas = VentasMetCrearDetalle();
        private bool _Cargando = true;
        private int _IdCotizacion;
        private int _UsuarioInicial;
        private string _EstadoGuardado = "";
        private string _NumeroGuardado = "";
        public int VentasIdUsuario
        {
            get { return V.Id(VentasCboUsuarioRegistro); }
            set { _UsuarioInicial = value; if (VentasCboUsuarioRegistro.DataSource != null) VentasCboUsuarioRegistro.SelectedValue = value; }
        }
        private static DataTable VentasMetCrearDetalle()
        {
            var T = new DataTable(); T.Columns.Add("idProducto", typeof(int)); T.Columns.Add("Descripcion", typeof(string)); T.Columns.Add("Tipo", typeof(string));
            foreach (string C in new[] { "Cantidad", "Precio", "Descuento", "Tasa", "Base", "Impuesto", "Total" }) T.Columns.Add(C, typeof(decimal));
            return T;
        }
        private void VentasMetConectarEventos()
        {
            Load += (s, e) => V.Ejecutar(this, VentasMetCargar);
            VentasBtnCotAgregar.Click += (s, e) => V.Ejecutar(this, VentasMetAgregar);
            VentasBtnCotGuardar.Click += (s, e) => V.Ejecutar(this, VentasMetGuardar);
            VentasBtnCotLimpiar.Click += (s, e) => V.Ejecutar(this, VentasMetNuevo);
            VentasBtnCotConsultar.Click += (s, e) => V.Ejecutar(this, () => VentasMetConsultar(VentasTxtCotNumero.Text));
            VentasTxtCotNumero.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; V.Ejecutar(this, () => VentasMetConsultar(VentasTxtCotNumero.Text)); } };
            VentasBtnCotPedido.Click += (s, e) => V.Ejecutar(this, () => VentasMetConvertir(false));
            VentasBtnCotFactura.Click += (s, e) => V.Ejecutar(this, () => VentasMetConvertir(true));
            VentasBtnCotEliminar.Click += (s, e) => V.Ejecutar(this, () => { if (VentasDgvCotProductos.CurrentRow != null && VentasMetEditable()) { _Lineas.Rows.RemoveAt(VentasDgvCotProductos.CurrentRow.Index); VentasMetMostrarLineas(); } });
            VentasBtnCotCerrar.Click += (s, e) => Close();
            VentasBtnCotImprimir.Click += (s, e) => V.Ejecutar(this, () =>
            {
                if (_IdCotizacion <= 0) throw new InvalidOperationException("Guarde la cotizacion antes de imprimir.");
                V.Imprimir(this, "Cotizacion " + _NumeroGuardado, VentasCboCotCliente.Text + " / " + VentasCboCotMoneda.Text + " / " + VentasCboCotEstado.Text + " / Vigencia " + VentasDtpCotVigencia.Value.ToString("dd/MM/yyyy"), _Lineas);
            });
            VentasCboCotCliente.SelectedIndexChanged += (s, e) => { if (!_Cargando) V.Ejecutar(this, VentasMetCliente); };
            VentasCboCotMoneda.SelectedIndexChanged += (s, e) => { if (!_Cargando) V.Ejecutar(this, VentasMetPrecios); };
            VentasDtpCotFecha.ValueChanged += (s, e) => { if (!_Cargando) V.Ejecutar(this, VentasMetPrecios); };
            VentasCboCotProducto.SelectedIndexChanged += (s, e) =>
            { DataRow P = V.Fila(VentasCboCotProducto); VentasTxtCotPrecio.Text = P == null ? "" : Convert.ToDecimal(P["Precio"]).ToString("N4", V.Cultura); };
            VentasTxtCotNit.ReadOnly = true; VentasTxtCotContacto.ReadOnly = true; VentasTxtCotDireccion.ReadOnly = true;
            VentasNudCotCantidad.DecimalPlaces = 4; VentasNudCotCantidad.Minimum = 0.0001m; VentasNudCotCantidad.Value = 1;
            VentasTtpAcciones.SetToolTip(VentasNudCotDescuento, "Descuento como importe de la linea, en la moneda de la cotizacion.");
            VentasTtpAcciones.SetToolTip(VentasTxtCotNumero, "Numero o vacio para generar correlativo. Enter para consultar una cotizacion guardada.");
        }
        private void VentasMetCargar()
        {
            _Cargando = true;
            try
            {
                V.Combo(VentasCboUsuarioRegistro, _Controlador.Catalogo("Usuarios"), _UsuarioInicial);
                V.Combo(VentasCboCotCliente, _Controlador.Catalogo("Clientes"));
                V.Combo(VentasCboCotMoneda, _Controlador.Catalogo("Monedas"));
                V.Combo(VentasCboCotVendedor, _Controlador.Catalogo("Vendedores"));
                V.Combo(VentasCboCotEstablecimiento, _Controlador.Catalogo("Establecimientos"));
            }
            finally { _Cargando = false; }
            VentasMetNuevo();
        }
        private bool VentasMetEditable() { return _IdCotizacion == 0 || _EstadoGuardado == "BORRADOR" && VentasDtpCotVigencia.Value.Date >= DateTime.Today; }
        private void VentasMetActualizarEdicion()
        {
            bool Editable = VentasMetEditable();
            VentasBtnCotGuardar.Enabled = Editable; VentasBtnCotAgregar.Enabled = Editable; VentasBtnCotEliminar.Enabled = Editable;
            VentasPnlCotProducto.Enabled = Editable;
            VentasCboCotCliente.Enabled = Editable && _Lineas.Rows.Count == 0; VentasCboCotMoneda.Enabled = Editable && _Lineas.Rows.Count == 0;
            VentasCboCotVendedor.Enabled = Editable; VentasCboCotEstado.Enabled = Editable;
            VentasDtpCotFecha.Enabled = Editable && _Lineas.Rows.Count == 0; VentasDtpCotVigencia.Enabled = Editable;
            VentasNudCotTipoCambio.Enabled = Editable;
            bool Vigente = _IdCotizacion > 0 && (_EstadoGuardado == "EMITIDA" || _EstadoGuardado == "ACEPTADA") && VentasDtpCotFecha.Value.Date <= DateTime.Today && VentasDtpCotVigencia.Value.Date >= DateTime.Today;
            VentasBtnCotPedido.Enabled = Vigente; VentasBtnCotFactura.Enabled = Vigente;
        }
        private void VentasMetCliente()
        {
            DataRow C = V.Fila(VentasCboCotCliente);
            VentasTxtCotNit.Text = C == null ? "" : Convert.ToString(C["numeroIdentificacion"]);
            VentasTxtCotDireccion.Text = C == null ? "" : Convert.ToString(C["direccionCliente"]);
            VentasTxtCotContacto.Text = C == null ? "" : Convert.ToString(C["contactoCliente"]);
            if (C != null) VentasCboCotMoneda.SelectedValue = Convert.ToInt32(C["idMonedaCredito"]);
            VentasMetPrecios();
        }
        private void VentasMetPrecios()
        {
            if (_Cargando || !VentasMetEditable()) return;
            int Cliente = V.Id(VentasCboCotCliente), Moneda = V.Id(VentasCboCotMoneda);
            if (Cliente <= 0 || Moneda <= 0) { VentasTxtCotListaPrecios.Clear(); V.Combo(VentasCboCotProducto, new DataTable()); return; }
            DataTable Lista = _Controlador.ListaCliente(Cliente, Moneda, VentasDtpCotFecha.Value);
            VentasTxtCotListaPrecios.Text = Lista.Rows.Count == 1 ? Convert.ToString(Lista.Rows[0]["nombreListaPrecio"]) : "Sin lista vigente asignada para esta moneda";
            V.Combo(VentasCboCotProducto, _Controlador.Productos(Cliente, Moneda, VentasDtpCotFecha.Value));
        }
        private void VentasMetAgregar()
        {
            if (!VentasMetEditable()) throw new InvalidOperationException("La cotizacion emitida conserva su detalle.");
            DataRow P = V.Fila(VentasCboCotProducto); if (P == null) throw new InvalidOperationException("Seleccione un producto de la lista asignada al cliente.");
            decimal Cantidad = VentasNudCotCantidad.Value, Precio = Convert.ToDecimal(P["Precio"]), D = VentasNudCotDescuento.Value, Tasa = Convert.ToDecimal(P["Tasa"]);
            decimal Base = Math.Round(Cantidad * Precio, 2, MidpointRounding.AwayFromZero);
            if (D < 0 || D > Base) throw new InvalidOperationException("El descuento no puede superar el subtotal de la linea.");
            decimal Impuesto = Math.Round((Base - D) * Tasa / 100m, 2, MidpointRounding.AwayFromZero);
            _Lineas.Rows.Add(P["Id"], P["Descripcion"], P["Tipo"], Cantidad, Precio, D, Tasa, Base, Impuesto, Base - D + Impuesto);
            VentasMetMostrarLineas(); VentasNudCotDescuento.Value = 0;
        }
        private void VentasMetMostrarLineas()
        {
            VentasDgvCotProductos.Rows.Clear();
            foreach (DataRow F in _Lineas.Rows) VentasDgvCotProductos.Rows.Add(F["Descripcion"], F["Cantidad"], F["Precio"], F["Descuento"], F["Base"], F["Impuesto"], F["Total"]);
            string Moneda = V.Fila(VentasCboCotMoneda) == null ? "" : Convert.ToString(V.Fila(VentasCboCotMoneda)["codigoMoneda"]);
            VentasLblCotSubtotal.Text = V.Dinero(_Lineas.AsEnumerable().Sum(F => (decimal)F["Base"]), Moneda);
            VentasLblCotDescuento.Text = V.Dinero(_Lineas.AsEnumerable().Sum(F => (decimal)F["Descuento"]), Moneda);
            VentasLblCotImpuesto.Text = V.Dinero(_Lineas.AsEnumerable().Sum(F => (decimal)F["Impuesto"]), Moneda);
            VentasLblCotTotal.Text = V.Dinero(_Lineas.AsEnumerable().Sum(F => (decimal)F["Total"]), Moneda);
            VentasMetActualizarEdicion();
        }
        private void VentasMetGuardar()
        {
            string Estado = VentasCboCotEstado.Text == "Borrador" ? "BORRADOR" : VentasCboCotEstado.Text == "Vigente" ? "EMITIDA" : "";
            string Numero = _Controlador.Guardar(_IdCotizacion, VentasTxtCotNumero.Text, V.Id(VentasCboCotCliente), V.Id(VentasCboCotVendedor), V.Id(VentasCboCotMoneda), VentasIdUsuario,
                VentasDtpCotFecha.Value, VentasDtpCotVigencia.Value, VentasNudCotTipoCambio.Value, Estado, _Lineas.Copy());
            VentasMetConsultar(Numero); V.Informar(this, "Cotizacion guardada: " + Numero);
        }
        private void VentasMetConsultar(string Numero)
        {
            if (string.IsNullOrWhiteSpace(Numero)) throw new InvalidOperationException("Ingrese el numero de cotizacion a consultar.");
            DataSet D = _Controlador.Consultar(Numero); DataRow C = D.Tables[0].Rows[0]; _Cargando = true;
            try
            {
                _IdCotizacion = Convert.ToInt32(C["idCotizacion"]); _EstadoGuardado = Convert.ToString(C["estadoCotizacion"]); _NumeroGuardado = Convert.ToString(C["numeroCotizacion"]);
                VentasTxtCotNumero.Text = _NumeroGuardado; VentasDtpCotFecha.Value = Convert.ToDateTime(C["fechaEmision"]); VentasDtpCotVigencia.Value = Convert.ToDateTime(C["vigenteHasta"]);
                VentasCboCotCliente.SelectedValue = Convert.ToInt32(C["idCliente"]); VentasCboCotMoneda.SelectedValue = Convert.ToInt32(C["idMoneda"]); VentasCboCotVendedor.SelectedValue = Convert.ToInt32(C["idVendedor"]);
                DataRow Cliente = V.Fila(VentasCboCotCliente);
                VentasTxtCotNit.Text = Cliente == null ? "" : Convert.ToString(Cliente["numeroIdentificacion"]);
                VentasTxtCotDireccion.Text = Cliente == null ? "" : Convert.ToString(Cliente["direccionCliente"]);
                VentasTxtCotContacto.Text = Cliente == null ? "" : Convert.ToString(Cliente["contactoCliente"]);
                VentasNudCotTipoCambio.Value = Convert.ToDecimal(C["tipoCambio"]); VentasTxtCotListaPrecios.Text = Convert.ToString(C["nombreListaPrecio"]);
                string Visual = Convert.ToString(C["EstadoVisual"]);
                VentasCboCotEstado.SelectedIndex = Visual == "BORRADOR" ? 0 : Visual == "EMITIDA" || Visual == "ACEPTADA" ? 1 : Visual == "VENCIDA" ? 2 : Visual == "CONVERTIDA" ? 3 : 4;
                _Lineas.Rows.Clear();
                foreach (DataRow F in D.Tables[1].Rows) _Lineas.Rows.Add(F["idProducto"], F["descripcionItem"], F["tipoItem"], F["cantidad"], F["precioUnitario"], F["descuentoMonto"], F["porcentajeImpuesto"], Math.Round((decimal)F["cantidad"] * (decimal)F["precioUnitario"], 2, MidpointRounding.AwayFromZero), F["impuestoMonto"], F["totalLinea"]);
            }
            finally { _Cargando = false; }
            VentasMetMostrarLineas(); VentasMetMostrarVigencia();
            if (VentasMetEditable()) VentasMetPrecios();
        }
        private void VentasMetNuevo()
        {
            _Cargando = true;
            try
            {
                _IdCotizacion = 0; _EstadoGuardado = ""; _NumeroGuardado = ""; _Lineas.Rows.Clear();
                VentasTxtCotNumero.Clear(); VentasTxtCotNit.Clear(); VentasTxtCotDireccion.Clear(); VentasTxtCotContacto.Clear(); VentasTxtCotListaPrecios.Clear(); VentasTxtCotPrecio.Clear();
                VentasCboCotCliente.SelectedIndex = -1; VentasCboCotMoneda.SelectedIndex = -1; VentasCboCotVendedor.SelectedIndex = -1;
                VentasDtpCotFecha.Value = DateTime.Today; VentasDtpCotVigencia.Value = DateTime.Today.AddDays(30); VentasCboCotEstado.SelectedIndex = 0;
                VentasNudCotTipoCambio.Value = 1; V.Combo(VentasCboCotProducto, new DataTable());
            }
            finally { _Cargando = false; }
            VentasMetMostrarLineas(); VentasMetMostrarVigencia();
            if (VentasCboCotCliente.Items.Count == 0) VentasLblCotVigencia.Text = "Configure clientes, vendedores, productos y la lista asignada al cliente para registrar cotizaciones.";
        }
        private void VentasMetConvertir(bool AFactura)
        {
            string Numero = _Controlador.Convertir(_IdCotizacion, AFactura, V.Id(VentasCboCotEstablecimiento), VentasIdUsuario);
            VentasMetConsultar(_NumeroGuardado);
            V.Informar(this, AFactura ? "Factura borrador preparada con los precios originales: " + Numero + ". Su emision e inventarios corresponden a Facturas." : "Pedido creado con los precios originales: " + Numero);
        }
    }
}
