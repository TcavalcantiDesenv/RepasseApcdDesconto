using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Entity
{  
   public class Familiar
    {
        [Key]
        public int Id_Familiar { get; set; }
        public string Id_Macon { get; set; }
        public int Id_Loja { get; set; }
        public int Cim { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public string Grau { get; set; }
        public DateTime Aniversario { get; set; }

    }
}
