using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaBico.Web.Models.Configuration;
using SistemaBico.Web.Models.Filters;
using SistemaBico.Web.Models.Reponse;
using SistemaBico.Web.Services.Interfaces;
using System.Text;

namespace SistemaBico.Web.Controllers
{
    [Route("[controller]")]
    public class ProfileController : Controller
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IConfiguration _configuration;

        public ProfileController(IAuthenticateService authenticateService, IConfiguration configuration)
        {
            _authenticateService = authenticateService;
            _configuration = configuration;
        }

        [Route("id/{perfil}")]
        public async Task<IActionResult> Index(string perfil)
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });

            var filter = new FilterPaginatedProfileModel();
            filter.Profile = perfil;

            var result = await GetProfessionalPerfil(filter);

            var pagesSize = Math.Ceiling((decimal)result.CountRegister / filter.Take);
            result.PagesSize = (int)pagesSize;
            return View("index", result);
        }

        [HttpPost("ProfilePage")]
        public async Task<IActionResult> ProfilePage(FilterPaginatedProfileModel filter)
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });

            var result = await GetProfessionalPerfil(filter);

            result.Page = filter.Page;

            var pagesSize = Math.Ceiling((decimal)result.CountRegister / filter.Take);
            result.PagesSize = (int)pagesSize;
            return View("index", result);
        }


        private async Task<ProfileWorkerProfessionalPaginationResponse> GetProfessionalPerfil(FilterPaginatedProfileModel filter)
        {
          
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "professional/GetProfessionalPerfil";
            using (HttpClient htppClient = new HttpClient())
            {
                var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);

                var clientToken = await _authenticateService.TokenAuth(HttpContext, htppClient);
                HttpResponseMessage response = clientToken.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(filter), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ProfileWorkerProfessionalPaginationResponse>(json);
                result.SetReputation();

                return result;
            }

        }
    }
}
