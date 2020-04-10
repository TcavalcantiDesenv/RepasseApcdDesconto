using Model.Entity;
using Model.Neg;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class ExecucoesController : Controller
    {
        public ActionResult Index(int? pagina)
        {
            return View();
        }

    }
}