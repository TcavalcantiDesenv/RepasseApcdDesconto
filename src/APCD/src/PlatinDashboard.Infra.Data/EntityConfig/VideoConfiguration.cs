using PlatinDashboard.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.EntityConfig
{
    public class VideoConfiguration : EntityTypeConfiguration<Video>
    {
        public VideoConfiguration()
        {
            //Table
            ToTable("Videos");

            //Key
            HasKey(v => v.VideoId);

            //Properties
            Property(v => v.Title)
                .IsRequired();

            Property(v => v.Description)
                .IsRequired();

            Property(v => v.FileName)
                .IsOptional();

            Property(v => v.IsPublic)
                .IsRequired();

            Property(v => v.CreatedAt)
                .IsRequired();


            //Relationships
            HasRequired(v => v.Company)
                .WithMany(c => c.Videos)
                .HasForeignKey(v => v.CompanyId);
        }
    }
}
