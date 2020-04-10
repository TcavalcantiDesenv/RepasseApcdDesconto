using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Presenca
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
    }

    public class PresencaModel
    {
        public List<Presenca> ListaPresenca { get; set; }
    }
}
