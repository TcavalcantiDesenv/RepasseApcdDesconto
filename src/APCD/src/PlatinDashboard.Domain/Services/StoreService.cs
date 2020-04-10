using System.Collections.Generic;
using PlatinDashboard.Domain.Entities;
using PlatinDashboard.Domain.Interfaces.Repositories;
using PlatinDashboard.Domain.Interfaces.Services;

namespace PlatinDashboard.Domain.Services
{
    public class StoreService : ServiceBase<Store>, IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository) : base(storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public IEnumerable<Store> GetByCompany(int companyId)
        {
            return _storeRepository.GetByCompany(companyId);
        }

        public Store GetByCompanyAndExternalId(int companyId, int externalStoreId)
        {
            return _storeRepository.GetByCompanyAndExternalId(companyId, externalStoreId);
        }

        public void RemoveAllUserStores(string userId)
        {
            _storeRepository.RemoveAllUserStores(userId);
        }

        public void SetUserStores(string userId, int[] storesIds)
        {
            _storeRepository.SetUserStores(userId, storesIds);
        }

        public void SetUserStores(string userId, int[] storesIds, string editingUserId)
        {
            _storeRepository.SetUserStores(userId, storesIds, editingUserId);
        }
    }
}
