using System;

namespace PlatinDashboard.Domain.Entities
{
    public class Video
    {
        public int VideoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public bool IsPublic { get; set; }
        public int CompanyId { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Company Company { get; set; }
    }
}
