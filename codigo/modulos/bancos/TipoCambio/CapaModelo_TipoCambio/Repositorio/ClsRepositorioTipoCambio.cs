using System;
using System.Collections.Generic;
using CapaModelo_TipoCambio.Entidades;

namespace CapaModelo_TipoCambio.Repositorio
{

    public class ClsRepositorioTipoCambio
    {
        // ── Catálogo de tasas actuales (demo) 
        private static readonly List<ClsTipoCambio> _tasasActuales =
            new List<ClsTipoCambio>
            {
                new ClsTipoCambio
                {
                    FechaTasa     = new DateTime(2026, 10, 13),
                    MonedaOrigen  = "USD",
                    MonedaDestino = "GTQ",
                    Tasa          = 7.7532m,
                    Fuente        = "Demo – No proviene de Banguat"
                },
                new ClsTipoCambio
                {
                    FechaTasa     = new DateTime(2026, 10, 13),
                    MonedaOrigen  = "EUR",
                    MonedaDestino = "GTQ",
                    Tasa          = 8.4215m,
                    Fuente        = "Demo – No proviene de Banguat"
                },
                new ClsTipoCambio
                {
                    FechaTasa     = new DateTime(2026, 10, 13),
                    MonedaOrigen  = "EUR",
                    MonedaDestino = "USD",
                    Tasa          = 1.0862m,
                    Fuente        = "Demo – No proviene de Banguat"
                }
            };

        // Historial demo (09/10/2026 al 13/10/2026) 
        private static readonly List<ClsTipoCambio> _historico =
            new List<ClsTipoCambio>
            {
                new ClsTipoCambio
                {
                    FechaTasa     = new DateTime(2026, 10, 13),
                    MonedaOrigen  = "USD",
                    MonedaDestino = "GTQ",
                    Tasa          = 7.7532m,
                    Fuente        = "Demostración"
                },
                new ClsTipoCambio
                {
                    FechaTasa     = new DateTime(2026, 10, 12),
                    MonedaOrigen  = "USD",
                    MonedaDestino = "GTQ",
                    Tasa          = 7.7510m,
                    Fuente        = "Demostración"
                },
                new ClsTipoCambio
                {
                    FechaTasa     = new DateTime(2026, 10, 11),
                    MonedaOrigen  = "USD",
                    MonedaDestino = "GTQ",
                    Tasa          = 7.7495m,
                    Fuente        = "Demostración"
                },
                new ClsTipoCambio
                {
                    FechaTasa     = new DateTime(2026, 10, 10),
                    MonedaOrigen  = "USD",
                    MonedaDestino = "GTQ",
                    Tasa          = 7.7480m,
                    Fuente        = "Demostración"
                },
                new ClsTipoCambio
                {
                    FechaTasa     = new DateTime(2026, 10, 9),
                    MonedaOrigen  = "USD",
                    MonedaDestino = "GTQ",
                    Tasa          = 7.7472m,
                    Fuente        = "Demostración"
                }
            };

        // summary>Devuelve las tasas actuales disponibles
        public List<ClsTipoCambio> BancosMetObtenerTasasActuales()
        {
            return _tasasActuales;
        }


        // Devuelve la tasa actual del par indicado, o null si no existe

        public ClsTipoCambio BancosMetObtenerTasaActual(
            string monedaOrigen, string monedaDestino)
        {
            return _tasasActuales.Find(
                t => t.MonedaOrigen == monedaOrigen &&
                     t.MonedaDestino == monedaDestino);
        }

     
        // Devuelve el historial filtrado por rango de fechas y par de monedas

        public List<ClsTipoCambio> BancosMetObtenerHistorico(
            DateTime desde,
            DateTime hasta,
            string monedaOrigen,
            string monedaDestino)
        {
            return _historico.FindAll(t =>
                t.FechaTasa >= desde &&
                t.FechaTasa <= hasta &&
                (string.IsNullOrEmpty(monedaOrigen)  || monedaOrigen  == "Todas" || t.MonedaOrigen  == monedaOrigen) &&
                (string.IsNullOrEmpty(monedaDestino) || monedaDestino == "Todas" || t.MonedaDestino == monedaDestino));
        }

        // Obtiene el resumen del historial (Mínima, Máxima, Variación)
 
        public (decimal Minima, decimal Maxima, decimal Variacion) BancosMetObtenerResumenHistorico()
        {
            decimal min = 7.7472m;
            decimal max = 7.7532m;
            decimal var = max - min;
            return (min, max, var);
        }
    }
}
