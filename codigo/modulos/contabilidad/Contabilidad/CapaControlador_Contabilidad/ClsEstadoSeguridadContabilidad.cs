/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 21/10/2026
 * ==================================================================
 * Propósito :
 *  Datos de la sesión y de los permisos que el MDI muestra: usuario,
 *  roles, aviso y el permiso de cada pantalla por código de ventana.
 * ===================================================================
*/

using System.Collections.Generic;

namespace CapaControlador_Contabilidad
{
    public sealed class ClsEstadoSeguridadContabilidad
    {
        public bool HaySesion { get; internal set; }
        public string NombreUsuario { get; internal set; }
        public string Roles { get; internal set; }
        public string Mensaje { get; internal set; }
        public Dictionary<int, ClsPermisosPantallaContabilidad> Permisos { get; } = new Dictionary<int, ClsPermisosPantallaContabilidad>
        {
            { 10001, ClsPermisosPantallaContabilidad.Ninguno }, { 10002, ClsPermisosPantallaContabilidad.Ninguno },
            { 10003, ClsPermisosPantallaContabilidad.Ninguno }, { 10004, ClsPermisosPantallaContabilidad.Ninguno },
            { 10005, ClsPermisosPantallaContabilidad.Ninguno },
            { 10011, ClsPermisosPantallaContabilidad.Ninguno },
            { 10012, ClsPermisosPantallaContabilidad.Ninguno },
            { 10013, ClsPermisosPantallaContabilidad.Ninguno },
            { 10015, ClsPermisosPantallaContabilidad.Ninguno },
            { 10014, ClsPermisosPantallaContabilidad.Ninguno }
        };
    }
}
