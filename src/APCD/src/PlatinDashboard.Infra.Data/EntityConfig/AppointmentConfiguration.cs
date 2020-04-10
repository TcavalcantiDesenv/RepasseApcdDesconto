using PlatinDashboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinDashboard.Infra.Data.EntityConfig
{
    public class AppointmentConfiguration : EntityTypeConfiguration<Appointment>
    {
        public AppointmentConfiguration()
        {
            //Table
            ToTable("Appointment");

            //Key
            HasKey(c => c.Id);

            //Properties
            Property(c => c.Description)
                    .IsRequired();

            Property(c => c.StartDate)
                    .IsRequired();
            Property(c => c.EndDate)
                    .IsRequired();
        }
    }
}
