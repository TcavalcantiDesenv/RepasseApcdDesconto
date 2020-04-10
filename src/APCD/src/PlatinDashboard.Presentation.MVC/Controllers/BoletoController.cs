using BoletoNet;
//using BoletoNet.Enums;
using BoletoNet.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// using Boleto.Net.MVC.Models;

namespace PlatinDashboard.Presentation.MVC.Models
{
    public class BoletoController : Controller
    {
        public ActionResult Index()
        {
           // Exemplos exemplos;
            ViewBag.Message = "Boleto.Net.MVC";
            var bancos = from Bancos s in Enum.GetValues(typeof(Bancos))
                         select new
                         {
                             ID = Convert.ChangeType(s, typeof(int)),
                             Name = s.ToString()
                         };
            ViewData["bancos"] = new SelectList(bancos, "ID", "Name", Bancos.Bradesco);
            return View();
        }

        public ActionResult VisualizarBoleto(int Id)
        {
            var boleto = ObterBoletoBancario(Id);
            ViewBag.Boleto = boleto.MontaHtmlEmbedded();
            return View();
        }

        public ActionResult GerarBoletoPDF(int Id)
        {
            var boleto = ObterBoletoBancario(Id);
            var pdf = boleto.MontaBytesPDF();
            return File(pdf, "application/pdf");
        }

        public BoletoBancario ObterBoletoBancario(int Id)
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
                //case Bancos.Bradesco:
                //    return exemplos.Bradesco();
                //case Bancos.BRB:
                //    return exemplos.BRB();
                //case Bancos.Caixa:
                //    return exemplos.Caixa();
                //case Bancos.HSBC:
                //    return exemplos.HSBC();
                //case Bancos.Itau:
                //    return exemplos.Itau();
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

        // GET: Boleto
        // public ActionResult Index()
        // {
        //     BoletoBancario boletoBancario = new BoletoBancario();
        //     DateTime vencimento = DateTime.Now.AddDays(5);

        //     Instrucao_Bradesco item = new Instrucao_Bradesco(9, 5);

        //     Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "1234", "5", "123456", "7");
        //     c.Codigo = "13000";


        //     //Carteiras 
        //     Boleto b = new Boleto(vencimento, 1.01m, "09", "01000000001", c);
        //     b.NumeroDocumento = "01000000001";

        //     b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
        //     b.Sacado.Endereco.End = "Endereço do seu Cliente ";
        //     b.Sacado.Endereco.Bairro = "Bairro";
        //     b.Sacado.Endereco.Cidade = "Cidade";
        //     b.Sacado.Endereco.CEP = "00000000";
        //     b.Sacado.Endereco.UF = "UF";

        //     item.Descricao += " após " + item.QuantidadeDias.ToString() + " dias corridos do vencimento.";
        //     b.Instrucoes.Add(item); //"Não Receber após o vencimento");

        //     Instrucao i = new Instrucao(237);
        //     i.Descricao = "Nova Instrução";
        //     b.Instrucoes.Add(i);

        //     /* 
        //      * A data de vencimento não é usada
        //      * Usado para mostrar no lugar da data de vencimento o termo "Contra Apresentação";
        //      * Usado na carteira 06
        //      */
        //     boletoBancario.MostrarContraApresentacaoNaDataVencimento = true;
        //     Boleto boleto = new Boleto();
        //     boleto = b;
        ////     boleto.Valida();

        ////     boletoBancario.Boleto = b;
        ////     boletoBancario.Boleto.Valida();

        //     boletoBancario.MostrarComprovanteEntrega = (Request.Url.Query == "?show");

        //     return View();
        // }



    }
}