using System.Collections.Generic;
using PlatinDashboard.Domain.Entities;
using PlatinDashboard.Domain.Interfaces.Repositories;
using PlatinDashboard.Domain.Interfaces.Services;

namespace PlatinDashboard.Domain.Services
{
    public class VideoService : ServiceBase<Video>, IVideoService
    {
        private readonly IVideoRepository _videoRepository;

        public VideoService(IVideoRepository videoRepository) : base(videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public IEnumerable<Video> GetByCompany(int companyId)
        {
            return _videoRepository.GetByCompany(companyId);
        }

        public IEnumerable<Video> GetPublicVideos()
        {
            return _videoRepository.GetPublicVideos();
        }

        new Video Add(Video video)
        {
            return _videoRepository.Add(video);
        }
    }
}
