using PlatinDashboard.Application.ViewModels.MidiaViewModels;
using System.Collections.Generic;

namespace PlatinDashboard.Application.Interfaces
{
    public interface IVideoAppService
    {
        IEnumerable<VideoViewModel> GetAll();
        IEnumerable<VideoViewModel> GetByCompany(int companyId);
        IEnumerable<VideoViewModel> GetPublicVideos();
        VideoViewModel GetById(int id);
        VideoViewModel Add(VideoViewModel videoViewModel);
        VideoViewModel Update(VideoViewModel videoViewModel);
        void AddFile(VideoViewModel videoViewModel);
        void RemoveFile(int videoId);
        void Remove(int videoId);
    }
}
