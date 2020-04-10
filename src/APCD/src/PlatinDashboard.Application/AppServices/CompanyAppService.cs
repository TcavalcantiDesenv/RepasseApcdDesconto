using AutoMapper;
using PlatinDashboard.Application.Interfaces;
using PlatinDashboard.Application.ViewModels.ChartViewModels;
using PlatinDashboard.Application.ViewModels.CompanyViewModels;
using PlatinDashboard.Domain.Entities;
using PlatinDashboard.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

namespace PlatinDashboard.Application.AppServices
{
    public class CompanyAppService : ICompanyAppService
    {
        private readonly ICompanyService _companyService;

        public CompanyAppService(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public CreateCompanyViewModel Add(CreateCompanyViewModel companyViewModel)
        {
            var company = Mapper.Map<CreateCompanyViewModel, Company>(companyViewModel);
            if (company.CompanyType == "Cliente")
            {
                var chart = _companyService.GetChartByClaimType("ChartDashboard");
                if (chart != null)
                {
                    company.Charts.Add(chart);
                }
            }
            company = _companyService.Add(company);
            companyViewModel.CompanyId = company.CompanyId;
            return companyViewModel;
        }

        public IEnumerable<CompanyViewModel> GetAll()
        {
            var companies = _companyService.GetAll();
            return Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyViewModel>>(companies);
        }
        
        public IEnumerable<CompanyViewModel> GetAllClients()
        {
            var companies = _companyService.GetAll().Where(c => c.CompanyType == "Cliente");
            return Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyViewModel>>(companies);
        }

        public CompanyViewModel GetById(int companyId)
        {
            return Mapper.Map<Company, CompanyViewModel>(_companyService.GetById(companyId));
        }

        public ChartViewModel GetChartById(int chartId)
        {
            return Mapper.Map<Chart, ChartViewModel>(_companyService.GetChartById(chartId));
        }

        public IEnumerable<ChartViewModel> GetChartsByCompany(int companyId)
        {
            return Mapper.Map<IEnumerable<Chart>, IEnumerable<ChartViewModel>>(_companyService.GetChartsByCompany(companyId));
        }

        public CompanyChartViewModel GetCompanyCharts(int companyId)
        {
            var companyViewModel = new CompanyChartViewModel { CompanyId = companyId };
            //Buscando todos os gráficos
            var charts = _companyService.GetAllCharts();
            //Buscando os gráficos o qual a empresa possui acesso
            var companyCharts = _companyService.GetChartsByCompany(companyId);
            foreach (var chart in charts)
            {
                var chartViewModel = Mapper.Map<Chart, EditChartViewModel>(chart);
                //Setando se a empresa possui acesso ao gráfico corrente
                if (companyCharts.Any(c => c.ChartId == chart.ChartId))
                    chartViewModel.HasAccess = true;
                companyViewModel.ChartViewModels.Add(chartViewModel);
            }
            return companyViewModel;
        }

        public IEnumerable<ChartViewModel> GetAllCharts()
        {
            return Mapper.Map<IEnumerable<Chart>, IEnumerable<ChartViewModel>>(_companyService.GetAllCharts());
        }

        public void Remove(int companyId)
        {
            var company = _companyService.GetById(companyId);
            var chart = _companyService.GetChartByClaimType("ChartDashboard");
            company.Charts.Remove(chart);
            _companyService.Remove(company);
        }

        public CompanyViewModel Update(CompanyViewModel companyViewModel)
        {
            var company = _companyService.GetById(companyViewModel.CompanyId);
            Mapper.Map(companyViewModel, company);
            _companyService.Update(company);
            return Mapper.Map<Company, CompanyViewModel>(company);
        }

        public void UpdateCompanyCharts(CompanyChartViewModel companyChartViewModel)
        {
            var allowedCharts = companyChartViewModel.ChartViewModels.Where(c => c.HasAccess);
            var charts = Mapper.Map<IEnumerable<EditChartViewModel>, IEnumerable<Chart>>(allowedCharts);
            _companyService.UpdateCompanyCharts(companyChartViewModel.CompanyId, charts);
        }

        public CompanyViewModel GetByCustomerCode(int customerCode)
        {
            return Mapper.Map<Company, CompanyViewModel>(_companyService.GetByCustomerCode(customerCode));
        }
    }
}
