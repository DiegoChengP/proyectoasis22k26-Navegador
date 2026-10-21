
using System;

// Oskar Cermeño

namespace CapaModelo_Contabilidad.Entidades
{
    public class ClsPolizaDetalleContabilidad
    {
        // id_poliza_detalle INT AUTO_INCREMENT PRIMARY KEY
        public int IdPolizaDetalle { get; set; }

        // id_poliza INT NOT NULL (FK -> poliza_encabezado)
        public int IdPoliza { get; set; }

        // id_cuenta INT NOT NULL (FK -> cuenta)
        public int IdCuenta { get; set; }

        // debe DECIMAL(15,2) NOT NULL DEFAULT 0
        public decimal Debe { get; set; } = 0m;

        // haber DECIMAL(15,2) NOT NULL DEFAULT 0
        public decimal Haber { get; set; } = 0m;

        // --- Propiedades auxiliares para vistas / JOINs con la tabla 'cuenta' ---
        public string Codigo { get; set; }
        public string Cuenta { get; set; }
    }
}