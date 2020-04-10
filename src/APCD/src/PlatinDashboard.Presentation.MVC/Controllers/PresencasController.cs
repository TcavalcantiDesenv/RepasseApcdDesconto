using Model.Entity;
using Model.Neg;
using PlatinDashboard.Presentation.MVC.Helpers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class PresencasController : Controller
    {
        MaconNeg objMaconNeg;
        PresencaNeg ObjPresencaNeg;

        public PresencasController()
        {
            objMaconNeg = new MaconNeg();
            ObjPresencaNeg = new PresencaNeg();
        }

        // GET: Presencas
        public ActionResult Index()
        {
            ViewBag.Days = ListGeneratorHelper.GenerateDays();
            ViewBag.Years = ListGeneratorHelper.GenerateYears();
            ViewBag.Months = ListGeneratorHelper.GenerateMonths();

            MaconModel macon = new MaconModel();
            using (DB_A1938F_CavaleirosEntities1 db = new DB_A1938F_CavaleirosEntities1())
            {
                macon.ListaMacon = objMaconNeg.findAll().ToList<Macon>(); // db. Macon.ToList<Macon>();
            }
            return View(macon);
        }

        [HttpPost]
        public ActionResult Index(MaconModel model)
        {
            ViewBag.Days = model.ListaMacon[0].Dt_Emissao_Di;
            var participou = new DateTime();
            participou = Convert.ToDateTime(ViewBag.Days);
            var maconSelecionado = model.ListaMacon.Where(x => x.Nome == "xx").ToList<Macon>();
            var listaMacon = new Presenca();
            if (model.ListaMacon != null)
            {
                maconSelecionado = model.ListaMacon.Where(x => x.IsChecked == true).ToList<Macon>(); //Where(x => x.IsChecked == true).

                foreach (var company in maconSelecionado)
                {
                    listaMacon.Nome = company.Nome + " " + company.Nome_Tratamento;
                    listaMacon.Data = participou;
                    ObjPresencaNeg.create(listaMacon);
                }

            }

            return RedirectToAction("Index", "ListaPresenca");
        }
    }
}