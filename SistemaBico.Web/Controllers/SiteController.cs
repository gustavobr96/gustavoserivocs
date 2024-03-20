using Microsoft.AspNetCore.Mvc;

namespace SistemaBico.Web.Controllers
{
    public class SiteController : Controller
    {
        [Route("site")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
