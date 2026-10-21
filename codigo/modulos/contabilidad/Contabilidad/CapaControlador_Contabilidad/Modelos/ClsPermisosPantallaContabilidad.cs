/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 21/10/2026
 * ==================================================================
 * Propósito :
 *  Permisos del usuario sobre una pantalla de Contabilidad. Es el
 *  tipo que ve la Vista, así no depende de las clases de Seguridad.
 * ===================================================================
*/

namespace CapaControlador_Contabilidad
{
    // Acciones que Seguridad permite asignar.
    public enum TipoPermisoContabilidad
    {
        Insertar,
        Editar,
        Eliminar,
        Imprimir
    }

    public sealed class ClsPermisosPantallaContabilidad
    {
        // Puede abrir la pantalla.
        public bool TieneAcceso { get; set; }
        // Puede insertar.
        public bool PuedeInsertar { get; set; }
        // Puede editar.
        public bool PuedeEditar { get; set; }
        // Puede eliminar.
        public bool PuedeEliminar { get; set; }
        // Puede imprimir.
        public bool PuedeImprimir { get; set; }

        // Sin ningún permiso (usuario sin sesión o sin asignación).
        public static ClsPermisosPantallaContabilidad Ninguno
        {
            get { return new ClsPermisosPantallaContabilidad(); }
        }

        // Todos los permisos (pruebas y diseñador).
        public static ClsPermisosPantallaContabilidad Total
        {
            get
            {
                return new ClsPermisosPantallaContabilidad
                {
                    TieneAcceso = true, PuedeInsertar = true, PuedeEditar = true, PuedeEliminar = true, PuedeImprimir = true
                };
            }
        }

        // Indica si la acción está permitida.
        public bool ContabilidadFuncPermite(TipoPermisoContabilidad Tipo)
        {
            switch (Tipo)
            {
                case TipoPermisoContabilidad.Insertar: return PuedeInsertar;
                case TipoPermisoContabilidad.Editar: return PuedeEditar;
                case TipoPermisoContabilidad.Eliminar: return PuedeEliminar;
                default: return PuedeImprimir;
            }
        }
    }
}
