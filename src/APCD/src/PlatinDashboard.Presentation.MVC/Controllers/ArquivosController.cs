using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Model.Entity;
using Model.Neg;
using PlatinDashboard.Presentation.MVC.ApiServices;
using PlatinDashboard.Presentation.MVC.Models;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class ArquivosController : Controller
    {
        // GET: Arquivos
        public ActionResult Index()
        {
           
            return View();

        }
    }
}