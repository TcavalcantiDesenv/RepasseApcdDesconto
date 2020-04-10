using System;

namespace Model.Entity
{

    public partial class Profissao
    {
        public int Id_Profissao { get; set; }
        public Nullable<int> Cim { get; set; }
        public string Aposentado { get; set; }
        public string Profissao1 { get; set; }
        public string Empresa_Orgao { get; set; }
        public string Cargo { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public string Email { get; set; }
        public string Fone { get; set; }
        public string Fax { get; set; }
        public int Estados { get; set; }

    }
}
