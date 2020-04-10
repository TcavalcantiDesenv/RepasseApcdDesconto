using Model.Entity;
using Model.Neg;
using PlatinDashboard.Presentation.MVC.MvcFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class MaconController : Controller
    {
        LojasNeg objLojasNeg;
        MaconNeg objMaconNeg;
        GraduacaoNeg objGraduacaoNeg;

        public MaconController()
        {
            objMaconNeg = new MaconNeg();
            objLojasNeg = new LojasNeg();
            objGraduacaoNeg = new GraduacaoNeg();
        }

        // GET: Macon
        public ActionResult Index()
        {
            List<Macon> lista = objMaconNeg.findAll();
            return View(lista);
        }

        public ActionResult CadastroUsuario()
        {
            var objMacon = new Macon();
            var guide = Session["GUIDE"].ToString();
            objMacon.Guide = guide;
            List<Macon> lista = objMaconNeg.FindPorGuide(objMacon);
            return View(lista);
        }
        [HttpPost]
        public ActionResult Atualiza(Macon objMacon)
        {
            List<Lojas> data = objLojasNeg.findAll();
            SelectList lista = new SelectList(data, "Id_Loja", "Nome");
            ViewBag.ListaLojas = lista;

            List<Graduacao> graduacao = objGraduacaoNeg.findAll();
            SelectList listagrad = new SelectList(graduacao, "Id_Graduacao", "Descricao");
            ViewBag.ListaGraduacao = listagrad;

            objMaconNeg.update(objMacon);
            return RedirectToAction("CadastroUsuario", "Macon");
        }

        [HttpGet]
        public ActionResult Atualiza(int id)
        {
            List<Situacao> data0 = objMaconNeg.ListaSituacao();
            SelectList lista0 = new SelectList(data0, "Id", "Nome");
            ViewBag.Situacao = lista0;



            List<Graduacao> graduacao = objGraduacaoNeg.findAll();
            SelectList listagrad = new SelectList(graduacao, "Id_Graduacao", "Descricao");
            ViewBag.ListaGraduacao = listagrad;

            List<Lojas> datax = objLojasNeg.findAll();
            SelectList lista = new SelectList(datax, "Id_Loja", "Nome");
            ViewBag.ListaLojas = lista;


            var data3 = objMaconNeg.ListaEscolaridade();
            ViewBag.Escolaridade = data3;

            var data4 = objMaconNeg.ListaReligiao();
            ViewBag.Religiao = data4;

            var data5 = objMaconNeg.ListaSexo();
            ViewBag.Sexo = data5;

            var data1 = objMaconNeg.ListaTipoSangue();
            ViewBag.TipoSangue = data1;

            Macon objMacon = new Macon(id);
            objMaconNeg.find(objMacon);
            //string N = objMacon.Situacao;
            //int num = 0;

            var tipos = objMaconNeg.ListaSituacao();
            ViewBag.Tipos = tipos;

            var civil = objMaconNeg.ListaEstado_Civil();
            ViewBag.civil = civil;

            List<Macon> macon = objMaconNeg.FindPorId(objMacon);
            objMacon.Tipo_Sanguíneo = macon[0].Tipo_Sanguíneo.ToString();

            return View(objMacon);
        }

        [HttpGet]
        [ClaimsAuthorize("UserType", "Admin,Subadmin,Manager")]
        public ActionResult Detalhe(int id)
        {
            //ViewBag.Situacao = new SelectList(new Situacao().ListaSituacao(), "Id", "Nome");
            //ViewBag.TipoSangue = new SelectList(new Tipo_Sanguíneo().ListaSangue(), "Id", "Nome");
            //ViewBag.Sexo = new SelectList(new Sexo().ListaSexo(), "Id", "Nome");
            //ViewBag.Civil = new SelectList(new Estado_Civil().ListaCivil(), "Id", "Nome");
            //ViewBag.Escolaridade = new SelectList(new Escolaridade().ListaEscolaridade(), "Id", "Nome");
            //ViewBag.Religiao = new SelectList(new Religiao().ListaReligiao(), "Id", "Nome");

            List<Situacao> data0 = objMaconNeg.ListaSituacao();
            SelectList lista0 = new SelectList(data0, "Id", "Nome");
            ViewBag.Situacao = lista0;

            List<Tipo_Sanguíneo> data1 = objMaconNeg.ListaTipoSangue();
            SelectList lista1 = new SelectList(data1, "Id", "Nome");
            ViewBag.TipoSangue = lista1;

            List<Estado_Civil> data2 = objMaconNeg.ListaEstado_Civil();
            SelectList lista2 = new SelectList(data2, "Id", "Nome");
            ViewBag.Civil = lista2;

            List<Escolaridade> data3 = objMaconNeg.ListaEscolaridade();
            SelectList lista3 = new SelectList(data3, "Id", "Nome");
            ViewBag.Escolaridade = lista3;

            List<Religiao> data4 = objMaconNeg.ListaReligiao();
            SelectList lista4 = new SelectList(data4, "Id", "Nome");
            ViewBag.Religiao = lista4;

            List<Sexo> data5 = objMaconNeg.ListaSexo();
            SelectList lista5 = new SelectList(data5, "Id", "Nome");
            ViewBag.Sexo = lista5;

            List<Lojas> data = objLojasNeg.findAll();
            SelectList lista = new SelectList(data, "Id_Loja", "Nome");
            ViewBag.ListaLojas = lista;

            List<Graduacao> graduacao = objGraduacaoNeg.findAll();
            SelectList listagrad = new SelectList(graduacao, "Id_Graduacao", "Descricao");
            ViewBag.ListaGraduacao = listagrad;

            //Macon objMacon = new Macon(id);
            //List<Macon> lstMacon = objMaconNeg.FindPorId(objMacon);
            //return View(lstMacon);
            Macon objMacon = new Macon(id);
            objMaconNeg.find(objMacon);
            
            //string N = objMacon.Situacao;
            //int num = 0;
            //if (objMacon.Situacao == "Em Loja") num = 0;
            //if (objMacon.Situacao == "Adormecido") num = 1;
            //objMacon.Situacao = ViewBag.Situacao.Items[num].Nome;
            return View(objMacon);
        }


        [HttpGet]
        [ClaimsAuthorize("CompanyType", "Master")]
        public ActionResult Novo()
        {
            MaconNeg objMaconNeg = new MaconNeg();
            //ViewBag.Situacao = new SelectList(new Situacao().ListaSituacao(), "Id", "Nome");
            //ViewBag.TipoSangue = new SelectList(new Tipo_Sanguíneo().ListaSangue(), "Id", "Nome");
            //ViewBag.Sexo = new SelectList(new Sexo().ListaSexo(), "Id", "Nome");
            //ViewBag.Civil = new SelectList(new Estado_Civil().ListaCivil(), "Id", "Nome");
            //ViewBag.Escolaridade = new SelectList(new Escolaridade().ListaEscolaridade(), "Id", "Nome");
            //ViewBag.Religiao = new SelectList(new Religiao().ListaReligiao(), "Id", "Nome");

            List<Situacao> data0 = objMaconNeg.ListaSituacao();
            SelectList lista0 = new SelectList(data0, "Id", "Nome");
            ViewBag.Situacao = lista0;

            List<Tipo_Sanguíneo> data1 = objMaconNeg.ListaTipoSangue();
            SelectList lista1 = new SelectList(data1, "Id", "Nome");
            ViewBag.TipoSangue = lista1;

            List<Estado_Civil> data2 = objMaconNeg.ListaEstado_Civil();
            SelectList lista2 = new SelectList(data2, "Id", "Nome");
            ViewBag.Civil = lista2;

            List<Escolaridade> data3 = objMaconNeg.ListaEscolaridade();
            SelectList lista3 = new SelectList(data3, "Id", "Nome");
            ViewBag.Escolaridade = lista3;

            List<Religiao> data4 = objMaconNeg.ListaReligiao();
            SelectList lista4 = new SelectList(data4, "Id", "Nome");
            ViewBag.Religiao = lista4;

            List<Sexo> data5 = objMaconNeg.ListaSexo();
            SelectList lista5 = new SelectList(data5, "Id", "Nome");
            ViewBag.Sexo = lista5;


            List<Lojas> data = objLojasNeg.findAll();
            SelectList lista = new SelectList(data, "Id_Loja", "Nome");
            ViewBag.ListaLojas = lista;

            List<Graduacao> graduacao = objGraduacaoNeg.findAll();
            SelectList listagrad = new SelectList(graduacao, "Id_Graduacao", "Descricao");
            ViewBag.ListaGraduacao = listagrad;


            List<Macon> macon = objMaconNeg.findAll();
            SelectList listam = new SelectList(macon, "Id_Macon", "Nome");
            ViewBag.ListaMacon = listam;
            return View();
        }
        public ActionResult Novo(Macon ObjMacon)
        {
            MaconNeg objMaconNeg = new MaconNeg();
            //ViewBag.Situacao = new SelectList(new Situacao().ListaSituacao(), "Id", "Nome");
            //ViewBag.TipoSangue = new SelectList(new Tipo_Sanguíneo().ListaSangue(), "Id", "Nome");
            //ViewBag.Sexo = new SelectList(new Sexo().ListaSexo(), "Id", "Nome");
            //ViewBag.Civil = new SelectList(new Estado_Civil().ListaCivil(), "Id", "Nome");
            //ViewBag.Escolaridade = new SelectList(new Escolaridade().ListaEscolaridade(), "Id", "Nome");
            //ViewBag.Religiao = new SelectList(new Religiao().ListaReligiao(), "Id", "Nome");

            List<Situacao> data0 = objMaconNeg.ListaSituacao();
            SelectList lista0 = new SelectList(data0, "Id", "Nome");
            ViewBag.Situacao = lista0;

            List<Tipo_Sanguíneo> data1 = objMaconNeg.ListaTipoSangue();
            SelectList lista1 = new SelectList(data1, "Id", "Nome");
            ViewBag.TipoSangue = lista1;

            List<Estado_Civil> data2 = objMaconNeg.ListaEstado_Civil();
            SelectList lista2 = new SelectList(data2, "Id", "Nome");
            ViewBag.Civil = lista2;

            List<Escolaridade> data3 = objMaconNeg.ListaEscolaridade();
            SelectList lista3 = new SelectList(data3, "Id", "Nome");
            ViewBag.Escolaridade = lista3;

            List<Religiao> data4 = objMaconNeg.ListaReligiao();
            SelectList lista4 = new SelectList(data4, "Id", "Nome");
            ViewBag.Religiao = lista4;

            List<Sexo> data5 = objMaconNeg.ListaSexo();
            SelectList lista5 = new SelectList(data5, "Id", "Nome");
            ViewBag.Sexo = lista5;


            List<Lojas> data = objLojasNeg.findAll();
            SelectList lista = new SelectList(data, "Id_Loja", "Nome");
            ViewBag.ListaLojas = lista;

            List<Graduacao> graduacao = objGraduacaoNeg.findAll();
            SelectList listagrad = new SelectList(graduacao, "Id_Graduacao", "Descricao");
            ViewBag.ListaGraduacao = listagrad;


            List<Macon> data6 = objMaconNeg.findAll();
            SelectList lista6 = new SelectList(data6, "Id_Macon", "Nome");
            ViewBag.ListaMacon = lista6;
            objMaconNeg.create(ObjMacon);
            return RedirectToAction("Index", "Macon");
        }


        [HttpGet]
        public ActionResult Update(int id)
        {
            List<Situacao> data0 = objMaconNeg.ListaSituacao();
            SelectList lista0 = new SelectList(data0, "Id", "Nome");
            ViewBag.Situacao = lista0;



            List<Graduacao> graduacao = objGraduacaoNeg.findAll();
            SelectList listagrad = new SelectList(graduacao, "Id_Graduacao", "Descricao");
            ViewBag.ListaGraduacao = listagrad;

            List<Lojas> datax = objLojasNeg.findAll();
            SelectList lista = new SelectList(datax, "Id_Loja", "Nome");
            ViewBag.ListaLojas = lista;


            var data3 = objMaconNeg.ListaEscolaridade();
            ViewBag.Escolaridade = data3;

            var data4 = objMaconNeg.ListaReligiao();
            ViewBag.Religiao = data4;

            var data5 = objMaconNeg.ListaSexo();
            ViewBag.Sexo = data5;

            var data1 = objMaconNeg.ListaTipoSangue();
            ViewBag.TipoSangue = data1;

            Macon objMacon = new Macon(id);
            objMaconNeg.find(objMacon);
            //string N = objMacon.Situacao;
            //int num = 0;

            var tipos = objMaconNeg.ListaSituacao();
            ViewBag.Tipos = tipos;

            var civil = objMaconNeg.ListaEstado_Civil();
            ViewBag.civil = civil;

            return View(objMacon);
        }
        [HttpPost]
        public ActionResult Update(Macon objMacon)
        {

            if (objMacon.Situacao == "1") objMacon.Situacao = "Em Loja";
            if (objMacon.Situacao == "2") objMacon.Situacao = "Adormecido";

            List<Lojas> data = objLojasNeg.findAll();
            SelectList lista = new SelectList(data, "Id_Loja", "Nome");
            ViewBag.ListaLojas = lista;



            objMaconNeg.update(objMacon);
            return RedirectToAction("Index", "Macon");
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            Macon objMacon = new Macon(id);
            objMaconNeg.find(objMacon);
            ViewBag.Irmao = objMacon.Nome + " " + objMacon.Nome_Tratamento;
            ViewBag.ID = objMacon.Id_Macon;
            return View(objMacon);
        }
        //[HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmado(string id)
        {
            List<Lojas> data = objLojasNeg.findAll();
            SelectList lista = new SelectList(data, "Id_Loja", "Nome");
            ViewBag.ListaLojas = lista;

            List<Graduacao> graduacao = objGraduacaoNeg.findAll();
            SelectList listagrad = new SelectList(graduacao, "Id_Graduacao", "Descricao");
            ViewBag.ListaGraduacao = listagrad;
            int idx = Convert.ToInt32(id);
            Macon objMacon = new Macon(idx);
            objMacon.Id_Macon = idx;
            objMaconNeg.delete(objMacon);
            return RedirectToAction("Index", "Macon");
        }

    }
}