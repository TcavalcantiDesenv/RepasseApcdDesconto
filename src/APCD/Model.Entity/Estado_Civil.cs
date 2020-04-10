using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Estado_Civil
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public List<Estado_Civil> ListaCivil()
        {
            return new List<Estado_Civil>
            {
                new Estado_Civil { Id = 1, Nome = "Casado"},
                new Estado_Civil { Id = 2, Nome = "Solteiro"},
                new Estado_Civil { Id = 2, Nome = "Divorciado"},
                new Estado_Civil { Id = 2, Nome = "Viuvo"},
                new Estado_Civil { Id = 2, Nome = "Outros"},
            };
        }
    }
}
