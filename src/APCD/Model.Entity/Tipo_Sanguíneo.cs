using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Tipo_Sanguíneo
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public List<Tipo_Sanguíneo> ListaSangue()
        {
            return new List<Tipo_Sanguíneo>
            {
                new Tipo_Sanguíneo { Id = 1, Nome = "A"},
                new Tipo_Sanguíneo { Id = 2, Nome = "B"},
                new Tipo_Sanguíneo { Id = 3, Nome = "O"},
                new Tipo_Sanguíneo { Id = 4, Nome = "AB"},
                new Tipo_Sanguíneo { Id = 5, Nome = "O−"},
                new Tipo_Sanguíneo { Id = 6, Nome = "O+"},
                new Tipo_Sanguíneo { Id = 7, Nome = "A−"},
                new Tipo_Sanguíneo { Id = 8, Nome = "A+"},
                new Tipo_Sanguíneo { Id = 9, Nome = "B−"},
                new Tipo_Sanguíneo { Id = 10, Nome = "B+"},
                new Tipo_Sanguíneo { Id = 11, Nome = "AB−"},
                new Tipo_Sanguíneo { Id = 12, Nome = "AB+"}
            };
        }
    }
}
