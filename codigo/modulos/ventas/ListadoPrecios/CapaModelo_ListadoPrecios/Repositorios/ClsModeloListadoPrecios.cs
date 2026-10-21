using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Globalization;
using System.Linq;
using CapaModelo_ListadoPrecios.Entidades;
using CapaModelo_ListadoPrecios.Contratos;

namespace CapaModelo_ListadoPrecios
{

    // Acceso a datos. Detecta una tabla existente por los nombres de columnas conocidos;
    // si el DSN o el esquema del equipo aún no está disponible, usa datos de demostración.
    public class ClsModeloListadoPrecios : IRepositorioListadoPrecios
    {
        private static string CadenaConexion { get { return CapaModelo_Ventas.ClsConexion.CadenaConexion; } }
        private static readonly List<ClsPrecioCliente> DatosDemostracion = new List<ClsPrecioCliente>
        {
            new ClsPrecioCliente { TipoCliente = "Mayorista", TipoProducto = "Producto A", PorcentajeDescuento = 12m, PrecioBase = 100m, PrecioFinal = 88m },
            new ClsPrecioCliente { TipoCliente = "Minorista", TipoProducto = "Producto A", PorcentajeDescuento = 5m, PrecioBase = 100m, PrecioFinal = 95m },
            new ClsPrecioCliente { TipoCliente = "Público", TipoProducto = "Producto B", PorcentajeDescuento = 0m, PrecioBase = 75m, PrecioFinal = 75m }
        };
        private static readonly object Bloqueo = new object();
        private TablaPrecio _tablaDetectada;
        public bool UsaBaseDeDatos { get; private set; }
        public string MensajeEstado { get; private set; }

        public ClsModeloListadoPrecios()
        {
            MensajeEstado = "Modo demostración: no se ha confirmado una tabla de precios compatible.";
        }

        public List<ClsPrecioCliente> ObtenerListado()
        {
            try
            {
                using (OdbcConnection conexion = new OdbcConnection(CadenaConexion))
                {
                    conexion.Open();
                    _tablaDetectada = DetectarTabla(conexion);
                    if (_tablaDetectada != null)
                    {
                        List<ClsPrecioCliente> datos = ConsultarTabla(conexion, _tablaDetectada);
                        UsaBaseDeDatos = true;
                        MensajeEstado = "Listado cargado desde la base de datos.";
                        return datos;
                    }
                    MensajeEstado = "Conexión disponible, pero no se encontró una tabla con columnas de listado de precios reconocibles. Se muestran datos de demostración.";
                }
            }
            catch (Exception ex)
            {
                MensajeEstado = "Modo demostración: no fue posible consultar la conexión configurada en Ventas (" + ex.Message + ").";
            }
            UsaBaseDeDatos = false;
            lock (Bloqueo) return DatosDemostracion.Select(Clonar).ToList();
        }

        public bool Guardar(ClsPrecioCliente registro, out string mensaje)
        {
            if (registro == null) { mensaje = "No se recibió información para guardar."; return false; }
            registro.PrecioFinal = CalcularPrecioFinal(registro.PrecioBase, registro.PorcentajeDescuento);
            try
            {
                using (OdbcConnection conexion = new OdbcConnection(CadenaConexion))
                {
                    conexion.Open();
                    TablaPrecio tabla = _tablaDetectada ?? DetectarTabla(conexion);
                    if (tabla != null)
                    {
                        GuardarEnTabla(conexion, tabla, registro);
                        _tablaDetectada = tabla;
                        UsaBaseDeDatos = true;
                        MensajeEstado = "Cambios guardados en la base de datos.";
                        mensaje = MensajeEstado;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Si nunca se detectó una tabla compatible, permite la prueba de interfaz sin fingir persistencia.
                if (_tablaDetectada != null)
                {
                    mensaje = "No se pudo guardar en la tabla detectada: " + ex.Message;
                    return false;
                }
                lock (Bloqueo)
                {
                    ClsPrecioCliente existente = DatosDemostracion.FirstOrDefault(x =>
                        Igual(x.TipoCliente, registro.TipoCliente) && Igual(x.TipoProducto, registro.TipoProducto));
                    if (existente == null) DatosDemostracion.Add(Clonar(registro));
                    else
                    {
                        existente.PorcentajeDescuento = registro.PorcentajeDescuento;
                        existente.PrecioBase = registro.PrecioBase;
                        existente.PrecioFinal = registro.PrecioFinal;
                    }
                }
                UsaBaseDeDatos = false;
                MensajeEstado = "Guardado solo en memoria de demostración; la conexión ODBC no está disponible (" + ex.Message + ").";
                mensaje = MensajeEstado;
                return true;
            }
            // Fallback explícito para permitir probar el formulario mientras se configura el esquema.
            lock (Bloqueo)
            {
                ClsPrecioCliente existente = DatosDemostracion.FirstOrDefault(x =>
                    Igual(x.TipoCliente, registro.TipoCliente) && Igual(x.TipoProducto, registro.TipoProducto));
                if (existente == null) DatosDemostracion.Add(Clonar(registro));
                else
                {
                    existente.PorcentajeDescuento = registro.PorcentajeDescuento;
                    existente.PrecioBase = registro.PrecioBase;
                    existente.PrecioFinal = registro.PrecioFinal;
                }
            }
            UsaBaseDeDatos = false;
            MensajeEstado = "Guardado solo en memoria de demostración; no se persistió en la base de datos.";
            mensaje = MensajeEstado;
            return true;
        }

        public static decimal CalcularPrecioFinal(decimal precioBase, decimal descuento)
        {
            return Math.Round(precioBase * (1m - descuento / 100m), 2, MidpointRounding.AwayFromZero);
        }

        private static TablaPrecio DetectarTabla(OdbcConnection conexion)
        {
            DataTable tablas = conexion.GetSchema("Tables");
            foreach (DataRow fila in tablas.Rows)
            {
                string tipo = Convert.ToString(fila["TABLE_TYPE"], CultureInfo.InvariantCulture);
                if (!string.IsNullOrEmpty(tipo) && !tipo.Equals("TABLE", StringComparison.OrdinalIgnoreCase) && !tipo.Equals("BASE TABLE", StringComparison.OrdinalIgnoreCase)) continue;
                string nombre = Convert.ToString(fila["TABLE_NAME"], CultureInfo.InvariantCulture);
                string esquema = tablas.Columns.Contains("TABLE_SCHEM") ? Convert.ToString(fila["TABLE_SCHEM"], CultureInfo.InvariantCulture) : null;
                if (string.IsNullOrWhiteSpace(nombre)) continue;
                DataTable columnas;
                try { columnas = conexion.GetSchema("Columns", new string[] { null, esquema, nombre, null }); }
                catch { continue; }
                Dictionary<string, string> mapa = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (DataRow columna in columnas.Rows)
                {
                    string campo = Convert.ToString(columna["COLUMN_NAME"], CultureInfo.InvariantCulture);
                    if (!string.IsNullOrWhiteSpace(campo)) mapa[campo] = campo;
                }
                string cliente = BuscarColumna(mapa, "tipo_cliente", "tipocliente", "clasificacion_cliente", "categoria_cliente", "cliente_tipo");
                string producto = BuscarColumna(mapa, "tipo_producto", "tipoproducto", "nombre_producto", "producto", "descripcion_producto");
                string descuento = BuscarColumna(mapa, "porcentaje_descuento", "porcentajedescuento", "descuento_porcentaje", "descuento", "porcentaje");
                string precio = BuscarColumna(mapa, "precio_base", "preciobase", "precio_unitario", "precio", "price");
                string final = BuscarColumna(mapa, "precio_final", "preciofinal", "precio_con_descuento");
                if (cliente != null && producto != null && descuento != null && precio != null)
                    return new TablaPrecio { Esquema = esquema, Nombre = nombre, ColumnaCliente = cliente, ColumnaProducto = producto, ColumnaDescuento = descuento, ColumnaBase = precio, ColumnaFinal = final };
            }
            return null;
        }

        private static string BuscarColumna(Dictionary<string, string> mapa, params string[] nombres)
        {
            foreach (string candidato in nombres)
                foreach (KeyValuePair<string, string> campo in mapa)
                    if (Normalizar(campo.Key) == Normalizar(candidato)) return campo.Value;
            return null;
        }
        private static string Normalizar(string valor) { return new string((valor ?? "").Where(char.IsLetterOrDigit).Select(char.ToLowerInvariant).ToArray()); }
        private static string Q(string identificador) { return "\"" + (identificador ?? "").Replace("\"", "\"\"") + "\""; }
        private static string TablaSql(TablaPrecio t) { return string.IsNullOrWhiteSpace(t.Esquema) ? Q(t.Nombre) : Q(t.Esquema) + "." + Q(t.Nombre); }

        private static List<ClsPrecioCliente> ConsultarTabla(OdbcConnection conexion, TablaPrecio t)
        {
            string sql = "SELECT " + Q(t.ColumnaCliente) + "," + Q(t.ColumnaProducto) + "," + Q(t.ColumnaDescuento) + "," + Q(t.ColumnaBase) +
                (t.ColumnaFinal == null ? "" : "," + Q(t.ColumnaFinal)) + " FROM " + TablaSql(t);
            List<ClsPrecioCliente> resultado = new List<ClsPrecioCliente>();
            using (OdbcCommand cmd = new OdbcCommand(sql, conexion))
            using (OdbcDataReader lector = cmd.ExecuteReader())
            {
                while (lector.Read())
                {
                    decimal descuento = DecimalSeguro(lector[2]);
                    decimal basePrecio = DecimalSeguro(lector[3]);
                    decimal final = t.ColumnaFinal == null || lector.IsDBNull(4) ? CalcularPrecioFinal(basePrecio, descuento) : DecimalSeguro(lector[4]);
                    resultado.Add(new ClsPrecioCliente { TipoCliente = Convert.ToString(lector[0]), TipoProducto = Convert.ToString(lector[1]), PorcentajeDescuento = descuento, PrecioBase = basePrecio, PrecioFinal = final });
                }
            }
            return resultado;
        }

        private static void GuardarEnTabla(OdbcConnection conexion, TablaPrecio t, ClsPrecioCliente r)
        {
            string tabla = TablaSql(t);
            string sqlBuscar = "SELECT COUNT(*) FROM " + tabla + " WHERE " + Q(t.ColumnaCliente) + "=? AND " + Q(t.ColumnaProducto) + "=?";
            bool existe;
            using (OdbcCommand cmd = new OdbcCommand(sqlBuscar, conexion))
            {
                cmd.Parameters.Add("p1", OdbcType.VarChar).Value = r.TipoCliente;
                cmd.Parameters.Add("p2", OdbcType.VarChar).Value = r.TipoProducto;
                existe = Convert.ToInt32(cmd.ExecuteScalar(), CultureInfo.InvariantCulture) > 0;
            }
            List<string> campos = new List<string> { t.ColumnaDescuento, t.ColumnaBase };
            List<object> valores = new List<object> { r.PorcentajeDescuento, r.PrecioBase };
            if (t.ColumnaFinal != null) { campos.Add(t.ColumnaFinal); valores.Add(r.PrecioFinal); }
            string sql;
            if (existe)
                sql = "UPDATE " + tabla + " SET " + string.Join(",", campos.Select(c => Q(c) + "=?")) + " WHERE " + Q(t.ColumnaCliente) + "=? AND " + Q(t.ColumnaProducto) + "=?";
            else
            {
                campos.Insert(0, t.ColumnaProducto); campos.Insert(0, t.ColumnaCliente);
                valores.Insert(0, r.TipoProducto); valores.Insert(0, r.TipoCliente);
                sql = "INSERT INTO " + tabla + " (" + string.Join(",", campos.Select(Q)) + ") VALUES (" + string.Join(",", campos.Select(c => "?")) + ")";
            }
            using (OdbcCommand cmd = new OdbcCommand(sql, conexion))
            {
                foreach (object valor in valores) cmd.Parameters.Add("p", valor is decimal ? OdbcType.Decimal : OdbcType.VarChar).Value = valor;
                if (existe)
                {
                    cmd.Parameters.Add("pCliente", OdbcType.VarChar).Value = r.TipoCliente;
                    cmd.Parameters.Add("pProducto", OdbcType.VarChar).Value = r.TipoProducto;
                }
                cmd.ExecuteNonQuery();
            }
        }

        private static decimal DecimalSeguro(object valor)
        {
            if (valor == null || valor == DBNull.Value) return 0m;
            decimal d;
            if (decimal.TryParse(Convert.ToString(valor, CultureInfo.InvariantCulture), NumberStyles.Any, CultureInfo.InvariantCulture, out d)) return d;
            if (decimal.TryParse(Convert.ToString(valor), NumberStyles.Any, CultureInfo.GetCultureInfo("es-GT"), out d)) return d;
            return 0m;
        }
        private static bool Igual(string a, string b) { return string.Equals(a, b, StringComparison.OrdinalIgnoreCase); }
        private static ClsPrecioCliente Clonar(ClsPrecioCliente x)
        { return new ClsPrecioCliente { TipoCliente = x.TipoCliente, TipoProducto = x.TipoProducto, PorcentajeDescuento = x.PorcentajeDescuento, PrecioBase = x.PrecioBase, PrecioFinal = x.PrecioFinal }; }

        private sealed class TablaPrecio
        {
            public string Esquema, Nombre, ColumnaCliente, ColumnaProducto, ColumnaDescuento, ColumnaBase, ColumnaFinal;
        }
    }
}
