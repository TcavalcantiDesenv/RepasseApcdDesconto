using PlatinDashboard.Domain.Entities;
using PlatinDashboard.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PlatinDashboard.Infra.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public bool CheckEmail(string email)
        {
            return !Db.Users.Any(u => u.Email == email);
        }

        public bool CheckUserName(string userName)
        {
            return !Db.Users.Any(u => u.UserName == userName);
        }

        public IEnumerable<User> GetByCompany(int companyId)
        {
            return Db.Users.Where(u => u.CompanyId == companyId);
        }

        public User GetById(string userId)
        {
            return Db.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public void RemoveClaim(UserClaim userClaim)
        {
            var claim = Db.UserClaims.FirstOrDefault(u => u.Id == userClaim.Id);
            Db.UserClaims.Remove(claim);
            Db.SaveChanges();
        }

        public new void Remove(User user)
        {
            var usersStores = Db.UsersStores.Where(us => us.UserId == user.UserId).ToList();
            foreach (var userStore in usersStores)
            {
                Db.UsersStores.Remove(Db.UsersStores.FirstOrDefault(us => us.UserStoreId == userStore.UserStoreId));
            }
            Db.Users.Remove(user);
            Db.SaveChanges();
        }
    }
}
