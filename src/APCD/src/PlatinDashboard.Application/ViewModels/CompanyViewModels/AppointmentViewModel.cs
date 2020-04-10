using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinDashboard.Application.ViewModels.CompanyViewModels
{
    public class AppointmentViewModel
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("text")]
        public string Description { get; set; }
        [DisplayName("start_date")]
        public DateTime StartDate { get; set; }
        [DisplayName("end_date")]
        public DateTime EndDate { get; set; }

    }
}
