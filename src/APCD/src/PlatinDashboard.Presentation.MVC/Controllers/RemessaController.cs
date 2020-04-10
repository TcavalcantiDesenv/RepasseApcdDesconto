using BoletoNet;
using Model.Entity;
using Model.Neg;
using PlatinDashboard.Presentation.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using static Model.Entity.Enderecos;
using System.Web;
using System.Web.UI;
using Rotativa;
using PagedList;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using OfficeOpenXml;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    [Authorize]
    public class RemessaController : Controller
    {
        RemessaNeg objRemessaNeg;
        RemessaModel remessa = new RemessaModel();
        EnderecosNeg objEnderecosNeg;
        EnderecosModel enderecos = new EnderecosModel();

        Exporting expt = new Exporting();

        public RemessaController()
        {
            objRemessaNeg = new RemessaNeg();
            objEnderecosNeg = new EnderecosNeg();
        }

        public void DownloadExcel()
        {
            bool valorAtivo = Convert.ToBoolean(Session["Ativo"]);
            string novoValor = Session["NovoValor"].ToString();
            string dataInicial = Session["DataInicial"].ToString();
            string dataFinal = Session["DataFinal"].ToString();
            DateTime DataIni = Convert.ToDateTime(dataInicial);
            DateTime DataFim = Convert.ToDateTime(dataFinal);
            DateTime DataAtual = DateTime.Now;
            string MesAtual = DataAtual.ToString("MM/yyyy");
            string MesInicio = DataIni.ToString("MM/yyyy");
            string MesFim = DataFim.ToString("MM/yyyy");


            var regionalcod = Session["CODREG"].ToString();
            string mesano = DateTime.Now.ToString("MMyyyy");
            //     var collection = db.GetCollection<EmployeeDetails>("EmployeeDetails").Find(new BsonDocument()).ToList();
            List<RemessaWeb> collection = objRemessaNeg.BuscaPorRegional(regionalcod, true, "false", mesano, DataIni, DataFim, valorAtivo, novoValor).OrderBy(x => x.NomeRegional).ToList<RemessaWeb>();

            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1"].Value = "Regional";
            Sheet.Cells["B1"].Value = "LCM";
            Sheet.Cells["C1"].Value = "Nome";
            Sheet.Cells["D1"].Value = "Valor";
            Sheet.Cells["E1"].Value = "Data Pagto.";
            int row = 2;
            foreach (var item in collection)
            {

                Sheet.Cells[string.Format("A{0}", row)].Value = item.NomeRegional;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.LCM;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.Nome;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.Valor;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.DataPagto;
                row++;
            }

            var ObjAcessosNeg = new AcessosNeg();
            var acessos = new Acessos();

            acessos.DataEntrada = DateTime.Now;
            acessos.Nome = Session["NOMREG"].ToString();// company.NomeRegional;
            acessos.Empresa = "Baixa de Relatorio em Excel da Regional " + ViewBag.NomeRegional;
            acessos.IP = Session["IP"].ToString();
            acessos.Usuario = Session["Username"].ToString();
            ObjAcessosNeg.create(acessos);


            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

        public ActionResult PrintViewToPdf()
        {
            var report = new ActionAsPdf("Index");
            return report;
        }

        public ActionResult PrintPartialViewToPdf(string id)
        {
            bool valorAtivo = Convert.ToBoolean(Session["Ativo"]);
            string novoValor = Session["NovoValor"].ToString();
            string dataInicial = Session["DataInicial"].ToString();
            string dataFinal = Session["DataFinal"].ToString();
            DateTime DataIni = Convert.ToDateTime(dataInicial);
            DateTime DataFim = Convert.ToDateTime(dataFinal);
            DateTime DataAtual = DateTime.Now;
            string MesAtual = DataAtual.ToString("MM/yyyy");
            string MesInicio = DataIni.ToString("MM/yyyy");
            string MesFim = DataFim.ToString("MM/yyyy");

            ViewBag.ID = id;
            var regionalcod = Session["ID"].ToString();
            string mesano = DateTime.Now.ToString("MMyyyy");
            try
            {
                List<RemessaWeb> remessa2 = objRemessaNeg.BuscaPorRegional(regionalcod, true, "false", mesano, DataIni, DataFim, valorAtivo, novoValor).OrderBy(x => x.NomeRegional).ToList<RemessaWeb>();
                //   return View(remessa2);
                var report = new PartialViewAsPdf("~/Views/Remessa/pdf.cshtml", remessa2);
                return report;
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Acesso", FormMethod.Post);
            }



        }

        public ActionResult PDF()
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=TestPage.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            //     pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            return RedirectToAction("Index", "Remessa", new { id = Session["CODREGIONAL"] });
        }

        public ActionResult Index(string id, string busca, int pagina = 1, int tamanhoPagina = 10)
        {
            ViewBag.ID = id;
            Session["ID"] = id;
            var ObjAcessosNeg = new AcessosNeg();
            var acessos = new Acessos();

            bool valorAtivo = Convert.ToBoolean(Session["Ativo"]);
            ViewBag.Ativo = Session["Ativo"];
            ViewBag.DataIni = Session["DataInicial"].ToString();
            ViewBag.DataFim = Session["DataFinal"].ToString(); 
            ViewBag.Valor = Session["NovoValor"].ToString();

           decimal  precoAcordo = Convert.ToDecimal(Session["NovoValor"].ToString());
            ViewBag.ValorAcordo = String.Format("{0:C}", precoAcordo); 

            string novoValor = Session["NovoValor"].ToString();
            string dataInicial = Session["DataInicial"].ToString();
            string dataFinal = Session["DataFinal"].ToString();
            DateTime DataIni = Convert.ToDateTime(dataInicial);
            DateTime DataFim = Convert.ToDateTime(dataFinal);
            DateTime DataAtual = DateTime.Now;
            string MesAtual = DataAtual.ToString("MM/yyyy");
            string MesInicio = DataIni.ToString("MM/yyyy");
            string MesFim = DataFim.ToString("MM/yyyy");

            Session["DATAPAG"] = DateTime.Now.ToString("dd/MM/yyyy");
            try
            {
                ViewBag.Message = "Boleto.Net.MVC";
                var bancos = from Bancos s in Enum.GetValues(typeof(Bancos))
                             select new
                             {
                                 ID = Convert.ChangeType(s, typeof(int)),
                                 Name = s.ToString()
                             };
                ViewData["bancos"] = new SelectList(bancos, "ID", "Name", Bancos.Bradesco);
                string mesano = DateTime.Now.ToString("MMyyyy");
                var codregional = Session["CODREG"].ToString();
                var nomregional = Session["NOMREG"].ToString();
                ViewBag.CodRegional = codregional;
                ViewBag.NomeRegional = nomregional;// "NENUMA INFORMAÇÃO ENCONTRADA";
                RemessaModel remessa = new RemessaModel();
                if (codregional == "000")
                {
                    ViewBag.DataVencimento = DateTime.Now.AddDays(10);
                    ViewBag.ValorTotal = "R$ 0,00";
                    decimal preco, total = 0;
                    var valorTotal = objRemessaNeg.BuscaPorRegional(id, true, "true", mesano, DataIni, DataFim, valorAtivo, novoValor).OrderBy(x => x.NomeRegional).ToList<RemessaWeb>();
                    foreach (var company in valorTotal)
                    {
                        if (company.Ativo == "true")
                        {
                            DateTime venceu = Convert.ToDateTime(company.Vencimento);
                            DiferencaDatas diferencaData = new DiferencaDatas(DateTime.Now, venceu);
                            int meses = Convert.ToInt32(diferencaData.ToString());

                            //                    if (meses <= 4)
                            //                    {
                            Session["MESANO"] = company.MesAno;
                            preco = Convert.ToDecimal(company.Valor);
                            total = total + preco;
                            ViewBag.Total = total;
                            string v_total = String.Format("{0:C}", total);
                            ViewBag.ValorTotal = v_total;
                            ViewBag.ValorBoleto = v_total.Replace("R$ ", "").Replace(",", "").PadLeft(13, '0');

                            //                    }
                        }
                    }
                    Session["ValorTotal"] = ViewBag.Total;
                    remessa.ListaRemessa = objRemessaNeg.BuscaPorRegional(id, false, "true", mesano, DataIni, DataFim, valorAtivo, novoValor).OrderBy(x => x.NomeRegional).ToList<RemessaWeb>();
                    if (remessa.ListaRemessa.Count > 0)
                    {
                        Session["MESANO"] = remessa.ListaRemessa[0].MesAno;
                        ViewBag.NomeRegional = remessa.ListaRemessa[0].NomeRegional;
                        ViewBag.CodRegional = remessa.ListaRemessa[0].CodRegional;
                        Session["CODREGIONAL"] = remessa.ListaRemessa[0].CodRegional;
                        Session["REGIONAL"] = remessa.ListaRemessa[0].NomeRegional;
                        string enderecos = Session["CODREGIONAL"].ToString();
                        ViewBag.Enderecos = objEnderecosNeg.BuscaPorCodigo(enderecos);

                    }

                }
                else
                {
                    ViewBag.DataVencimento = DateTime.Now.AddDays(10);
                    ViewBag.ValorTotal = "R$ 0,00";
                    decimal preco, total = 0;
                    var valorTotal = objRemessaNeg.BuscaPorRegional(codregional, true, "true", mesano, DataIni, DataFim, valorAtivo, novoValor).OrderBy(x => x.NomeRegional).ToList<RemessaWeb>();
                    foreach (var company in valorTotal)
                    {
                        if (company.Ativo == "true")
                        {
                            DateTime venceu = Convert.ToDateTime(company.Vencimento);
                            DiferencaDatas diferencaData = new DiferencaDatas(DateTime.Now, venceu);
                            int meses = Convert.ToInt32(diferencaData.ToString());

                            //                  if (meses <= 4)
                            //                  {
                            Session["MESANO"] = company.MesAno;
                            preco = Convert.ToDecimal(company.Valor);
                            total = total + preco;
                            string v_total = String.Format("{0:C}", total);
                            ViewBag.ValorTotal = v_total;
                            ViewBag.ValorBoleto = v_total.Replace("R$ ", "").Replace(",", "").PadLeft(13, '0');
                            //                }
                        }
                    }

                    if (ViewBag.Total == null)
                    {
                        ViewBag.Total = "0";
                    }
                    Session["ValorTotal"] = ViewBag.Total;
                    remessa.ListaRemessa = objRemessaNeg.BuscaPorRegional(codregional, false, "true", mesano, DataIni, DataFim, valorAtivo, novoValor).OrderBy(x => x.NomeRegional).ToList<RemessaWeb>();

                    if (remessa.ListaRemessa.Count > 0)
                    {
                        Session["MESANO"] = remessa.ListaRemessa[0].MesAno;
                        ViewBag.NomeRegional = remessa.ListaRemessa[0].NomeRegional;
                        ViewBag.CodRegional = remessa.ListaRemessa[0].CodRegional;
                        Session["CODREGIONAL"] = remessa.ListaRemessa[0].CodRegional;
                        Session["REGIONAL"] = remessa.ListaRemessa[0].NomeRegional;
                        string enderecos = Session["CODREGIONAL"].ToString();
                        ViewBag.Enderecos = objEnderecosNeg.BuscaPorCodigo(enderecos);
                    }


                }
                if (remessa.ListaRemessa.Count > 0)
                {
                    GeraDadosBradesco(TipoArquivo.CNAB400);
                }
                ViewBag.Busca = busca;
                ViewBag.TamanhoPagina = tamanhoPagina;
                ViewBag.Pagina = pagina;

                if (id == null)
                {
                    id = Session["CODREGIONAL"].ToString();
                }
                decimal preco2, total2 = 0;
                var valorTotal2 = objRemessaNeg.BuscaPorRegional(id, true, "false", mesano, DataIni, DataFim, valorAtivo, novoValor).OrderBy(x => x.NomeRegional).ToList<RemessaWeb>();
                foreach (var company in valorTotal2)
                {
                    if (company.Ativo == "false")
                    {
                        Session["MESANO"] = company.MesAno;
                        preco2 = Convert.ToDecimal(company.Valor);
                        total2 = total2 + preco2;
                        ViewBag.Total = total2;
                        string v_total2 = String.Format("{0:C}", total2);
                        ViewBag.ValorTotal2 = v_total2;
                        ViewBag.ValorBoleto2 = v_total2.Replace("R$ ", "").Replace(",", "").PadLeft(13, '0');
                    }
                }

                if (ViewBag.ValorTotal2 == null)
                {
                    ViewBag.ValorTotal2 = "0";
                }
                Session["VALORTOTAL"] = ViewBag.ValorTotal2;
                //    return View(produtos);

                acessos.DataEntrada = DateTime.Now;
                acessos.Nome = nomregional;
                acessos.Empresa = "Listando os Associados";
                acessos.IP = Session["IP"].ToString();
                acessos.Usuario = Session["Username"].ToString();
                ObjAcessosNeg.create(acessos);

                string codregional1 = ViewBag.CodRegional;

                List<RemessaWeb> ListaRegionais = new List<RemessaWeb>();
                var remessa2 = objRemessaNeg.BuscaPorCodRegional(codregional1, mesano, DataIni, DataFim, valorAtivo, novoValor).Where(x => x.Matricula == "WSAWAD").ToList<RemessaWeb>().ToPagedList(pagina, tamanhoPagina); // = objRemessaNeg.BuscaPorCodRegional(codregional1, mesano).OrderBy(x => x.Nome).ToList<RemessaWeb>().ToPagedList(pagina, tamanhoPagina); 
                if (busca == "" || busca == null)
                {
                    remessa2 = objRemessaNeg.BuscaPorCodRegional(codregional1, mesano, DataIni, DataFim, valorAtivo, novoValor).OrderBy(x => x.Nome).ToList<RemessaWeb>().ToPagedList(pagina, tamanhoPagina);
                    //foreach (var company in remessa2)
                    //{
                    //    RemessaWeb Model = new RemessaWeb();
                    //    Model.Ativo = company.Ativo;
                    //    Model.Categoria = company.Categoria;
                    //    Model.Codigo = company.Codigo;
                    //    Model.CodRegional = company.CodRegional;
                    //    Model.Conta = company.Conta;
                    //    Model.DataDia = company.DataDia;
                    //    Model.DataPagto = company.DataPagto;
                    //    Model.Documento = company.Documento;
                    //    Model.Empresa = company.Empresa;
                    //    Model.indice = company.indice;
                    //    Model.LCM = company.LCM;
                    //    Model.Matricula = company.Matricula;
                    //    Model.MesAno = company.MesAno;
                    //    Model.Nome = company.Nome;
                    //    Model.NomeRegional = company.NomeRegional;
                    //    Model.Pago = company.Pago;
                    //    Model.PN = company.PN;
                    //    Model.sequencia = company.sequencia;
                    //    if ((DataIni >= DateTime.Now || DataFim <= DateTime.Now) && valorAtivo == true)
                    //    {
                    //        Model.Valor = novoValor;
                    //    }
                    //    else
                    //    {
                    //        Model.Valor = company.Valor;
                    //    }
                    //    Model.ValorRepasse = company.ValorRepasse;
                    //    Model.Vencimento = company.Vencimento;

                    //    ListaRegionais.Add(Model);

                    //}

                }
                else
                {
                    remessa2 = objRemessaNeg.BuscaPorCodRegional(codregional1, mesano, DataIni, DataFim, valorAtivo, novoValor).Where(x => x.Matricula == busca).OrderBy(x => x.Nome).ToList<RemessaWeb>().ToPagedList(pagina, tamanhoPagina);
                    foreach (var company in remessa2)
                    {
                        RemessaWeb Model = new RemessaWeb();
                        Model.Ativo = company.Ativo;
                        Model.Categoria = company.Categoria;
                        Model.Codigo = company.Codigo;
                        Model.CodRegional = company.CodRegional;
                        Model.Conta = company.Conta;
                        Model.DataDia = company.DataDia;
                        Model.DataPagto = company.DataPagto;
                        Model.Documento = company.Documento;
                        Model.Empresa = company.Empresa;
                        Model.indice = company.indice;
                        Model.LCM = company.LCM;
                        Model.Matricula = company.Matricula;
                        Model.MesAno = company.MesAno;
                        Model.Nome = company.Nome;
                        Model.NomeRegional = company.NomeRegional;
                        Model.Pago = company.Pago;
                        Model.PN = company.PN;
                        Model.sequencia = company.sequencia;
                        if ((DataIni >= DateTime.Now || DataFim <= DateTime.Now) && valorAtivo == true)
                        {
                            Model.Valor = novoValor;
                        }
                        else
                        {
                            Model.Valor = company.Valor;
                        }
                        Model.ValorRepasse = company.ValorRepasse;
                        Model.Vencimento = company.Vencimento;

                        ListaRegionais.Add(Model);
                    }

                }

                //foreach (var company in remessa2)
                //{
                //    RemessaWeb Model = new RemessaWeb();
                //    Model.Ativo = company.Ativo;
                //    Model.Categoria = company.Categoria;
                //    Model.Codigo = company.Codigo;
                //    Model.CodRegional = company.CodRegional;
                //    Model.Conta = company.Conta;
                //    Model.DataDia = company.DataDia;
                //    Model.DataPagto = company.DataPagto;
                //    Model.Documento = company.Documento;
                //    Model.Empresa = company.Empresa;
                //    Model.indice = company.indice;
                //    Model.LCM = company.LCM;
                //    Model.Matricula = company.Matricula;
                //    Model.MesAno = company.MesAno;
                //    Model.Nome = company.Nome;
                //    Model.NomeRegional = company.NomeRegional;
                //    Model.Pago = company.Pago;
                //    Model.PN = company.PN;
                //    Model.sequencia = company.sequencia;
                //    if ((DataIni >= DateTime.Now || DataFim <= DateTime.Now) && valorAtivo == true)
                //    {
                //        Model.Valor = novoValor;
                //    }
                //    else
                //    {
                //        Model.Valor = company.Valor;
                //    }
                //    Model.ValorRepasse = company.ValorRepasse;
                //    Model.Vencimento = company.Vencimento;

                //    ListaRegionais.Add(Model);
                //}
                //SearchUsuario search = new SearchUsuario(pagina, tamanhoPagina);
                //search.lista = ListaRegionais.ToPagedList(pagina, tamanhoPagina);
                return View(remessa2);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Acesso", FormMethod.Post);
            }

        }

        public class SearchUsuario
        {
            public IPagedList<RemessaWeb> lista { get; set; }
            public SearchUsuario(int pagina, int tamanhoPagina)
            {
                lista = new List<RemessaWeb>().ToPagedList(pagina, tamanhoPagina);
            }
        }
        //  GET: Remessa
        public ActionResult IndexOriginal(string id, string myText)
        {
            ViewBag.ID = id;
            Session["ID"] = id;
            bool valorAtivo = Convert.ToBoolean(Session["Ativo"]);
            string novoValor = Session["NovoValor"].ToString();
            string dataInicial = Session["DataInicial"].ToString();
            string dataFinal = Session["DataFinal"].ToString();
            DateTime DataIni = Convert.ToDateTime(dataInicial);
            DateTime DataFim = Convert.ToDateTime(dataFinal);
            DateTime DataAtual = DateTime.Now;
            string MesAtual = DataAtual.ToString("MM/yyyy");
            string MesInicio = DataIni.ToString("MM/yyyy");
            string MesFim = DataFim.ToString("MM/yyyy");

            try
            {
                ViewBag.Message = "Boleto.Net.MVC";
                var bancos = from Bancos s in Enum.GetValues(typeof(Bancos))
                             select new
                             {
                                 ID = Convert.ChangeType(s, typeof(int)),
                                 Name = s.ToString()
                             };
                ViewData["bancos"] = new SelectList(bancos, "ID", "Name", Bancos.Bradesco);
                string mesano = DateTime.Now.ToString("MMyyyy");
                var codregional = Session["CODREG"].ToString();
                var nomregional = Session["NOMREG"].ToString();
                ViewBag.CodRegional = codregional;
                ViewBag.NomeRegional = nomregional;// "NENUMA INFORMAÇÃO ENCONTRADA";
                RemessaModel remessa = new RemessaModel();
                if (codregional == "000")
                {
                    ViewBag.DataVencimento = DateTime.Now.AddDays(10);
                    ViewBag.ValorTotal = "R$ 0,00";
                    decimal preco, total = 0;
                    var valorTotal = objRemessaNeg.BuscaPorRegional(id, true, "true", mesano, DataIni, DataFim, valorAtivo, novoValor).OrderBy(x => x.NomeRegional).ToList<RemessaWeb>();
                    foreach (var company in valorTotal)
                    {
                        if (company.Ativo == "true")
                        {
                            preco = Convert.ToDecimal(company.Valor);
                            total = total + preco;
                            ViewBag.Total = total;
                            string v_total = String.Format("{0:C}", total);
                            ViewBag.ValorTotal = v_total;
                            ViewBag.ValorBoleto = v_total.Replace("R$ ", "").Replace(",", "").PadLeft(13, '0');
                        }
                    }
                    Session["ValorTotal"] = ViewBag.Total;
                    remessa.ListaRemessa = objRemessaNeg.BuscaPorRegional(id, false, "true", mesano, DataIni, DataFim, valorAtivo, novoValor).OrderBy(x => x.NomeRegional).ToList<RemessaWeb>();
                    if (remessa.ListaRemessa.Count > 0)
                    {
                        ViewBag.NomeRegional = remessa.ListaRemessa[0].NomeRegional;
                        ViewBag.CodRegional = remessa.ListaRemessa[0].CodRegional;
                        Session["CODREGIONAL"] = remessa.ListaRemessa[0].CodRegional;
                        Session["REGIONAL"] = remessa.ListaRemessa[0].NomeRegional;
                        string enderecos = Session["CODREGIONAL"].ToString();
                        ViewBag.Enderecos = objEnderecosNeg.BuscaPorCodigo(enderecos);

                    }

                }
                else
                {
                    ViewBag.DataVencimento = DateTime.Now.AddDays(10);
                    ViewBag.ValorTotal = "R$ 0,00";
                    decimal preco, total = 0;
                    var valorTotal = objRemessaNeg.BuscaPorRegional(codregional, true, "true", mesano, DataIni, DataFim, valorAtivo, novoValor).OrderBy(x => x.NomeRegional).ToList<RemessaWeb>();
                    foreach (var company in valorTotal)
                    {
                        if (company.Ativo == "true")
                        {
                            preco = Convert.ToDecimal(company.Valor);
                            total = total + preco;
                            string v_total = String.Format("{0:C}", total);
                            ViewBag.ValorTotal = v_total;
                            ViewBag.ValorBoleto = v_total.Replace("R$ ", "").Replace(",", "").PadLeft(13, '0');
                        }
                    }
                    remessa.ListaRemessa = objRemessaNeg.BuscaPorRegional(codregional, false, "true", mesano, DataIni, DataFim, valorAtivo, novoValor).OrderBy(x => x.NomeRegional).ToList<RemessaWeb>();
                    if (remessa.ListaRemessa.Count > 0)
                    {
                        ViewBag.NomeRegional = remessa.ListaRemessa[0].NomeRegional;
                        ViewBag.CodRegional = remessa.ListaRemessa[0].CodRegional;
                        Session["CODREGIONAL"] = remessa.ListaRemessa[0].CodRegional;
                        Session["REGIONAL"] = remessa.ListaRemessa[0].NomeRegional;
                        string enderecos = Session["CODREGIONAL"].ToString();
                        ViewBag.Enderecos = objEnderecosNeg.BuscaPorCodigo(enderecos);
                    }


                }
                if (remessa.ListaRemessa.Count > 0)
                {
                    GeraDadosBradesco(TipoArquivo.CNAB400);
                }
                string codregional1 = ViewBag.CodRegional;
                List<RemessaWeb> remessa2 = objRemessaNeg.BuscaPorCodRegional(codregional1, mesano, DataIni, DataFim, valorAtivo, novoValor).OrderBy(x => x.Nome).ToList<RemessaWeb>();
                return View(remessa2);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Acesso", FormMethod.Post);
            }
        }

        [HttpPost]
        public ActionResult Index(RemessaWeb person, string id, string pago)
        {
            string datadia = person.DataDia;

            var venc = ViewBag.Vencimento;

            List<RemessaWeb> model = new List<RemessaWeb>();
            try
            {
                var listaMacon = new RemessaWeb();
                if (pago != null)
                {
                    var maconSelecionado = objRemessaNeg.BuscaPagos("true").Where(x => x.CodRegional == id).ToList<RemessaWeb>();
                    foreach (var company in maconSelecionado)
                    {
                        listaMacon.Ativo = "false";
                        listaMacon.Categoria = company.Categoria;
                        listaMacon.Codigo = company.Codigo;
                        listaMacon.CodRegional = company.CodRegional;
                        listaMacon.Conta = company.Conta;
                        listaMacon.Documento = company.Documento;
                        listaMacon.Empresa = company.Empresa;
                        listaMacon.indice = company.indice;
                        listaMacon.LCM = company.LCM;
                        listaMacon.Matricula = company.Matricula;
                        listaMacon.MesAno = company.MesAno;
                        listaMacon.Nome = company.Nome;
                        listaMacon.NomeRegional = company.NomeRegional;
                        listaMacon.PN = company.PN;
                        listaMacon.Pago = company.Pago;
                        listaMacon.sequencia = company.sequencia;
                        listaMacon.Valor = company.Valor;
                        listaMacon.ValorRepasse = company.ValorRepasse;
                        listaMacon.Vencimento = company.Vencimento;// DateTime.Now.ToString("dd/MM/yyyy");
                        objRemessaNeg.update(listaMacon);
                    }
                    //            var resultado = Export();
                    GeraDadosBradesco(TipoArquivo.CNAB400);
                }
                else
                {
                }

                return RedirectToAction("Index", "Remessa", new { id = Session["CODREGIONAL"] });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Voltar", "Entrar");
            }
        }

        [HttpGet]
        public ActionResult GeraRemessa(string id)
        {
            bool valorAtivo = Convert.ToBoolean(Session["Ativo"]);
            string novoValor = Session["NovoValor"].ToString();
            string dataInicial = Session["DataInicial"].ToString();
            string dataFinal = Session["DataFinal"].ToString();
            DateTime DataIni = Convert.ToDateTime(dataInicial);
            DateTime DataFim = Convert.ToDateTime(dataFinal);
            DateTime DataAtual = DateTime.Now;
            string MesAtual = DataAtual.ToString("MM/yyyy");
            string MesInicio = DataIni.ToString("MM/yyyy");
            string MesFim = DataFim.ToString("MM/yyyy");


            string mesano = DateTime.Now.ToString("MMyyyy");
            var codregional = Session["CODREG"].ToString();
            var nomregional = Session["NOMREG"].ToString();
            RemessaModel remessa = new RemessaModel();
            if (codregional == "000")
            {
                ViewBag.ValorTotal = "R$ 0,00";
                decimal preco, total = 0;
                var valorTotal = objRemessaNeg.BuscaPorRegional(codregional, true, "true", mesano, DataIni, DataFim, valorAtivo, novoValor).OrderBy(x => x.NomeRegional).ToList<RemessaWeb>();
                foreach (var company in valorTotal)
                {
                    preco = Convert.ToDecimal(company.Valor);
                    total = total + preco;
                    string v_total = String.Format("{0:C}", total);
                    ViewBag.ValorTotal = v_total;
                    ViewBag.ValorBoleto = v_total.Replace("R$ ", "").Replace(",", "").PadLeft(13, '0');

                }

                remessa.ListaRemessa = objRemessaNeg.BuscaPorRegional(codregional, true, "true", mesano, DataIni, DataFim, valorAtivo, novoValor).ToList<RemessaWeb>();
                ViewBag.NomeRegional = remessa.ListaRemessa[0].NomeRegional;
                ViewBag.DataVencimento = DateTime.Now.AddDays(10);
            }
            else
            {
                ViewBag.ValorTotal = "R$ 0,00";
                decimal preco, total = 0;
                var valorTotal = objRemessaNeg.BuscaPorRegional(codregional, true, "true", mesano, DataIni, DataFim, valorAtivo, novoValor).ToList<RemessaWeb>();
                foreach (var company in valorTotal)
                {
                    preco = Convert.ToDecimal(company.Valor);
                    total = total + preco;
                    string v_total = String.Format("{0:C}", total);
                    ViewBag.ValorTotal = v_total;
                    ViewBag.ValorBoleto = v_total.Replace("R$ ", "").Replace(",", "").PadLeft(13, '0');
                }
                remessa.ListaRemessa = objRemessaNeg.BuscaPorRegional(codregional, true, "true", mesano, DataIni, DataFim, valorAtivo, novoValor).ToList<RemessaWeb>();
                ViewBag.NomeRegional = remessa.ListaRemessa[0].NomeRegional;
                ViewBag.DataVencimento = DateTime.Now.AddDays(10);
            }
            GeraDadosBradesco(TipoArquivo.CNAB400);

            ViewBag.Message = "Boleto.Net.MVC";
            var bancos = from Bancos s in Enum.GetValues(typeof(Bancos))
                         select new
                         {
                             ID = Convert.ChangeType(s, typeof(int)),
                             Name = s.ToString()
                         };
            ViewData["bancos"] = new SelectList(bancos, "ID", "Name", Bancos.Bradesco);
            return View();
            //  return RedirectToAction("Index", "Regional");
        }


        public ActionResult Gravar(int id, bool ch)
        {
            string NomeReg = "";
            string LCM = "";
            string Associado = "";
            List<RemessaWeb> model = new List<RemessaWeb>();
            try
            {
                var listaMacon = new RemessaWeb();
                if (model != null)
                {
                    var maconSelecionado = objRemessaNeg.BuscaPorId(id.ToString()).ToList<RemessaWeb>();
                    foreach (var company in maconSelecionado)
                    {
                        listaMacon.Ativo = company.Ativo;
                        listaMacon.Categoria = company.Categoria;
                        listaMacon.Codigo = company.Codigo;
                        listaMacon.CodRegional = company.CodRegional;
                        listaMacon.Conta = company.Conta;
                        listaMacon.Documento = company.Documento;
                        listaMacon.Empresa = company.Empresa;
                        listaMacon.indice = company.indice;
                        listaMacon.LCM = company.LCM;
                        listaMacon.Matricula = company.Matricula;
                        listaMacon.MesAno = company.MesAno;
                        listaMacon.Nome = company.Nome;
                        listaMacon.NomeRegional = company.NomeRegional;
                        listaMacon.PN = company.PN;
                        listaMacon.Pago = ch;
                        listaMacon.sequencia = company.sequencia;
                        listaMacon.Valor = company.Valor;
                        listaMacon.ValorRepasse = company.ValorRepasse;
                        listaMacon.Vencimento = company.Vencimento;
                        NomeReg = company.NomeRegional;
                        LCM = company.LCM;
                        Associado = company.Nome;
                        objRemessaNeg.update(listaMacon);
                    }

                    var ObjAcessosNeg = new AcessosNeg();
                    var acessos = new Acessos();
                    if (ch == true)
                    {
                        acessos.DataEntrada = DateTime.Now;
                        acessos.Nome = NomeReg;// company.NomeRegional;
                        acessos.Empresa = "Registro selecionado LCM: " + LCM + " - Associado: " + Associado + " - Regional: " + NomeReg;
                        acessos.IP = Session["IP"].ToString();
                        acessos.Usuario = Session["Username"].ToString();
                        ObjAcessosNeg.create(acessos);
                    }
                    else
                    {
                        acessos.DataEntrada = DateTime.Now;
                        acessos.Nome = NomeReg;// company.NomeRegional;
                        acessos.Empresa = "Registro desmarcado LCM: " + LCM + " - Associado: " + Associado + " - Regional: " + NomeReg;
                        acessos.IP = Session["IP"].ToString();
                        acessos.Usuario = Session["Username"].ToString();
                        ObjAcessosNeg.create(acessos);
                    }
                    //     var resultado = Export();
                    GeraDadosBradesco(TipoArquivo.CNAB400);
                }
                else
                {
                }

                return RedirectToAction("Index", "Remessa", new { id = Session["CODREGIONAL"] });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Voltar", "Entrar");
            }
        }

        public ActionResult GravarValor(string textIndice, string txtValor)
        {
         //   string id = ViewBag.Indice;
            bool ch = true;
            string NomeReg = "";
            string LCM = "";
            string Associado = "";
            List<RemessaWeb> model = new List<RemessaWeb>();
            try
            {
                var listaMacon = new RemessaWeb();
                if (model != null)
                {
                    var maconSelecionado = objRemessaNeg.BuscaPorId(textIndice.ToString()).ToList<RemessaWeb>();
                    foreach (var company in maconSelecionado)
                    {
                        listaMacon.Ativo = company.Ativo;
                        listaMacon.Categoria = company.Categoria;
                        listaMacon.Codigo = company.Codigo;
                        listaMacon.CodRegional = company.CodRegional;
                        listaMacon.Conta = company.Conta;
                        listaMacon.Documento = company.Documento;
                        listaMacon.Empresa = company.Empresa;
                        listaMacon.indice = company.indice;
                        listaMacon.LCM = company.LCM;
                        listaMacon.Matricula = company.Matricula;
                        listaMacon.MesAno = company.MesAno;
                        listaMacon.Nome = company.Nome;
                        listaMacon.NomeRegional = company.NomeRegional;
                        listaMacon.PN = company.PN;
                        listaMacon.Pago = ch;
                        listaMacon.sequencia = company.sequencia;
                        listaMacon.Valor = txtValor;
                        listaMacon.ValorRepasse = company.ValorRepasse;
                        listaMacon.Vencimento = company.Vencimento;
                        NomeReg = company.NomeRegional;
                        LCM = company.LCM;
                        Associado = company.Nome;
                        objRemessaNeg.update(listaMacon);
                    }

                    var ObjAcessosNeg = new AcessosNeg();
                    var acessos = new Acessos();
                    if (ch == true)
                    {
                        acessos.DataEntrada = DateTime.Now;
                        acessos.Nome = NomeReg;// company.NomeRegional;
                        acessos.Empresa = "Registro selecionado LCM: " + LCM + " - Associado: " + Associado + " - Regional: " + NomeReg;
                        acessos.IP = Session["IP"].ToString();
                        acessos.Usuario = Session["Username"].ToString();
                        ObjAcessosNeg.create(acessos);
                    }
                    else
                    {
                        acessos.DataEntrada = DateTime.Now;
                        acessos.Nome = NomeReg;// company.NomeRegional;
                        acessos.Empresa = "Registro desmarcado LCM: " + LCM + " - Associado: " + Associado + " - Regional: " + NomeReg;
                        acessos.IP = Session["IP"].ToString();
                        acessos.Usuario = Session["Username"].ToString();
                        ObjAcessosNeg.create(acessos);
                    }
                    //     var resultado = Export();
                    GeraDadosBradesco(TipoArquivo.CNAB400);
                }
                else
                {
                }

                return RedirectToAction("Index", "Remessa", new { id = Session["CODREGIONAL"] });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Voltar", "Entrar");
            }
        }

        public ActionResult GravarAcordo(string textIndice, string txtValor)
        {
            string id = Session["CODREGIONAL"].ToString();
            bool ch = true;
            string NomeReg = "";
            string LCM = "";
            string Associado = "";
            List<RemessaWeb> model = new List<RemessaWeb>();
            try
            {
                var listaMacon = new RemessaWeb();
                if (model != null)
                {
                    var maconSelecionado = objRemessaNeg.BuscaPorId(id.ToString()).ToList<RemessaWeb>();
                    foreach (var company in maconSelecionado)
                    {
                        listaMacon.Ativo = company.Ativo;
                        listaMacon.Categoria = company.Categoria;
                        listaMacon.Codigo = company.Codigo;
                        listaMacon.CodRegional = company.CodRegional;
                        listaMacon.Conta = company.Conta;
                        listaMacon.Documento = company.Documento;
                        listaMacon.Empresa = company.Empresa;
                        listaMacon.indice = company.indice;
                        listaMacon.LCM = company.LCM;
                        listaMacon.Matricula = company.Matricula;
                        listaMacon.MesAno = company.MesAno;
                        listaMacon.Nome = company.Nome;
                        listaMacon.NomeRegional = company.NomeRegional;
                        listaMacon.PN = company.PN;
                        listaMacon.Pago = ch;
                        listaMacon.sequencia = company.sequencia;
                        listaMacon.Valor = ViewBag.ValorAcordo;
                        listaMacon.ValorRepasse = company.ValorRepasse;
                        listaMacon.Vencimento = company.Vencimento;
                        NomeReg = company.NomeRegional;
                        LCM = company.LCM;
                        Associado = company.Nome;
                        objRemessaNeg.update(listaMacon);
                    }

                    var ObjAcessosNeg = new AcessosNeg();
                    var acessos = new Acessos();
                    if (ch == true)
                    {
                        acessos.DataEntrada = DateTime.Now;
                        acessos.Nome = NomeReg;// company.NomeRegional;
                        acessos.Empresa = "Registro selecionado LCM: " + LCM + " - Associado: " + Associado + " - Regional: " + NomeReg;
                        acessos.IP = Session["IP"].ToString();
                        acessos.Usuario = Session["Username"].ToString();
                        ObjAcessosNeg.create(acessos);
                    }
                    else
                    {
                        acessos.DataEntrada = DateTime.Now;
                        acessos.Nome = NomeReg;// company.NomeRegional;
                        acessos.Empresa = "Registro desmarcado LCM: " + LCM + " - Associado: " + Associado + " - Regional: " + NomeReg;
                        acessos.IP = Session["IP"].ToString();
                        acessos.Usuario = Session["Username"].ToString();
                        ObjAcessosNeg.create(acessos);
                    }
                    //     var resultado = Export();
                    GeraDadosBradesco(TipoArquivo.CNAB400);
                }
                else
                {
                }

                return RedirectToAction("Index", "Remessa", new { id = Session["CODREGIONAL"] });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Voltar", "Entrar");
            }
        }


        public ActionResult FechaPacote(string textBoxReg, string pago, string textBoxStringData)
        {
            var venc = ViewBag.Vencimento;
            textBoxReg = Session["CODREGIONAL"].ToString();
            Session["DATAPAG"] = textBoxStringData;
            List<RemessaWeb> model = new List<RemessaWeb>();
            try
            {
                var listaMacon = new RemessaWeb();
                if (textBoxStringData != "")
                {
                    Random random = new Random();
                    string nrand = random.Next(10000, 99999).ToString();
                    var maconSelecionado = objRemessaNeg.BuscaPagos("true").Where(x => x.CodRegional == textBoxReg && x.Codigo == "0").ToList<RemessaWeb>();
                    foreach (var company in maconSelecionado)
                    {
                        DateTime venceu = Convert.ToDateTime(company.Vencimento);
                        DiferencaDatas diferencaData = new DiferencaDatas(DateTime.Now, venceu);
                        int meses = Convert.ToInt32(diferencaData.ToString());

                        listaMacon.Ativo = "false";
                        listaMacon.Categoria = company.Categoria;
                        listaMacon.Codigo = nrand;
                        listaMacon.CodRegional = textBoxReg;
                        listaMacon.Conta = company.Conta;
                        listaMacon.Documento = company.Documento;
                        listaMacon.Empresa = company.Empresa;
                        listaMacon.indice = company.indice;
                        listaMacon.LCM = company.LCM;
                        listaMacon.Matricula = company.Matricula;
                        listaMacon.MesAno = company.MesAno;
                        Session["MESANO"] = company.MesAno;
                        listaMacon.Nome = company.Nome;
                        listaMacon.NomeRegional = company.NomeRegional;
                        listaMacon.PN = company.PN;
                        listaMacon.Pago = company.Pago;
                        listaMacon.sequencia = company.sequencia;
                        listaMacon.Valor = company.Valor;
                        listaMacon.ValorRepasse = company.ValorRepasse;
                        listaMacon.Vencimento = company.Vencimento;
                        listaMacon.DataPagto = textBoxStringData;
                        objRemessaNeg.update(listaMacon);

                        var ObjAcessosNeg = new AcessosNeg();
                        var acessos = new Acessos();
                        acessos.DataEntrada = DateTime.Now;
                        acessos.Nome = company.NomeRegional;// company.NomeRegional;
                        acessos.Empresa = "Registro selecionado LCM: " + company.LCM + " - Associado: " + company.Nome + " - Regional: " + textBoxReg;
                        acessos.IP = Session["IP"].ToString();
                        acessos.Usuario = Session["Username"].ToString();
                        ObjAcessosNeg.create(acessos);


                        //********************* Rotina para validar associados bloqueados
                        //if (meses <= 4)
                        //{
                        //    listaMacon.Ativo = "false";
                        //    listaMacon.Categoria = company.Categoria;
                        //    listaMacon.Codigo = nrand;
                        //    listaMacon.CodRegional = textBoxReg;
                        //    listaMacon.Conta = company.Conta;
                        //    listaMacon.Documento = company.Documento;
                        //    listaMacon.Empresa = company.Empresa;
                        //    listaMacon.indice = company.indice;
                        //    listaMacon.LCM = company.LCM;
                        //    listaMacon.Matricula = company.Matricula;
                        //    listaMacon.MesAno = company.MesAno;
                        //    Session["MESANO"] = company.MesAno;
                        //    listaMacon.Nome = company.Nome;
                        //    listaMacon.NomeRegional = company.NomeRegional;
                        //    listaMacon.PN = company.PN;
                        //    listaMacon.Pago = company.Pago;
                        //    listaMacon.sequencia = company.sequencia;
                        //    listaMacon.Valor = company.Valor;
                        //    listaMacon.ValorRepasse = company.ValorRepasse;
                        //    listaMacon.Vencimento = company.Vencimento;
                        //    listaMacon.DataPagto = textBoxStringData;
                        //    objRemessaNeg.update(listaMacon);
                        //}else
                        //{
                        //    listaMacon.Ativo = "false";
                        //    listaMacon.Categoria = company.Categoria;
                        //    listaMacon.Codigo = nrand;
                        //    listaMacon.CodRegional = textBoxReg;
                        //    listaMacon.Conta = company.Conta;
                        //    listaMacon.Documento = company.Documento;
                        //    listaMacon.Empresa = company.Empresa;
                        //    listaMacon.indice = company.indice;
                        //    listaMacon.LCM = company.LCM;
                        //    listaMacon.Matricula = company.Matricula;
                        //    listaMacon.MesAno = company.MesAno;
                        //    Session["MESANO"] = company.MesAno;
                        //    listaMacon.Nome = company.Nome;
                        //    listaMacon.NomeRegional = company.NomeRegional;
                        //    listaMacon.PN = company.PN;
                        //    listaMacon.Pago = company.Pago;
                        //    listaMacon.sequencia = company.sequencia;
                        //    listaMacon.Valor = company.Valor;
                        //    listaMacon.ValorRepasse = company.ValorRepasse;
                        //    listaMacon.Vencimento = company.Vencimento;
                        //    listaMacon.DataPagto = textBoxStringData;
                        //    objRemessaNeg.baixaLote("0", "true", textBoxReg, company.MesAno, company.LCM);
                        //}
                    }

                    var resultado = Export(nrand);

                    GeraDadosBradesco(TipoArquivo.CNAB400);
                    return RedirectToAction("Index", "Remessa", new { id = Session["CODREGIONAL"] });
                }
                else
                {
                    return RedirectToAction("Baixa", "Entrar");
                }
                //        return RedirectToAction("Voltar", "Entrar");


            }
            catch (Exception ex)
            {
                return RedirectToAction("Voltar", "Entrar");
            }
        }

        public ActionResult BaixaLote(string textLcm)
        {


            try
            {
                string codreg = Session["CODREGIONAL"].ToString();
                string mesano = Session["MESANO"].ToString();
                textLcm = textLcm.Replace(",", "','");
                objRemessaNeg.baixaLote("1", "true", codreg, mesano, textLcm);
                var ObjAcessosNeg = new AcessosNeg();
                var acessos = new Acessos();
                acessos.DataEntrada = DateTime.Now;
                acessos.Nome = Session["NOMREG"].ToString(); // company.NomeRegional;
                acessos.Empresa = "Baixa em Lote - LCMs: " + textLcm;
                acessos.IP = Session["IP"].ToString();
                acessos.Usuario = Session["Username"].ToString();
                ObjAcessosNeg.create(acessos);

                return RedirectToAction("Index", "Remessa", new { id = Session["CODREGIONAL"] });

            }
            catch (Exception ex)
            {
                return RedirectToAction("Voltar", "Entrar");
            }
        }
        public ActionResult Retorna(string textLcm)
        {
            try
            {
                string codreg = Session["CODREGIONAL"].ToString();
                string mesano = Session["MESANO"].ToString();
                textLcm = textLcm.Replace(",", "','");
                objRemessaNeg.baixaLote("0", "true", codreg, mesano, textLcm);

                var ObjAcessosNeg = new AcessosNeg();
                var acessos = new Acessos();
                acessos.DataEntrada = DateTime.Now;
                acessos.Nome = Session["NOMREG"].ToString(); // company.NomeRegional;
                acessos.Empresa = "Retorno em Lote - LCMs: " + textLcm;
                acessos.IP = Session["IP"].ToString();
                acessos.Usuario = Session["Username"].ToString();
                ObjAcessosNeg.create(acessos);

                return RedirectToAction("Index", "Remessa", new { id = Session["CODREGIONAL"] });

            }
            catch (Exception ex)
            {
                return RedirectToAction("Voltar", "Entrar");
            }
        }
        public FileResult Export(string codigo)
        {
            DateTime paga = DateTime.Now;
            string mesano = DateTime.Now.ToString("MMyyyy");
            var codregional = Session["CODREGIONAL"].ToString();
            var nomregional = Session["REGIONAL"].ToString();
            bool valorAtivo = Convert.ToBoolean(Session["Ativo"]);
            string novoValor = Session["NovoValor"].ToString();
            string dataInicial = Session["DataInicial"].ToString();
            string dataFinal = Session["DataFinal"].ToString();
            DateTime DataIni = Convert.ToDateTime(dataInicial);
            DateTime DataFim = Convert.ToDateTime(dataFinal);
            DateTime DataAtual = DateTime.Now;
            string MesAtual = DataAtual.ToString("MM/yyyy");
            string MesInicio = DataIni.ToString("MM/yyyy");
            string MesFim = DataFim.ToString("MM/yyyy");

            try
            {
                paga = Convert.ToDateTime(Session["DATAPAG"].ToString());
            }
            catch
            {
                paga = DateTime.Now;
            }

            //  NorthwindEntities entities = new NorthwindEntities();
            RemessaModel model = new RemessaModel();
            //     IEnumerable stu = model.ListaRemessa.Where(x => x.Pago == true ).ToList<RemessaWeb>();
            //     var valorTotal = objRemessaNeg.BuscaPorRegional("35").Where(x => x.Pago == true).ToList<RemessaWeb>();
            List<object> customers =
                (from customer in objRemessaNeg.BuscaPorRegional(codregional, true, "true", mesano, DataIni, DataFim, valorAtivo, novoValor).Where(x => x.Codigo == codigo)
                 select new[] {
                        "1",  //Est
                        "LC", //ESp
                        "",   //Ser
                        customer.LCM,//Titulo
                        "1",  // /P
                        customer.PN, //Cliente
                        "2",  //Porta
                        "Padrao",  //Cart
                        "Baixa", //Referencia
                        "Real", //Moeda
                        customer.Valor, //Valor
                        "0", //Factoring
                        "0", //Multa
                        "0", //Descont
                        "0", //Juros
                        "0", //ISS
                        "0", //IR
                        "0", //INSS
                        "0", //PIS
                        "0", //CONFINS
                        "0", //CSLL
                        customer.DataPagto, //Liquid/Credito
                        //paga.ToString("dd/MM/yyyy"), //Liquid/Credito
                        "", //Conta Contabil
                      }).ToList<object>();

            StringBuilder sb = new StringBuilder();

            sb.Append("Est" + ';');
            sb.Append("Esp" + ';');
            sb.Append("Ser" + ';');
            sb.Append("Titulo" + ';');
            sb.Append("/P" + ';');
            sb.Append("Cliente" + ';');
            sb.Append("Port" + ';');
            sb.Append("Cart" + ';');
            sb.Append("Referencia" + ';');
            sb.Append("Moeda" + ';');
            sb.Append("Valor" + ';');
            sb.Append("Factoring" + ';');
            sb.Append("Multa" + ';');
            sb.Append("Desconto " + ';');
            sb.Append("Juros" + ';');
            sb.Append("ISS" + ';');
            sb.Append("IR" + ';');
            sb.Append("INSS" + ';');
            sb.Append("PIS" + ';');
            sb.Append("COFINS" + ';');
            sb.Append("CSLL" + ';');
            sb.Append("Liquidac/Credito" + ';');
            sb.Append("ContaContabil" + ';');
            sb.Append("\r\n");


            for (int i = 0; i < customers.Count; i++)
            {
                string[] customer = (string[])customers[i];
                for (int j = 0; j < customer.Length; j++)
                {
                    sb.Append(customer[j] + ';');
                }
                sb.Append("\r\n");
            }
            Random random = new Random();
            string nrand = random.Next(100, 999).ToString();
            string hgravado = DateTime.Now.ToString("ddMMyyyy_hhmm");
            string data = ViewBag.CodRegional + DateTime.Now.ToString("ddMMyy");
            string _FileName = Path.GetFileName(codregional + "retorno.csv");
            if (System.IO.File.Exists(Server.MapPath("~/Content/Arquivo/" + nomregional + "_" + hgravado + "_" + _FileName)))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/Arquivo/" + nomregional + "_" + hgravado + "_" + _FileName));
                StreamWriter valor = new StreamWriter(Server.MapPath("~/Content/Arquivo/" + nomregional + "_" + hgravado + "_" + _FileName), true, Encoding.ASCII);
                valor.WriteLine(sb.ToString());
                valor.Close();
            }
            else
            {
                StreamWriter valor = new StreamWriter(Server.MapPath("~/Content/Arquivo/" + nomregional + "_" + hgravado + "_" + _FileName), true, Encoding.ASCII);
                valor.WriteLine(sb.ToString());
                valor.Close();
            }


            return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "~/Content/Grid.csv");
        }

        public ActionResult CarregaRelatorio()
        {
            var codregional = Session["CODREGIONAL"].ToString();
            List<RemessaWeb> allRemessa = new List<RemessaWeb>();
            allRemessa = objRemessaNeg.BuscaPagos("false").Where(x => x.CodRegional == codregional).ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "Repasses.rpt"));

            rd.SetDataSource(allRemessa);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Repasses.pdf");
        }

        public static DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                if (csv_file_path.EndsWith(".csv"))
                {
                    using (Microsoft.VisualBasic.FileIO.TextFieldParser csvReader = new Microsoft.VisualBasic.FileIO.TextFieldParser(csv_file_path))
                    {
                        csvReader.SetDelimiters(new string[] { "," });
                        csvReader.HasFieldsEnclosedInQuotes = true;
                        //read column
                        string[] colFields = csvReader.ReadFields();
                        foreach (string column in colFields)
                        {
                            DataColumn datecolumn = new DataColumn(column);
                            datecolumn.AllowDBNull = true;
                            csvData.Columns.Add(datecolumn);
                        }
                        while (!csvReader.EndOfData)
                        {
                            string[] fieldData = csvReader.ReadFields();
                            for (int i = 0; i < fieldData.Length; i++)
                            {
                                if (fieldData[i] == "")
                                {
                                    fieldData[i] = null;
                                }
                            }
                            csvData.Rows.Add(fieldData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //       MessageBox.Show("Exce " + ex);
            }
            return csvData;
        }

        static DataTable ConvertListToDataTable(List<string[]> list)
        {
            // New table.
            DataTable table = new DataTable();
            // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (array.Length > columns)
                {
                    columns = array.Length;
                }
            }
            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }
            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }
            return table;
        }


        public ActionResult VisualizarBoleto(int Id)
        {
            bool valorAtivo = Convert.ToBoolean(Session["Ativo"]);
            string novoValor = Session["NovoValor"].ToString();
            string dataInicial = Session["DataInicial"].ToString();
            string dataFinal = Session["DataFinal"].ToString();
            DateTime DataIni = Convert.ToDateTime(dataInicial);
            DateTime DataFim = Convert.ToDateTime(dataFinal);
            DateTime DataAtual = DateTime.Now;
            string MesAtual = DataAtual.ToString("MM/yyyy");
            string MesInicio = DataIni.ToString("MM/yyyy");
            string MesFim = DataFim.ToString("MM/yyyy");


            string mesano = DateTime.Now.ToString("MMyyyy");

            var codregional = Session["CODREGIONAL"].ToString();
            var nomregional = Session["REGIONAL"].ToString();
            ViewBag.RegionaisPagas = objRemessaNeg.BuscaPorRegional(codregional, true, "true", mesano, DataIni, DataFim, valorAtivo, novoValor).ToList<RemessaWeb>();
            string enderecos = Session["CODREGIONAL"].ToString();
            ViewBag.Enderecos = objEnderecosNeg.BuscaPorCodigo(enderecos);


            decimal valor = Convert.ToDecimal(Session["ValorTotal"].ToString());
            var boleto = ObterBoletoBancario(Id, valor, codregional, nomregional);
            ViewBag.Boleto = boleto.MontaHtmlEmbedded();
            return View();
        }

        public ActionResult GerarBoletoPDF(int Id)
        {
            bool valorAtivo = Convert.ToBoolean(Session["Ativo"]);
            string novoValor = Session["NovoValor"].ToString();
            string dataInicial = Session["DataInicial"].ToString();
            string dataFinal = Session["DataFinal"].ToString();
            DateTime DataIni = Convert.ToDateTime(dataInicial);
            DateTime DataFim = Convert.ToDateTime(dataFinal);
            DateTime DataAtual = DateTime.Now;
            string MesAtual = DataAtual.ToString("MM/yyyy");
            string MesInicio = DataIni.ToString("MM/yyyy");
            string MesFim = DataFim.ToString("MM/yyyy");


            string mesano = DateTime.Now.ToString("MMyyyy");

            var codregional = Session["CODREGIONAL"].ToString();
            var nomregional = Session["REGIONAL"].ToString();
            ViewBag.RegionaisPagas = objRemessaNeg.BuscaPorRegional(codregional, true, "true", mesano, DataIni, DataFim, valorAtivo, novoValor).ToList<RemessaWeb>();
            string enderecos = Session["CODREGIONAL"].ToString();
            ViewBag.Enderecos = objEnderecosNeg.BuscaPorCodigo(enderecos);


            decimal valor = Convert.ToDecimal(Session["ValorTotal"].ToString());
            var boleto = ObterBoletoBancario(Id, valor, codregional, nomregional);
            var pdf = boleto.MontaBytesPDF();
            return File(pdf, "application/pdf");
        }

        public BoletoBancario ObterBoletoBancario(int Id, decimal valor, string codregional, string nomregional)
        {
            var exemplos = new Exemplos(Id);

            switch ((Bancos)Id)
            {
                //case Bancos.BancodoBrasil:
                //    return exemplos.BancodoBrasil();
                //case Bancos.Banrisul:
                //    return exemplos.Banrisul();
                //case Bancos.Basa:
                //    return exemplos.Basa();
                case Bancos.Bradesco:
                    return exemplos.Bradesco(valor, codregional, nomregional);
                //case Bancos.BRB:
                //    return exemplos.BRB();
                //case Bancos.Caixa:
                //    return exemplos.Caixa();
                //case Bancos.HSBC:
                //    return exemplos.HSBC();
                case Bancos.Itau:
                    return exemplos.Itau();
                //case Bancos.Real:
                //    return exemplos.Real();
                //case Bancos.Safra:
                //    return exemplos.Safra();
                //case Bancos.Santander:
                //    return exemplos.Santander();
                //case Bancos.Sicoob:
                //    return exemplos.Sicoob();
                //case Bancos.Sicred:
                //    return exemplos.Sicred();
                //case Bancos.Sudameris:
                //    return exemplos.Sudameris();
                //case Bancos.Unibanco:
                //    return exemplos.Unibanco();
                //case Bancos.Semear:
                //    return exemplos.Semear();
                default:
                    throw new ArgumentException("Banco não implementado");
            }
        }



































        public ActionResult ImprimeBoleto()
        {
            DateTime vencimento = DateTime.Now.AddDays(5);

            Instrucao_Bradesco item = new Instrucao_Bradesco(9, 5);

            Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "1234", "5", "123456", "7");
            c.Codigo = "13000";


            //Carteiras 
            //Boleto b = new Boleto(vencimento, 1.01m, "09", "01000000001", c);
            //b.NumeroDocumento = "01000000001";

            //b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            //b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            //b.Sacado.Endereco.Bairro = "Bairro";
            //b.Sacado.Endereco.Cidade = "Cidade";
            //b.Sacado.Endereco.CEP = "00000000";
            //b.Sacado.Endereco.UF = "UF";

            //item.Descricao += " após " + item.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            //b.Instrucoes.Add(item); //"Não Receber após o vencimento");

            //Instrucao i = new Instrucao(237);
            //i.Descricao = "Nova Instrução";
            //b.Instrucoes.Add(i);

            /* 
             * A data de vencimento não é usada
             * Usado para mostrar no lugar da data de vencimento o termo "Contra Apresentação";
             * Usado na carteira 06
             */
            //boletoBancario.MostrarContraApresentacaoNaDataVencimento = true;

            //boletoBancario.Boleto = b;
            //boletoBancario.Boleto.Valida();

            //boletoBancario.MostrarComprovanteEntrega = (Request.Url.Query == "?show");

            return View();
        }

        #region Remessa
        public void GeraArquivoCNAB400(IBanco banco, Cedente cedente, Boletos boletos, string numeroConvenio = null)
        {
            try
            {
                //RemessaNeg remessasNeg = new RemessaNeg();
                //List<Remessa> remessas = remessasNeg.BuscaPorRegional(ViewBag.CodRegional);
                ArquivoRemessa arquivo = new ArquivoRemessa(TipoArquivo.CNAB400);
                string data = ViewBag.CodRegional + DateTime.Now.ToString("ddMMyy");
                string _FileName = Path.GetFileName("CB" + data + ".REM");
                string _path = Path.Combine(HttpContext.Server.MapPath("~/Content/Remessa/"), _FileName);
                string fileName = Path.GetFileName(_path);
                FileStream stream = null;
                stream = new FileStream(_path, FileMode.OpenOrCreate);
                arquivo.GerarArquivoRemessa(numeroConvenio != null ? numeroConvenio : "0", banco, cedente, boletos, stream, 1);
                GeraArquivoCNAB400Bradesco();
            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
            }
        }
        void StreamToFileAttachment(Stream str, string fileName)
        {
            byte[] buf = new byte[str.Length];  //declare arraysize
            str.Read(buf, 0, buf.Length);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.AddHeader("Content-Length", str.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.OutputStream.Write(buf, 0, buf.Length);
            Response.End();
        }
        public void GeraArquivoCNAB240(IBanco banco, Cedente cedente, Boletos boletos)
        {
            //Create a stream for the file
            Stream stream = null;

            //This controls how many bytes to read at a time and send to the client
            int bytesToRead = 10000;

            // Buffer to read bytes in chunk size specified above
            byte[] buffer = new Byte[bytesToRead];

            // The number of bytes read
            try
            {
                string url = "www.uol.com.br";
                string fileName = "arquivo.txt";
                //Create a WebRequest to get the file
                HttpWebRequest fileReq = (HttpWebRequest)HttpWebRequest.Create(url);

                //Create a response for this request
                HttpWebResponse fileResp = (HttpWebResponse)fileReq.GetResponse();

                if (fileReq.ContentLength > 0)
                    fileResp.ContentLength = fileReq.ContentLength;

                //Get the Stream returned from the response
                stream = fileResp.GetResponseStream();

                // prepare the response to the client. resp is the client Response
                var resp = HttpContext.Response;

                //Indicate the type of data being sent
                resp.ContentType = "application/octet-stream";

                //Name the file 
                resp.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");
                resp.AddHeader("Content-Length", fileResp.ContentLength.ToString());

                int length;
                do
                {
                    // Verify that the client is connected.
                    if (resp.IsClientConnected)
                    {
                        // Read data into the buffer.
                        length = stream.Read(buffer, 0, bytesToRead);

                        // and write it out to the response's output stream
                        resp.OutputStream.Write(buffer, 0, length);

                        // Flush the data
                        resp.Flush();

                        //Clear the buffer
                        buffer = new Byte[bytesToRead];
                    }
                    else
                    {
                        // cancel the download if client has disconnected
                        length = -1;
                    }
                } while (length > 0); //Repeat until no data is read
            }
            finally
            {
                if (stream != null)
                {
                    //Close the input stream
                    stream.Close();
                }
            }
            //saveFileDialog.Filter = "Arquivos de Retorno (*.rem)|*.rem|Todos Arquivos (*.*)|*.*";
            //if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //{
            ArquivoRemessa arquivo = new ArquivoRemessa(TipoArquivo.CNAB240);
            //             arquivo.GerarArquivoRemessa("1200303001417053", banco, cedente, boletos, saveFileDialog.OpenFile(), 1);

            //    MessageBox.Show("Arquivo gerado com sucesso!", "Teste",
            //                    MessageBoxButtons.OK,
            //                    MessageBoxIcon.Information);
            //}
        }
        //
        public void GeraDadosBradesco(TipoArquivo tipoArquivo)
        {
            Cedente objCEDENTE = new Cedente(
                   "47331822000119",
                   "ASSOCIACAO PAULISTA DE CIRURGI",
                   "0091",
                   "52974",
                   "9"
                   );
            objCEDENTE.Codigo = "4551915";
            objCEDENTE.Convenio = 9;

            Instrucao_Bradesco item1 = new Instrucao_Bradesco(9, 5);
            Random numAleatorio = new Random();
            int NumeroDocumento = numAleatorio.Next(75000, 79999);

            //Instancia de Boleto
            Boleto objBOLETO = new Boleto();
            ViewBag.DataVencimento = DateTime.Now.AddDays(10);
            //O nosso-numero deve ser de 11 posi��es
            objBOLETO.EspecieDocumento = new EspecieDocumento(237, "01");
            objBOLETO.DataVencimento = ViewBag.DataVencimento;
            objBOLETO.ValorBoleto = Convert.ToDecimal(ViewBag.ValorBoleto);
            objBOLETO.Carteira = "09";
            string codRegional = Session["CODREGIONAL"].ToString();
            string nossonumero = ViewBag.CodRegional + DateTime.Now.ToString("MMyyy");
            nossonumero = nossonumero.PadLeft(11, '0');
            objBOLETO.NossoNumero = (nossonumero);// ("00000012345");
            objBOLETO.Cedente = objCEDENTE;
            //O num do documento deve ser de 10 posi��es
            objBOLETO.NumeroDocumento = NumeroDocumento.ToString().PadRight(10, ' ');// "1234567890";
            objBOLETO.NumeroControle = codRegional.PadLeft(5, '0');// "100";
            //A data do documento � a data de emiss�o do boleto
            objBOLETO.DataDocumento = DateTime.Now;
            EnderecosNeg enderecosNeg = new EnderecosNeg();
            var enderecos = enderecosNeg.BuscaPorCodigo(codRegional);

            //A data de processamento � a data em que foi processado o documento, portanto � da data de emissao do boleto
            objBOLETO.DataProcessamento = DateTime.Now;
            objBOLETO.Sacado = new Sacado("99999999999999", ViewBag.NomeRegional);
            objBOLETO.Sacado.Endereco.End = enderecos[0].Descricao;// "ENDERECO DA REGIONAL";
            objBOLETO.Sacado.Endereco.Bairro = enderecos[0].Bairro;// "BAIRRO REGIONAL";
            objBOLETO.Sacado.Endereco.Cidade = enderecos[0].Cidade;// "CIDADE REGIONAL";
            objBOLETO.Sacado.Endereco.CEP = enderecos[0].Cep;// "CEP REGIONAL";
            objBOLETO.Sacado.Endereco.UF = enderecos[0].UF;// "UF";

            objBOLETO.PercMulta = 10;
            objBOLETO.JurosMora = 5;
            objBOLETO.Instrucoes.Add(item1);

            objBOLETO.Banco = new Banco(237);

            // nao precisa desta parte no boleto do brasdesco.
            /*objBOLETO.Remessa = new Remessa()
            {
                Ambiente = Remessa.TipoAmbiemte.Producao,
                CodigoOcorrencia = "01",
            };*/

            Boletos objBOLETOS = new Boletos();
            objBOLETOS.Add(objBOLETO);
            //            objBOLETOS.Add(objBOLETO);

            var mem = new MemoryStream();
            var objREMESSA = new ArquivoRemessa(TipoArquivo.CNAB400);

            switch (tipoArquivo)
            {
                case TipoArquivo.CNAB240:
                    //GeraArquivoCNAB240(b2.Banco, c, boletos);
                    break;
                case TipoArquivo.CNAB400:
                    GeraArquivoCNAB400(objBOLETO.Banco, objCEDENTE, objBOLETOS, "09");
                    break;
                default:
                    break;
            }

        }
        public void GeraArquivoCNAB400Bradesco()
        {
            bool valorAtivo = Convert.ToBoolean(Session["Ativo"]);
            string novoValor = Session["NovoValor"].ToString();
            string dataInicial = Session["DataInicial"].ToString();
            string dataFinal = Session["DataFinal"].ToString();
            DateTime DataIni = Convert.ToDateTime(dataInicial);
            DateTime DataFim = Convert.ToDateTime(dataFinal);
            DateTime DataAtual = DateTime.Now;
            string MesAtual = DataAtual.ToString("MM/yyyy");
            string MesInicio = DataIni.ToString("MM/yyyy");
            string MesFim = DataFim.ToString("MM/yyyy");

            try
            {
                //                StreamWriter gravaLinha = new StreamWriter(arquivo);
                //                StreamWriter gravaLinha = new StreamWriter(arquivo);
                Random numAleatorio = new Random();
                int valorInteiro = numAleatorio.Next(100, 999);
                string data = ViewBag.CodRegional + DateTime.Now.ToString("ddMMyy");
                string _FileName = Path.GetFileName("CB" + data + valorInteiro + ".RET");
                string _path = Path.Combine(HttpContext.Server.MapPath("~/Content/Remessa/"), _FileName);
                string fileName = Path.GetFileName(_path);
                FileStream stream = null;
                stream = new FileStream(_path, FileMode.OpenOrCreate);

                #region Variáveis

                string _header;
                string _detalhe1;
                string _trailer;

                string n104 = new string('0', 104);
                string n266 = new string(' ', 266);
                string n387 = new string(' ', 387);
                string n025 = new string(' ', 25);
                string n017 = new string(' ', 17);
                string n023 = new string(' ', 23);
                string n066 = new string(' ', 66);
                string n039 = new string('0', 39);
                string n026 = new string('0', 26);
                string n090 = new string(' ', 90);
                string n08 = new string(' ', 8);
                string n09 = new string(' ', 9);
                string n010 = new string(' ', 10);
                string n012 = new string('0', 12);
                string n160 = new string(' ', 160);


                string databanco = DateTime.Now.ToString("ddMMyy");
                string nossonumerobd = "87236";
                nossonumerobd = nossonumerobd.PadRight(10, ' ');
                string titulobanco = "0000000000290000001P";


                string jurosmora = "0";
                jurosmora = jurosmora.PadLeft(13, '0');
                string outroscreditos = "0";
                outroscreditos = outroscreditos.PadLeft(13, '0');
                string datadocredito = DateTime.Now.ToString("ddMMyy"); //Trazer da APCD
                #endregion

                #region HEADER

                _header = "02RETORNO01COBRANCA       00000000000004551915ASSOCIACAO PAULISTA DE CIRURGI237BRADESCO       ";
                _header += DateTime.Now.ToString("ddMMyy");// "08010800000BPI00000201207";
                _header += "0160000001467";
                _header += n266;
                _header += DateTime.Now.ToString("ddMMyy");
                _header += n09;
                _header += "000001";

                StreamWriter incluiLinha = new StreamWriter(stream, Encoding.GetEncoding("ISO-8859-1"));
                incluiLinha.WriteLine(_header);

                #endregion

                #region DETALHE
                string id = Session["CODREGIONAL"].ToString();
                RemessaNeg remessasNeg = new RemessaNeg();
                string mesano = DateTime.Now.ToString("MMyyyy");
                RemessaModel model = new RemessaModel();
                remessa.ListaRemessa = objRemessaNeg.BuscaPorRegional(id, true, "true", mesano, DataIni, DataFim, valorAtivo, novoValor).ToList<RemessaWeb>();

                int sequencia = 2;
                foreach (var videoViewModel in remessa.ListaRemessa)
                {
                    string sequencial = Convert.ToString(sequencia);
                    sequencial = sequencial.PadLeft(6, '0'); //Incrementar vindo da APCD

                    string nossonumero = videoViewModel.LCM; // LCM da APCD
                    nossonumero = nossonumero.PadLeft(12, '0');
                    int dv = CalculaDVNossoNumero(nossonumero);
                    string dvnossonumero = Convert.ToString(dv);
                    string lcm = videoViewModel.LCM;// "37740"; // LCM da APCD
                    lcm = lcm.PadRight(25, ' ');
                    string datavencimento = Convert.ToDateTime(videoViewModel.Vencimento).ToString("ddMMyy"); //Trazer da APCD
                    string valordotitulo = videoViewModel.Valor.Replace(",", "");
                    valordotitulo = valordotitulo.PadLeft(13, '0');
                    string valorpago = videoViewModel.Valor.Replace(",", "");
                    valorpago = valorpago.PadLeft(13, '0');

                    _detalhe1 = "102473318220001190000009000910152974" + lcm + n08 + nossonumero + dvnossonumero + n010 + n012 + "000906" + databanco + nossonumerobd + titulobanco + datavencimento;
                    _detalhe1 += valordotitulo + "23704151" + "  000000000020800000000000000000000000000000000000000000000000000000000000000000";
                    _detalhe1 += valorpago + jurosmora + outroscreditos + "   " + datadocredito + n017 + n010 + n066;
                    _detalhe1 += sequencial;
                    sequencia++;
                    incluiLinha.WriteLine(_detalhe1);
                }

                #endregion

                #region TRAILER
                //     sequencia++;
                string sequencialtrailler = Convert.ToString(sequencia); //Incrementa mais 1 do sequencial
                sequencialtrailler = sequencialtrailler.PadLeft(6, '0'); //Incrementar vindo da APCD
                string n200 = new string(' ', 250);
                _trailer = "9201237" + n090 + n104 + sequencialtrailler;
                incluiLinha.WriteLine(_trailer);
                #endregion

                //    gravaLinha.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar arquivo.", ex);
            }
        }
        private int CalculaDVNossoNumero(string nossoNumero, short peso = 9)
        {
            int S = 0;
            int P = 0;
            int N = 0;
            int d = 0;

            for (int i = 0; i < nossoNumero.Length; i++)
            {
                N = Convert.ToInt32(nossoNumero.Substring(i, 1));

                P = N * peso--;

                S += P;
            }

            int R = S % 11;

            if (R == 0 || R == 1)
                d = 0;

            if (R > 1)
                d = 11 - R;

            return d;
        }

        #endregion Remessa

    }
}