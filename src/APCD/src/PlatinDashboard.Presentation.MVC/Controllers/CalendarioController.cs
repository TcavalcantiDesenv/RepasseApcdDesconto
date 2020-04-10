using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    [Authorize]
    public class CalendarioController : Controller
    {
        private DB_A1938F_CavaleirosEntities1 db = new DB_A1938F_CavaleirosEntities1();

        public ActionResult Index()
        {
            var scheduler = new DHXScheduler(this);
            scheduler.Skin = DHXScheduler.Skins.Flat;
            scheduler.InitialView = "month";
            scheduler.Config.api_date = "%m/%d/%Y &H:%i:%s";

            scheduler.Config.first_hour = 6;
            scheduler.Config.last_hour = 22;

            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;

            return View(scheduler);
        }


        public ContentResult Data()
        {


            var apps = db.Appointment.ToList();
            return new SchedulerAjaxData(apps);
        }

        public ActionResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
            string tipo = Session["TipoUser"].ToString();
            if (tipo != "Comum")
            {


                try
                {
                    var changedEvent = DHXEventsHelper.Bind<Appointment>(actionValues);
                    switch (action.Type)
                    {
                        case DataActionTypes.Insert:
                            db.Appointment.Add(changedEvent);
                            break;
                        case DataActionTypes.Delete:
                            db.Entry(changedEvent).State = EntityState.Deleted;
                            break;
                        default:// "update"  
                            db.Entry(changedEvent).State = EntityState.Modified;
                            break;
                    }
                    db.SaveChanges();
                    action.TargetId = changedEvent.Id;
                }
                catch (Exception a)
                {
                    action.Type = DataActionTypes.Error;
                }
            }
            return (new AjaxSaveResponse(action));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //public ActionResult Calendario()
        //{
        //    return View();
        //}
        //public JsonResult GetEvents()
        //{
        //    var events = objEventosNeg.findAll().ToList();
        //    return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        //[HttpPost]
        //public JsonResult SaveEvent(Eventos e)
        //{
        //    var status = false;
        //    //using (MyDatabaseEntities dc = new MyDatabaseEntities())
        //    //{
        //    if (e.Subject !="")
        //    {
        //        //Update the event
        //        var v = objEventosNeg.findAll().Where(a => a.Id_Evento == e.Id_Evento).FirstOrDefault();
        //        if (v != null)
        //        {
        //            v.Subject = e.Subject;
        //            v.Start = e.Start;
        //            v.Fim = e.Fim;
        //            v.Description = e.Description;
        //            v.IsFullDay = e.IsFullDay;
        //            v.ThemeColor = e.ThemeColor;

        //            objEventosNeg.update(e);
        //        }
        //    }
        //    else
        //    {
        //        objEventosNeg.create(e);
        //    }
        //    //dc.SaveChanges();
        //    status = true;
        //    //}
        //    return new JsonResult { Data = new { status = status } };
        //}

        //[HttpPost]
        //public JsonResult DeleteEvent(int eventID)
        //{
        //    var status = false;
        //    //using (MyDatabaseEntities dc = new MyDatabaseEntities())
        //    //{
        //    var v = objEventosNeg.findAll().Where(a => a.Id_Evento == eventID).FirstOrDefault();
        //    if (v != null)
        //    {
        //        objEventosNeg.delete(v);
        //        //dc.SaveChanges();
        //        status = true;
        //    }
        //    //}
        //    return new JsonResult { Data = new { status = status } };
        //}
    }
}