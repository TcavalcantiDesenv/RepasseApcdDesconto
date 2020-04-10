using Model.Entity;
using Model.Neg;
using OfficeOpenXml;
using PlatinDashboard.Presentation.MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class ImportaRegionaisController : Controller
    {
        RemessaNeg objRemessaNeg;

        public ImportaRegionaisController()
        {

            objRemessaNeg = new RemessaNeg();
        }

        // GET: ImportaRegionais
        public ActionResult Index()
        {
            //RemessaModel remessa = new RemessaModel();
            //remessa.ListaRemessa = objRemessaNeg.BuscaPorRegional("235").ToList<RemessaWeb>();
            //return View(remessa);
            return View(new List<RemessaWeb>());
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            string sequencial = "";
            string matricula = "";
            string nome = "";
            string codcategoria = "";
            string nomcategoria = "";
            string obs = "";
            string lancamento = "";
            string vencimento = "";
            string lcm = "";
            string valor = "";
            string valorrepasse = "";
            string documento = "";
            string docgerado = "";
            string codregional = "";
            string nomeregional = "";
            string dataregistro = "";
            string empresa = "";
            string conta = "";
            string pn = "";
            string pular = "";

            List<RemessaWeb> repasse2 = new List<RemessaWeb>();
            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);


                if (path == null || path.Length == 0)
                    return Content("File Not Selected");

                string fileExtension = Path.GetExtension(postedFile.FileName);


                string docPath2 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                TextWriter tw = new StreamWriter(Path.Combine(docPath2, "Remessa.txt"));
                FileInfo existingFile = new FileInfo(filePath);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row;     //get row count
                    for (int row = 1; row <= rowCount; row++)
                    {
                        for (int col = 1; col <= colCount; col++)
                        {
                            Console.WriteLine(" Row:" + row + " column:" + col + " Value:" + worksheet.Cells[row, col].Value?.ToString().Trim());


                            if (col == 1) sequencial = worksheet.Cells[row, col].Value?.ToString().Trim();
                            if (col == 2) codregional = worksheet.Cells[row, col].Value?.ToString().Trim();
                            if (col == 3) nomeregional = worksheet.Cells[row, col].Value?.ToString().Trim();
                            if (col == 4) lcm = worksheet.Cells[row, col].Value?.ToString().Trim();
                            if (col == 5) matricula = worksheet.Cells[row, col].Value?.ToString().Trim();
                            if (col == 6) nome = worksheet.Cells[row, col].Value?.ToString().Trim();
                            if (col == 7) codcategoria = worksheet.Cells[row, col].Value?.ToString().Trim();
                            if (col == 8) nomcategoria = worksheet.Cells[row, col].Value?.ToString().Trim();
                            if (col == 9) pn = worksheet.Cells[row, col].Value?.ToString().Trim();
                            if (col == 10) lancamento = worksheet.Cells[row, col].Value?.ToString().Trim();
                            if (col == 11) vencimento = worksheet.Cells[row, col].Value?.ToString().Trim();
                            if (col == 12) valor = worksheet.Cells[row, col].Value?.ToString().Trim();
                            if (col == 13) obs = worksheet.Cells[row, col].Value?.ToString().Trim();
                            if (col == 14) empresa = worksheet.Cells[row, col].Value?.ToString().Trim();
                            if (col == 15) conta = worksheet.Cells[row, col].Value?.ToString().Trim();

                            if (sequencial == "#")
                            {
                                pular = sequencial;
                     //           sequencial = "000001";
                                codregional = codregional.PadLeft(3, '0');
                                nomeregional = nomeregional.PadRight(25, ' ');
                                dataregistro = DateTime.Now.ToString().Substring(0, 2) + DateTime.Now.ToString().Substring(3, 2) + DateTime.Now.ToString().Substring(6, 4);
                                dataregistro = dataregistro.PadRight(68, ' ');
                                docgerado = "H" + codregional + nomeregional + dataregistro + sequencial;// 055APCD JUNDIAI             04122019                                                            000001";
                                tw.WriteLine(docgerado);
                            }

                            documento = "LC";
                            valorrepasse = "000000000";

                            docgerado = "D" + matricula + nome + codcategoria + nomcategoria + vencimento + documento + lcm + valor + valorrepasse + sequencial;
                            var repasse = new RemessaWeb();

                            if (sequencial != "#")
                            {
                                if (col == 15)
                                {
                                    if (pular != "#")
                                    {
                                        tw.WriteLine(docgerado);
                                        repasse2.Add(new RemessaWeb
                                        {

                                            CodRegional = codregional,
                                            NomeRegional = nomeregional,
                                            PN = pn,
                                            Matricula = matricula,
                                            Nome = nome,
                                            Categoria = codcategoria + "" + nomcategoria,
                                            Vencimento = vencimento.Substring(0, 10),
                                            LCM = lcm,
                                            sequencia = sequencial,
                                            Valor = valor,
                                            Codigo = "0",
                                            Documento = "",
                                            indice = Convert.ToInt32(sequencial),
                                            Pago = false,
                                            ValorRepasse = "0",
                                            Conta = conta,
                                            Empresa = empresa
                                        });

                                        repasse.CodRegional = codregional;
                                        repasse.NomeRegional = nomeregional;
                                        repasse.PN = pn;
                                        repasse.Matricula = matricula;
                                        repasse.Nome = nome;
                                        repasse.Categoria = codcategoria + "" + nomcategoria;
                                        repasse.Vencimento = vencimento.Substring(0, 10);
                                        repasse.LCM = lcm;
                                        repasse.sequencia = sequencial;
                                        repasse.Valor = valor;
                                        repasse.Codigo = "0";
                                        repasse.Documento = "";
                                        repasse.indice = Convert.ToInt32(sequencial);
                                        repasse.Pago = false;
                                        repasse.ValorRepasse = "0";
                                        repasse.MesAno = DateTime.Now.ToString("MMyyyy");
                                        repasse.Ativo = "true";
                                        repasse.Empresa = empresa;
                                        repasse.Conta = conta;
                                        objRemessaNeg.create(repasse);
                                    }
                                    ////if (lcm == "503661")
                                    ////{
                                    //    pular = "";
                                    //}
                                    pular = "";
                                }
                            }
                        //}
                        }
                    }
                }
                nomeregional = "";
                nomeregional = nomeregional.PadRight(87, ' ');
                docgerado = "T" + nomeregional + "000000000" + sequencial;// 055APCD JUNDIAI             04122019                                                            000001";
                tw.WriteLine(docgerado);
                tw.Close();
                return RedirectToAction("Index", "Remessa");
            }


            return View(repasse2);
        }

        //[HttpPost]
        //public ActionResult Index(ImportExcel importExcel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string path = Server.MapPath("~/Content/Upload/" + importExcel.file.FileName);
        //        importExcel.file.SaveAs(path);

        //        string excelConnectionString = @"Provider='Microsoft.ACE.OLEDB.12.0';Data Source='" + path + "';Extended Properties='Excel 12.0 Xml;IMEX=1'";
        //        OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);

        //        //Sheet Name
        //        excelConnection.Open();
        //        string tableName = excelConnection.GetSchema("Tables").Rows[0]["TABLE_NAME"].ToString();
        //        excelConnection.Close();
        //        //End

        //        OleDbCommand cmd = new OleDbCommand("Select * from [" + tableName + "]", excelConnection);

        //        excelConnection.Open();

        //        OleDbDataReader dReader;
        //        dReader = cmd.ExecuteReader();
        //        SqlBulkCopy sqlBulk = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);

        //        //Give your Destination table name
        //        sqlBulk.DestinationTableName = "sale";

        //        //Mappings
        //        sqlBulk.ColumnMappings.Add("Date", "AddedOn");
        //        sqlBulk.ColumnMappings.Add("Region", "Region");
        //        sqlBulk.ColumnMappings.Add("Person", "Person");
        //        sqlBulk.ColumnMappings.Add("Item", "Item");
        //        sqlBulk.ColumnMappings.Add("Units", "Units");
        //        sqlBulk.ColumnMappings.Add("Unit Cost", "UnitCost");
        //        sqlBulk.ColumnMappings.Add("Total", "Total");

        //        sqlBulk.WriteToServer(dReader);
        //        excelConnection.Close();

        //        ViewBag.Result = "Successfully Imported";
        //    }
        //    return View();
        //}
    }
}