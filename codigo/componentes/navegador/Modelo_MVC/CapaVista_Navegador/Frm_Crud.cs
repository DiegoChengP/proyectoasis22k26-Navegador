using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaControlador_Navegador;

namespace CapaVista_Navegador
{
    public partial class Frm_Crud : Form
    {
        // cambiar nombre de la tabla a la que se desea hacer el CRUD
        string nombreTabla = "tbl_puestos";

        // Campos para el formulario dinámico de ingreso (ventana flotante)
        string[] campos =
        {
            "id_empleado",
            "dpi_emp",
            "nit_emp",
            "nombre_emp",
            "apellido_emp",
            "fecha_nacimiento",
            "direccion_emp",
            "fecha_contratacion",
            "estado_emp",
            "id_puesto"
        };

        string[] camposPK = { "id_empleado" };
        string[] camposUnicos = { "dpi_emp", "nit_emp" };
        string[] camposCorreo = { };

        // Configuración opcional de llaves foráneas: si un campo aparece aquí, en vez de un
        // TextBox se genera un ComboBox cargado con los registros de la tabla referenciada.
        // Deja el diccionario vacío para usar el CRUD con una tabla sin llaves foráneas.
        // Ajusta "nombre_puesto" si la columna a mostrar en tbl_puestos se llama distinto.
        Dictionary<string, (string tablaReferenciada, string columnaId, string columnaTexto)> camposForaneos =
            new Dictionary<string, (string, string, string)>
        {
            { "id_puesto", ("tbl_puestos", "id_puesto", "nombre_puesto") }
        };

        Controlador controlador = new Controlador();

        //Modificación realizada por: Natali Sofía Montenegro Portillo validaciones de permisos del MVC

        private string _UsuarioActual = "gerente1";
        private string _CodigoModulo = "123";
        private ClsPermisoControlador _PermisoControlador = new ClsPermisoControlador();

        public Frm_Crud()
            : this(
                "USUARIO_PRUEBA",
                "EMPLEADOS")
        {
        }

        public Frm_Crud(
            string UsuarioActual,
            string CodigoModulo)
        {
            InitializeComponent();

            // FIX: antes no se asignaban los parámetros a los campos, así que
            // siempre se usaban los valores por defecto ("gerente1"/"123")
            // sin importar qué usuario/módulo se pasara al constructor.
            _UsuarioActual = UsuarioActual;
            _CodigoModulo = CodigoModulo;

            // FIX: se usa -= antes de += para garantizar una sola suscripción
            // por evento, sin importar si el Designer ya lo enganchó.
            Btn_ingresar.Click -= Btn_ingresar_Click;
            Btn_ingresar.Click += Btn_ingresar_Click;

            Btn_cancelar.Click -= Btn_cancelar_Click;
            Btn_cancelar.Click += Btn_cancelar_Click;
        }

        // Método que verifica el permiso vigente del usuario antes de ejecutar una acción.
        private bool TieneAcceso()
        {
            // Garantizar que la instancia exista usando el nombre correcto con guion bajo (_)
            if (_PermisoControlador == null)
            {
                _PermisoControlador = new ClsPermisoControlador();
            }

            // Si no hay usuario o módulo asignado, permitir acceso de prueba
            if (string.IsNullOrEmpty(_UsuarioActual) || string.IsNullOrEmpty(_CodigoModulo))
            {
                return true;
            }

            try
            {
                return _PermisoControlador.ValidarAcceso(_UsuarioActual, _CodigoModulo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar permisos: " + ex.Message, "Error de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void Btn_ingresar_Click(object sender, EventArgs e)
        {
            if (!TieneAcceso())
            {
                return;
            }
            CrearFormularioIngreso();
        }

        private void CrearFormularioIngreso()
        {
            Form formulario = new Form();
            formulario.Text = "Ingresar empleado";
            formulario.StartPosition = FormStartPosition.CenterParent;
            formulario.Size = new Size(520, 680);
            formulario.FormBorderStyle = FormBorderStyle.FixedDialog;
            formulario.MaximizeBox = false;
            formulario.MinimizeBox = false;

            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.AutoScroll = true;
            formulario.Controls.Add(panel);

            Dictionary<string, Control> controles = new Dictionary<string, Control>();
            int posicionY = 20;

            foreach (string campo in campos)
            {
                Label etiqueta = new Label();
                etiqueta.Text = campo;
                etiqueta.Location = new Point(25, posicionY);
                etiqueta.AutoSize = true;

                Control control;

                if (campo == "fecha_nacimiento" || campo == "fecha_contratacion")
                {
                    DateTimePicker calendario = new DateTimePicker();
                    calendario.Name = "dtp_" + campo;
                    calendario.Location = new Point(180, posicionY - 3);
                    calendario.Width = 260;
                    calendario.Format = DateTimePickerFormat.Short;
                    calendario.Value = DateTime.Today;
                    control = calendario;
                }
                else if (camposForaneos.ContainsKey(campo))
                {
                    var config = camposForaneos[campo];
                    ComboBox combo = new ComboBox();
                    combo.Name = "cbo_" + campo;
                    combo.Location = new Point(180, posicionY - 3);
                    combo.Width = 260;
                    combo.DropDownStyle = ComboBoxStyle.DropDownList;

                    try
                    {
                        DataTable dtOpciones = controlador.llenarDgv(config.tablaReferenciada);
                        combo.DataSource = dtOpciones;
                        combo.DisplayMember = config.columnaTexto;
                        combo.ValueMember = config.columnaId;
                        combo.SelectedIndex = -1; // obliga a elegir explícitamente
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            $"No se pudieron cargar las opciones para '{campo}' desde '{config.tablaReferenciada}':\n\n{ex.Message}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    control = combo;
                }
                else
                {
                    TextBox caja = new TextBox();
                    caja.Name = "txt_" + campo;
                    caja.Location = new Point(180, posicionY - 3);
                    caja.Width = 260;

                    // id_empleado lo genera la base de datos automáticamente
                    if (campo == "id_empleado")
                    {
                        caja.Enabled = false;
                        caja.Text = "(automático)";
                    }

                    control = caja;
                }

                panel.Controls.Add(etiqueta);
                panel.Controls.Add(control);
                controles.Add(campo, control);

                posicionY += 45;
            }

            Button botonGuardar = new Button();
            botonGuardar.Text = "Guardar";
            botonGuardar.Width = 100;
            botonGuardar.Height = 35;
            botonGuardar.Location = new Point(180, posicionY + 10);

            panel.Controls.Add(botonGuardar);

            botonGuardar.Click += (s, ev) =>
            {
                GuardarRegistroIngreso(formulario, controles);
            };

            formulario.ShowDialog();
        }

        private void GuardarRegistroIngreso(Form formulario, Dictionary<string, Control> controles)
        {
            try
            {
                Dictionary<string, string> datos = new Dictionary<string, string>();

                foreach (string campo in campos)
                {
                    if (campo == "id_empleado") continue; // autogenerado, no se envía

                    string valor;

                    if (campo == "fecha_nacimiento" || campo == "fecha_contratacion")
                    {
                        DateTimePicker calendario = (DateTimePicker)controles[campo];
                        valor = calendario.Value.ToString("yyyy-MM-dd");
                    }
                    else if (camposForaneos.ContainsKey(campo))
                    {
                        ComboBox combo = (ComboBox)controles[campo];
                        if (combo.SelectedValue == null)
                        {
                            MessageBox.Show("Seleccione un valor para '" + campo + "'.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            combo.Focus();
                            return;
                        }
                        valor = combo.SelectedValue.ToString();
                    }
                    else
                    {
                        TextBox caja = (TextBox)controles[campo];
                        valor = caja.Text.Trim();
                    }

                    if (string.IsNullOrWhiteSpace(valor))
                    {
                        MessageBox.Show("El campo '" + campo + "' es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        controles[campo].Focus();
                        return;
                    }

                    datos.Add(campo, valor);
                }

                foreach (string campoCorreo in camposCorreo)
                {
                    if (datos.ContainsKey(campoCorreo) && !ValidarCorreo(datos[campoCorreo]))
                    {
                        MessageBox.Show("El correo ingresado en '" + campoCorreo + "' no tiene un formato válido.", "Correo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        controles[campoCorreo].Focus();
                        return;
                    }
                }

                // Reutiliza tu validación de esquema/duplicados existente
                List<string> errores = ValidarRegistroConEsquema(datos);
                if (errores != null && errores.Count > 0)
                {
                    MessageBox.Show(string.Join("\n", errores), "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Construye el INSERT igual que tu Btn_guardar_Click original
                List<string> columnas = new List<string>();
                List<string> valoresSql = new List<string>();

                foreach (var par in datos)
                {
                    columnas.Add(par.Key);
                    valoresSql.Add("'" + par.Value.Replace("'", "''") + "'");
                }

                string sql = $"INSERT INTO {nombreTabla} ({string.Join(", ", columnas)}) VALUES ({string.Join(", ", valoresSql)});";

                controlador.guardarDatos(sql);

                MessageBox.Show("Empleado ingresado correctamente.", "Registro guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                formulario.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ObtenerMensajeAmigable(ex), "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCorreo(string correo)
        {
            return Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private void Btn_cancelar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Operación cancelada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ---------------- Manejo genérico de errores de base de datos (cualquier motor) ----------------

        /// <summary>
        /// Traduce errores comunes de base de datos (llave foránea, duplicados, nulos, tipo de dato)
        /// a un mensaje entendible para el usuario, sin depender del motor específico (MySQL, SQL Server,
        /// PostgreSQL, SQLite, Oracle, etc.). Si no reconoce el patrón, devuelve el mensaje original.
        /// </summary>
        private string ObtenerMensajeAmigable(Exception ex)
        {
            string msg = ex.Message ?? "";
            string lower = msg.ToLowerInvariant();

            // Violación de llave foránea
            // MySQL/ODBC: "Cannot add or update a child row: a foreign key constraint fails ... REFERENCES `tbl_puestos` (`id_puesto`)"
            // SQL Server: "The INSERT statement conflicted with the FOREIGN KEY constraint ..."
            // PostgreSQL: "violates foreign key constraint ... is not present in table \"tbl_puestos\""
            if (lower.Contains("foreign key") || lower.Contains("fk_") || lower.Contains("reference constraint"))
            {
                string tablaReferenciada = ExtraerTablaReferenciada(msg);
                if (!string.IsNullOrEmpty(tablaReferenciada))
                {
                    return $"El valor ingresado no existe en la tabla relacionada '{tablaReferenciada}'. " +
                           "Verifique que el dato corresponda a un registro existente e intente nuevamente.";
                }
                return "El valor ingresado no existe en una tabla relacionada (llave foránea). " +
                       "Verifique los datos e intente nuevamente.";
            }

            // Duplicados / valores únicos
            // MySQL: "Duplicate entry ... for key"
            // SQL Server: "Violation of UNIQUE KEY constraint" / "Violation of PRIMARY KEY constraint"
            // PostgreSQL: "duplicate key value violates unique constraint"
            if (lower.Contains("duplicate entry") || lower.Contains("duplicate key") ||
                lower.Contains("unique constraint") || lower.Contains("violation of unique") ||
                lower.Contains("violation of primary key"))
            {
                return "Ya existe un registro con ese mismo valor en un campo único (por ejemplo un identificador, DPI, NIT o correo). " +
                       "Verifique los datos.";
            }

            // Campos obligatorios (NOT NULL)
            // MySQL: "Column 'x' cannot be null"
            // SQL Server: "Cannot insert the value NULL into column"
            // PostgreSQL: "null value in column ... violates not-null constraint"
            if (lower.Contains("cannot be null") || lower.Contains("null value") ||
                lower.Contains("not-null constraint") || lower.Contains("insert the value null"))
            {
                return "Hay un campo obligatorio que no puede quedar vacío. Complete todos los datos requeridos.";
            }

            // Valores demasiado largos / truncados
            if (lower.Contains("data too long") || lower.Contains("truncat") || lower.Contains("string or binary data would be truncated"))
            {
                return "Uno de los valores ingresados es demasiado largo para el campo correspondiente.";
            }

            // Tipo de dato incorrecto (texto en campo numérico, fecha inválida, etc.)
            if ((lower.Contains("incorrect") && lower.Contains("value")) ||
                lower.Contains("conversion failed") || lower.Contains("invalid input syntax"))
            {
                return "Uno de los valores ingresados tiene un formato incorrecto para su campo " +
                       "(por ejemplo texto en un campo numérico o una fecha inválida).";
            }

            // No se reconoce el patrón: se muestra el mensaje original tal cual
            return msg;
        }

        /// <summary>
        /// Intenta extraer el nombre de la tabla referenciada de un mensaje de error de llave foránea.
        /// Cubre los formatos más comunes; si no encuentra nada, devuelve null.
        /// </summary>
        private string ExtraerTablaReferenciada(string mensaje)
        {
            try
            {
                // Formato MySQL/ODBC:  REFERENCES `tbl_puestos` (`id_puesto`)
                var m1 = Regex.Match(mensaje, @"REFERENCES\s+[`""\[]?(\w+)[`""\]]?", RegexOptions.IgnoreCase);
                if (m1.Success) return m1.Groups[1].Value;

                // Formato PostgreSQL:  is not present in table "tbl_puestos"
                var m2 = Regex.Match(mensaje, @"present in table\s+[""']?(\w+)[""']?", RegexOptions.IgnoreCase);
                if (m2.Success) return m2.Groups[1].Value;

                // Formato SQL Server:  table "dbo.tbl_puestos"
                var m3 = Regex.Match(mensaje, @"table\s+[""']?(?:\w+\.)?(\w+)[""']?", RegexOptions.IgnoreCase);
                if (m3.Success) return m3.Groups[1].Value;
            }
            catch
            {
                // Si el parseo falla, simplemente no se muestra el nombre de tabla
            }

            return null;
        }

        public List<string> ValidarRegistroConEsquema(Dictionary<string, string> datos)
        {
            try
            {
                return controlador.ValidarRegistro(datos, nombreTabla);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al validar los datos:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return new List<string>();
            }
        }

        public DataTable ObtenerEsquemaTabla()
        {
            try
            {
                return controlador.ObtenerEsquemaTabla(nombreTabla);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al obtener el esquema de la tabla:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return null;
            }
        }
    }
}