using Hangfire;
using Model.Entity;
using Model.Neg;
using Npgsql;
using Servicos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PlatinDashboard.Presentation.MVC.Sincronizador
{
    public class Sincronizador
    {

        public DateTime PegaHoraBrasilia()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
        }
    }
}