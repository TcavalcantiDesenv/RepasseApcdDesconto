using Model.Entity;
using Model.Neg;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class ProdutosSincronizadosController : Controller
    {
        public ActionResult Index(int? id, int? pagina, string busca)
        {
            

            return View();
        }

    }
}