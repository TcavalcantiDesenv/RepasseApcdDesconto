using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class ValorRegionais
    {
        [Key]
        public int Id { get; set; }
        public string CodRegional { get; set; }
        public string Regional { get; set; }
        public string DataInicial { get; set; }
        public string DataFinal { get; set; }
        public string Descricao { get; set; }
        public string Valor { get; set; }
        public bool Ativo { get; set; }
        public string DataCadastro { get; set; }
    }
}
