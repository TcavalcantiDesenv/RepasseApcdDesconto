using Model.Entity;
using Model.Neg;
using PlatinDashboard.Presentation.MVC.MvcFilters;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class ClienteController : Controller
    {
        MaconNeg objMaconNeg;

        public ClienteController()
        {
            objMaconNeg = new MaconNeg();
        }
        // GET: Macon
        public ActionResult Index()
        {
            List<Macon> lista = objMaconNeg.findAll();
            return View(lista);
        }

        [HttpGet]
        [ClaimsAuthorize("CompanyType", "Master")]
        public ActionResult Details(int id)
        {
            Macon objCliente = new Macon(id);
            objMaconNeg.find(objCliente); 
            return View(objCliente);
        }

        [HttpPost]
        [ClaimsAuthorize("CompanyType", "Master")]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Macon objMacon)
        {
            objMaconNeg.update(objMacon);
            return View();
        }

        // GET: Macon/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Macon/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Macon/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Macon/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Macon/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Macon/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
