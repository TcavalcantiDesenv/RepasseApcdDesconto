using PagedList;
using Servicos;
using System.Linq;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class ProdutosOrigemVannonController : Controller
    {
        private readonly ApiService _vannonService;

        public ProdutosOrigemVannonController()
        {
            _vannonService = new ApiService();
        }

        // GET: Produtos
        public ActionResult Index(int? pagina, string busca)
        {
            

            return View();
        }
    }
}