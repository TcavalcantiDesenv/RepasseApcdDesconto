using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace PlatinDashboard.Application.ViewModels.MidiaViewModels
{
    public class VideoViewModel
    {
        [Key]
        public int VideoId { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        [MinLength(3, ErrorMessage = "O título deve conter no mínimo 3 caracteres")]
        [MaxLength(255, ErrorMessage = "O título deve conter no máximo 255 caracteres")]
        [DisplayName("Título")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatório")]
        [MinLength(3, ErrorMessage = "A descrição deve conter no mínimo 3 caracteres")]
        [MaxLength(255, ErrorMessage = "A descrição deve conter no máximo 255 caracteres")]
        [DisplayName("Descrição")]
        public string Description { get; set; }

        [ScaffoldColumn(false)]
        public string FileName { get; set; }

        [ScaffoldColumn(false)]
        public bool IsPublic { get; set; }

        [ScaffoldColumn(false)]
        public int CompanyId { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreatedAt { get; set; }

        public VideoViewModel()
        {

        }

        public VideoViewModel(HttpPostedFileBase video, int videoId)
        {
            VideoId = videoId;
            IsPublic = true;
            FileName = string.Format("/Midia/Videos/{0}.{1}", Guid.NewGuid().ToString(), video.FileName.Substring(video.FileName.LastIndexOf('.') + 1));
        }
    }
}
