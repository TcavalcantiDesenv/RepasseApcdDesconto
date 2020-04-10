using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.Entity
{
    
    public class Macon
    {
        [Key]
        public int Id_Macon { get; set; }
        [Required(ErrorMessage = "Nome é obrigatório")]
        [DisplayName("Loja")]
        public int Id_Loja { get; set; }
        [Required(ErrorMessage = "Grau do Maçon é obrigatório")]
        [DisplayName("Grau")]
        public int Id_Graduacao { get; set; }
        public int Id_Cargo { get; set; }
        public int Id_Condecoracao { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [MinLength(3, ErrorMessage = "Nome deve conter no mínimo 3 caracteres")]
        [MaxLength(255, ErrorMessage = "Nome deve conter no máximo 255 caracteres")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Números e caracteres especiais não são permitidos.")]
        [DisplayName("Nome do Maçon")]
        public string Nome { get; set; }

        [Display(Name = "CIM", Description = "Informe somente numeros entre 1 e 99999999.")]
        [Range(1, 99999999)]
        [Required(ErrorMessage = "CIM é obrigatório")]
        public string Cim { get; set; }

        [MinLength(3, ErrorMessage = "Nome tratamento deve conter no mínimo 3 caracteres")]
        [MaxLength(255, ErrorMessage = "Nome tratamento deve conter no máximo 255 caracteres")]
        public string Nome_Tratamento { get; set; }
        public string Senha { get; set; }
        public string Situacao { get; set; }

        [MinLength(8, ErrorMessage = "CPF deve conter no mínimo 6 caracteres")]
        [MaxLength(12, ErrorMessage = "CPF deve conter no máximo 12 caracteres")]
        public string CPF { get; set; }

        public string Grau { get; set; }
        public string Login { get; set; }
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        public string Email { get; set; }
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        public string Email_Outros { get; set; }

        [DisplayName("Data de Nascimento")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime Dt_Nascimento { get; set; }

        [DisplayName("Data de Iniciação")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime Dt_Iniciado { get; set; }

        [DisplayName("Data de Casamento")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime Data_Casamento { get; set; }

        [DisplayName("Data de Emissao")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime Dt_Emissao_Di { get; set; }

        public string Naturalidade { get; set; }
        public string Uf { get; set; }
        public string Nacionalidade { get; set; }
        public string Religiao { get; set; }
        public string Escolaridade { get; set; }
        public string Sexo { get; set; }
        public string Tipo_Sanguíneo { get; set; }
        public string Estado_Civil { get; set; }


        public string Tipo_Di { get; set; }
        public string Num_Di { get; set; }
        public string Emissor_Di { get; set; }
        public string Uf_Di { get; set; }
        public string Tit_Eleitor { get; set; }
        public string Zona_Eleitoral { get; set; }
        public string Secao_Eleitoral { get; set; }
        public string Nome_Pai { get; set; }
        public string Nome_Mae { get; set; }
        public string End_Res { get; set; }
        public string Bairro_Res { get; set; }
        public string Cidade_Res { get; set; }
        public string Uf_Res { get; set; }
        public string Cep_Res { get; set; }
        public string cx_postal { get; set; }
        public string celular { get; set; }
        public string tel_res { get; set; }
        public string fax { get; set; }
        public string obs { get; set; }
        public string Guide { get; set; }
        public string correspondencia_end { get; set; }
        public byte[] foto { get; set; }
        public int Estados { get; set; }
        public PagedList<Macon> Macons { get; set; }

        public bool IsChecked { get; set; }

        public Macon()
        {

        }
        public Macon(int id)  
        {
            Id_Macon = id;
        }
    }
    public class MaconModel
    {
        public List<Macon> ListaMacon { get; set; }
    }
}
