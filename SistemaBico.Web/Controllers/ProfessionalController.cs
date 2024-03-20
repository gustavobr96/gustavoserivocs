using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaBico.Web.Models;
using SistemaBico.Web.Models.Configuration;
using SistemaBico.Web.Models.Filters;
using SistemaBico.Web.Models.Reponse;
using SistemaBico.Web.Services;
using SistemaBico.Web.Services.Interfaces;
using SistemaBico.Web.Util;
using System.Text;

namespace SistemaBico.Web.Controllers
{
    [Route("[controller]")]
    public class ProfessionalController : Controller
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IProfessionalClientService _professionalClientService;
        private readonly IConfiguration _configuration;

        public ProfessionalController(IAuthenticateService authenticateService, IProfessionalClientService professionalClientService, IConfiguration configuration)
        {
            _authenticateService = authenticateService;
            _professionalClientService = professionalClientService;
            _configuration = configuration;
        }

        #region views
        public async Task<IActionResult> Index()
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });

            var clientApproval = await _professionalClientService.GetClientApproval(HttpContext);
            if (clientApproval.ProfessionalClient.Any())
                return RedirectToAction("ClientApproval", "Professional", new { area = "" });

            var filter = new FilterPaginatedProfessionalModel();
            var result = await GetProfessionalProfilePaginated(filter);

            var pagesSize = Math.Ceiling((decimal)result.CountRegister/filter.Take);
            result.PagesSize = (int)pagesSize;
            return View("index", result);
        }

        #endregion

        #region Call API

        [HttpPost("ProfessionalPage")]
        public async Task<IActionResult> ProfessionalPage(FilterPaginatedProfessionalModel filter)
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });

            var result = await GetProfessionalProfilePaginated(filter);
            
            if(filter.Especiality != null)
             result.Especiality = ListGeneric.GetProfessionalDynamicEspeciality(filter.Especiality);
            
            result.Page = filter.Page;
            result.City = filter.City;
            result.Area = filter.Area;
            result.Profession = filter.Profession;
            
            var pagesSize = Math.Ceiling((decimal)result.CountRegister / filter.Take);
            result.PagesSize = (int)pagesSize;
            return View("index", result);
        }


        [Route("ClientApproval")]
        public async Task<IActionResult> ClientApproval()
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });

            var clientApproval = await _professionalClientService.GetClientApproval(HttpContext);
            if(!clientApproval.ProfessionalClient.Any())
                return RedirectToAction("Index", "Worker", new { area = "" });

            var pagesSize = Math.Ceiling((decimal)clientApproval.CountRegister / clientApproval.Take);
            clientApproval.PagesSize = (int)pagesSize;
            return View("ClientApproval", clientApproval);
        }

        [HttpPost]
        [Route("ClientApprovalOrRecused")]
        public async Task<IActionResult> ClientApprovalOrRecused([FromBody] ApprovalOrRecusedDto dto)
        {
         
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "professionalClient/ClientApprovalOrRecused";
            using (HttpClient htppClient = new HttpClient())
            {
                var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);

                var clientToken = await _authenticateService.TokenAuth(HttpContext, htppClient);
                HttpResponseMessage response = clientToken.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json")).Result;

                return Ok();
            }
        }

        [HttpPost]
        [Route("ProfessionalProfilePaginated")]
        public async Task<ProfessionalProfilePaginationResponse> ProfessionalProfilePaginated([FromBody] FilterPaginatedProfessionalModel model)
        {
            return await GetProfessionalProfilePaginated(model);
        }

        private async Task<ProfessionalProfilePaginationResponse> GetProfessionalProfilePaginated(FilterPaginatedProfessionalModel model)
        {
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "professional/GetProfessionalPaginated";
            using (HttpClient htppClient = new HttpClient())
            {
                var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);
                model.ClientId = Guid.Parse(idClient);
                model.RemoveSpacing();

                var clientToken = await _authenticateService.TokenAuth(HttpContext, htppClient);
                HttpResponseMessage response = clientToken.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ProfessionalProfilePaginationResponse>(json);

                return result;
            }
        }

        [HttpPost]
        [Route("GetInterested")]
        public async Task<ProfessionalProfilePaginationResponse> GetInterested([FromBody] GuidDto model)
        {
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "professional/GetInterested";
            using (HttpClient htppClient = new HttpClient())
            {
                var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);

                var clientToken = await _authenticateService.TokenAuth(HttpContext, htppClient);
                HttpResponseMessage response = clientToken.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ProfessionalProfilePaginationResponse>(json);

                return result;
            }
        }

        [HttpPost]
        [Route("ApplyProfessional")]
        public async Task<IActionResult> ApplyProfessional([FromBody] string perfil)
        {
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "professionalClient/ApplyProfessional";
            using (HttpClient htppClient = new HttpClient())
            {
                var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);
                var worker = new { ClientId = idClient, Perfil = perfil };

                var clientToken = await _authenticateService.TokenAuth(HttpContext, htppClient);
                HttpResponseMessage response = clientToken.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(worker), Encoding.UTF8, "application/json")).Result;

                return Ok();
            }
        }


        #endregion

    }
}
