/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 21/10/2026
 * ==================================================================
 * Propósito :
 *  Lee la sesión que dejó el login de Seguridad y resuelve los
 *  permisos del usuario sobre las cinco pantallas de Contabilidad.
 * Reglas especificas:
 *  Todo sale de las DLL de Seguridad (igual que el Navegador): la
 *  sesión de ClsSesionSeguridad, los IDs de módulo y aplicación por
 *  nombre y los permisos por rol. Contabilidad no tiene SQL propio
 *  para esto. Si algo falla no se conservan permisos parciales.
 * ===================================================================
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using CapaControlador_Seguridad;
using CapaControlador_Seguridad.Modelos_de_controladores;
using CapaControlador_Seguridad.Objetos_de_valor;

namespace CapaControlador_Contabilidad
{
    public class ClsControladorSeguridadContabilidad
    {
        // Nombre del módulo registrado en Seguridad.
        private const string _NombreModulo = "Contabilidad";

        // Código de ventana y nombre de cada aplicación registrada en Seguridad.
        private readonly Dictionary<int, string> _Aplicaciones = new Dictionary<int, string>
        {
            { 10001, "Pólizas locales" }, { 10002, "Presupuestos" },
            { 10003, "Activos fijos" }, { 10004, "Cierre contable" },
            { 10005, "Estados financieros" },
            // Mantenimientos (usan el control Navegador).
            { 10011, "Mantenimiento de países" }
        };

        // Arma el estado de la sesión y los permisos de cada pantalla.
        public ClsEstadoSeguridadContabilidad ContabilidadFuncObtenerEstado()
        {
            ClsEstadoSeguridadContabilidad Estado = new ClsEstadoSeguridadContabilidad
            {
                HaySesion = ClsSesionSeguridad.HaySesionActiva,
                NombreUsuario = "Sin sesión",
                Roles = "Sin sesión",
                Mensaje = ""
            };
            if (!Estado.HaySesion) return Estado;

            Estado.NombreUsuario = string.IsNullOrWhiteSpace(ClsSesionSeguridad.NombreEmpleado)
                ? ClsSesionSeguridad.NombreUsuario : ClsSesionSeguridad.NombreEmpleado;
            Estado.Roles = ClsSesionSeguridad.SeguridadMetRolesComoTexto();
            if (string.IsNullOrWhiteSpace(Estado.Roles)) Estado.Roles = "Sin roles";

            try
            {
                // Aplicaciones del módulo Contabilidad según Seguridad.
                List<ClsModeloMantenimientoApp> Registradas = new ClsModeloMantenimientoApp().SeguridadMetObtenerTodos()
                    .Where(Aplicacion => Aplicacion.IsActive && ContabilidadFuncNormalizarNombre(Aplicacion.NombreModulo) == ContabilidadFuncNormalizarNombre(_NombreModulo))
                    .ToList();
                // Permisos por rol, igual que ClsNavegadorPermisos.
                ClsModeloAsigAppPerf Permisos = new ClsModeloAsigAppPerf();
                Permisos.SeguridadMetObtenerTodos();

                int Encontradas = 0;
                foreach (KeyValuePair<int, string> Aplicacion in _Aplicaciones)
                {
                    List<ClsModeloMantenimientoApp> Coincidencias = Registradas.Where(Registro =>
                        ContabilidadFuncNormalizarNombre(Registro.NombreAplicacion) == ContabilidadFuncNormalizarNombre(Aplicacion.Value)).ToList();
                    if (Coincidencias.Count > 1)
                        throw new InvalidOperationException("Existe más de un registro para " + Aplicacion.Value + ".");
                    if (Coincidencias.Count == 0) continue;

                    Encontradas++;
                    ClsPermisoAplicacion Permiso = Permisos.SeguridadMetObtenerPermisosSesion(Coincidencias[0].IdModulo, Coincidencias[0].IdAplicacion);
                    Estado.Permisos[Aplicacion.Key] = new ClsPermisosPantallaContabilidad
                    {
                        IdModulo = Coincidencias[0].IdModulo, IdAplicacion = Coincidencias[0].IdAplicacion,
                        TieneAcceso = Permiso.TieneAcceso, PuedeInsertar = Permiso.PuedeInsertar,
                        PuedeEditar = Permiso.PuedeEditar, PuedeEliminar = Permiso.PuedeEliminar, PuedeImprimir = Permiso.PuedeImprimir
                    };
                }
                if (Encontradas == 0)
                    Estado.Mensaje = "Las aplicaciones de Contabilidad aún no están registradas en Seguridad.";
                else if (Encontradas != _Aplicaciones.Count)
                    Estado.Mensaje = "Algunas aplicaciones de Contabilidad aún no están registradas en Seguridad.";
            }
            catch (Exception Excepcion)
            {
                // Un fallo de consulta nunca conserva permisos parciales o anteriores.
                foreach (int Codigo in _Aplicaciones.Keys) Estado.Permisos[Codigo] = ClsPermisosPantallaContabilidad.Ninguno;
                Estado.Mensaje = "No se pudieron consultar los permisos de Contabilidad. " + Excepcion.Message;
            }
            return Estado;
        }

        // Permisos de una sola pantalla por su código de ventana.
        public ClsPermisosPantallaContabilidad ContabilidadFuncObtenerPermisos(int CodigoVentana)
        {
            ClsPermisosPantallaContabilidad Permisos;
            return ContabilidadFuncObtenerEstado().Permisos.TryGetValue(CodigoVentana, out Permisos) ? Permisos : ClsPermisosPantallaContabilidad.Ninguno;
        }

        // Quita tildes y espacios para comparar nombres.
        private static string ContabilidadFuncNormalizarNombre(string Nombre)
        {
            if (Nombre == null) return string.Empty;
            StringBuilder Resultado = new StringBuilder();
            foreach (char Caracter in Nombre.Normalize(NormalizationForm.FormD))
            {
                if (CharUnicodeInfo.GetUnicodeCategory(Caracter) != UnicodeCategory.NonSpacingMark &&
                    !char.IsWhiteSpace(Caracter)) Resultado.Append(char.ToUpperInvariant(Caracter));
            }
            return Resultado.ToString();
        }
    }
}
