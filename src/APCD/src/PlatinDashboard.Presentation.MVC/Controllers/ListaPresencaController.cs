using Model.Entity;
using Model.Neg;
using PlatinDashboard.Presentation.MVC.MvcFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class ListaPresencaController : Controller
    {
        PresencaNeg objPresencaNeg;

        public ListaPresencaController()
        {
            objPresencaNeg = new PresencaNeg();
        }
        // GET: ListaPresenca
        public ActionResult Index()
        {
            PresencaModel macon = new PresencaModel();
            macon.ListaPresenca = objPresencaNeg.BuscarTodos().ToList<Presenca>();
            ViewBag.Presenca = macon;
            return View(macon);
        }


        //[ClaimsAuthorize("CompanyType", "Master")]
        public ActionResult ModalRemover(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var presenca = new Presenca();
            presenca.Id = id.Value;
            var resultado = objPresencaNeg.BuscarPorId(presenca);
            presenca.Nome = resultado[0].Nome;
            presenca.Data = resultado[0].Data;
            presenca.Id = resultado[0].Id;
            if (presenca == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            objPresencaNeg.delete(presenca);
            //     return PartialView("RemoverPresenca", presenca);
            return RedirectToAction("Index", "ListaPresenca");
        }

        [HttpPost]
        //[ClaimsAuthorize("CompanyType", "Master")]
        [ValidateAntiForgeryToken]
        public ActionResult Remover(int? companyId)
        {
            if (companyId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var presenca = new Presenca();
            presenca.Id = companyId.Value;
            var company = objPresencaNeg.BuscarPorId(presenca);
            if (company == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //Removendo os videos cadastrados pela empresa
            objPresencaNeg.delete(presenca);
            return Json(new { deleted = true, companyId = presenca.Id });
        }
    }
}