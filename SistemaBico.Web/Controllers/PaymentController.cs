using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaBico.Web.Models;
using SistemaBico.Web.Models.Configuration;
using SistemaBico.Web.Services.Interfaces;
using System.Text;

namespace SistemaBico.Web.Controllers
{
    [Route("[controller]")]
    public class PaymentController : Controller
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IConfiguration _configuration;

        public PaymentController(IAuthenticateService authenticateService, IConfiguration configuration)
        {
            _authenticateService = authenticateService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });


            return View("Index");
        }

        [Route("ExpirationPlan")]
        public async Task<IActionResult> ExpirationPlan()
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });


            return View("ExpirationPlan");
        }

        [HttpPost("ProcessPayment")]
        public async Task<long?> ProcessPayment([FromBody] PaymentRequestDto pay)
        {
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "Payment/Payment";
            using (HttpClient htppClient = new HttpClient())
            {
                var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);
                var command = new 
                {
                    ClientId = Guid.Parse(idClient),
                    Payment = pay.Payment,
                    TypePayment = pay.TypePayment
                };

                var clientToken = await _authenticateService.TokenAuth(HttpContext, htppClient);
                HttpResponseMessage response = clientToken.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<long>(json);

                return result;
            }
          
        }

        [HttpPost("GetPaymentId")]
        public async Task<long?> GetPaymentId()
        {
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "Payment/GetPaymentId";
            using (HttpClient htppClient = new HttpClient())
            {
                var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);

                var clientToken = await _authenticateService.TokenAuth(HttpContext, htppClient);
                HttpResponseMessage response = clientToken.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(new GuidDto { Guid = Guid.Parse(idClient) } ), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<long>(json);

                return result;
            }

        }
    }
}
