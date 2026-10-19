/*
 * INICIO DEL CÓDIGO — Ventas y cuentas por cobrar
 * Asignación: Matthew, Diego
 * Matthew Andreé Juárez Xoy | Carné: 0901-23-4250
 * Diego Alejandro Cheng Peña | Carné: 0901-22-8091
 * Encargado: Diego Alejandro Cheng Peña
 * Fecha de actualización: 18/10/2026
 * Propósito: Listado de precios por tipo de cliente con arquitectura Vista-Controlador-Modelo.
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using CapaControlador_ListadoPrecios;

namespace CapaVista_Ventas
{
    public partial class FrmListadoPrecios : Form
    {
        private readonly ClsCtrlListadoPrecios _controlador;
        private readonly CultureInfo _cultura = CultureInfo.GetCultureInfo("es-GT");
        private bool _cargando;
        private List<ClsPrecioCliente> _datosListado = new List<ClsPrecioCliente>();

        public FrmListadoPrecios()
        {
            InitializeComponent();
            _controlador = new ClsCtrlListadoPrecios();
            VentasNudPorcentajeDescuento.DecimalPlaces = 2;
            VentasNudPorcentajeDescuento.Minimum = 0;
            VentasNudPorcentajeDescuento.Maximum = 100;
            VentasNudPorcentajeDescuento.ValueChanged += VentasNudPorcentajeDescuento_ValueChanged;
            VentasTxtPrecioBase.TextChanged += VentasTxtPrecioBase_TextChanged;
            VentasDgvListadoClientes.CellClick += VentasDgvListadoClientes_CellClick;
            VentasBtnConsultar.Click += VentasBtnConsultar_Click;
            VentasBtnGuardar.Click += VentasBtnGuardar_Click;
            VentasBtnCancelar.Click += VentasBtnCancelar_Click;
            VentasCboTipoCliente.SelectedIndexChanged += VentasCboTipoCliente_SelectedIndexChanged;
            comboBox1.SelectedIndexChanged += VentasCboTipoProducto_SelectedIndexChanged;
            VentasMetAgregarOpcion(VentasCboTipoCliente, "Todos");
            VentasMetAgregarOpcion(comboBox1, "Todos");
            VentasCboTipoCliente.SelectedItem = "Todos";
            comboBox1.SelectedItem = "Todos";
            VentasTxtPrecioFinal.ReadOnly = true;
            VentasTxtPrecioBase.Text = "0.00";
            VentasTxtPrecioFinal.Text = "0.00";
            VentasMetCargarListado();
        }

        private void VentasMetCargarListado()
        {
            try
            {
                _cargando = true;
                _datosListado = _controlador.ConsultarListado();
                foreach (ClsPrecioCliente item in _datosListado)
                {
                    VentasMetAgregarOpcion(VentasCboTipoCliente, item.TipoCliente);
                    VentasMetAgregarOpcion(comboBox1, item.TipoProducto);
                }
                VentasMetAplicarFiltros();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible cargar el listado de precios.\n" + ex.Message,
                    "Listado de precios", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { _cargando = false; }
        }

        private void VentasMetAgregarOpcion(ComboBox combo, string valor)
        {
            if (!string.IsNullOrWhiteSpace(valor) && !combo.Items.Contains(valor)) combo.Items.Add(valor);
        }

        // Aplica ambos filtros al listado sin modificar los registros originales.
        private void VentasMetAplicarFiltros()
        {
            string tipoCliente = Convert.ToString(VentasCboTipoCliente.SelectedItem);
            string tipoProducto = Convert.ToString(comboBox1.SelectedItem);
            VentasDgvListadoClientes.Rows.Clear();
            foreach (ClsPrecioCliente item in _datosListado)
            {
                bool coincideCliente = string.IsNullOrWhiteSpace(tipoCliente) || tipoCliente == "Todos" ||
                    string.Equals(item.TipoCliente, tipoCliente, StringComparison.OrdinalIgnoreCase);
                bool coincideProducto = string.IsNullOrWhiteSpace(tipoProducto) || tipoProducto == "Todos" ||
                    string.Equals(item.TipoProducto, tipoProducto, StringComparison.OrdinalIgnoreCase);
                if (!coincideCliente || !coincideProducto) continue;
                VentasDgvListadoClientes.Rows.Add(item.TipoCliente, item.TipoProducto,
                    item.PorcentajeDescuento.ToString("N2", _cultura), item.PrecioBase.ToString("N2", _cultura),
                    item.PrecioFinal.ToString("N2", _cultura), "Seleccionar");
            }
            VentasDgvListadoClientes.ClearSelection();
        }

        private void VentasMetActualizarPrecioFinal()
        {
            decimal basePrecio;
            if (!decimal.TryParse(VentasTxtPrecioBase.Text, NumberStyles.Currency, _cultura, out basePrecio) &&
                !decimal.TryParse(VentasTxtPrecioBase.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out basePrecio))
            { VentasTxtPrecioFinal.Text = ""; return; }
            if (basePrecio < 0) { VentasTxtPrecioFinal.Text = ""; return; }
            try { VentasTxtPrecioFinal.Text = _controlador.CalcularPrecioFinal(basePrecio, VentasNudPorcentajeDescuento.Value).ToString("N2", _cultura); }
            catch { VentasTxtPrecioFinal.Text = ""; }
        }

        private void VentasBtnConsultar_Click(object sender, EventArgs e)
        {
            VentasMetCargarListado();
            MessageBox.Show(_controlador.MensajeEstado, "Consulta de precios", MessageBoxButtons.OK,
                _controlador.UsaBaseDeDatos ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }

        private void VentasBtnGuardar_Click(object sender, EventArgs e)
        {
            decimal precioBase;
            if (!decimal.TryParse(VentasTxtPrecioBase.Text, NumberStyles.Currency, _cultura, out precioBase) &&
                !decimal.TryParse(VentasTxtPrecioBase.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out precioBase))
            { MessageBox.Show("Ingrese un precio base numérico.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); VentasTxtPrecioBase.Focus(); return; }
            string mensaje;
            bool guardado = _controlador.Guardar(Convert.ToString(VentasCboTipoCliente.SelectedItem),
                Convert.ToString(comboBox1.SelectedItem), VentasNudPorcentajeDescuento.Value, precioBase, out mensaje);
            MessageBox.Show(mensaje, guardado ? "Listado de precios" : "No se pudo guardar", MessageBoxButtons.OK,
                guardado && _controlador.UsaBaseDeDatos ? MessageBoxIcon.Information : (guardado ? MessageBoxIcon.Warning : MessageBoxIcon.Error));
            if (guardado) { VentasMetCargarListado(); VentasMetLimpiarCampos(false); }
        }

        private void VentasBtnCancelar_Click(object sender, EventArgs e) { VentasMetLimpiarCampos(true); }
        private void VentasCboTipoCliente_SelectedIndexChanged(object sender, EventArgs e) { if (!_cargando) VentasMetAplicarFiltros(); }
        private void VentasCboTipoProducto_SelectedIndexChanged(object sender, EventArgs e) { if (!_cargando) VentasMetAplicarFiltros(); }
        private void VentasNudPorcentajeDescuento_ValueChanged(object sender, EventArgs e) { VentasMetActualizarPrecioFinal(); }
        private void VentasTxtPrecioBase_TextChanged(object sender, EventArgs e) { VentasMetActualizarPrecioFinal(); }

        private void VentasDgvListadoClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            VentasMetCargarFila(VentasDgvListadoClientes.Rows[e.RowIndex]);
        }

        private void VentasMetCargarFila(DataGridViewRow fila)
        {
            _cargando = true;
            try
            {
                string cliente = Convert.ToString(fila.Cells[0].Value);
                string producto = Convert.ToString(fila.Cells[1].Value);
                if (VentasCboTipoCliente.Items.Contains(cliente)) VentasCboTipoCliente.SelectedItem = cliente;
                if (comboBox1.Items.Contains(producto)) comboBox1.SelectedItem = producto;
                decimal descuento, basePrecio;
                if (decimal.TryParse(Convert.ToString(fila.Cells[2].Value), NumberStyles.Any, _cultura, out descuento))
                    VentasNudPorcentajeDescuento.Value = Math.Min(VentasNudPorcentajeDescuento.Maximum, Math.Max(VentasNudPorcentajeDescuento.Minimum, descuento));
                if (decimal.TryParse(Convert.ToString(fila.Cells[3].Value), NumberStyles.Any, _cultura, out basePrecio)) VentasTxtPrecioBase.Text = basePrecio.ToString("N2", _cultura);
            }
            finally { _cargando = false; VentasMetActualizarPrecioFinal(); }
        }

        private void VentasMetLimpiarCampos(bool limpiarSeleccion)
        {
            _cargando = true;
            try
            {
                if (VentasCboTipoCliente.Items.Contains("Todos")) VentasCboTipoCliente.SelectedItem = "Todos";
                if (comboBox1.Items.Contains("Todos")) comboBox1.SelectedItem = "Todos";
                VentasNudPorcentajeDescuento.Value = 0;
                VentasTxtPrecioBase.Text = "0.00";
                VentasTxtPrecioFinal.Text = "0.00";
                if (limpiarSeleccion) VentasDgvListadoClientes.ClearSelection();
            }
            finally { _cargando = false; VentasMetAplicarFiltros(); VentasMetActualizarPrecioFinal(); }
        }

        private void VentasLblSimboloPorcentaje_Click(object sender, EventArgs e) { }
    }
}

// FIN DEL CÓDIGO — Equipo asignado al formulario | Fecha: 18/10/2026
