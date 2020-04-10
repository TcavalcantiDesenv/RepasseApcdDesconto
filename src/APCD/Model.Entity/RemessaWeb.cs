using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model.Entity
{
   public class RemessaWeb
    {
        [Key]
        public int indice { get; set; }
        public string CodRegional { get; set; }
        public string NomeRegional { get; set; }
        public string PN { get; set; }
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Vencimento { get; set; }
        public string Valor { get; set; }
        public string LCM { get; set; }
        public string Codigo { get; set; }
        public string sequencia { get; set; }
        public string Documento { get; set; }
        public string ValorRepasse { get; set; }
        public string MesAno { get; set; }
        public string Empresa { get; set; }
        public string Conta { get; set; }
        public string Ativo { get; set; }
        [DisplayName("Data Pagamento:")]
        public string DataPagto { get; set; }
        public string DataDia { get; set; }
        public bool Pago { get; set; }
    }
    public class RemessaModel
    {
        public List<RemessaWeb> ListaRemessa { get; set; }
    }
}
