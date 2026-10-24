using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CapaVista_Componentes.Componentes
{
    internal sealed class ClsActualizacionDisenoCompras : IDisposable
    {

        private readonly List<Control> _Contenedores = new List<Control>();
        private readonly ClsTableLayoutPanelCompras _PanelOculto;
        private bool _Finalizado;

        internal ClsActualizacionDisenoCompras(Control Contenedor) : this(Contenedor, null)
        {
        }

        internal ClsActualizacionDisenoCompras(Control Contenedor, ClsTableLayoutPanelCompras PanelOculto)
        {
            for (Control Actual = Contenedor; Actual != null; Actual = Actual.Parent) _Contenedores.Add(Actual);
            for (int Indice = _Contenedores.Count - 1; Indice >= 0; Indice--) _Contenedores[Indice].SuspendLayout();
            _PanelOculto = PanelOculto;
            _PanelOculto?.ComprasProcOcultarActualizacion();
        }

        public void Dispose()
        {
            if (_Finalizado) return;
            _Finalizado = true;
            try
            {
                ComprasProcReanudar(0);
            }
            finally
            {
                _PanelOculto?.ComprasProcRestaurarActualizacion();
            }
        }

        private void ComprasProcReanudar(int Indice)
        {
            if (Indice == _Contenedores.Count) return;
            try
            {
                _Contenedores[Indice].ResumeLayout(true);
            }
            finally
            {
                ComprasProcReanudar(Indice + 1);
            }
        }

        internal static void ComprasProcAltura(RowStyle Fila, float Altura)
        {
            if (Fila.SizeType != SizeType.Absolute) Fila.SizeType = SizeType.Absolute;
            if (Fila.Height != Altura) Fila.Height = Altura;
        }

    }
}
