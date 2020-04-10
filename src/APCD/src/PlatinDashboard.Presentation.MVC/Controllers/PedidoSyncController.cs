using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class PedidoSyncController : Controller
    {
        // GET: PedidoSync
        public ActionResult Index()
        {
            ViewBag.TesteCliente = "Dorflex";
            return View();
        }
    }
}