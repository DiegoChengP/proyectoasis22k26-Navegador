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
        private static readonly List<ClsPrecioCliente> _DatosDemostracion = new List<ClsPrecioCliente>
        {
            new ClsPrecioCliente { TipoCliente = "Mayorista", TipoProducto = "Producto A", PorcentajeDescuento = 12m, PrecioBase = 100m, PrecioFinal = 88m },
            new ClsPrecioCliente { TipoCliente = "Minorista", TipoProducto = "Producto A", PorcentajeDescuento = 5m, PrecioBase = 100m, PrecioFinal = 95m },
            new ClsPrecioCliente { TipoCliente = "Público", TipoProducto = "Producto B", PorcentajeDescuento = 0m, PrecioBase = 75m, PrecioFinal = 75m }
        };
        private static readonly object _Bloqueo = new object();
        private ClsTablaPrecio _TablaDetectada;
        public bool UsaBaseDeDatos { get; private set; }
        public string MensajeEstado { get; private set; }

        public ClsModeloListadoPrecios()
        {
            MensajeEstado = "Modo demostración: no se ha confirmado una tabla de precios compatible.";
        }

        public List<ClsPrecioCliente> VentasFuncObtenerListado()
        {
            try
            {
                using (OdbcConnection Conexion = new OdbcConnection(CadenaConexion))
                {
                    Conexion.Open();
                    _TablaDetectada = VentasFuncDetectarTabla(Conexion);
                    if (_TablaDetectada != null)
                    {
                        List<ClsPrecioCliente> Datos = VentasFuncConsultarTabla(Conexion, _TablaDetectada);
                        UsaBaseDeDatos = true;
                        MensajeEstado = "Listado cargado desde la base de datos.";
                        return Datos;
                    }
                    MensajeEstado = "Conexión disponible, pero no se encontró una tabla con columnas de listado de precios reconocibles. Se muestran datos de demostración.";
                }
            }
            catch (Exception Error)
            {
                MensajeEstado = "Modo demostración: no fue posible consultar la conexión configurada en Ventas (" + Error.Message + ").";
            }
            UsaBaseDeDatos = false;
            lock (_Bloqueo) return _DatosDemostracion.Select(VentasFuncClonar).ToList();
        }

        public bool VentasFuncGuardar(ClsPrecioCliente Registro, out string Mensaje)
        {
            if (Registro == null) { Mensaje = "No se recibió información para guardar."; return false; }
            Registro.PrecioFinal = VentasFuncCalcularPrecioFinal(Registro.PrecioBase, Registro.PorcentajeDescuento);
            try
            {
                using (OdbcConnection Conexion = new OdbcConnection(CadenaConexion))
                {
                    Conexion.Open();
                    ClsTablaPrecio Tabla = _TablaDetectada ?? VentasFuncDetectarTabla(Conexion);
                    if (Tabla != null)
                    {
                        VentasMetGuardarEnTabla(Conexion, Tabla, Registro);
                        _TablaDetectada = Tabla;
                        UsaBaseDeDatos = true;
                        MensajeEstado = "Cambios guardados en la base de datos.";
                        Mensaje = MensajeEstado;
                        return true;
                    }
                }
            }
            catch (Exception Error)
            {
                // Si nunca se detectó una tabla compatible, permite la prueba de interfaz sin fingir persistencia.
                if (_TablaDetectada != null)
                {
                    Mensaje = "No se pudo guardar en la tabla detectada: " + Error.Message;
                    return false;
                }
                lock (_Bloqueo)
                {
                    ClsPrecioCliente Existente = _DatosDemostracion.FirstOrDefault(Elemento =>
                        VentasFuncIgual(Elemento.TipoCliente, Registro.TipoCliente) && VentasFuncIgual(Elemento.TipoProducto, Registro.TipoProducto));
                    if (Existente == null) _DatosDemostracion.Add(VentasFuncClonar(Registro));
                    else
                    {
                        Existente.PorcentajeDescuento = Registro.PorcentajeDescuento;
                        Existente.PrecioBase = Registro.PrecioBase;
                        Existente.PrecioFinal = Registro.PrecioFinal;
                    }
                }
                UsaBaseDeDatos = false;
                MensajeEstado = "Guardado solo en memoria de demostración; la conexión ODBC no está disponible (" + Error.Message + ").";
                Mensaje = MensajeEstado;
                return true;
            }
            // Fallback explícito para permitir probar el formulario mientras se configura el esquema.
            lock (_Bloqueo)
            {
                ClsPrecioCliente Existente = _DatosDemostracion.FirstOrDefault(Elemento =>
                    VentasFuncIgual(Elemento.TipoCliente, Registro.TipoCliente) && VentasFuncIgual(Elemento.TipoProducto, Registro.TipoProducto));
                if (Existente == null) _DatosDemostracion.Add(VentasFuncClonar(Registro));
                else
                {
                    Existente.PorcentajeDescuento = Registro.PorcentajeDescuento;
                    Existente.PrecioBase = Registro.PrecioBase;
                    Existente.PrecioFinal = Registro.PrecioFinal;
                }
            }
            UsaBaseDeDatos = false;
            MensajeEstado = "Guardado solo en memoria de demostración; no se persistió en la base de datos.";
            Mensaje = MensajeEstado;
            return true;
        }

        public static decimal VentasFuncCalcularPrecioFinal(decimal PrecioBase, decimal Descuento)
        {
            return Math.Round(PrecioBase * (1m - Descuento / 100m), 2, MidpointRounding.AwayFromZero);
        }

        private static ClsTablaPrecio VentasFuncDetectarTabla(OdbcConnection Conexion)
        {
            DataTable Tablas = Conexion.GetSchema("Tables");
            foreach (DataRow Fila in Tablas.Rows)
            {
                string Tipo = Convert.ToString(Fila["TABLE_TYPE"], CultureInfo.InvariantCulture);
                if (!string.IsNullOrEmpty(Tipo) && !Tipo.Equals("TABLE", StringComparison.OrdinalIgnoreCase) && !Tipo.Equals("BASE TABLE", StringComparison.OrdinalIgnoreCase)) continue;
                string Nombre = Convert.ToString(Fila["TABLE_NAME"], CultureInfo.InvariantCulture);
                string Esquema = Tablas.Columns.Contains("TABLE_SCHEM") ? Convert.ToString(Fila["TABLE_SCHEM"], CultureInfo.InvariantCulture) : null;
                if (string.IsNullOrWhiteSpace(Nombre)) continue;
                DataTable Columnas;
                try { Columnas = Conexion.GetSchema("Columns", new string[] { null, Esquema, Nombre, null }); }
                catch { continue; }
                Dictionary<string, string> Mapa = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (DataRow Columna in Columnas.Rows)
                {
                    string Campo = Convert.ToString(Columna["COLUMN_NAME"], CultureInfo.InvariantCulture);
                    if (!string.IsNullOrWhiteSpace(Campo)) Mapa[Campo] = Campo;
                }
                string Cliente = VentasFuncBuscarColumna(Mapa, "tipo_cliente", "tipocliente", "clasificacion_cliente", "categoria_cliente", "cliente_tipo");
                string Producto = VentasFuncBuscarColumna(Mapa, "tipo_producto", "tipoproducto", "nombre_producto", "producto", "descripcion_producto");
                string Descuento = VentasFuncBuscarColumna(Mapa, "porcentaje_descuento", "porcentajedescuento", "descuento_porcentaje", "descuento", "porcentaje");
                string Precio = VentasFuncBuscarColumna(Mapa, "precio_base", "preciobase", "precio_unitario", "precio", "price");
                string Final = VentasFuncBuscarColumna(Mapa, "precio_final", "preciofinal", "precio_con_descuento");
                if (Cliente != null && Producto != null && Descuento != null && Precio != null)
                    return new ClsTablaPrecio { Esquema = Esquema, Nombre = Nombre, ColumnaCliente = Cliente, ColumnaProducto = Producto, ColumnaDescuento = Descuento, ColumnaBase = Precio, ColumnaFinal = Final };
            }
            return null;
        }

        private static string VentasFuncBuscarColumna(Dictionary<string, string> Mapa, params string[] Nombres)
        {
            foreach (string Candidato in Nombres)
                foreach (KeyValuePair<string, string> Campo in Mapa)
                    if (VentasFuncNormalizar(Campo.Key) == VentasFuncNormalizar(Candidato)) return Campo.Value;
            return null;
        }
        private static string VentasFuncNormalizar(string Valor) { return new string((Valor ?? "").Where(char.IsLetterOrDigit).Select(char.ToLowerInvariant).ToArray()); }
        private static string VentasFuncDelimitarIdentificador(string Identificador) { return "\"" + (Identificador ?? "").Replace("\"", "\"\"") + "\""; }
        private static string VentasFuncTablaSql(ClsTablaPrecio TablaPrecios) { return string.IsNullOrWhiteSpace(TablaPrecios.Esquema) ? VentasFuncDelimitarIdentificador(TablaPrecios.Nombre) : VentasFuncDelimitarIdentificador(TablaPrecios.Esquema) + "." + VentasFuncDelimitarIdentificador(TablaPrecios.Nombre); }

        private static List<ClsPrecioCliente> VentasFuncConsultarTabla(OdbcConnection Conexion, ClsTablaPrecio TablaPrecios)
        {
            string SentenciaSql = "SELECT " + VentasFuncDelimitarIdentificador(TablaPrecios.ColumnaCliente) + "," + VentasFuncDelimitarIdentificador(TablaPrecios.ColumnaProducto) + "," + VentasFuncDelimitarIdentificador(TablaPrecios.ColumnaDescuento) + "," + VentasFuncDelimitarIdentificador(TablaPrecios.ColumnaBase) +
                (TablaPrecios.ColumnaFinal == null ? "" : "," + VentasFuncDelimitarIdentificador(TablaPrecios.ColumnaFinal)) + " FROM " + VentasFuncTablaSql(TablaPrecios);
            List<ClsPrecioCliente> Resultado = new List<ClsPrecioCliente>();
            using (OdbcCommand Comando = new OdbcCommand(SentenciaSql, Conexion))
            using (OdbcDataReader Lector = Comando.ExecuteReader())
            {
                while (Lector.Read())
                {
                    decimal Descuento = VentasFuncDecimalSeguro(Lector[2]);
                    decimal BasePrecio = VentasFuncDecimalSeguro(Lector[3]);
                    decimal Final = TablaPrecios.ColumnaFinal == null || Lector.IsDBNull(4) ? VentasFuncCalcularPrecioFinal(BasePrecio, Descuento) : VentasFuncDecimalSeguro(Lector[4]);
                    Resultado.Add(new ClsPrecioCliente { TipoCliente = Convert.ToString(Lector[0]), TipoProducto = Convert.ToString(Lector[1]), PorcentajeDescuento = Descuento, PrecioBase = BasePrecio, PrecioFinal = Final });
                }
            }
            return Resultado;
        }

        private static void VentasMetGuardarEnTabla(OdbcConnection Conexion, ClsTablaPrecio TablaPrecios, ClsPrecioCliente PrecioCliente)
        {
            string Tabla = VentasFuncTablaSql(TablaPrecios);
            string SqlBuscar = "SELECT COUNT(*) FROM " + Tabla + " WHERE " + VentasFuncDelimitarIdentificador(TablaPrecios.ColumnaCliente) + "=? AND " + VentasFuncDelimitarIdentificador(TablaPrecios.ColumnaProducto) + "=?";
            bool Existe;
            using (OdbcCommand Comando = new OdbcCommand(SqlBuscar, Conexion))
            {
                Comando.Parameters.Add("p1", OdbcType.VarChar).Value = PrecioCliente.TipoCliente;
                Comando.Parameters.Add("p2", OdbcType.VarChar).Value = PrecioCliente.TipoProducto;
                Existe = Convert.ToInt32(Comando.ExecuteScalar(), CultureInfo.InvariantCulture) > 0;
            }
            List<string> Campos = new List<string> { TablaPrecios.ColumnaDescuento, TablaPrecios.ColumnaBase };
            List<object> Valores = new List<object> { PrecioCliente.PorcentajeDescuento, PrecioCliente.PrecioBase };
            if (TablaPrecios.ColumnaFinal != null) { Campos.Add(TablaPrecios.ColumnaFinal); Valores.Add(PrecioCliente.PrecioFinal); }
            string SentenciaSql;
            if (Existe)
                SentenciaSql = "UPDATE " + Tabla + " SET " + string.Join(",", Campos.Select(NombreColumna => VentasFuncDelimitarIdentificador(NombreColumna) + "=?")) + " WHERE " + VentasFuncDelimitarIdentificador(TablaPrecios.ColumnaCliente) + "=? AND " + VentasFuncDelimitarIdentificador(TablaPrecios.ColumnaProducto) + "=?";
            else
            {
                Campos.Insert(0, TablaPrecios.ColumnaProducto); Campos.Insert(0, TablaPrecios.ColumnaCliente);
                Valores.Insert(0, PrecioCliente.TipoProducto); Valores.Insert(0, PrecioCliente.TipoCliente);
                SentenciaSql = "INSERT INTO " + Tabla + " (" + string.Join(",", Campos.Select(VentasFuncDelimitarIdentificador)) + ") VALUES (" + string.Join(",", Campos.Select(NombreColumna => "?")) + ")";
            }
            using (OdbcCommand Comando2 = new OdbcCommand(SentenciaSql, Conexion))
            {
                foreach (object Valor in Valores) Comando2.Parameters.Add("p", Valor is decimal ? OdbcType.Decimal : OdbcType.VarChar).Value = Valor;
                if (Existe)
                {
                    Comando2.Parameters.Add("pCliente", OdbcType.VarChar).Value = PrecioCliente.TipoCliente;
                    Comando2.Parameters.Add("pProducto", OdbcType.VarChar).Value = PrecioCliente.TipoProducto;
                }
                Comando2.ExecuteNonQuery();
            }
        }

        private static decimal VentasFuncDecimalSeguro(object Valor)
        {
            if (Valor == null || Valor == DBNull.Value) return 0m;
            decimal ValorDecimal;
            if (decimal.TryParse(Convert.ToString(Valor, CultureInfo.InvariantCulture), NumberStyles.Any, CultureInfo.InvariantCulture, out ValorDecimal)) return ValorDecimal;
            if (decimal.TryParse(Convert.ToString(Valor), NumberStyles.Any, CultureInfo.GetCultureInfo("es-GT"), out ValorDecimal)) return ValorDecimal;
            return 0m;
        }
        private static bool VentasFuncIgual(string ValorIzquierdo, string ValorDerecho) { return string.Equals(ValorIzquierdo, ValorDerecho, StringComparison.OrdinalIgnoreCase); }
        private static ClsPrecioCliente VentasFuncClonar(ClsPrecioCliente Elemento)
        { return new ClsPrecioCliente { TipoCliente = Elemento.TipoCliente, TipoProducto = Elemento.TipoProducto, PorcentajeDescuento = Elemento.PorcentajeDescuento, PrecioBase = Elemento.PrecioBase, PrecioFinal = Elemento.PrecioFinal }; }

        private sealed class ClsTablaPrecio
        {
            public string Esquema, Nombre, ColumnaCliente, ColumnaProducto, ColumnaDescuento, ColumnaBase, ColumnaFinal;
        }
    }
}
