using System;

namespace Loja
{

    public partial class Visitante
    {
        public int Id_Visitante { get; set; }
        public Nullable<int> Id_Loja { get; set; }
        public Nullable<int> Cim { get; set; }
        public string Obediencia { get; set; }
        public Nullable<System.DateTime> Dt_Visita { get; set; }
        public int Estados { get; set; }

    }
}
