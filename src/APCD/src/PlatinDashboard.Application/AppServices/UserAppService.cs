using AutoMapper;
using PlatinDashboard.Application.Interfaces;
using PlatinDashboard.Application.ViewModels.ChartViewModels;
using PlatinDashboard.Application.ViewModels.CompanyViewModels;
using PlatinDashboard.Application.ViewModels.UserViewModels;
using PlatinDashboard.Domain.Entities;
using PlatinDashboard.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlatinDashboard.Application.AppServices
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserService _userService;

        public UserAppService(IUserService userService)
        {
            _userService = userService;
        }

        public Tuple<CreateUserViewModel, bool, string, string> Add(CreateUserViewModel userViewModel, string passwordHash, string companyType)
        {
            var user = Mapper.Map<CreateUserViewModel, User>(userViewModel);
            user.PasswordHash = passwordHash;
            var userTuple = _userService.Add(user, companyType);
            Mapper.Map(userTuple.Item1, userViewModel);
            return new Tuple<CreateUserViewModel, bool, string, string>(userViewModel, userTuple.Item2, userTuple.Item3, userTuple.Item4);
        }

        public void Add(CreateCompanyViewModel companyUserViewModel, string passwordHash)
        {
            var user = Mapper.Map<CreateCompanyViewModel, User>(companyUserViewModel);
            user.PasswordHash = passwordHash;
            user.UserType = "Manager";
            var userTuple = _userService.Add(user, "Cliente");
        }

        public Tuple<CreateCompanyViewModel, bool, string, string> VerifyUser(CreateCompanyViewModel companyUserViewModel)
        {
            var user = Mapper.Map<CreateCompanyViewModel, User>(companyUserViewModel);
            var userTuple = _userService.VerifyUser(user);
            return new Tuple<CreateCompanyViewModel, bool, string, string>(companyUserViewModel, userTuple.Item2, userTuple.Item3, userTuple.Item4);
        }

        public IEnumerable<UserViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(_userService.GetAll());
        }

        public IEnumerable<UserViewModel> GetByCompany(int companyId)
        {
            return Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(_userService.GetByCompany(companyId));
        }

        public UserViewModel GetById(string userId)
        {
            var user = _userService.GetById(userId);
            var userViewModel =  Mapper.Map<User, UserViewModel>(user);
            userViewModel.UserStoresIds = new int[user.UsersStores.Count()];
            var i = 0;
            foreach (var userStore in user.UsersStores)
            {
                userViewModel.UserStoresIds[i] = userStore.Store.ExternalStoreId;
                i++;
            }
            return userViewModel;
        }

        public void Remove(string userId)
        {
            var user = _userService.GetById(userId);
            _userService.Remove(user);
        }

        public EditUserViewModel Update(EditUserViewModel userViewModel)
        {
            var user = _userService.GetById(userViewModel.UserId);
            user.Descricao = userViewModel.Descricao;
            Mapper.Map(userViewModel, user);
            _userService.Update(user);
            return Mapper.Map<User, EditUserViewModel>(user);
        }

        public ProfileUserViewModel Update(ProfileUserViewModel profileUserViewModel)
        {
            var user = _userService.GetById(profileUserViewModel.UserId);
            Mapper.Map(profileUserViewModel, user);
            _userService.Update(user);
            return Mapper.Map<User, ProfileUserViewModel>(user);
        }

        public UserChartViewModel GetUserCharts(string userId)
        {
            var userViewModel = new UserChartViewModel { UserId = userId };
            var user = _userService.GetById(userId);
            //Buscando as claims dos gráficos o qual o usuario possui acesso
            var chartClaims = user.UserClaims.Where(u => u.ClaimType.StartsWith("Chart"));
            foreach (var chartClaim in chartClaims)
            {
                userViewModel.ChartViewModels.Add(new EditChartViewModel { ClaimType = chartClaim.ClaimType, HasAccess = true });
            }
            return userViewModel;
        }

        public void UpdateUserCharts(UserChartViewModel userChartViewModel)
        {
            var allowedCharts = userChartViewModel.ChartViewModels.Where(c => c.HasAccess);
            var charts = Mapper.Map<IEnumerable<EditChartViewModel>, IEnumerable<Chart>>(allowedCharts);
            _userService.UpdateUserCharts(userChartViewModel.UserId, charts);
        }
    }
}
