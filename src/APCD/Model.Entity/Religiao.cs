using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Religiao
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        //public List<Religiao> ListaReligiao()
        //{
        //    return new List<Religiao>
        //    {
        //        new Religiao { Id = 1, Nome = "Catolico"},
        //        new Religiao { Id = 2, Nome = "Evangélico"},
        //         new Religiao { Id = 3, Nome = "Espírita"},
        //          new Religiao { Id = 4, Nome = "Umbandista"},
        //           new Religiao { Id = 5, Nome = "Muçulmano"},
        //             new Religiao { Id = 7, Nome = "Outros"},
        //    };
        //}
    }
}
