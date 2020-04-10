using Model.Entity;
using Model.Neg;
using PlatinDashboard.Presentation.MVC.MvcFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class FamiliarController : Controller
    {
        LojasNeg objLojasNeg;
        FamiliarNeg objFamiliarNeg;
        GraduacaoNeg objGraduacaoNeg;
        MaconNeg objMaconNeg;

        public FamiliarController()
        {
            objFamiliarNeg = new FamiliarNeg();
            objLojasNeg = new LojasNeg();
            objGraduacaoNeg = new GraduacaoNeg();
            objMaconNeg = new MaconNeg();
        }

        // GET: Familiar
        public ActionResult Index()
        {
            var GUIDE = Session["GUIDE"].ToString();
            var macon = new Macon();
            macon.Guide = GUIDE.ToString();
            List<Macon> listaMacon = objMaconNeg.FindPorGuide(macon);
            var idMacon = listaMacon[0].Id_Macon;
            Session["Nome"] = listaMacon[0].Nome.ToString();
            Session["Sobrenome"] = listaMacon[0].Nome_Tratamento.ToString();
            Session["CompanyId"] = listaMacon[0].Id_Loja.ToString();
            ViewBag.Macon = Session["Nome"].ToString() + " " + Session["Sobrenome"].ToString();
            List<Familiar> lista = objFamiliarNeg.findAll().Where(x => x.Id_Macon == idMacon.ToString()).ToList();
            return View(lista);
        }

        [HttpGet]
        [ClaimsAuthorize("CompanyType", "Master")]
        public ActionResult Novo()
        {
            var GUIDE = Session["GUIDE"].ToString();
            var Nome = Session["Nome"].ToString();
            var Sobrenome = Session["Sobrenome"].ToString();
            var CompanyId = Session["CompanyId"].ToString();
            var macon = new Macon();
            macon.Guide = GUIDE.ToString();
            
            List<Macon> listaMacon = objMaconNeg.FindPorGuide(macon);
            Session["IdMacon"] = listaMacon[0].Id_Macon.ToString();

            ViewBag.ListaParentesco = new SelectList
             (
                 new Parentesco().ListaParentesco(),
                 "Id",
                 "Nome"
             );
            ViewBag.Sexo = new SelectList
             (
                 new Parentesco().ListaSexo(),
                 "Id",
                 "Nome"
             );

            List<Parentesco> parentesco = objGraduacaoNeg.Listar();
            SelectList Flistagrad = new SelectList(parentesco, "IdParentesco", "Nome");
            ViewBag.ListaParentesco = Flistagrad;

            return View();
        }
        public ActionResult Novo(Familiar ObjMacon)
        {
            if (ObjMacon.Sexo == "1") ObjMacon.Sexo = "Masculino";
            if (ObjMacon.Sexo == "2") ObjMacon.Sexo = "Feminino";

            if (ObjMacon.Grau == "1") ObjMacon.Grau = "Esposa";
            if (ObjMacon.Grau == "2") ObjMacon.Grau = "Filho";
            if (ObjMacon.Grau == "3") ObjMacon.Grau = "Filha";
            if (ObjMacon.Grau == "4") ObjMacon.Grau = "Pai";
            if (ObjMacon.Grau == "5") ObjMacon.Grau = "Mae";
            if (ObjMacon.Grau == "6") ObjMacon.Grau = "Irmão";

            var GUIDE = Session["GUIDE"].ToString();
            var Nome = Session["Nome"].ToString();
            var Sobrenome = Session["Sobrenome"].ToString();
            var CompanyId = Session["CompanyId"].ToString();
            var macon = new Macon();
            macon.Guide = GUIDE.ToString();
            ObjMacon.Id_Loja = Convert.ToInt32(CompanyId);
            ObjMacon.Id_Macon = Session["IdMacon"].ToString();

            FamiliarNeg objMaconNeg = new FamiliarNeg();
            objMaconNeg.create(ObjMacon);
            return RedirectToAction("Index", "Familiar");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var GUIDE = Session["GUIDE"].ToString();
            var macon = new Macon();
            macon.Guide = id.ToString();
            var listaMacon = objFamiliarNeg.findAll().Where(x => x.Id_Familiar == id) ;
            return View(listaMacon);
        }
        [HttpPost]
        public ActionResult Update(Familiar objMacon)
        {
            if (objMacon.Sexo == "1") objMacon.Sexo = "Masculino";
            if (objMacon.Sexo == "2") objMacon.Sexo = "Feminino";

            if (objMacon.Grau == "1") objMacon.Grau = "Esposa";
            if (objMacon.Grau == "2") objMacon.Grau = "Filho";
            if (objMacon.Grau == "3") objMacon.Grau = "Filha";
            if (objMacon.Grau == "4") objMacon.Grau = "Pai";
            if (objMacon.Grau == "5") objMacon.Grau = "Mae";
            if (objMacon.Grau == "6") objMacon.Grau = "Irmão";

            objFamiliarNeg.update(objMacon);
            return RedirectToAction("Index", "Familiar");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var GUIDE = Session["GUIDE"].ToString();
            var macon = new Macon();
            macon.Guide = id.ToString();
            ViewBag.ID = id.ToString();
            List<Familiar> lista = objFamiliarNeg.findAll().Where(x => x.Id_Familiar == id).ToList();
            ViewBag.Nome = lista[0].Nome.ToString();
            return View(lista);
        }
        //[HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmado(string id)
        {

            var familiar = new Familiar();
            familiar.Id_Familiar = Convert.ToInt32(id);
            objFamiliarNeg.delete(familiar);
            return RedirectToAction("Index", "Familiar");
        }
    }
}