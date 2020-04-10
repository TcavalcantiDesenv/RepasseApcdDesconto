using AutoMapper;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlatinDashboard.Application.ViewModels.UserViewModels
{
    public class ProfileUserViewModel
    {
        [Key]
        public string UserId { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [MinLength(3, ErrorMessage = "O campo nome deve conter no mínimo 3 caracteres")]
        [MaxLength(128, ErrorMessage = "O campo nome deve conter no máximo 128 caracteres")]
        [DisplayName("Nome do Usuário")]
        public string Name { get; set; }

        [MinLength(3, ErrorMessage = "O campo sobrenome deve conter no mínimo 3 caracteres")]
        [MaxLength(128, ErrorMessage = "O campo sobrenome deve conter no máximo 128 caracteres")]
        [DisplayName("Sobrenome")]
        public string LastName { get; set; }

        [DisplayName("Login")]
        [ScaffoldColumn(false)]
        public string UserName { get; set; }

        [DisplayName("E-mail")]
        [ScaffoldColumn(false)]
        public string Email { get; set; }

        [DisplayName("Status")]
        [ScaffoldColumn(false)]
        public bool Active { get; set; }

        [DisplayName("Tipo de Usuário")]
        [ScaffoldColumn(false)]
        public string UserType { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreatedAt { get; set; }

        [ScaffoldColumn(false)]
        public DateTime UpdatedAt { get; set; }








        public int Cim { get; set; }
        public Nullable<int> Id_Loja { get; set; }
        public Nullable<int> Id_Graduacao { get; set; }
        public Nullable<int> Id_Cargo { get; set; }
        public Nullable<int> Id_Condecoracao { get; set; }
        public string Nome { get; set; }
        public string Nome_Tratamento { get; set; }
        public string Senha { get; set; }
        public string Situacao { get; set; }
        public string CPF { get; set; }
        public string Grau { get; set; }
        public string Login { get; set; }
        public string Email_Outros { get; set; }
        public string Dt_Nascimento { get; set; }
        public DateTime Dt_Iniciado { get; set; }
        public string Naturalidade { get; set; }
        public string Uf { get; set; }
        public string Nacionalidade { get; set; }
        public string Religiao { get; set; }
        public string Escolaridade { get; set; }
        public string Sexo { get; set; }
        public string Tipo_Sanguíneo { get; set; }
        public string Estado_Civil { get; set; }
        public string Data_Casamento { get; set; }
        public string Tipo_Di { get; set; }
        public string Num_Di { get; set; }
        public string Emissor_Di { get; set; }
        public string Dt_Emissao_Di { get; set; }
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












        public ProfileUserViewModel()
        {

        }

        public ProfileUserViewModel(UserViewModel userViewModel)
        {
            Mapper.Map(userViewModel, this);
        }
    }
}
