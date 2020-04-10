using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Helpers
{
    public static class ListGeneratorHelper
    {
        public static SelectList GenerateMonths()
        {
            //Método para gerar uma lista com os meses
            return new SelectList(
                new[] {
                    new { Value = "01", Label = "Janeiro" },
                    new { Value = "02", Label = "Fevereiro" },
                    new { Value = "03", Label = "Março" },
                    new { Value = "04", Label = "Abril" },
                    new { Value = "05", Label = "Maio" },
                    new { Value = "06", Label = "Junho" },
                    new { Value = "07", Label = "Julho" },
                    new { Value = "08", Label = "Agosto" },
                    new { Value = "09", Label = "Setembro" },
                    new { Value = "10", Label = "Outubro" },
                    new { Value = "11", Label = "Novembro" },
                    new { Value = "12", Label = "Dezembro" }
                }, "Value", "Label", DateTime.Now.ToString("MM"));
        }

        public static SelectList GenerateDays()
        {
            //Método para gerar uma lista com os meses
            return new SelectList(
                new[] {
                    new { Value = "01", Label = "01" },
                    new { Value = "02", Label = "02" },
                    new { Value = "03", Label = "03" },
                    new { Value = "04", Label = "04" },
                    new { Value = "05", Label = "05" },
                    new { Value = "06", Label = "06" },
                    new { Value = "07", Label = "07" },
                    new { Value = "08", Label = "08" },
                    new { Value = "09", Label = "09" },
                    new { Value = "10", Label = "10" },
                    new { Value = "11", Label = "11" },
                    new { Value = "12", Label = "12" },
                     new { Value = "13", Label = "13" },
                     new { Value = "14", Label = "14" },
                     new { Value = "15", Label = "15" },
                     new { Value = "16", Label = "16" },
                     new { Value = "17", Label = "17" },
                     new { Value = "18", Label = "18" },
                     new { Value = "19", Label = "19" },
                     new { Value = "20", Label = "20" },
                     new { Value = "21", Label = "21" },
                     new { Value = "22", Label = "22" },
                     new { Value = "23", Label = "23" },
                     new { Value = "24", Label = "24" },
                     new { Value = "25", Label = "25" },
                     new { Value = "26", Label = "26" },
                     new { Value = "27", Label = "27" },
                     new { Value = "28", Label = "28" },
                     new { Value = "29", Label = "29" },
                     new { Value = "30", Label = "30" },
                     new { Value = "31", Label = "31" },
                }, "Value", "Label", DateTime.Now.ToString("dd"));
        }

        public static SelectList GenerateYears()
        {
            //Método para gerar uma lista com os anos
            List<SelectListItem> listItens = new List<SelectListItem>();
            for (int i = DateTime.Now.Year; i >= 2016; i--)
            {
                listItens.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }
            return new SelectList(listItens, "Value", "Text", DateTime.Now.Year.ToString());
        }

        public static SelectList GenerateProviders()
        {
            //Método para gerar uma lista com os providers de banco de dados
            return new SelectList(
                new[] {
                    new { Value = "CONTROLADA", Label = "CONTROLADA" },
                    new { Value = "NÃO CONTROLADA", Label = "NÃO CONTROLADA" }
                }, "Value", "Label");
        }
    }
}