using Microsoft.AspNetCore.Mvc;

namespace SistemaBico.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("ErrorPage")]
        public IActionResult ErrorPage()
        {
            return View("ErrorPage");
        }
    }
}
