using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PlatinDashboard.Presentation.MVC.Models
{
    public class UploadFileResult
    {
        public int IDArquivo { get; set; }
        public string Nome { get; set; }
        public int Tamanho { get; set; }
        public string Tipo { get; set; }
        public string Caminho { get; set; }

        public List<UploadFileResult> ListaArquivos(string path)
        {
            List<UploadFileResult> lstArquivos = new List<UploadFileResult>();
            DirectoryInfo dirInfo = new DirectoryInfo(path);

            int i = 0;
            foreach (var item in dirInfo.GetFiles())
            {
                lstArquivos.Add(new UploadFileResult()
                {
                    IDArquivo = i + 1,
                    Nome = item.Name,
                    Caminho = dirInfo.FullName + @"\" + item.Name
                });
                i = i + 1;
            }
            return lstArquivos;
        }
    }
}