using System;
using System.Data;
using CapaModelo_Cotizaciones;
using CapaControlador_Cotizaciones.Validaciones;
using VentasValidaciones;

namespace CapaControlador_Cotizaciones.Servicios
{
    internal sealed partial class ClsCotizacionesServicio
    {
        public DataTable Listar(string Buscar)
        {
            Buscar = (Buscar ?? "").Trim();
            using (var Db = new ClsCotizacionesRepositorio()) return Db.Tabla(ConsultaCotizaciones.ListarConsultarCotizacion, Buscar, Buscar, Buscar);
        }
    }
}
