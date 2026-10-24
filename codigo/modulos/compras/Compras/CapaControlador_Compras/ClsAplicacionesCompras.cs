namespace CapaControlador_Compras
{
    public static class ClsAplicacionesCompras
    {
        public const int MantenimientoProveedor = 6010;
        public const int MantenimientoMetodoPago = 6011;

        public static int ComprasFuncObtenerIdModulo()
        {
            return CapaModelo_Compras.ClsModuloCompras.ComprasFuncObtenerIdModulo();
        }
    }
}
