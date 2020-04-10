using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlatinDashboard.Application.ViewModels.UserViewModels
{
    public class CreateUserViewModel
    {
        public string UserId { get; set; }
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [DisplayName("Nome da Regional")]
        public string Name { get; set; }

        [DisplayName("Codigo da Regional")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O campo login é obrigatório")]
        [DisplayName("Login")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Tipo de Usuário")]
        public string UserType { get; set; }

        [DisplayName("Novo Valor")]
        public string NovoValor { get; set; }

        [DisplayName("Data Inicial")]
        public string DataInicial { get; set; }

        [DisplayName("Data Final")]
        public string DataFinal { get; set; }

        [DisplayName("Observação")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório")]
        [MaxLength(255, ErrorMessage = "O campo e-mail deve conter no máximo 255 caracteres")]
        [DisplayName("Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo confirmação de senha é obrigatório")]
        [DisplayName("Confirmação de Senha")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "A senha informada não confere")]
        public string ConfirmPassword { get; set; }

        public int[] UserStoresIds { get; set; }

        public CreateUserViewModel()
        {
            UserType = "Comum";
            UserStoresIds = new int[] { };
        }
    }
}
