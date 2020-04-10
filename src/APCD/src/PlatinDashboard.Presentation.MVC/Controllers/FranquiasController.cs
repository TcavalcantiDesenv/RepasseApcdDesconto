using PlatinDashboard.Presentation.MVC.MvcFilters;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class FranquiasController : Controller
    {
        [ClaimsAuthorize("ChartCuboFranquias", "Allowed")]
        public ActionResult Index()
        {
            return View();
        }
    }
}