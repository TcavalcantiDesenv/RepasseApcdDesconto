using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class PerguntaArquivoController : Controller
    {
        // GET: PerguntaArquivo
        public ActionResult Index()
        {
            return View("GeraArquivoAbcFarma");
        }
    }
}