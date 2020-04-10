using PlatinDashboard.Application.ViewModels.ChartViewModels;
using PlatinDashboard.Application.ViewModels.CompanyViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlatinDashboard.Application.ViewModels.UserViewModels
{
    public class UserChartViewModel
    {
        [Key]
        public string UserId { get; set; }
        public List<EditChartViewModel> ChartViewModels { get; set; }

        public UserChartViewModel()
        {
            ChartViewModels = new List<EditChartViewModel>();
        }

        public void FillOutChartsInformation(CompanyChartViewModel companyChartViewModel)
        {
            //Método para preencher os demais dados de cada grafico com base na entidade Chart
            foreach (var chartViewModel in ChartViewModels)
            {
                var companyChart = companyChartViewModel.ChartViewModels.First(c => c.ClaimType == chartViewModel.ClaimType);
                if (companyChart != null)
                {
                    chartViewModel.ChartId = companyChart.ChartId;
                    chartViewModel.Name = companyChart.Name;
                }
            }
        }
    }
}
