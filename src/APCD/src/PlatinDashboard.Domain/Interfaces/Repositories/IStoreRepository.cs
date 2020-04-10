using PlatinDashboard.Domain.Entities;
using System.Collections.Generic;

namespace PlatinDashboard.Domain.Interfaces.Repositories
{
    public interface IStoreRepository : IRepositoryBase<Store>
    {
        IEnumerable<Store> GetByCompany(int companyId);
        Store GetByCompanyAndExternalId(int companyId, int externalStoreId);
        void SetUserStores(string userId, int[] storesIds);
        void SetUserStores(string userId, int[] storesIds, string editingUserId);
        void RemoveAllUserStores(string userId);
    }
}
