using System;
using System.Collections.Generic;
using System.Data;
using CapaModelo_TipoCambio.Entidades;
using CapaModelo_TipoCambio.Repositorio;

namespace CapaControlador_TipoCambio
{

    public class ClsControladorTipoCambio
    {
        private readonly ClsRepositorioTipoCambio _repositorio;

        public ClsControladorTipoCambio()
        {
            _repositorio = new ClsRepositorioTipoCambio();
        }

        // Tasas actuales 

       
        // Devuelve un DataTable con las tasas actuales para el DataGridView.
    
        public DataTable BancosMetObtenerTasasActuales()
        {
            var lista = _repositorio.BancosMetObtenerTasasActuales();
            return BancosMetListaATablaTasas(lista);
        }


        // Simula actualización desde Banguat (pendiente de integración real).

        public string BancosMetActualizarDesdeBanguat()
        {
            return "⚠ Integración con Banguat pendiente.\n" +
                   "Cuando se implemente, esta acción consultará el WSDL de Banguat\n" +
                   "y almacenará las tasas oficiales en la base de datos MySQL.";
        }

        //  Histórico 


        // Devuelve el historial de tasas como DataTable.

        public DataTable BancosMetObtenerHistorico(
            DateTime desde,
            DateTime hasta,
            string monedaOrigen,
            string monedaDestino)
        {
            var lista = _repositorio.BancosMetObtenerHistorico(
                desde, hasta, monedaOrigen, monedaDestino);
            return BancosMetListaATablaTasas(lista);
        }


        // Obtiene el resumen del historial (Mínima, Máxima, Variación).

        public (decimal Minima, decimal Maxima, decimal Variacion) BancosMetObtenerResumenHistorico()
        {
            return _repositorio.BancosMetObtenerResumenHistorico();
        }

        //  Conversor 


        // Convierte un importe de monedaOrigen a monedaDestino usando la tasa demo.
        // Retorna (resultado, tasa aplicada, mensaje).

        public (decimal Resultado, decimal TasaAplicada, string Mensaje)
            BancosMetConvertir(
                decimal importe,
                string monedaOrigen,
                string monedaDestino)
        {
            if (monedaOrigen == monedaDestino)
                return (importe, 1.0000m, "Misma moneda — tasa = 1.0000");

            var tasa = _repositorio.BancosMetObtenerTasaActual(
                monedaOrigen, monedaDestino);

            if (tasa != null)
            {
                decimal res = Math.Round(importe * tasa.Tasa, 4);
                return (res, tasa.Tasa,
                    $"1 {monedaOrigen} = {tasa.Tasa:N4} {monedaDestino} (Demo)");
            }

            // Intentar tasa inversa
            var tasaInv = _repositorio.BancosMetObtenerTasaActual(
                monedaDestino, monedaOrigen);

            if (tasaInv != null && tasaInv.Tasa != 0)
            {
                decimal tasaCalculada = Math.Round(1m / tasaInv.Tasa, 4);
                decimal res = Math.Round(importe * tasaCalculada, 4);
                return (res, tasaCalculada,
                    $"1 {monedaOrigen} = {tasaCalculada:N4} {monedaDestino} (Demo, inversa)");
            }

            return (0m, 0m,
                $"No se encontró tasa demo para {monedaOrigen}/{monedaDestino}.");
        }

        // ayudas

        private DataTable BancosMetListaATablaTasas(List<ClsTipoCambio> lista)
        {
            var tabla = new DataTable();
            tabla.Columns.Add("Fecha",          typeof(string));
            tabla.Columns.Add("Moneda Origen",  typeof(string));
            tabla.Columns.Add("Moneda Destino", typeof(string));
            tabla.Columns.Add("Tasa",           typeof(decimal));
            tabla.Columns.Add("Fuente",         typeof(string));

            foreach (var t in lista)
            {
                tabla.Rows.Add(
                    t.FechaTasa.ToShortDateString(),
                    t.MonedaOrigen,
                    t.MonedaDestino,
                    t.Tasa,
                    t.Fuente);
            }
            return tabla;
        }
    }
}
