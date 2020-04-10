using Model.Entity;
using Model.Neg;
using PlatinDashboard.Presentation.MVC.ApiServices;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Net;
//using System.Web;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class GeraArquivoAbcFarmaController : Controller
    {
        private readonly ServicosApi _servicosApi;

        public GeraArquivoAbcFarmaController()
        {
            _servicosApi = new ServicosApi();
        }
        // GET: GeraArquivoAbcFarma
        public ActionResult Index()
        {
               return View();
        }
    }
}