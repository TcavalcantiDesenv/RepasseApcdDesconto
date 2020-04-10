using System.Configuration;

namespace PlatinDashboard.Presentation.MVC.ApiServices
{
    public class ServicosApi
    {
        private readonly string _abcfarmaUrl = ConfigurationManager.AppSettings["abcfarmaUrl"];
        private readonly string _cnpj_cpf = ConfigurationManager.AppSettings["cnpj_cpf"];
        private readonly string _senha = ConfigurationManager.AppSettings["senha"];
        private readonly string _cnpj_sh = ConfigurationManager.AppSettings["cnpj_sh"];


    }
}