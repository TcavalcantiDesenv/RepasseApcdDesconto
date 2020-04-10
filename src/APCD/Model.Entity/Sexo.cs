using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Sexo
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public List<Sexo> ListaSexo()
        {
            return new List<Sexo>
            {
                new Sexo { Id = 1, Nome = "Masculino"},
                new Sexo { Id = 2, Nome = "Feminino"},
            };
        }
    }
}
