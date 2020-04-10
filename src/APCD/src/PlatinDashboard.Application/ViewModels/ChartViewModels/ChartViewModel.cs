using System.ComponentModel.DataAnnotations;

namespace PlatinDashboard.Application.ViewModels.ChartViewModels
{
    public class ChartViewModel
    {
        [Key]
        public int ChartId { get; set; }
        public string Name { get; set; }
        public string ClaimType { get; set; }
    }
}
