using Model.Entity;
using Model.Neg;
using PagedList;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    [Authorize]
    public class AgendamentosController : Controller
    {
        // GET: Clientes

    public AgendamentosController()
    {

    }
    public ActionResult Index(string busca = "", int pagina = 1, int tamanhoPagina = 10)
    {

        return View();
    }


}
}