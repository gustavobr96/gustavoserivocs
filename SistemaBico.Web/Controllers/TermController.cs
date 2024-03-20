using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaBico.Web.Models;
using SistemaBico.Web.Models.Configuration;
using SistemaBico.Web.Services.Interfaces;

namespace SistemaBico.Web.Controllers
{
    [Route("[controller]")]
    public class TermController : Controller
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IConfiguration _configuration;

        public TermController(IAuthenticateService authenticateService, IConfiguration configuration)
        {
            _authenticateService = authenticateService;
            _configuration = configuration;
        }

        [Route("TermoUso")]
        public async Task<IActionResult> TermoUso()
        {
            return View("Term");
        }

        [Route("PoliticaPrivacidade")]
        public async Task<IActionResult> PoliticaPrivacidade()
        {
            return View("PoliticaPrivacidade");
        }

        [Route("GetTerm/{id}")]
        [HttpGet]
        public async Task<TermUse> GetId(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                
                var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);
                string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "term/GetTerm/" + id;

                var clientToken = await _authenticateService.TokenAuth(HttpContext, client);
                var response = await clientToken.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<TermUse>(response);
                return result;
            }
        }
    }
}
