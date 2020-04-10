using PlatinDashboard.Application.ViewModels.CompanyViewModels;
using PlatinDashboard.Application.ViewModels.UserViewModels;
using System;
using System.Collections.Generic;

namespace PlatinDashboard.Application.Interfaces
{
    public interface IUserAppService
    {
        IEnumerable<UserViewModel> GetAll();
        IEnumerable<UserViewModel> GetByCompany(int companyId);
        UserViewModel GetById(string userId);
        Tuple<CreateUserViewModel, bool, string, string> Add(CreateUserViewModel userViewModel, string passwordHash, string companyType);
        void Add(CreateCompanyViewModel companyUserViewModel, string passwordHash);
        Tuple<CreateCompanyViewModel, bool, string, string> VerifyUser(CreateCompanyViewModel companyUserViewModel);
        EditUserViewModel Update(EditUserViewModel userViewModel);
        ProfileUserViewModel Update(ProfileUserViewModel profileUserViewModel);
        void Remove(string userId);
        UserChartViewModel GetUserCharts(string userId);
        void UpdateUserCharts(UserChartViewModel userChartViewModel);
    }
}
