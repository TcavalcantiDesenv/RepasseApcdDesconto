using PlatinDashboard.Domain.Entities;
using System.Collections.Generic;

namespace PlatinDashboard.Domain.Interfaces.Repositories
{
    public interface IVideoRepository : IRepositoryBase<Video>
    {
        IEnumerable<Video> GetByCompany(int companyId);
        IEnumerable<Video> GetPublicVideos();
        new Video Add(Video video);
    }
}
