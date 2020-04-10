using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class ProdutosSyncController : Controller
    {
        // GET: ProdutosSync
        public ActionResult Index()
        {
            ViewBag.TesteCliente = "Teste";
            return View();
        }
    }
}