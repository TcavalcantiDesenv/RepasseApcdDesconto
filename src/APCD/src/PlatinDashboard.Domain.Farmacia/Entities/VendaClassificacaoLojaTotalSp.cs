using System;
using System.Data;

namespace PlatinDashboard.Domain.Farmacia.Entities
{
    public class VendaClassificacaoLojaTotalSp
    {
        public DateTime Dias { get; set; }
        public string Mes { get; set; }
        public int Uad { get; set; }
        public decimal ValorBruto { get; set; }
        public decimal? Meta { get; set; }
        public decimal Percentual { get; set; }

        public VendaClassificacaoLojaTotalSp()
        {

        }

        public VendaClassificacaoLojaTotalSp(DataRow dataRow)
        {
            Dias = dataRow["dias"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataRow["dias"]);
            Mes = dataRow["mes"].ToString();
            Uad = Convert.ToInt32(dataRow["uad"]);
            ValorBruto = Convert.ToDecimal(dataRow["valorbruto"]);
            Meta = Convert.ToDecimal(dataRow["meta"]);
            Percentual = Convert.ToDecimal(dataRow["percentual"]);
        }
    }
}
