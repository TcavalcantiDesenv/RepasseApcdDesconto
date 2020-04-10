using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
   public  class Acessos
    {
        public int IdAcesso { get; set; }
        public int Estados { get; set; }
        public string Usuario { get; set; }
        public string Nome { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime? DataSaida { get; set; }
        public string IP { get; set; }
        public string Empresa { get; set; }

    }
}
