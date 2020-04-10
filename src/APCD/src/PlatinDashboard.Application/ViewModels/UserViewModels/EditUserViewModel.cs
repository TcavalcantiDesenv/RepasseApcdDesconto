using AutoMapper;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlatinDashboard.Application.ViewModels.UserViewModels
{
    public class EditUserViewModel
    {
        [Key]
        public string UserId { get; set; }

        [DisplayName("Nome da Regional")]
        public string Name { get; set; }

        [DisplayName("Codigo da Regional")]
        public string LastName { get; set; }

        [DisplayName("Login")]
        [ScaffoldColumn(false)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório")]
        [DisplayName("E-mail")]
        [ScaffoldColumn(false)]
        public string Email { get; set; }

        [DisplayName("Novo Valor")]
        public string NovoValor { get; set; }

        [DisplayName("Data Inicial")]
        public string DataInicial { get; set; }

        [DisplayName("Data Final")]
        public string DataFinal { get; set; }

        [DisplayName("Valor Ativo")]
        public bool PhoneNumberConfirmed { get; set; }

        [DisplayName("Observação")]
        public string Descricao { get; set; }

        [DisplayName("Status")]
        public bool Active { get; set; }

        [DisplayName("Tipo de Usuário")]
        public string UserType { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreatedAt { get; set; }

        [ScaffoldColumn(false)]
        public DateTime UpdatedAt { get; set; }

        public int[] UserStoresIds { get; set; }

        public EditUserViewModel()
        {
            UserStoresIds = new int[] { };
        }

        public EditUserViewModel(UserViewModel userViewModel)
        {
            UserStoresIds = userViewModel.UserStoresIds;
            Mapper.Map(userViewModel, this);
        }
    }
}
