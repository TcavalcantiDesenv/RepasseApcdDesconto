using DHTMLX.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlatinDashboard.Presentation.MVC.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [DHXJson(Alias = "text")]
        public string Description { get; set; }
        [DHXJson(Alias = "start_date")]
        public DateTime StartDate { get; set; }
        [DHXJson(Alias = "end_date")]
        public DateTime EndDate { get; set; }
    }
}