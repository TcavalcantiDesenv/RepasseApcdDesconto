using System;

namespace Model.Entity
{

    public partial class Graduacao
    {
        public int Id_Graduacao { get; set; }
        public string Descricao { get; set; }
        public Nullable<int> Numero { get; set; }
        public string Sigla { get; set; }
        public string Meses_Intersticio { get; set; }
        public Nullable<System.DateTime> Dt_Inicial { get; set; }
        public Nullable<System.DateTime> Dt_Final { get; set; }
        public int Estados { get; set; }

    }
}
