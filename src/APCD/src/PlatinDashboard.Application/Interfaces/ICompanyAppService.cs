using PlatinDashboard.Application.ViewModels.ChartViewModels;
using PlatinDashboard.Application.ViewModels.CompanyViewModels;
using System.Collections.Generic;

namespace PlatinDashboard.Application.Interfaces
{
    public interface ICompanyAppService
    {
        CreateCompanyViewModel Add(CreateCompanyViewModel companyViewModel);
        IEnumerable<CompanyViewModel> GetAll();
        IEnumerable<CompanyViewModel> GetAllClients();
        CompanyViewModel GetById(int companyId);
        CompanyViewModel GetByCustomerCode(int customerCode);
        CompanyViewModel Update(CompanyViewModel companyViewModel);
        void Remove(int companyId);
        IEnumerable<ChartViewModel> GetAllCharts();
        ChartViewModel GetChartById(int chartId);
        IEnumerable<ChartViewModel> GetChartsByCompany(int companyId);
        CompanyChartViewModel GetCompanyCharts(int companyId);
        void UpdateCompanyCharts(CompanyChartViewModel companyChartViewModel);
    }
}
