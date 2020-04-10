using System;
using System.Data;

namespace PlatinDashboard.Domain.Farmacia.Entities
{
    public class VendaClassificacaoLojaSp
    {
        public DateTime Dias { get; set; }
        public string Mes { get; set; }
        public int Uad { get; set; }
        public int Cls { get; set; }
        public decimal ValorBruto { get; set; }
        public decimal? Meta { get; set; }
        public decimal Percentual { get; set; }

        public VendaClassificacaoLojaSp()
        {

        }

        public VendaClassificacaoLojaSp(DataRow dataRow)
        {
            Dias = dataRow["dias"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataRow["dias"]);
            Mes = dataRow["mes"].ToString();
            Uad = Convert.ToInt32(dataRow["uad"]);
            Cls = Convert.ToInt32(dataRow["cls"]);
            ValorBruto = Convert.ToDecimal(dataRow["valorbruto"]);
            Meta = Convert.ToDecimal(dataRow["meta"]);
            Percentual = Convert.ToDecimal(dataRow["percentual"]);
        }
    }
}
