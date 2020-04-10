using AutoMapper;
using PlatinDashboard.Application.Interfaces;
using PlatinDashboard.Application.ViewModels.MidiaViewModels;
using PlatinDashboard.Domain.Entities;
using PlatinDashboard.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace PlatinDashboard.Application.AppServices
{
    public class VideoAppService : IVideoAppService
    {
        private readonly IVideoService _videoService;

        public VideoAppService(IVideoService videoService)
        {
            _videoService = videoService;
        }

        public VideoViewModel Add(VideoViewModel videoViewModel)
        {
            var video = _videoService.Add(Mapper.Map<VideoViewModel, Video>(videoViewModel));
            return Mapper.Map<Video, VideoViewModel>(video);
        }

        public VideoViewModel Update(VideoViewModel videoViewModel)
        {
            var video = _videoService.GetById(videoViewModel.VideoId);
            video.Title = videoViewModel.Title;
            video.Description = videoViewModel.Description;
            _videoService.Update(video);
            return Mapper.Map<Video, VideoViewModel>(video);
        }

        public void AddFile(VideoViewModel videoViewModel)
        {
            var video = _videoService.GetById(videoViewModel.VideoId);
            video.FileName = videoViewModel.FileName;
            _videoService.Update(video);
        }

        public IEnumerable<VideoViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Video>, IEnumerable<VideoViewModel>>(_videoService.GetAll());
        }
        public IEnumerable<VideoViewModel> GetAllPorPdf()
        {
            return Mapper.Map<IEnumerable<Video>, IEnumerable<VideoViewModel>>(_videoService.GetAll());
        }
        public IEnumerable<VideoViewModel> GetAllPorJpg()
        {
            return Mapper.Map<IEnumerable<Video>, IEnumerable<VideoViewModel>>(_videoService.GetAll());
        }

        public IEnumerable<VideoViewModel> GetByCompany(int companyId)
        {
            return Mapper.Map<IEnumerable<Video>, IEnumerable<VideoViewModel>>(_videoService.GetByCompany(companyId));
        }

        public VideoViewModel GetById(int id)
        {
            return Mapper.Map<Video, VideoViewModel>(_videoService.GetById(id));
        }

        public void Remove(int videoId)
        {
            var video = _videoService.GetById(videoId);
            _videoService.Remove(video);
        }

        public void RemoveFile(int videoId)
        {
            var video = _videoService.GetById(videoId);
            video.FileName = null;
            _videoService.Update(video);
        }

        public IEnumerable<VideoViewModel> GetPublicVideos()
        {
            return Mapper.Map<IEnumerable<Video>, IEnumerable<VideoViewModel>>(_videoService.GetPublicVideos());
        }
    }
}
