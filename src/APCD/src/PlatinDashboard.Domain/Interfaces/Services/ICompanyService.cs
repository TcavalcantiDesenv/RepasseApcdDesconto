using PlatinDashboard.Domain.Entities;
using System.Collections.Generic;

namespace PlatinDashboard.Domain.Interfaces.Services
{
    public interface ICompanyService : IServiceBase<Company>
    {
        IEnumerable<Chart> GetAllCharts();
        Chart GetChartById(int chartId);
        Chart GetChartByClaimType(string claimType);
        IEnumerable<Chart> GetChartsByCompany(int companyId);
        void UpdateCompanyCharts(int companyId, IEnumerable<Chart> charts);
        Company GetByCustomerCode(int customerCode);
        new void Remove(Company company);
    }
}
