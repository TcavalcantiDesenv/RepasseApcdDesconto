using PlatinDashboard.Presentation.MVC.Models;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult Forbidden()
        {
            var error = new ErrorInfo
            {
                Message = "Você não possui permissão para acessar está página.",
                Description = "Erro 403",
            };
            return View("Error", error);
        }

        public ActionResult NotFound()
        {
            var error = new ErrorInfo
            {
                Message = "Página não encontrada.",
                Description = "Erro 404",
            };
            return View("Error", error);
        }

        public ActionResult InternalServerError()
        {
            var error = new ErrorInfo
            {
                Message = "Houve um problema ao realizar a requisição.",
                Description = "Erro 500",
            };
            return View("Error", error);
        }
    }
}