using PlatinDashboard.Application.Interfaces;
using PlatinDashboard.Application.ViewModels.MidiaViewModels;
using PlatinDashboard.Presentation.MVC.MvcFilters;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class MidiasController : BaseController
    {
        private readonly IVideoAppService _videoAppService;
        public MidiasController(IUserAppService userAppService,
                                  ICompanyAppService companyAppService,
                                  IVideoAppService videoAppService)
                                  : base(userAppService, companyAppService)
        {
            _videoAppService = videoAppService;
        }

        [HttpGet]
        [ClaimsAuthorize("UserType", "Admin,Subadmin,Manager")]
        public ActionResult Index()
        {
            var videosViewModels = _videoAppService.GetByCompany(CompanyUser.CompanyId);
            ViewBag.Cliente = (Session["userId"] == null ? "" : LoggedUser.Name);
            return View(videosViewModels);
        }

        [HttpGet]
        [ClaimsAuthorize("UserType", "Admin,Subadmin,Manager")]
        public ActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("UserType", "Admin,Subadmin,Manager")]
        public ActionResult Novo(VideoViewModel videoViewModel)
        {
            if (ModelState.IsValid)
            {
                videoViewModel.CompanyId = LoggedUser.CompanyId;
                videoViewModel.IsPublic = LoggedUser.UserType == "Admin";
                var addedVideo = _videoAppService.Add(videoViewModel);
                return Json(addedVideo);
            }
            return View(videoViewModel);
        }

        [ClaimsAuthorize("UserType", "Admin,Subadmin,Manager")]
        public ActionResult NovoVideo(HttpPostedFileBase video, int? videoId)
        {
            if (video == null || videoId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var videoViewModel = new VideoViewModel(video, videoId.Value);
            _videoAppService.AddFile(videoViewModel);
            video.SaveAs(Server.MapPath(videoViewModel.FileName));
            return Json("success", string.Format("O video {0} foi cadastrado com sucesso!", video.FileName));
        }

        [HttpGet]
        [ClaimsAuthorize("UserType", "Admin,Subadmin,Manager")]
        public ActionResult Detalhes(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var videoViewModel = _videoAppService.GetById(id.Value);
            return View(videoViewModel);
        }

        [HttpPost]
        [ClaimsAuthorize("UserType", "Admin,Subadmin,Manager")]
        public ActionResult RemoverVideo(int? key)
        {
            if (key == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var videoViewModel = _videoAppService.GetById(key.Value);
            _videoAppService.RemoveFile(key.Value);
            if (System.IO.File.Exists(Server.MapPath(videoViewModel.FileName)))
            {
                System.IO.File.Delete(Server.MapPath(videoViewModel.FileName));
            }
            return Json("success", "A imagem foi removida com sucesso!");
        }

        [HttpGet]
        [ClaimsAuthorize("UserType", "Admin,Subadmin,Manager")]
        public ActionResult ModalRemover(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var video = _videoAppService.GetById(id.Value);
            if (video == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return PartialView("_ModalRemoverVideo", video);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("UserType", "Admin,Subadmin,Manager")]
        public ActionResult Editar(VideoViewModel videoViewModel)
        {
            if (ModelState.IsValid)
            {
                videoViewModel = _videoAppService.Update(videoViewModel);
                return Json(new { updated = true, view = RenderPartialView("_Editar", videoViewModel) });
            }
            return Json(new { updated = false, view = RenderPartialView("_Editar", videoViewModel) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("UserType", "Admin,Subadmin,Manager")]
        public ActionResult Remover(int? videoId)
        {
            if (videoId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var videoViewModel = _videoAppService.GetById(videoId.Value);
            if (videoViewModel == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _videoAppService.Remove(videoViewModel.VideoId);
            if (System.IO.File.Exists(Server.MapPath(videoViewModel.FileName)))
            {
                System.IO.File.Delete(Server.MapPath(videoViewModel.FileName));
            }
            return Json(new { deleted = true, videoId = videoViewModel.VideoId });
        }
    }
}