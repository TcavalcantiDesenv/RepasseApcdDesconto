using PlatinDashboard.Application.ViewModels.CompanyViewModels;
using System.Collections.Generic;

namespace PlatinDashboard.Application.Interfaces
{
    interface IAppointmentAppService
    {
        AppointmentViewModel Add(AppointmentViewModel appointmentViewModel);
        IEnumerable<AppointmentViewModel> GetAll();
        IEnumerable<AppointmentViewModel> GetAllClients();
        AppointmentViewModel GetById(int Id);
        AppointmentViewModel Update(AppointmentViewModel appointmentViewModel);
        void Remove(int Id);
      
    }
}
