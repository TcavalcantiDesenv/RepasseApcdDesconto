using System.Collections.Generic;
using System.Linq;
using PlatinDashboard.Domain.Entities;
using PlatinDashboard.Domain.Interfaces.Repositories;

namespace PlatinDashboard.Infra.Data.Repositories
{
    public class VideoRepository : RepositoryBase<Video>, IVideoRepository
    {
        public IEnumerable<Video> GetByCompany(int companyId)
        {
            return Db.Videos.Where(v => v.CompanyId == companyId);
        }

        public IEnumerable<Video> GetPublicVideos()
        {
            return Db.Videos.ToList();// Where(v => v.IsPublic);
        }

        public new Video Add(Video video)
        {
            Db.Videos.Add(video);
            Db.SaveChanges();
            Db.Entry(video).GetDatabaseValues();
            return video;
        }
    }
}
