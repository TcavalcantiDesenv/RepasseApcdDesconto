using PlatinDashboard.Domain.Entities;
using System.Collections.Generic;

namespace PlatinDashboard.Domain.Interfaces.Repositories
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        IEnumerable<Chart> GetAllCharts();
        Chart GetChartById(int chartId);
        Chart GetChartByClaimType(string claimType);
        IEnumerable<Chart> GetChartsByCompany(int companyId);
        void RemoveClaim(int companyId, string ClaimType);
        Company GetByCustomerCode(int customerCode);
        new void Remove(Company company);
    }
}
