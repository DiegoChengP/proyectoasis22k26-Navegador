using System;

namespace CapaModelo_TipoCambio.Entidades
{

    public class ClsTipoCambio
    {
        public DateTime FechaTasa { get; set; }
        public string MonedaOrigen  { get; set; }   // "USD", "EUR", "GTQ"
        public string MonedaDestino { get; set; }   // "GTQ", "USD", etc.
        public decimal Tasa { get; set; }            // 4 decimales
        public string Fuente { get; set; }           // "Demo – No proviene de Banguat"
    }
}
