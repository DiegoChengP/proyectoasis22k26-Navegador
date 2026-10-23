/*
 * Área: Contabilidad
 * Autor: Gabriel André Guillén Pocón
 * Carné: 0901-23-1998
 * Fecha: 14/10/2026
 * Propósito: prototipo no funcional de pólizas locales de Embutidos de Calidad,
 * basado en tblCuenta, tblPolizaEncabezado y tblPolizaDetalle de dbContabilidad.
 */
using CapaControlador_Contabilidad;
using CapaControlador_Contabilidad.Modelos;
using CapaVista_Contabilidad.Componentes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace CapaVista_Contabilidad
{
    // Ventana de pólizas locales.
    public partial class FrmPolizasLocales : Form
    {
        // Permisos del usuario en esta pantalla.
        private ClsPermisosPantallaContabilidad _Permisos;
        // Oskar Cermeño:
        // Atributo por modulo ERP
        private readonly int _idModulo;

        // Controlador de pólizas.
        private readonly ClsControladorPolizasContabilidad _Controlador = new ClsControladorPolizasContabilidad();
        // Líneas del detalle.
        private readonly BindingList<ClsLineaPolizaContabilidad> _Lineas = new BindingList<ClsLineaPolizaContabilidad>();
        // Cultura es-GT para los importes.
        private readonly CultureInfo _Cultura = CultureInfo.GetCultureInfo("es-GT");
        // Errores por campo.
        private readonly ClsValidacionContabilidad _Validacion = new ClsValidacionContabilidad();

        // Constructor para el diseñador y las pruebas: todos los permisos.
        public FrmPolizasLocales() : this(ClsPermisosPantallaContabilidad.Total, 1)
        {
        }

        // Abre la ventana para un módulo ERP con los permisos que dio Seguridad.
        public FrmPolizasLocales(ClsPermisosPantallaContabilidad Permisos, int idModulo)
        {
            InitializeComponent();
            _idModulo = idModulo;
            _Permisos = Permisos;
            ContabilidadMetAplicarPermisos(Permisos);
            ClsPresentacionContabilidad.ContabilidadMetPrepararVentana(this, "Pólizas locales", new Size(1180, 688));
            ContabilidadLblTitulo.Text = "Póliza " + ContabilidadFuncNombreModulo(_idModulo);
            foreach (Control Campo in new Control[] { ContabilidadTxtDescripcion, ContabilidadDtpFecha, ContabilidadCboEstado, ContabilidadCboOrigen, ContabilidadCboCuenta })

                _Validacion.ContabilidadMetObligatorio(Campo);
            Disposed += (Remitente, Argumentos) => _Validacion.Dispose();
            ContabilidadColDebe.DefaultCellStyle.FormatProvider = _Cultura;
            ContabilidadColHaber.DefaultCellStyle.FormatProvider = _Cultura;
            ContabilidadMetCargarCatalogos();
            ContabilidadDgvDetalle.DataSource = _Lineas;
            ContabilidadDtpFecha.ValueChanged += ContabilidadMetActualizarPeriodo;
            ContabilidadBtnAgregar.Click += ContabilidadMetAgregarCuenta;
            ContabilidadBtnNueva.Click += ContabilidadMetNueva;
            ContabilidadBtnLimpiarDetalle.Click += ContabilidadMetLimpiarDetalle;
            ContabilidadBtnAplicar.Click += ContabilidadMetAplicar;
            ContabilidadBtnGuardar.Click += ContabilidadMetGuardar;
            ContabilidadDgvDetalle.CellContentClick += ContabilidadMetQuitarCuenta;
            ContabilidadDgvDetalle.CellEndEdit += ContabilidadMetTerminarEdicion;
            ContabilidadDgvDetalle.CellParsing += ContabilidadMetInterpretarImporte;
            ContabilidadDgvDetalle.DataError += ContabilidadMetErrorImporte;
            ContabilidadDgvDetalle.ColumnWidthChanged += ContabilidadMetAlinearTotales;
            ContabilidadDgvDetalle.SizeChanged += ContabilidadMetAlinearTotales;
            _Lineas.ListChanged += ContabilidadMetCambiarDetalle;
            ContabilidadMetNueva(this, EventArgs.Empty);

        }

        // Nombre del módulo ERP según su id.
        private static string ContabilidadFuncNombreModulo(int idModulo)
        {
            switch (idModulo)
            {
                case 1: return "Logística";
                case 2: return "Ventas";
                case 3: return "Compras";
                case 4: return "Recursos Humanos";
                case 5: return "Bancos";
                default: return "Encabezado";
            }
        }

        // Deshabilita los botones que el usuario no puede usar.
        private void ContabilidadMetAplicarPermisos(ClsPermisosPantallaContabilidad Permisos)
        {
            ClsPermisosMenuContabilidad.ContabilidadMetRestringir(ContabilidadBtnNueva, Permisos.PuedeInsertar);
            ClsPermisosMenuContabilidad.ContabilidadMetRestringir(ContabilidadBtnAgregar, Permisos.PuedeInsertar);
            ClsPermisosMenuContabilidad.ContabilidadMetRestringir(ContabilidadBtnGuardar, Permisos.PuedeInsertar);
            ClsPermisosMenuContabilidad.ContabilidadMetRestringir(ContabilidadBtnAplicar, Permisos.PuedeEditar);
            ClsPermisosMenuContabilidad.ContabilidadMetRestringir(ContabilidadBtnLimpiarDetalle, Permisos.PuedeEliminar);
        }

        // Llena cuentas, estados y orígenes.
        private void ContabilidadMetCargarCatalogos()
        {
            ContabilidadCboCuenta.DisplayMember = "Nombre";
            ContabilidadCboCuenta.ValueMember = "IdCuenta";
            var controladorCuentas = new ClsModeloCuentaContabilidad();
            ContabilidadCboCuenta.DataSource = controladorCuentas.ContabilidadFuncObtenerTodos(_idModulo);
            // ContabilidadCboCuenta.DataSource = _Controlador.ContabilidadFuncObtenerCuentas(_idModulo);
            ContabilidadCboEstado.DataSource = _Controlador.ContabilidadFuncObtenerEstados();
            ContabilidadCboOrigen.DataSource = _Controlador.ContabilidadFuncObtenerOrigenes();
        }

        private void ContabilidadMetActualizarPeriodo(object Remitente, EventArgs Argumentos)
        {
            ContabilidadTxtPeriodo.Text = _Controlador.ContabilidadFuncObtenerPeriodo(ContabilidadDtpFecha.Value);
        }

        private void ContabilidadMetAgregarCuenta(object Remitente, EventArgs Argumentos)
        {
            ContabilidadDgvDetalle.EndEdit();
            string Nombre = ContabilidadCboCuenta.Text.Trim();

            ClsModeloCuentaContabilidad Cuenta = ContabilidadCboCuenta.Items
                .Cast<ClsModeloCuentaContabilidad>()
                .FirstOrDefault(Elemento => string.Equals(Elemento.Nombre, Nombre, StringComparison.CurrentCultureIgnoreCase));

            if (Cuenta == null)
            {
                _Validacion.ContabilidadFuncValidar(ContabilidadCboCuenta, "Seleccione una cuenta válida del catálogo.");
                ContabilidadCboCuenta.Focus();
                return;
            }

            int IdCuenta = Cuenta.IdCuenta;
            if (_Lineas.Any(Linea => Linea.IdCuenta == IdCuenta))
            {
                _Validacion.ContabilidadFuncValidar(ContabilidadCboCuenta, "La cuenta ya está agregada al detalle.");
                return;
            }

            _Lineas.Add(new ClsLineaPolizaContabilidad
            {
                IdCuenta = IdCuenta,
                Cuenta = Cuenta.Nombre,
                Debe = 0m,
                Haber = 0m
            });

            int Indice = _Lineas.Count - 1;
            ContabilidadDgvDetalle.CurrentCell = ContabilidadDgvDetalle.Rows[Indice].Cells[ContabilidadColDebe.Index];
            ContabilidadDgvDetalle.Focus();
            ContabilidadDgvDetalle.BeginEdit(true);
            ContabilidadCboCuenta.SelectedIndex = -1;
            ContabilidadCboCuenta.Text = string.Empty;
        }

        // Quita la línea seleccionada.
        private void ContabilidadMetQuitarCuenta(object Remitente, DataGridViewCellEventArgs Argumentos)
        {
            if (Argumentos.RowIndex < 0 || Argumentos.ColumnIndex != ContabilidadColQuitar.Index) return;
            // Quitar una cuenta es eliminar: requiere ese permiso.
            if (!_Permisos.PuedeEliminar) return;
            ContabilidadDgvDetalle.EndEdit();
            ClsLineaPolizaContabilidad Linea = ContabilidadDgvDetalle.Rows[Argumentos.RowIndex].DataBoundItem as ClsLineaPolizaContabilidad;
            if (Linea != null) _Lineas.Remove(Linea);
        }

        // Convierte el texto a importe con dos decimales.
        private void ContabilidadMetInterpretarImporte(object Remitente, DataGridViewCellParsingEventArgs Argumentos)
        {
            if (Argumentos.ColumnIndex != ContabilidadColDebe.Index && Argumentos.ColumnIndex != ContabilidadColHaber.Index) return;
            string Texto = Convert.ToString(Argumentos.Value, _Cultura);
            decimal Importe;
            if (string.IsNullOrWhiteSpace(Texto)) Importe = 0m;
            else if (!decimal.TryParse(Texto, NumberStyles.Number, _Cultura, out Importe)) return;
            // Dentro del límite de EST-01 y con dos decimales también en el valor enlazado.
            if (Importe < -ClsLimitesContabilidad.MaximoMonetario || Importe > ClsLimitesContabilidad.MaximoMonetario)
            {
                Argumentos.Value = "Importe fuera de rango";
                return;
            }
            Argumentos.Value = decimal.Round(Importe, 2, MidpointRounding.AwayFromZero);
            Argumentos.ParsingApplied = true;
        }

        // Avisa si el importe no es válido.
        private void ContabilidadMetErrorImporte(object Remitente, DataGridViewDataErrorEventArgs Argumentos)
        {
            Argumentos.ThrowException = false;
            Argumentos.Cancel = false;
            if (Argumentos.RowIndex < 0 ||
                (Argumentos.ColumnIndex != ContabilidadColDebe.Index && Argumentos.ColumnIndex != ContabilidadColHaber.Index)) return;
            ContabilidadDgvDetalle.CancelEdit();
            ClsLineaPolizaContabilidad Linea = ContabilidadDgvDetalle.Rows[Argumentos.RowIndex].DataBoundItem as ClsLineaPolizaContabilidad;
            if (Linea != null)
            {
                if (Argumentos.ColumnIndex == ContabilidadColDebe.Index) Linea.Debe = 0m;
                else Linea.Haber = 0m;
                _Lineas.ResetItem(Argumentos.RowIndex);
            }
            ContabilidadMetRecalcularTotales();
            ClsDialogosContabilidad.ContabilidadMetError(this, "Importe inválido. Ingrese un número dentro del rango permitido. Se asignó 0.00.");
        }

        // Recalcula al terminar de editar.
        private void ContabilidadMetTerminarEdicion(object Remitente, DataGridViewCellEventArgs Argumentos)
        {
            ContabilidadMetRecalcularTotales();
        }

        // Recalcula al cambiar el detalle.
        private void ContabilidadMetCambiarDetalle(object Remitente, ListChangedEventArgs Argumentos)
        {
            ContabilidadMetRecalcularTotales();
        }

        // Actualiza totales, cuadre y botones.
        private void ContabilidadMetRecalcularTotales()
        {
            decimal Debe = _Controlador.ContabilidadFuncTotalDebe(_Lineas);
            decimal Haber = _Controlador.ContabilidadFuncTotalHaber(_Lineas);
            bool Cuadrada = _Controlador.ContabilidadFuncEstaCuadrada(_Lineas);
            ContabilidadLblTotalDebe.Text = "Total debe: Q " + Debe.ToString("N2", _Cultura);
            ContabilidadLblTotalHaber.Text = "Total haber: Q " + Haber.ToString("N2", _Cultura);
            if (_Lineas.Count == 0)
            {
                ContabilidadLblCuadre.Text = "Agregue las cuentas de la póliza";
                ContabilidadLblCuadre.ForeColor = Color.FromArgb(70, 104, 125);
            }
            else
            {
                ContabilidadLblCuadre.Text = Cuadrada ? "Cuadrada" : "Descuadrada · diferencia Q " + Math.Abs(Debe - Haber).ToString("N2", _Cultura);
                ContabilidadLblCuadre.ForeColor = Cuadrada ? Color.FromArgb(70, 104, 125) : Color.FromArgb(197, 143, 62);
            }
            ContabilidadBtnAplicar.Enabled = Cuadrada && _Lineas.Count >= 2;
            ContabilidadBtnGuardar.Enabled = _Lineas.Count > 0;
            ContabilidadBtnLimpiarDetalle.Enabled = _Lineas.Count > 0;
            ContabilidadMetAlinearTotales(this, EventArgs.Empty);
        }

        // Alinea los totales con sus columnas.
        private void ContabilidadMetAlinearTotales(object Remitente, EventArgs Argumentos)
        {
            if (Disposing || IsDisposed || ContabilidadDgvDetalle.Disposing || ContabilidadDgvDetalle.IsDisposed ||
                ContabilidadColDebe.Index < 0 || ContabilidadColHaber.Index < 0) return;
            Rectangle AreaDebe = ContabilidadDgvDetalle.GetColumnDisplayRectangle(ContabilidadColDebe.Index, false);
            Rectangle AreaHaber = ContabilidadDgvDetalle.GetColumnDisplayRectangle(ContabilidadColHaber.Index, false);
            ContabilidadLblTotalDebe.Left = ContabilidadDgvDetalle.Left + AreaDebe.Left;
            ContabilidadLblTotalDebe.Width = AreaDebe.Width;
            ContabilidadLblTotalHaber.Left = ContabilidadDgvDetalle.Left + AreaHaber.Left;
            ContabilidadLblTotalHaber.Width = AreaHaber.Width;
        }

        // Vacía el detalle.
        private void ContabilidadMetLimpiarDetalle(object Remitente, EventArgs Argumentos)
        {
            ContabilidadDgvDetalle.CancelEdit();
            _Lineas.Clear();
        }

        // Prepara una póliza nueva.
        private void ContabilidadMetNueva(object Remitente, EventArgs Argumentos)
        {
            _Validacion.ContabilidadMetLimpiar();
            ContabilidadMetLimpiarDetalle(Remitente, Argumentos);
            ContabilidadDtpFecha.Value = DateTime.Today;
            ContabilidadMetActualizarPeriodo(this, EventArgs.Empty);
            ContabilidadCboEstado.SelectedItem = "BORRADOR";
            ContabilidadCboOrigen.SelectedItem = "MANUAL";
            ContabilidadTxtDescripcion.Clear();
            ContabilidadCboCuenta.SelectedIndex = -1;
            ContabilidadCboCuenta.Text = string.Empty;
            ContabilidadMetRecalcularTotales();
            ContabilidadTxtDescripcion.Focus();
        }


        // Valida y graba la póliza. Si Aplicar es true, se graba con estado APLICADA.
        private void ContabilidadMetProcesarPoliza(bool Aplicar)
        {
            // 1. Cerrar cualquier celda en edición y recalcular.
            if (!ContabilidadDgvDetalle.EndEdit()) return;
            ContabilidadMetRecalcularTotales();

            // 2. Descripción obligatoria (marca el campo en rojo).
            if (!_Validacion.ContabilidadFuncValidar(ContabilidadTxtDescripcion,
                string.IsNullOrWhiteSpace(ContabilidadTxtDescripcion.Text) ? "Escriba la descripción de la póliza." : null)) return;

            // 3. Validación rápida del Controlador (líneas, cuadre, etc.).
            string Motivo = _Controlador.ContabilidadFuncValidar(ContabilidadTxtDescripcion.Text, _Lineas);
            if (!string.IsNullOrEmpty(Motivo))
            {
                ClsDialogosContabilidad.ContabilidadMetError(this, Motivo);
                return;
            }

            // 4. Confirmar antes de aplicar.
            if (Aplicar && MessageBox.Show(this,
                    "¿Desea aplicar la póliza? Una póliza aplicada ya no se podrá modificar.",
                    "Aplicar póliza", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // 5. Leer los valores del formulario.
            string Estado = Aplicar ? "APLICADA" : Convert.ToString(ContabilidadCboEstado.SelectedItem);
            string Origen = Convert.ToString(ContabilidadCboOrigen.SelectedItem);

            ClsModeloPolizaContabilidad Modelo = new ClsModeloPolizaContabilidad
            {
                Descripcion = ContabilidadTxtDescripcion.Text.Trim(),
                Fecha = ContabilidadDtpFecha.Value.Date,
                Estado = Estado,
                Origen = Origen,
                Lineas = _Lineas.ToList()
            };

            // 6. El Controlador valida y graba; la Vista solo muestra el resultado.
            string Mensaje;
            if (_Controlador.ContabilidadFuncGrabarCambios(EstadoEntidadContabilidad.Agregar, Modelo, out Mensaje))
            {
                ClsDialogosContabilidad.ContabilidadMetExito(this, Mensaje);
                ContabilidadMetNueva(this, EventArgs.Empty);
            }
            else
            {
                ClsDialogosContabilidad.ContabilidadMetError(this, Mensaje);
            }
        }

        // Graba la póliza con el estado del combo.
        private void ContabilidadMetGuardar(object Remitente, EventArgs Argumentos)
        {
            ContabilidadMetProcesarPoliza(false);
        }

        // Graba la póliza con estado APLICADA.
        private void ContabilidadMetAplicar(object Remitente, EventArgs Argumentos)
        {
            ContabilidadMetProcesarPoliza(true);
        }
    }
}
