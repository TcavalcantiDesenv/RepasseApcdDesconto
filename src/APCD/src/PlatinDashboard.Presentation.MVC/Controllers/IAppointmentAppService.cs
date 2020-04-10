using PlatinDashboard.Application.ViewModels.CompanyViewModels;
using PlatinDashboard.Presentation.MVC.Models;
using System.Collections.Generic;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public interface IAppointmentAppService
    {
        AppointmentViewModel Add(AppointmentViewModel appointmentViewModel);
        IEnumerable<AppointmentViewModel> GetAll();
        IEnumerable<AppointmentViewModel> GetAllClients();
        AppointmentViewModel GetById(int Id);
        AppointmentViewModel Update(AppointmentViewModel appointmentViewModel);
        void Remove(int Id);
        void Add(Appointment changedEvent);
    }
}