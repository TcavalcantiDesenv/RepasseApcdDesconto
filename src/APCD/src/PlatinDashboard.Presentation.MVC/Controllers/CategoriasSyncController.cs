using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class CategoriasSyncController : Controller
    {
        // GET: CategoriasSync
        public ActionResult Index()
        {
            ViewBag.Titulo = "Nome do Produto";
            ViewBag.TesteCliente = "Dorflex";
            return View();
        }
    }
}