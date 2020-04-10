using System.Collections.Generic;
using System.Linq;
using PlatinDashboard.Domain.Entities;
using PlatinDashboard.Domain.Interfaces.Repositories;
using PlatinDashboard.Domain.Interfaces.Services;

namespace PlatinDashboard.Domain.Services
{
    public class CompanyService : ServiceBase<Company>, ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService(ICompanyRepository companyRepository) : base(companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public IEnumerable<Chart> GetAllCharts()
        {
            return _companyRepository.GetAllCharts();
        }

        public Company GetByCustomerCode(int customerCode)
        {
            return _companyRepository.GetByCustomerCode(customerCode);
        }

        public Chart GetChartByClaimType(string claimType)
        {
            return _companyRepository.GetChartByClaimType(claimType);
        }

        public Chart GetChartById(int chartId)
        {
            return _companyRepository.GetChartById(chartId);
        }

        public IEnumerable<Chart> GetChartsByCompany(int companyId)
        {
            return _companyRepository.GetChartsByCompany(companyId);
        }

        public void UpdateCompanyCharts(int companyId, IEnumerable<Chart> charts)
        {
            //Método para alterar os gráficos que a empresa possui acesso
            var company = _companyRepository.GetById(companyId);
            for (int i = 0; i < company.Charts.Count(); i++)
            {
                var currentChart = company.Charts.ElementAt(i);
                //Se o grafico não estiver mais na relação de gráficos que a empresa possui acesso ele deve ser removido
                if (!charts.Any(c => c.ChartId == currentChart.ChartId))
                {
                    //Removendo acesso ao gráfico para os usuários da empresa
                    _companyRepository.RemoveClaim(companyId, currentChart.ClaimType);
                    //Removendo gráfico da empresa
                    company.Charts.Remove(currentChart);                    
                }
            }
            //Adicionando os novos graficos que a empresa possui acesso
            foreach (var chart in charts.Where(ch => !company.Charts.Any(c => c.ChartId == ch.ChartId)))
            {
                var domainChart = _companyRepository.GetChartById(chart.ChartId);
                if (domainChart != null)    company.Charts.Add(domainChart);
            }
            _companyRepository.Update(company);
        }

        public new void Remove(Company company)
        {
            _companyRepository.Remove(company);
        }
    }
}
