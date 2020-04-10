using System;

namespace Model.Entity
{


    public class Arquivos_Documentos
    {
        public int Id_Arquivos_Documentos { get; set; }
        public string Tipo { get; set; }
        public string Titulo { get; set; }
        public string Nome { get; set; }
        public string Autor { get; set; }
        public Nullable<System.DateTime> Dt { get; set; }
        public string Descricao { get; set; }
        public int Estados { get; set; }

    }
}
