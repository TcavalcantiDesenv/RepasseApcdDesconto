using AutoMapper;
using PlatinDashboard.Application.Interfaces;
using PlatinDashboard.Application.ViewModels.StoreViewModels;
using PlatinDashboard.Domain.Entities;
using PlatinDashboard.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace PlatinDashboard.Application.AppServices
{
    public class StoreAppService : IStoreAppService
    {
        private readonly IStoreService _storeService;

        public StoreAppService(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public StoreViewModel Add(StoreViewModel storeViewModel)
        {
            var store = _storeService.Add(Mapper.Map<StoreViewModel, Store>(storeViewModel));
            return Mapper.Map<Store, StoreViewModel>(store);
        }

        public IEnumerable<StoreViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Store>, IEnumerable<StoreViewModel>>(_storeService.GetAll());
        }

        public IEnumerable<StoreViewModel> GetByCompany(int companyId)
        {
            return Mapper.Map<IEnumerable<Store>, IEnumerable<StoreViewModel>>(_storeService.GetByCompany(companyId));
        }

        public StoreViewModel GetByCompanyAndExternalId(int companyId, int externalStoreId)
        {
            return Mapper.Map<Store, StoreViewModel>(_storeService.GetByCompanyAndExternalId(companyId, externalStoreId));
        }

        public StoreViewModel GetById(int storeId)
        {
            return Mapper.Map<Store, StoreViewModel>(_storeService.GetById(storeId));
        }

        public void RemoveAllUserStores(string userId)
        {
            _storeService.RemoveAllUserStores(userId);
        }

        public void SetUserStores(string userId, int[] storesIds)
        {
            _storeService.SetUserStores(userId, storesIds);
        }

        public void SetUserStores(string userId, int[] storesIds, string editingUserId)
        {
            _storeService.SetUserStores(userId, storesIds, editingUserId);
        }
    }
}
