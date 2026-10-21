using System;
using System.Collections.Generic;


// Oskar Cermeño

namespace CapaModelo_Contabilidad.Entidades
{
    public class ClsPolizaContabilidad
    {
        // id_poliza INT AUTO_INCREMENT PRIMARY KEY
        public int IdPoliza { get; set; }

        // codigo_poliza VARCHAR(40) NOT NULL
        public string CodigoPoliza { get; set; }

        // fecha DATE NOT NULL
        public DateTime Fecha { get; set; }

        // estado ENUM('BORRADOR','APLICADA','ANULADA') NOT NULL DEFAULT 'BORRADOR'
        public string Estado { get; set; } = "BORRADOR";

        // periodo_contable CHAR(7) NOT NULL (Formato 'YYYY-MM')
        public string PeriodoContable { get; set; }

        // origen VARCHAR(50) NOT NULL DEFAULT 'MANUAL' (MANUAL, ACTIVOS_FIJOS, PRESUPUESTO, etc.)
        public string Origen { get; set; } = "MANUAL";

        // descripcion VARCHAR(255) NULL
        public string Descripcion { get; set; }

        // Líneas del detalle asociadas a la póliza
        public List<ClsPolizaDetalleContabilidad> Detalle { get; set; } = new List<ClsPolizaDetalleContabilidad>();
    }
}