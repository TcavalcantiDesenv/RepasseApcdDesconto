﻿namespace PlatinDashboard.Domain.Farmacia.Entities
{
    public class VendaLojaPorHora
    {
        public int Loja { get; set; }
        public string My { get; set; }
        public int ClientesAtendidos { get; set; }
        public decimal? Valor { get; set; }
        public decimal? Desconto { get; set; }
        public decimal? Devolucao { get; set; }
        public int Hora { get; set; }
    }
}
