using PlatinDashboard.Presentation.MVC.ApiServices;
using PlatinDashboard.Presentation.MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class BaixarArquivoController : Controller
    {
        private readonly ServicosApi _servicosApi;

        public BaixarArquivoController()
        {
            _servicosApi = new ServicosApi();
        }
        // GET: BaixarArquivo
        public ActionResult Index()
        {
            return View();
        }
    }
}