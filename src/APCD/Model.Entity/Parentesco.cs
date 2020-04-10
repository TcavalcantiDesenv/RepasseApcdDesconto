using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Parentesco
    {
        public int IdParentesco { get; set; }
        public string Nome { get; set; }

        public List<Parentesco> ListaParentesco()
        {
            return new List<Parentesco>
            {
                new Parentesco { IdParentesco = 1, Nome = "Esposa"},
                new Parentesco { IdParentesco = 2, Nome = "Filho"},
                new Parentesco { IdParentesco = 3, Nome = "Filha"},
                new Parentesco { IdParentesco = 4, Nome = "Pai"},
                new Parentesco { IdParentesco = 5, Nome = "Mãe"},
                new Parentesco { IdParentesco = 6, Nome = "Irmão"},
                new Parentesco { IdParentesco = 7, Nome = "Irmã"},

            };
        }
        public List<Sexo> ListaSexo()
        {
            return new List<Sexo>
            {
                new Sexo { Id = 1, Nome = "Maculino"},
                new Sexo { Id = 2, Nome = "Feminino"},
            };
        }
    }
}
