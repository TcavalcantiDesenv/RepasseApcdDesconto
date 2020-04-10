using Model.Entity;
using Model.Neg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.Ajax.Utilities;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    [Authorize]
    public class RegionalController : Controller
    {
        RemessaNeg objRemessaNeg;
        RegionaisNeg objRegionaisNeg;
        EnderecosNeg objEnderecosNeg;
        Enderecos enderecos = new Enderecos();
      //  RemessaModel remessa = new RemessaModel();
        RegionaisModel regionais = new RegionaisModel();
        public RegionalController()
        {
            objRemessaNeg = new RemessaNeg();
            objRegionaisNeg = new RegionaisNeg();
            objEnderecosNeg = new EnderecosNeg();
        }

        // GET: Regional
        public ActionResult Index()
        {
            //people.DistinctBy(p => p.Id);
            try
            {
                var codregional = Session["CODREG"].ToString();
                var nomregional = Session["NOMREG"].ToString();
                RegionaisModel regionais = new RegionaisModel();
                if (codregional == "000" || codregional == "TI")
                {
                    regionais.ListaRegionais = objRegionaisNeg.findAll().ToList<Regionais>();
                }
                else
                {
                    regionais.ListaRegionais = objRegionaisNeg.BuscaPorNome(nomregional).ToList<Regionais>();
                }
                List<SelectListItem> MesAno = new List<SelectListItem>()
                {
           new SelectListItem{ Text="Janeiro", Value = "012020" },
           new SelectListItem{ Text="Fevereiro", Value = "022020" },
           new SelectListItem{ Text="Março", Value = "032020" },
           new SelectListItem{ Text="Abril", Value = "042020" },
           new SelectListItem{ Text="Maio", Value = "052020" },
           new SelectListItem{ Text="Junho", Value = "062020" },
           new SelectListItem{ Text="Julho", Value = "072020" },
           new SelectListItem{ Text="Agosto", Value = "082020" },
           new SelectListItem{ Text="Setembro", Value = "092020" },
           new SelectListItem{ Text="Outubro", Value = "102020" },
           new SelectListItem{ Text="Novembro", Value = "112020" },
           new SelectListItem{ Text="Dezembro", Value = "122020" },
                };
                ViewBag.MesAno = MesAno;
                //  GeraDadosBradesco(TipoArquivo.CNAB400);
                return View(regionais);
            }
            catch (Exception)
            {
                return RedirectToAction("Voltar", "Entrar");
            }
        }



    }
}