using Model.Neg;
using PagedList;
using PlatinDashboard.Presentation.MVC.ApiServices;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class AbcFarmaController : Controller
    {
        private readonly ServicosApi _servicosApi;

        public AbcFarmaController()
        {
            _servicosApi = new ServicosApi();
        }
        // GET: AbcFarma
        public ActionResult Index(string busca = "", int pagina = 1, int tamanhoPagina = 10)
        {
           
           return View();
        }
           
    }
}