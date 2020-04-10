using PlatinDashboard.Domain.Entities;
using PlatinDashboard.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PlatinDashboard.Infra.Data.Repositories
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public IEnumerable<Chart> GetAllCharts()
        {
            return Db.Charts.OrderBy(c => c.Name).ToList();
        }

        public Company GetByCustomerCode(int customerCode)
        {
            return Db.Companies.FirstOrDefault(c => c.CustomerCode == customerCode);
        }

        public Chart GetChartByClaimType(string claimType)
        {
            return Db.Charts.FirstOrDefault(c => c.ClaimType == claimType);
        }

        public Chart GetChartById(int chartId)
        {
            return Db.Charts.FirstOrDefault(c => c.ChartId == chartId);
        }

        public IEnumerable<Chart> GetChartsByCompany(int companyId)
        {
            return Db.Charts.Where(c => c.Companies.Any(cp => cp.CompanyId == companyId));
        }

        public void RemoveClaim(int companyId, string ClaimType)
        {
            //Método para remover a claims dos gráficos do usuário da empresa quando o acesso da empresa é alterado
            var userClaims = Db.UserClaims.Where(u => u.User.CompanyId == companyId && u.ClaimType == ClaimType);
            Db.UserClaims.RemoveRange(userClaims);
            Db.SaveChanges();
        }

        public new void Remove(Company company)
        {            
            Db.Companies.Remove(company);
            Db.SaveChanges();
        }
    }
}
