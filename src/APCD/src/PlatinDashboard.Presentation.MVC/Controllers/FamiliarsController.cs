using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.Entity;
using Model.Neg;
using PlatinDashboard.Presentation.MVC.Models;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class FamiliarsController : Controller
    {
        private FamiliarContext db = new FamiliarContext();

        // GET: Familiars
        public ActionResult Index()
        {
            return View(db.Familiars.ToList());
        }

        // GET: Familiars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familiar familiar = db.Familiars.Find(id);
            if (familiar == null)
            {
                return HttpNotFound();
            }
            return View(familiar);
        }

        // GET: Familiars/Create
        public ActionResult Create()
        {
            var Nome = "";
            var Sobrenome = "";
            try
            {
                Nome = Session["Nome"].ToString();
                Sobrenome = Session["Sobrenome"].ToString();
                ViewBag.Loja = Session["Loja"].ToString();
            }
            catch (Exception ex)
            {
                Nome = "Usuario";
                Sobrenome = " Não Definito";
                ViewBag.Loja = "Loja não identificada";
            }
           

            MaconNeg maconNeg = new MaconNeg();

            List<SelectListItem> Item = new List<SelectListItem>();
            Item.Add(new SelectListItem { Text = "Esposa", Value = "Esposa", Selected = true });
            Item.Add(new SelectListItem { Text = "Filho(a)", Value = "Filho(a)", Selected = false });
            Item.Add(new SelectListItem { Text = "Irmão(ã)", Value = "Irmão(ã)", Selected = false });
            Item.Add(new SelectListItem { Text = "Pai", Value = "Pai", Selected = false });
            Item.Add(new SelectListItem { Text = "Mãe", Value = "Mãe", Selected = false });
            ViewBag.Parente = Item;

            List<SelectListItem> Item2 = new List<SelectListItem>();
            Item2.Add(new SelectListItem { Text = "Maculino", Value = "Maculino", Selected = true });
            Item2.Add(new SelectListItem { Text = "Feminino", Value = "Feminino", Selected = false });
            ViewBag.Sexo = Item2;
            ViewBag.Nome = Nome + " " + Sobrenome;
            ViewBag.usuario = Session["usuarioId"].ToString();
            ViewBag.loja = Session["CompanyId"].ToString();

            return View();
        }

        // POST: Familiars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Familiar,Id_Macon,Id_Loja,Cim,Nome,Email,Sexo,Grau,Aniversario")] Familiar familiar)
        {
            List<SelectListItem> Item = new List<SelectListItem>();
            Item.Add(new SelectListItem { Text = "Esposa", Value = "Esposa", Selected = true });
            Item.Add(new SelectListItem { Text = "Filho(a)", Value = "Filho(a)", Selected = false });
            Item.Add(new SelectListItem { Text = "Irmão(ã)", Value = "Irmão(ã)", Selected = false });
            Item.Add(new SelectListItem { Text = "Pai", Value = "Pai", Selected = false });
            Item.Add(new SelectListItem { Text = "Mãe", Value = "Mãe", Selected = false });
            ViewBag.Parente = Item;

            List<SelectListItem> Item2 = new List<SelectListItem>();
            Item2.Add(new SelectListItem { Text = "Maculino", Value = "Maculino", Selected = true });
            Item2.Add(new SelectListItem { Text = "Feminino", Value = "Feminino", Selected = false });
            ViewBag.Sexo = Item2;

            int loja = Convert.ToInt32(Session["CompanyId"].ToString());

            if (ModelState.IsValid)
            {
                db.Familiars.Add(familiar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(familiar);
        }

        // GET: Familiars/Edit/5
        public ActionResult Edit(int? id)
        {
            var Nome = "";
            var Sobrenome = "";
            try
            {
                Nome = Session["Nome"].ToString();
                Sobrenome = Session["Sobrenome"].ToString();
            }
            catch (Exception ex)
            {
                Nome = "Usuario";
                Sobrenome = " Não Definito";
            }
            ViewBag.Loja = Session["Loja"].ToString();

            MaconNeg maconNeg = new MaconNeg();

            List<SelectListItem> Item = new List<SelectListItem>();
            Item.Add(new SelectListItem { Text = "Esposa", Value = "Esposa", Selected = true });
            Item.Add(new SelectListItem { Text = "Filho(a)", Value = "Filho(a)", Selected = false });
            Item.Add(new SelectListItem { Text = "Irmão(ã)", Value = "Irmão(ã)", Selected = false });
            Item.Add(new SelectListItem { Text = "Pai", Value = "Pai", Selected = false });
            Item.Add(new SelectListItem { Text = "Mãe", Value = "Mãe", Selected = false });
            ViewBag.Parente = Item;

            List<SelectListItem> Item2 = new List<SelectListItem>();
            Item2.Add(new SelectListItem { Text = "Maculino", Value = "Maculino", Selected = true });
            Item2.Add(new SelectListItem { Text = "Feminino", Value = "Feminino", Selected = false });
            ViewBag.Sexo = Item2;
            ViewBag.Nome = Nome + " " + Sobrenome;


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familiar familiar = db.Familiars.Find(id);
            if (familiar == null)
            {
                return HttpNotFound();
            }
            return View(familiar);
        }

        // POST: Familiars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Familiar,Id_Macon,Id_Loja,Cim,Nome,Email,Sexo,Grau,Aniversario")] Familiar familiar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(familiar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(familiar);
        }

        // GET: Familiars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familiar familiar = db.Familiars.Find(id);
            if (familiar == null)
            {
                return HttpNotFound();
            }
            return View(familiar);
        }

        // POST: Familiars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Familiar familiar = db.Familiars.Find(id);
            db.Familiars.Remove(familiar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
