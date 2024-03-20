using Microsoft.AspNetCore.Mvc;

namespace SistemaBico.Web.Controllers
{
    public class CapturaProfissionalController : Controller
    {
        [Route("profissional")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
