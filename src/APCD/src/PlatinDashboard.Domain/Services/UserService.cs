using PlatinDashboard.Domain.Entities;
using PlatinDashboard.Domain.Interfaces.Repositories;
using PlatinDashboard.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlatinDashboard.Domain.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetByCompany(int companyId)
        {
            return _userRepository.GetByCompany(companyId);
        }

        public Tuple<User, bool, string, string> Add(User user, string companyType)
        {
            var response = VerifyUser(user);
            //Caso houver problema retornar erro
            if (!response.Item2) return response;
            //Liberando o acesso ao dashboard para usuario de empresa cliente
            if (companyType == "Cliente")
            {
                user.UserClaims.Add(new UserClaim { ClaimType = "ChartDashboard", ClaimValue = "Allowed" });
            }
            //Adicionando Claims com o tipo da empresa do novo usuário
            user.UserClaims.Add(new UserClaim { ClaimType = "CompanyType", ClaimValue = companyType });
            user = _userRepository.Add(user);
            return new Tuple<User, bool, string, string>(user, true, string.Empty, string.Empty);
        }

        public Tuple<User, bool, string, string> VerifyUser(User user)
        {
            //Método para verificar a disponibilidade do login e e-mail
            //Verificando se o e-mail está disponível
            //if (!_userRepository.CheckEmail(user.Email))
            //    return new Tuple<User, bool, string, string>(user, false, "Email", "Endereço de e-mail já está em uso");
            //Verificando se o username está disponível
            if (!_userRepository.CheckUserName(user.UserName))
                return new Tuple<User, bool, string, string>(user, false, "UserName", "Login já está em uso");
            //Caso estiver disponível retornar sem mensagem de erro
            return new Tuple<User, bool, string, string>(user, true, string.Empty, string.Empty);
        }

        public User GetById(string userId)
        {
            return _userRepository.GetById(userId);
        }

        public void UpdateUserCharts(string userId, IEnumerable<Chart> charts)
        {
            //Método para alterar os graficos que o usuário possui acesso
            var user = _userRepository.GetById(userId);
            //Verificando se a empresa do usuário possui acesso a todos 
            // os graficos que foram configurados para o usuário
            var chartList = charts.ToList();
            for (int i = 0; i < chartList.Count(); i++)
            {
                if (!user.Company.Charts.Any(c => c.ClaimType == chartList.ElementAt(i).ClaimType))
                {
                    chartList.Remove(chartList.ElementAt(i));
                }
            }
            //Buscando as claims com acesso aos gráficos do usuário
            for (int i = 0; i < user.UserClaims.Count(); i++)
            {
                //Se o gráfico não estiver mais na relação de gráficos do usuário, ele deve ser removido da claims
                var currentClaim = user.UserClaims.ElementAt(i);
                if (!chartList.Any(c => c.ClaimType == currentClaim.ClaimType) 
                    && currentClaim.ClaimType.StartsWith("Chart"))
                {
                    _userRepository.RemoveClaim(currentClaim);
                }
            }
            //Adicionando os novos gráficos que o usuário possui acesso
            foreach (var chart in chartList.Where(ch => !user.UserClaims.Any(u => u.ClaimType == ch.ClaimType)))
            {
                var userClaim = new UserClaim { UserId = user.UserId, ClaimType = chart.ClaimType, ClaimValue = "Allowed" };
                user.UserClaims.Add(userClaim);
            }
            _userRepository.Update(user);
        }

        public new void Remove(User user)
        {
            _userRepository.Remove(user);
        }
    }
}
