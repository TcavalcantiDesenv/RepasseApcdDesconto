using PlatinDashboard.Domain.Entities;
using System.Collections.Generic;

namespace PlatinDashboard.Domain.Interfaces.Services
{
    public interface IVideoService : IServiceBase<Video>
    {
        IEnumerable<Video> GetByCompany(int companyId);
        IEnumerable<Video> GetPublicVideos();
        new Video Add(Video video);
    }
}
