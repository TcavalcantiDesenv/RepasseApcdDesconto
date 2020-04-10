using PlatinDashboard.Domain.Entities;
using System.Collections.Generic;

namespace PlatinDashboard.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetById(string userId);
        IEnumerable<User> GetByCompany(int companyId);
        bool CheckUserName(string userName);
        bool CheckEmail(string email);
        void RemoveClaim(UserClaim userClaim);
        new void Remove(User user);
    }
}
