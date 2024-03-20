using Microsoft.AspNetCore.Mvc;

namespace SistemaBico.Web.Controllers
{
    public class ValidatorController : Controller
    {
        [Route("email")]
        public IActionResult Email()
        {
            return View("Email");
        }

        [Route("requestWorker")]
        public IActionResult RequestWorker()
        {
            return View("RequestWorker");
        }


        [Route("Forgot")]
        public IActionResult Forgot()
        {
            return View("Forgot");
        }
    }
}
