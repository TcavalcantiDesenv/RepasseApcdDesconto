using PlatinDashboard.Application.Farmacia.ViewModels.Loja;
using PlatinDashboard.Application.ViewModels.CompanyViewModels;
using PlatinDashboard.Application.ViewModels.UserViewModels;

namespace PlatinDashboard.Application.ViewModels.StoreViewModels
{
    public class StoreViewModel
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        public int ExternalStoreId { get; set; }
        public int CompanyId { get; set; }

        public StoreViewModel()
        {

        }

        public StoreViewModel(CreateUserViewModel createUserViewModel, UadCabViewModel uadCabViewModel)
        {
            ExternalStoreId = uadCabViewModel.Uad;
            Name = uadCabViewModel.Des;
            CompanyId = createUserViewModel.CompanyId;
        }

        public StoreViewModel(EditUserViewModel editUserViewModel, CompanyViewModel companyViewModels, UadCabViewModel uadCabViewModel)
        {
            ExternalStoreId = uadCabViewModel.Uad;
            Name = uadCabViewModel.Des;
            CompanyId = companyViewModels.CompanyId;
        }
    }
}
