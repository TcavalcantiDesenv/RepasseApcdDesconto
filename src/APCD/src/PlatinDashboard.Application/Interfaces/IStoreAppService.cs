using PlatinDashboard.Application.ViewModels.StoreViewModels;
using System.Collections.Generic;

namespace PlatinDashboard.Application.Interfaces
{
    public interface IStoreAppService
    {
        StoreViewModel Add(StoreViewModel storeViewModel);
        StoreViewModel GetById(int storeId);
        IEnumerable<StoreViewModel> GetAll();
        StoreViewModel GetByCompanyAndExternalId(int companyId, int externalStoreId);
        IEnumerable<StoreViewModel> GetByCompany(int companyId);
        void SetUserStores(string userId, int[] storesIds);
        void SetUserStores(string userId, int[] storesIds, string editingUserId);
        void RemoveAllUserStores(string userId);
    }
}
