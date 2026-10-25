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
    public enum TipoPermisoContabilidad
    {
        Insertar,
        Editar,
        Eliminar,
        Imprimir
    }

    public sealed class ClsPermisosPantallaContabilidad
    {
        public int IdModulo { get; set; }
        public int IdAplicacion { get; set; }
        public bool TieneAcceso { get; set; }
        public bool PuedeInsertar { get; set; }
        public bool PuedeEditar { get; set; }
        public bool PuedeEliminar { get; set; }
        public bool PuedeImprimir { get; set; }

        public static ClsPermisosPantallaContabilidad Ninguno
        {
            get { return new ClsPermisosPantallaContabilidad(); }
        }

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
