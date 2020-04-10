using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class BaixaRetornoController : Controller
    {
        // GET: BaixaRetorno
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Arquivos()
        {
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/Content/Arquivo/"));
            List<ListItem> files = new List<ListItem>();
            foreach (string filePath in filePaths)
            {
                files.Add(new ListItem(Path.GetFileName(filePath), filePath));
            }
           ViewBag.Arquivos = files;


            return View(ViewBag.Arquivos);
        }

        [HttpGet]
        public ActionResult DownloadFile(string arquivo)
        {
            string[] filePath2 = Directory.GetFiles(Server.MapPath("~/Content/Arquivo/"));// (sender as LinkButton).CommandArgument;
            string filePath = "~/Content/Arquivo/" + arquivo;
          //  Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
            return View();

        }
        public ActionResult DeleteFile(string arquivo)
        {
            var bytes = System.IO.File.ReadAllBytes("~/Content/Arquivo/" + arquivo);
            System.IO.File.Delete(arquivo);
            //return File(bytes, contentType, fileDownloadName);
            //// string filePath = (sender as LinkButton).CommandArgument;
            //string filePath = "~/Content/Arquivo/" + arquivo;
            //System.IO.File.Delete(filePath);
            Response.Redirect(Request.Url.AbsoluteUri);
            return View();
        }
    }
}