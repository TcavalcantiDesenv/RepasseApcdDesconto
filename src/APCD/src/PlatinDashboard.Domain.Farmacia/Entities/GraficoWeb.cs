using System;
using System.Data;

namespace PlatinDashboard.Domain.Farmacia.Entities
{
    public class GraficoWeb
    {
        public DateTime? Dat { get; set; }
        public string My { get; set; }
        public int? TotaldeLojas { get; set; }
        public decimal? Lucro { get; set; }
        public decimal? VendaBruta { get; set; }
        public decimal? Liquida { get; set; }
        public decimal? Desconto { get; set; }
        public decimal? PercentualMargem { get; set; }
        public decimal? QtMediaClientes { get; set; }
        public decimal? ClientesAtendidos { get; set; }
        public decimal? Custo { get; set; }
        public decimal? Devolucao { get; set; }

        public GraficoWeb()
        {

        }

        public GraficoWeb(DataRow dataRow)
        {
            Dat = dataRow["dat"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataRow["dat"]);
            My = dataRow["my"].ToString();
            TotaldeLojas = Convert.ToInt32(dataRow["totaldelojas"]);
            Lucro = Convert.ToDecimal(dataRow["lucro"]);
            VendaBruta = Convert.ToDecimal(dataRow["vendabruta"]);
            Liquida = Convert.ToDecimal(dataRow["liquida"]);
            Desconto = Convert.ToDecimal(dataRow["desconto"]);
            PercentualMargem = Convert.ToDecimal(dataRow["percentual_margem"]);
            QtMediaClientes = dataRow["qt_media_clientes"] == DBNull.Value ? 0 : Convert.ToDecimal(dataRow["qt_media_clientes"]);
            ClientesAtendidos = Convert.ToDecimal(dataRow["clientes_atendidos"]);
            Custo = Convert.ToDecimal(dataRow["custo"]);
            Devolucao = Convert.ToDecimal(dataRow["devolucao"]);
        }
    }
}
