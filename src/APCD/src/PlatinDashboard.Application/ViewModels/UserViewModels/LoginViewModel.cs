using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlatinDashboard.Application.ViewModels.UserViewModels
{
    public class LoginViewModel
    {
        [DisplayName("Usuário")]
        [Required(ErrorMessage = "O campo usuário é obrigatório")]
        public string UserName { get; set; }

        [DisplayName("Senha")]
        [Required(ErrorMessage = "O campo senha é obrigatório")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
