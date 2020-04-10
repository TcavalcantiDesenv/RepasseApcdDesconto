using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlatinDashboard.Application.ViewModels.UserViewModels
{
    public class ChangePasswordViewModel
    {
        [Key]
        public string UserId { get; set; }

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
    }
}
