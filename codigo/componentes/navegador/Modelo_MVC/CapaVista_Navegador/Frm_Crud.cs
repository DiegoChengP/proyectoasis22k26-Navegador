using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CapaControlador_Navegador;

namespace CapaVista_Navegador
{
    public partial class Frm_Crud : Form
    {
        // cambiar nombre de la tabla a la que se desea hacer el CRUD
        private string nombreTabla = "tbl_puestos";
        private Controlador controlador = new Controlador();

        private DataGridView dgvDatos;
        private DataTable esquemaActual;

        private bool modoModificar = false;

        private Panel panelRegistro;
        private Dictionary<string, Control> controlesRegistro;

        private Button btnGuardarRegistro;
        private Button btnCancelarRegistro;

        //Modificación realizada por: Natali Sofía Montenegro Portillo validaciones de permisos del MVC
        private string _UsuarioActual = "gerente1";
        private string _CodigoModulo = "123";

        private ClsPermisoControlador _PermisoControlador =
            new ClsPermisoControlador();

        private Dictionary<string, string> clavesPrimariasModificar =
            new Dictionary<string, string>();

        public string NombreTabla
        {
            get
            {
                return nombreTabla;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    nombreTabla = value.Trim();
                    modoModificar = false;

                    if (dgvDatos != null)
                    {
                        ConsultarTabla();
                    }
                }
            }
        }

        public Frm_Crud()
            : this(
                "USUARIO_PRUEBA",
                "EMPLEADOS",
                "tbl_puestos")
        {
        }

        public Frm_Crud(
            string UsuarioActual,
            string CodigoModulo)
            : this(
                UsuarioActual,
                CodigoModulo,
                "tbl_puestos")
        {
        }

        public Frm_Crud(
            string UsuarioActual,
            string CodigoModulo,
            string tabla)
        {
            InitializeComponent();



            // FIX: antes no se asignaban los parámetros a los campos, así que
            // siempre se usaban los valores por defecto ("gerente1"/"123")
            // sin importar qué usuario/módulo se pasara al constructor.

            _UsuarioActual = UsuarioActual;
            _CodigoModulo = CodigoModulo;

            NombreTabla = tabla;

            // FIX: se usa -= antes de += para garantizar una sola suscripción
            // por evento, sin importar si el Designer ya lo enganchó.


            Btn_ingresar.Click -= Btn_ingresar_Click;
            Btn_ingresar.Click += Btn_ingresar_Click;

            Btn_cancelar.Click -= Btn_cancelar_Click;
            Btn_cancelar.Click += Btn_cancelar_Click;

            Btn_Consultar.Click -= Btn_Consultar_Click;
            Btn_Consultar.Click += Btn_Consultar_Click;

            Btn_refrescar.Click -= Btn_refrescar_Click;
            Btn_refrescar.Click += Btn_refrescar_Click;

            Btn_modificar.Click -= Btn_modificar_Click;
            Btn_modificar.Click += Btn_modificar_Click;

            Btn_eliminar.Click -= Btn_eliminar_Click;
            Btn_eliminar.Click += Btn_eliminar_Click;

            Btn_guardar.Click -= Btn_guardar_Click;
            Btn_guardar.Click += Btn_guardar_Click;

            Btn_salir.Click -= Btn_salir_Click;
            Btn_salir.Click += Btn_salir_Click;

            Load += Frm_Crud_Load;
            Resize += Frm_Crud_Resize;
        }

        private void Frm_Crud_Load(
            object sender,
            EventArgs e)
        {
            if (dgvDatos != null)
            {
                dgvDatos.Visible = false;
            }
        }

        private void Frm_Crud_Resize(
            object sender,
            EventArgs e)
        {
            PosicionarControles();
        }

        private bool TieneAcceso()
        {
            // Garantizar que la instancia exista usando el nombre correcto con guion bajo (_)

            if (_PermisoControlador == null)
            {
                _PermisoControlador =
                    new ClsPermisoControlador();
            }

            // Si no hay usuario o módulo asignado, permitir acceso de prueba

            if (string.IsNullOrEmpty(_UsuarioActual) ||
                string.IsNullOrEmpty(_CodigoModulo))
            {
                return true;
            }

            try
            {
                return _PermisoControlador.ValidarAcceso(
                    _UsuarioActual,
                    _CodigoModulo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al validar los permisos: " +
                    ex.Message,
                    "Error de seguridad",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }
        }


        // Ingresar


        private void Btn_ingresar_Click(
            object sender,
            EventArgs e)
        {
            if (!TieneAcceso())
            {
                return;
            }

            if (dgvDatos != null)
            {
                dgvDatos.Visible = false;
            }

            modoModificar = false;

            clavesPrimariasModificar =
                new Dictionary<string, string>();

            CrearFormularioRegistro(
                false,
                null);
        }

        // Consulta

        private void Btn_Consultar_Click(
            object sender,
            EventArgs e)
        {
            if (!TieneAcceso())
            {
                return;
            }

            CerrarFormularioRegistro();

            ConsultarTabla();
        }

        // Refrescar

        private void Btn_refrescar_Click(
            object sender,
            EventArgs e)
        {
            if (!TieneAcceso())
            {
                return;
            }

            CerrarFormularioRegistro();

            if (dgvDatos != null)
            {
                dgvDatos.Visible = false;
            }
        }


        // Modificar

        private void Btn_modificar_Click(
            object sender,
            EventArgs e)
        {
            if (!TieneAcceso())
            {
                return;
            }

            if (dgvDatos == null ||
                dgvDatos.CurrentRow == null ||
                dgvDatos.CurrentRow.IsNewRow)
            {
                MessageBox.Show(
                    "Seleccione un registro en la tabla para modificar.",
                    "Modificar registro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            try
            {
                clavesPrimariasModificar =
                    ObtenerClavesPrimarias(
                        dgvDatos.CurrentRow);

                if (clavesPrimariasModificar.Count == 0)
                {
                    MessageBox.Show(
                        "No se pudo obtener la llave primaria del registro seleccionado.",
                        "Modificar registro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                foreach (
                    KeyValuePair<string, string> clave
                    in clavesPrimariasModificar)
                {
                    if (string.IsNullOrWhiteSpace(clave.Value))
                    {
                        MessageBox.Show(
                            "La llave primaria del registro seleccionado no tiene un valor válido.",
                            "Modificar registro",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }
                }

                modoModificar = true;

                CrearFormularioRegistro(
                    true,
                    dgvDatos.CurrentRow);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ObtenerMensajeAmigable(ex),
                    "Error al modificar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Eliminar


        private void Btn_eliminar_Click(
            object sender,
            EventArgs e)
        {
            if (!TieneAcceso())
            {
                return;
            }

            if (dgvDatos == null ||
                dgvDatos.CurrentRow == null ||
                dgvDatos.CurrentRow.IsNewRow)
            {
                MessageBox.Show(
                    "Seleccione un registro en la tabla para eliminar.",
                    "Eliminar registro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            try
            {
                Dictionary<string, string> pk =
                    ObtenerClavesPrimarias(
                        dgvDatos.CurrentRow);

                if (pk.Count == 0)
                {
                    MessageBox.Show(
                        "La tabla no tiene una llave primaria detectable.",
                        "Eliminar registro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                foreach (
                    KeyValuePair<string, string> clave
                    in pk)
                {
                    if (string.IsNullOrWhiteSpace(clave.Value))
                    {
                        MessageBox.Show(
                            "No se pudo obtener el valor de la llave primaria del registro seleccionado.",
                            "Eliminar registro",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }
                }

                DialogResult respuesta =
                    MessageBox.Show(
                        "¿Desea eliminar el registro seleccionado?",
                        "Confirmar eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes)
                {
                    return;
                }

                if (controlador.EliminarRegistro(
                    nombreTabla,
                    pk))
                {
                    MessageBox.Show(
                        "Registro eliminado correctamente.",
                        "Eliminación exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    clavesPrimariasModificar =
                        new Dictionary<string, string>();

                    ConsultarTabla();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ObtenerMensajeAmigable(ex),
                    "Error al eliminar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // GUARDAR
        // =========================================================

        private void Btn_guardar_Click(
            object sender,
            EventArgs e)
        {
            if (panelRegistro != null &&
                panelRegistro.Visible)
            {
                GuardarFormularioRegistro();
                return;
            }

            MessageBox.Show(
                "Abra un registro con el botón Modificar antes de guardar.",
                "Guardar registro",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // =========================================================
        // SALIR
        // =========================================================

        private void Btn_salir_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }

        // =========================================================
        // CONSULTAR TABLA
        // =========================================================

        private void ConsultarTabla()
        {
            try
            {
                DataTable datos =
                    controlador.llenarDgv(
                        nombreTabla);

                esquemaActual =
                    controlador.ObtenerEsquemaTabla(
                        nombreTabla);

                if (dgvDatos == null)
                {
                    dgvDatos =
                        new DataGridView();

                    dgvDatos.Name =
                        "dgvDatos";

                    dgvDatos.AllowUserToAddRows =
                        false;

                    dgvDatos.AllowUserToDeleteRows =
                        false;

                    dgvDatos.AutoSizeColumnsMode =
                        DataGridViewAutoSizeColumnsMode
                            .DisplayedCells;

                    dgvDatos.SelectionMode =
                        DataGridViewSelectionMode
                            .FullRowSelect;

                    dgvDatos.MultiSelect =
                        false;

                    dgvDatos.ReadOnly =
                        true;

                    dgvDatos.BackgroundColor =
                        Color.White;

                    dgvDatos.Anchor =
                        AnchorStyles.Top |
                        AnchorStyles.Bottom |
                        AnchorStyles.Left |
                        AnchorStyles.Right;

                    Controls.Add(dgvDatos);
                }

                dgvDatos.DataSource =
                    datos;

                dgvDatos.Visible =
                    true;

                dgvDatos.ReadOnly =
                    true;

                foreach (
                    DataGridViewColumn columna
                    in dgvDatos.Columns)
                {
                    columna.ReadOnly = true;
                }

                PosicionarControles();

                Text =
                    "CRUD - " +
                    nombreTabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ObtenerMensajeAmigable(ex),
                    "Error al consultar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Posicion control

        private void PosicionarControles()
        {
            if (dgvDatos == null ||
                !dgvDatos.Visible)
            {
                return;
            }

            int margen = 10;

            int posicionY = 450;

            if (panelRegistro != null &&
                panelRegistro.Visible)
            {
                posicionY =
                    panelRegistro.Bottom + 10;
            }

            dgvDatos.Location =
                new Point(
                    margen,
                    posicionY);

            dgvDatos.Size =
                new Size(
                    Math.Max(
                        100,
                        ClientSize.Width - (margen * 2)),
                    Math.Max(
                        100,
                        ClientSize.Height -
                        posicionY -
                        margen));

            dgvDatos.BringToFront();

            if (panelRegistro != null &&
                panelRegistro.Visible)
            {
                panelRegistro.BringToFront();
            }

            Btn_ingresar.BringToFront();
            Btn_cancelar.BringToFront();
            Btn_Consultar.BringToFront();
            Btn_eliminar.BringToFront();
            Btn_refrescar.BringToFront();
            Btn_modificar.BringToFront();
            Btn_anterior.BringToFront();
            Btn_inicio.BringToFront();
            Btn_fin.BringToFront();
            Btn_siguiente.BringToFront();
            Btn_imprimir.BringToFront();
            Btn_guardar.BringToFront();
            Btn_ayuda.BringToFront();
            Btn_salir.BringToFront();
        }

        // =========================================================
        // CREAR FORMULARIO DE REGISTRO
        // =========================================================

        private void CrearFormularioRegistro(
            bool modificar,
            DataGridViewRow filaSeleccionada)
        {
            try
            {
                DataTable esquema =
                    controlador.ObtenerEsquemaTabla(
                        nombreTabla);

                esquemaActual =
                    esquema;

                CerrarFormularioRegistro();

                // Si acabamos de cerrar el formulario,
                // restauramos el modo que necesitamos.
                modoModificar = modificar;

                panelRegistro =
                    new Panel();

                panelRegistro.Name =
                    "panelRegistro";

                panelRegistro.Location =
                    new Point(
                        10,
                        450);

                panelRegistro.Width =
                    ClientSize.Width - 20;

                int altura =
                    60 +
                    esquema.Rows.Count * 42;

                if (altura < 180)
                {
                    altura = 180;
                }

                if (altura > 400)
                {
                    altura = 400;
                }

                panelRegistro.Height =
                    altura;

                panelRegistro.BackColor =
                    Color.Beige;

                panelRegistro.BorderStyle =
                    BorderStyle.FixedSingle;

                panelRegistro.AutoScroll =
                    true;

                panelRegistro.Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right;

                Controls.Add(
                    panelRegistro);

                controlesRegistro =
                    new Dictionary<string, Control>();

                int posicionY = 10;

                foreach (DataRow columna
                    in esquema.Rows)
                {
                    string campo =
                        Convert.ToString(
                            columna["COLUMN_NAME"]);

                    string tipo =
                        Convert.ToString(
                            columna["DATA_TYPE"]);

                    bool esPk =
                        ObtenerBooleanoEsquema(
                            columna,
                            "IS_PRIMARY_KEY");

                    bool esFk =
                        ObtenerBooleanoEsquema(
                            columna,
                            "IS_FOREIGN_KEY");

                    bool esAuto =
                        ObtenerBooleanoEsquema(
                            columna,
                            "IS_AUTOINCREMENT");

                    string tablaFk =
                        ObtenerTextoEsquema(
                            columna,
                            "FK_TABLE_NAME");

                    string columnaFk =
                        ObtenerTextoEsquema(
                            columna,
                            "FK_COLUMN_NAME");

                    Label etiqueta =
                        new Label();

                    etiqueta.Text =
                        campo +
                        (esPk
                            ? " [PK]"
                            : "") +
                        (esFk
                            ? " [FK]"
                            : "");

                    etiqueta.Location =
                        new Point(
                            15,
                            posicionY + 4);

                    etiqueta.AutoSize =
                        true;

                    Control control;

                    // =================================================
                    // FK = COMBOBOX
                    // =================================================

                    if (esFk &&
                        !string.IsNullOrWhiteSpace(
                            tablaFk) &&
                        !string.IsNullOrWhiteSpace(
                            columnaFk))
                    {
                        ComboBox combo =
                            CrearComboLlaveForanea(
                                tablaFk,
                                columnaFk,
                                filaSeleccionada,
                                campo);

                        combo.Location =
                            new Point(
                                190,
                                posicionY);

                        combo.Width =
                            250;

                        // La FK se selecciona desde las opciones
                        // existentes en la base de datos.
                        // No se escribe manualmente.
                        combo.Enabled =
                            !esPk;

                        control =
                            combo;
                    }

                    // =================================================
                    // FECHA
                    // =================================================

                    else if (EsFecha(tipo))
                    {
                        DateTimePicker fecha =
                            new DateTimePicker();

                        fecha.Name =
                            "dtp_" +
                            campo;

                        fecha.Location =
                            new Point(
                                190,
                                posicionY);

                        fecha.Width =
                            250;

                        fecha.Format =
                            DateTimePickerFormat.Short;

                        fecha.Value =
                            ObtenerFechaInicial(
                                filaSeleccionada,
                                campo);

                        fecha.Enabled =
                            !esPk;

                        control =
                            fecha;
                    }

                    // =================================================
                    // BOOLEANO
                    // =================================================

                    else if (EsBooleano(tipo))
                    {
                        CheckBox check =
                            new CheckBox();

                        check.Name =
                            "chk_" +
                            campo;

                        check.Location =
                            new Point(
                                190,
                                posicionY);

                        check.Width =
                            250;

                        check.Checked =
                            ObtenerBooleanoInicial(
                                filaSeleccionada,
                                campo);

                        check.Enabled =
                            !esPk;

                        control =
                            check;
                    }

                    // =================================================
                    // TEXTO / NUMERO
                    // =================================================

                    else
                    {
                        TextBox caja =
                            new TextBox();

                        caja.Name =
                            "txt_" +
                            campo;

                        caja.Location =
                            new Point(
                                190,
                                posicionY);

                        caja.Width =
                            250;

                        caja.Text =
                            ObtenerTextoInicial(
                                filaSeleccionada,
                                campo);

                        // Al insertar:
                        // PK y autoincremento son automáticos.
                        if (!modificar &&
                            (esPk || esAuto))
                        {
                            caja.Enabled =
                                false;

                            caja.Text =
                                "(automático)";
                        }

                        // Al modificar:
                        // PK no se puede cambiar.
                        if (modificar &&
                            esPk)
                        {
                            caja.ReadOnly =
                                true;

                            caja.BackColor =
                                Color.LightGray;
                        }

                        control =
                            caja;
                    }

                    // =================================================
                    // BLOQUEAR PK
                    // =================================================

                    if (esPk)
                    {
                        control.Enabled =
                            false;
                    }

                    panelRegistro.Controls.Add(
                        etiqueta);

                    panelRegistro.Controls.Add(
                        control);

                    controlesRegistro[campo] =
                        control;

                    posicionY += 42;
                }

                // =====================================================
                // BOTON GUARDAR
                // =====================================================

                btnGuardarRegistro =
                    new Button();

                btnGuardarRegistro.Text =
                    modificar
                        ? "Guardar cambios"
                        : "Guardar";

                btnGuardarRegistro.Width =
                    120;

                btnGuardarRegistro.Height =
                    32;

                btnGuardarRegistro.Location =
                    new Point(
                        190,
                        posicionY + 5);

                btnGuardarRegistro.Click +=
                    BtnGuardarRegistro_Click;

                panelRegistro.Controls.Add(
                    btnGuardarRegistro);

                // =====================================================
                // BOTON CANCELAR
                // =====================================================

                btnCancelarRegistro =
                    new Button();

                btnCancelarRegistro.Text =
                    "Cancelar";

                btnCancelarRegistro.Width =
                    100;

                btnCancelarRegistro.Height =
                    32;

                btnCancelarRegistro.Location =
                    new Point(
                        320,
                        posicionY + 5);

                btnCancelarRegistro.Click +=
                    BtnCancelarRegistro_Click;

                panelRegistro.Controls.Add(
                    btnCancelarRegistro);

                panelRegistro.Visible =
                    true;

                PosicionarControles();

                panelRegistro.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ObtenerMensajeAmigable(ex),
                    "Error al abrir el formulario",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // GUARDAR FORMULARIO
        // =========================================================

        private void BtnGuardarRegistro_Click(
            object sender,
            EventArgs e)
        {
            GuardarFormularioRegistro();
        }

        private void BtnCancelarRegistro_Click(
            object sender,
            EventArgs e)
        {
            CerrarFormularioRegistro();
        }

        private void GuardarFormularioRegistro()
        {
            if (controlesRegistro == null ||
                esquemaActual == null)
            {
                return;
            }

            try
            {
                Dictionary<string, string> datos =
                    new Dictionary<string, string>();

                // =====================================================
                // OBTENER DATOS DEL FORMULARIO
                // =====================================================

                foreach (DataRow columna
                    in esquemaActual.Rows)
                {
                    string campo =
                        Convert.ToString(
                            columna["COLUMN_NAME"]);

                    string tipo =
                        Convert.ToString(
                            columna["DATA_TYPE"]);

                    bool esPk =
                        ObtenerBooleanoEsquema(
                            columna,
                            "IS_PRIMARY_KEY");

                    bool esAuto =
                        ObtenerBooleanoEsquema(
                            columna,
                            "IS_AUTOINCREMENT");

                    Control control;

                    if (!controlesRegistro.TryGetValue(
                        campo,
                        out control))
                    {
                        continue;
                    }

                    // En INSERTAR:
                    // PK y autoincremento no se mandan.
                    if (!modoModificar &&
                        (esPk || esAuto))
                    {
                        continue;
                    }

                    // En MODIFICAR:
                    // La PK jamás se actualiza.
                    if (modoModificar &&
                        esPk)
                    {
                        continue;
                    }

                    string valor =
                        ObtenerValorControl(
                            control,
                            tipo);

                    bool nullable =
                        string.Equals(
                            ObtenerTextoEsquema(
                                columna,
                                "IS_NULLABLE"),
                            "YES",
                            StringComparison
                                .OrdinalIgnoreCase);

                    if (string.IsNullOrWhiteSpace(valor))
                    {
                        if (!nullable)
                        {
                            MessageBox.Show(
                                "El campo '" +
                                campo +
                                "' es obligatorio.",
                                "Validación",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            control.Focus();

                            return;
                        }

                        continue;
                    }

                    datos[campo] =
                        valor;
                }

                // =====================================================
                // VALIDAR
                // =====================================================

                List<string> errores =
                    controlador.ValidarRegistro(
                        datos,
                        nombreTabla);

                if (errores.Count > 0)
                {
                    MessageBox.Show(
                        string.Join(
                            "\n",
                            errores),
                        "Errores de validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // =====================================================
                // INSERTAR
                // =====================================================

                if (!modoModificar)
                {
                    if (controlador.InsertarRegistro(
                        nombreTabla,
                        datos))
                    {
                        MessageBox.Show(
                            "Registro guardado correctamente.",
                            "Guardado exitoso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        CerrarFormularioRegistro();

                        ConsultarTabla();
                    }

                    return;
                }

                // =====================================================
                // MODIFICAR
                // =====================================================

                Dictionary<string, string> pk =
                    new Dictionary<string, string>(
                        clavesPrimariasModificar);

                if (pk.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontró la llave primaria del registro seleccionado.",
                        "Modificar registro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                foreach (
                    KeyValuePair<string, string> clave
                    in pk)
                {
                    if (string.IsNullOrWhiteSpace(clave.Value))
                    {
                        MessageBox.Show(
                            "La llave primaria original no tiene un valor válido.",
                            "Modificar registro",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }
                }

                // Los datos contienen únicamente campos
                // que sí pueden modificarse.
                Dictionary<string, string> valores =
                    new Dictionary<string, string>();

                foreach (
                    KeyValuePair<string, string> dato
                    in datos)
                {
                    if (!EsLlavePrimaria(
                        dato.Key))
                    {
                        valores[dato.Key] =
                            dato.Value;
                    }
                }

                if (valores.Count == 0)
                {
                    MessageBox.Show(
                        "No hay campos disponibles para modificar.",
                        "Modificar registro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                // ActualizarRegistro utiliza la PK original
                // para modificar exactamente el registro seleccionado.
                if (controlador.ActualizarRegistro(
                    nombreTabla,
                    valores,
                    pk))
                {
                    MessageBox.Show(
                        "Registro modificado correctamente.",
                        "Modificación exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    CerrarFormularioRegistro();

                    clavesPrimariasModificar =
                        new Dictionary<string, string>();

                    ConsultarTabla();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo modificar el registro seleccionado.",
                        "Modificar registro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ObtenerMensajeAmigable(ex),
                    "Error al guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // CERRAR FORMULARIO
        // =========================================================

        private void CerrarFormularioRegistro()
        {
            if (panelRegistro != null)
            {
                Controls.Remove(
                    panelRegistro);

                panelRegistro.Dispose();

                panelRegistro =
                    null;
            }

            controlesRegistro =
                null;

            btnGuardarRegistro =
                null;

            btnCancelarRegistro =
                null;

            if (dgvDatos != null)
            {
                dgvDatos.ReadOnly =
                    true;

                dgvDatos.ClearSelection();
            }

            modoModificar = false;

            PosicionarControles();
        }

        // =========================================================
        // COMBOBOX PARA FK
        // =========================================================

        private ComboBox CrearComboLlaveForanea(
            string tablaFk,
            string columnaFk,
            DataGridViewRow fila,
            string campo)
        {
            ComboBox combo =
                new ComboBox();

            combo.Name =
                "cbo_" +
                campo;

            combo.DropDownStyle =
                ComboBoxStyle.DropDownList;

            try
            {
                DataTable opciones =
                    controlador.llenarDgv(
                        tablaFk);

                DataTable esquemaFk =
                    controlador.ObtenerEsquemaTabla(
                        tablaFk);

                string columnaMostrar =
                    columnaFk;

                // =====================================================
                // BUSCAR UNA COLUMNA DESCRIPTIVA
                // =====================================================

                foreach (DataRow c
                    in esquemaFk.Rows)
                {
                    string nombre =
                        Convert.ToString(
                            c["COLUMN_NAME"]);

                    string tipo =
                        Convert.ToString(
                            c["DATA_TYPE"])
                        .ToLowerInvariant();

                    if (nombre.Equals(
                        columnaFk,
                        StringComparison
                            .OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    if (tipo.Contains("char") ||
                        tipo.Contains("text"))
                    {
                        columnaMostrar =
                            nombre;

                        break;
                    }
                }

                if (!opciones.Columns.Contains(
                    columnaFk))
                {
                    return combo;
                }

                if (!opciones.Columns.Contains(
                    columnaMostrar))
                {
                    columnaMostrar =
                        columnaFk;
                }

                combo.DataSource =
                    opciones;

                combo.ValueMember =
                    columnaFk;

                combo.DisplayMember =
                    columnaMostrar;

                combo.SelectedIndex =
                    -1;

                // =====================================================
                // SELECCIONAR EL VALOR ACTUAL
                // =====================================================

                if (fila != null &&
                    fila.DataGridView != null)
                {
                    int indice =
                        ObtenerIndiceColumna(
                            fila.DataGridView,
                            campo);

                    if (indice >= 0)
                    {
                        object valorCelda =
                            fila.Cells[indice].Value;

                        if (valorCelda != null &&
                            valorCelda != DBNull.Value)
                        {
                            string valor =
                                Convert.ToString(
                                    valorCelda);

                            for (int i = 0;
                                i < combo.Items.Count;
                                i++)
                            {
                                DataRowView item =
                                    combo.Items[i]
                                    as DataRowView;

                                if (item == null)
                                {
                                    continue;
                                }

                                string valorItem =
                                    Convert.ToString(
                                        item.Row[
                                            columnaFk]);

                                if (string.Equals(
                                    valorItem,
                                    valor,
                                    StringComparison
                                        .OrdinalIgnoreCase))
                                {
                                    combo.SelectedIndex =
                                        i;

                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron cargar las opciones de la llave foránea '" +
                    campo +
                    "'.\n\n" +
                    ex.Message,
                    "Error al cargar opciones",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            return combo;
        }

        // =========================================================
        // OBTENER PK DEL REGISTRO SELECCIONADO
        // =========================================================

        private Dictionary<string, string>
            ObtenerClavesPrimarias(
                DataGridViewRow fila)
        {
            Dictionary<string, string> resultado =
                new Dictionary<string, string>();

            if (esquemaActual == null ||
                fila == null ||
                fila.DataGridView == null)
            {
                return resultado;
            }

            foreach (DataRow columna
                in esquemaActual.Rows)
            {
                bool esPk =
                    ObtenerBooleanoEsquema(
                        columna,
                        "IS_PRIMARY_KEY");

                if (!esPk)
                {
                    continue;
                }

                string nombre =
                    Convert.ToString(
                        columna["COLUMN_NAME"]);

                int indice =
                    ObtenerIndiceColumna(
                        fila.DataGridView,
                        nombre);

                if (indice < 0)
                {
                    continue;
                }

                object valor =
                    fila.Cells[indice].Value;

                resultado[nombre] =
                    valor == null ||
                    valor == DBNull.Value
                        ? ""
                        : Convert.ToString(
                            valor);
            }

            return resultado;
        }

        // =========================================================
        // OBTENER INDICE DE COLUMNA
        // =========================================================

        private int ObtenerIndiceColumna(
            DataGridView grid,
            string nombreCampo)
        {
            if (grid == null)
            {
                return -1;
            }

            foreach (DataGridViewColumn columna
                in grid.Columns)
            {
                if (string.Equals(
                    columna.DataPropertyName,
                    nombreCampo,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return columna.Index;
                }

                if (string.Equals(
                    columna.Name,
                    nombreCampo,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return columna.Index;
                }
            }

            return -1;
        }

        // =========================================================
        // COMPROBAR PK
        // =========================================================

        private bool EsLlavePrimaria(
            string nombreCampo)
        {
            if (esquemaActual == null)
            {
                return false;
            }

            foreach (DataRow fila
                in esquemaActual.Rows)
            {
                bool esPk =
                    ObtenerBooleanoEsquema(
                        fila,
                        "IS_PRIMARY_KEY");

                if (esPk &&
                    string.Equals(
                        Convert.ToString(
                            fila["COLUMN_NAME"]),
                        nombreCampo,
                        StringComparison
                            .OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        // =========================================================
        // OBTENER VALOR DEL CONTROL
        // =========================================================

        private string ObtenerValorControl(
            Control control,
            string tipo)
        {
            DateTimePicker fecha =
                control as DateTimePicker;

            if (fecha != null)
            {
                return fecha.Value.ToString(
                    "yyyy-MM-dd");
            }

            ComboBox combo =
                control as ComboBox;

            if (combo != null)
            {
                if (combo.SelectedIndex < 0 ||
                    combo.SelectedValue == null)
                {
                    return "";
                }

                return Convert.ToString(
                    combo.SelectedValue);
            }

            CheckBox check =
                control as CheckBox;

            if (check != null)
            {
                return check.Checked
                    ? "1"
                    : "0";
            }

            return control.Text.Trim();
        }

        // =========================================================
        // TEXTO INICIAL
        // =========================================================

        private string ObtenerTextoInicial(
            DataGridViewRow fila,
            string campo)
        {
            if (fila == null ||
                fila.DataGridView == null)
            {
                return "";
            }

            int indice =
                ObtenerIndiceColumna(
                    fila.DataGridView,
                    campo);

            if (indice < 0)
            {
                return "";
            }

            object valor =
                fila.Cells[indice].Value;

            return valor == null ||
                   valor == DBNull.Value
                ? ""
                : Convert.ToString(
                    valor);
        }

        // =========================================================
        // FECHA INICIAL
        // =========================================================

        private DateTime ObtenerFechaInicial(
            DataGridViewRow fila,
            string campo)
        {
            DateTime fecha;

            string texto =
                ObtenerTextoInicial(
                    fila,
                    campo);

            if (DateTime.TryParse(
                texto,
                out fecha))
            {
                return fecha;
            }

            return DateTime.Today;
        }

        // =========================================================
        // BOOLEANO INICIAL
        // =========================================================

        private bool ObtenerBooleanoInicial(
            DataGridViewRow fila,
            string campo)
        {
            string valor =
                ObtenerTextoInicial(
                    fila,
                    campo)
                .ToLowerInvariant();

            return valor == "1" ||
                   valor == "true" ||
                   valor == "yes" ||
                   valor == "si";
        }

        // =========================================================
        // DETECTAR FECHA
        // =========================================================

        private bool EsFecha(
            string tipo)
        {
            string t =
                (tipo ?? "")
                .ToLowerInvariant();

            return t.Contains("date") ||
                   t.Contains("time") ||
                   t.Contains("timestamp");
        }

        // =========================================================
        // DETECTAR BOOLEANO
        // =========================================================

        private bool EsBooleano(
            string tipo)
        {
            string t =
                (tipo ?? "")
                .ToLowerInvariant();

            return t == "bit" ||
                   t == "boolean" ||
                   t == "bool";
        }

        // =========================================================
        // VALIDAR CORREO
        // =========================================================

        private bool ValidarCorreo(
            string correo)
        {
            return Regex.IsMatch(
                correo,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // =========================================================
        // CANCELAR
        // =========================================================

        private void Btn_cancelar_Click(
            object sender,
            EventArgs e)
        {
            CerrarFormularioRegistro();

            clavesPrimariasModificar =
                new Dictionary<string, string>();
        }

        // =========================================================
        // OBTENER ESQUEMA
        // =========================================================

        public DataTable ObtenerEsquemaTabla()
        {
            try
            {
                return controlador
                    .ObtenerEsquemaTabla(
                        nombreTabla);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al obtener el esquema de la tabla:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return null;
            }
        }

        // =========================================================
        // OBTENER TEXTO DE ESQUEMA DE FORMA SEGURA
        // =========================================================

        private string ObtenerTextoEsquema(
            DataRow fila,
            string columna)
        {
            if (fila == null ||
                fila.Table == null ||
                !fila.Table.Columns.Contains(
                    columna) ||
                fila[columna] == DBNull.Value)
            {
                return "";
            }

            return Convert.ToString(
                fila[columna]);
        }

        // =========================================================
        // OBTENER BOOLEANO DE ESQUEMA DE FORMA SEGURA
        // =========================================================

        private bool ObtenerBooleanoEsquema(
            DataRow fila,
            string columna)
        {
            string valor =
                ObtenerTextoEsquema(
                    fila,
                    columna);

            if (string.IsNullOrWhiteSpace(valor))
            {
                return false;
            }

            bool resultado;

            if (bool.TryParse(
                valor,
                out resultado))
            {
                return resultado;
            }

            return valor == "1" ||
                   valor.Equals(
                       "YES",
                       StringComparison
                           .OrdinalIgnoreCase) ||
                   valor.Equals(
                       "SI",
                       StringComparison
                           .OrdinalIgnoreCase);
        }

        // =========================================================
        // MENSAJES AMIGABLES
        // =========================================================

        private string ObtenerMensajeAmigable(
            Exception ex)
        {
            string msg =
                ex.Message ?? "";

            string lower =
                msg.ToLowerInvariant();

            if (lower.Contains("foreign key") ||
                lower.Contains("fk_") ||
                lower.Contains("reference constraint"))
            {
                return
                    "El registro no puede guardarse o eliminarse " +
                    "porque existe una relación de llave foránea. " +
                    "Verifique los registros relacionados.";
            }

            if (lower.Contains("duplicate entry") ||
                lower.Contains("duplicate key") ||
                lower.Contains("unique constraint") ||
                lower.Contains("violation of unique") ||
                lower.Contains("violation of primary key"))
            {
                return
                    "Ya existe un registro con el mismo " +
                    "valor en un campo único.";
            }

            if (lower.Contains("cannot be null") ||
                lower.Contains("null value") ||
                lower.Contains("not-null constraint") ||
                lower.Contains("insert the value null"))
            {
                return
                    "Hay un campo obligatorio que no puede " +
                    "quedar vacío.";
            }

            if (lower.Contains("data too long") ||
                lower.Contains("truncat") ||
                lower.Contains(
                    "string or binary data would be truncated"))
            {
                return
                    "Uno de los valores ingresados es demasiado " +
                    "largo para el campo correspondiente.";
            }

            if ((lower.Contains("incorrect") &&
                 lower.Contains("value")) ||
                lower.Contains("conversion failed") ||
                lower.Contains("invalid input syntax"))
            {
                return
                    "Uno de los valores ingresados tiene un " +
                    "formato incorrecto para su campo.";
            }

            return msg;
        }
    }
}