using Model.Entity;
using Model.Neg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;



namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class TrabalhosController : Controller
    {
        LojasNeg objLojasNeg;
        MaconNeg objMaconNeg;
        GraduacaoNeg objGraduacaoNeg;

        public TrabalhosController()
        {
            objMaconNeg = new MaconNeg();
            objLojasNeg = new LojasNeg();
            objGraduacaoNeg = new GraduacaoNeg();
        }
        // GET: Trabalhos
        public ActionResult Index(string id)
        {
            string grau = "";
            if (id == "" || id == null) id = "0";
            Macon objMacon = new Macon();
           var identity = (ClaimsIdentity)HttpContext.User.Identity;
            var MaconGuide = objMaconNeg.PesquisarGuide(identity.Name);
            objMacon.Grau = MaconGuide[0].Grau;
            objMacon.Guide = MaconGuide[0].Guide;
            var MaconDados = objMaconNeg.FindPorGuide(objMacon);
            objMacon.Id_Macon = MaconDados[0].Id_Macon;
       
            objMaconNeg.find(objMacon);
            if (objMacon.Id_Graduacao == 1) grau = "Aprendiz";
            if (objMacon.Id_Graduacao == 2) grau = "Companheiro";
            if (objMacon.Id_Graduacao == 3) grau = "Mestre";

            if(MaconGuide[0].Grau == "Manager" || MaconGuide[0].Grau == "Admin")
            {
                ViewBag.ListaGraduacao = "http://hiltonslima-001-site9.btempurl.com/Trabalhos";
            }
            else
            {
                ViewBag.ListaGraduacao = "http://hiltonslima-001-site9.btempurl.com/Trabalhos?usuario=" + grau + "/" + objMacon.Nome;
            }
            
            return View(objMacon);
        }
    }
}