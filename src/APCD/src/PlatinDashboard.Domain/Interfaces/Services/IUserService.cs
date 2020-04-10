using PlatinDashboard.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PlatinDashboard.Domain.Interfaces.Services
{
    public interface IUserService : IServiceBase<User>
    {
        User GetById(string userId);
        IEnumerable<User> GetByCompany(int companyId);
        Tuple<User, bool, string, string> Add(User user, string companyType);
        Tuple<User, bool, string, string> VerifyUser(User user);
        void UpdateUserCharts(string userId, IEnumerable<Chart> charts);
        new void Remove(User user);
    }
}
