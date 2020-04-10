using PlatinDashboard.Application.ViewModels.ChartViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlatinDashboard.Application.ViewModels.CompanyViewModels
{
    public class CompanyChartViewModel
    {
        [Key]
        public int CompanyId { get; set; }

        public List<EditChartViewModel> ChartViewModels { get; set; }

        public CompanyChartViewModel()
        {
            ChartViewModels = new List<EditChartViewModel>();
        }
    }
}
