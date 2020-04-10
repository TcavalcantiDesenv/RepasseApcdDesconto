using PlatinDashboard.Domain.Entities;
using PlatinDashboard.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PlatinDashboard.Infra.Data.Repositories
{
    public class StoreRepository : RepositoryBase<Store>, IStoreRepository
    {
        public IEnumerable<Store> GetByCompany(int companyId)
        {
            return Db.Stores.Where(s => s.CompanyId == companyId);
        }

        public Store GetByCompanyAndExternalId(int companyId, int externalStoreId)
        {
            return Db.Stores.FirstOrDefault(s => s.CompanyId == companyId && s.ExternalStoreId == externalStoreId);
        }

        public void RemoveAllUserStores(string userId)
        {
            var usersStores = Db.UsersStores.Where(us => us.UserId == userId).ToList();
            foreach (var userStore in usersStores)
            {
                Db.UsersStores.Remove(Db.UsersStores.FirstOrDefault(us => us.UserStoreId == userStore.UserStoreId));
            }
            Db.SaveChanges();
        }

        public void SetUserStores(string userId, int[] storesIds)
        {
            //Método para renovar a lista de acesso das lojas do usuário
            var user = Db.Users.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                //Removendo todas as lojas do usuário
                RemoveAllUserStores(userId);
                //Adicionando a nova lista de lojas do usuário
                for (int i = 0; i < storesIds.Length; i++)
                {
                    var currentExternalStoreId = storesIds[i];
                    var store = Db.Stores.FirstOrDefault(s => s.CompanyId == user.CompanyId && s.ExternalStoreId == currentExternalStoreId);
                    Db.UsersStores.Add(new UserStore { UserId = user.UserId, StoreId = store.StoreId });
                }
                Db.SaveChanges();
            }
        }

        public void SetUserStores(string userId, int[] storesIds, string editingUserId)
        {
            //Método para renovar a lista de acesso das lojas do usuário de acordo o acesso do usuário que solicitou a alteração
            var user = Db.Users.FirstOrDefault(u => u.UserId == userId);
            var editingUserStores = Db.UsersStores.Where(us => us.UserId == editingUserId);
            //Removendo todas as lojas do usuário
            if (editingUserStores.Any())
            {                
                var usersStores = Db.UsersStores.Where(us => us.UserId == userId && editingUserStores.Any(eus => eus.StoreId == us.StoreId)).ToList();
                foreach (var userStore in usersStores)
                {
                    Db.UsersStores.Remove(Db.UsersStores.FirstOrDefault(us => us.UserStoreId == userStore.UserStoreId));
                }
                Db.SaveChanges();
            }
            else
            {
                RemoveAllUserStores(userId); 
            }            
            //Adicionando a nova lista de lojas do usuário
            for (int i = 0; i < storesIds.Length; i++)
            {
                var currentExternalStoreId = storesIds[i];
                if (editingUserStores.Any(us => us.Store.ExternalStoreId == currentExternalStoreId) || !editingUserStores.Any())
                {
                    var store = Db.Stores.FirstOrDefault(s => s.CompanyId == user.CompanyId && s.ExternalStoreId == currentExternalStoreId);
                    Db.UsersStores.Add(new UserStore { UserId = user.UserId, StoreId = store.StoreId });
                }                
            }
            Db.SaveChanges();
        }
    }
}
