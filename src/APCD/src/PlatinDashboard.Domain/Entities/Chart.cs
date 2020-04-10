using System.Collections.Generic;

namespace PlatinDashboard.Domain.Entities
{
    public class Chart
    {
        public int ChartId { get; set; }
        public string Name { get; set; }
        public string ClaimType { get; set; }
        public virtual  ICollection<Company> Companies { get; set; }
    }
}
