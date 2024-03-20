using Microsoft.AspNetCore.Mvc;

namespace SistemaBico.Web.Controllers
{

    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
