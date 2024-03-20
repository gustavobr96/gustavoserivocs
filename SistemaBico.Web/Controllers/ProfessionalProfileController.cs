using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaBico.Web.Enum;
using SistemaBico.Web.Models;
using SistemaBico.Web.Models.Configuration;
using SistemaBico.Web.Services.Interfaces;
using SistemaBico.Web.Util;
using System.Text;

namespace SistemaBico.Web.Controllers
{
    [Route("[controller]")]
    public class ProfessionalProfileController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticateService _authenticateService;
        private readonly IProfessionalClientService _professionalClientService;
        private readonly IConfiguration _configuration;

        public ProfessionalProfileController(IMapper mapper, IAuthenticateService authenticateService, IProfessionalClientService professionalClientService, IConfiguration configuration)
        {
            _mapper = mapper;
            _authenticateService = authenticateService;
            _professionalClientService = professionalClientService;
            _configuration = configuration;
        }


        #region views


        [Route("CreateProfessionalProfile")]
        public async Task<IActionResult> CreateProfessionalProfile()
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });

            return View("CreateProfessionalProfile");
        }


        [Route("Plan")]
        public async Task<IActionResult> Plan()
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });

            var result = await GetProfissional();
            return result == null ? View("CreateProfessionalProfile") : View("Plan", result);
        }

        [Route("Address")]
        public async Task<IActionResult> Address()
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });

            var result = await GetProfissional();
            return result == null ? View("CreateProfessionalProfile") : View("Address", result);
        }

        [Route("account")]
        public async Task<IActionResult> Account()
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });


            var clientApproval = await _professionalClientService.GetClientApproval(HttpContext);
            if (clientApproval.ProfessionalClient.Any())
                return RedirectToAction("ClientApproval", "Professional", new { area = "" });

            var result = await GetProfissional();
            return result == null ? View("CreateProfessionalProfile") : View("Account", result);
        }


        [Route("callError")]
        public IActionResult CallError()
        {
            return RedirectToAction("ErrorPage", "Error", new { area = "" });
        }

        #endregion

        #region Call API
        [HttpGet("GetProfessional")]
        public async Task<ProfessionalProfileDto> GetProfessional()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);
                    string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "professional/GetProfessionalProfileId/" + idClient;

                    var clientToken = await _authenticateService.TokenAuth(HttpContext, client);
                    var response = await clientToken.GetStringAsync(url);
                    var result = JsonConvert.DeserializeObject<ProfessionalProfileDto>(response);
                    return result;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        [HttpGet("GetProfissionalEspeciality")]
        public async Task<ProfessionalProfileDto> GetProfissionalEspeciality()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                   
                    var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);
                    string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "professional/GetProfessionalProfileId/" + idClient;

                    var clientToken = await _authenticateService.TokenAuth(HttpContext, client);
                    var response = await clientToken.GetStringAsync(url);
                    var result = JsonConvert.DeserializeObject<ProfessionalProfileDto>(response);
                    result.Especiality = ListGeneric.GetProfessionalDynamicEspeciality(result.Especiality);
                    return result;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Post(ProfessionalProfileDto model)
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });

          
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "professional/Register";
            using (HttpClient htppClient = new HttpClient())
            {
                var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);

                var professionalProfile = _mapper.Map<ProfessionalProfile>(model);
                professionalProfile.ClientId = Guid.Parse(idClient);
                professionalProfile.AddressId = professionalProfile.Address.Id;

                var clientToken = await _authenticateService.TokenAuth(HttpContext, htppClient);
                HttpResponseMessage response = clientToken.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(professionalProfile), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<TypeStatusCode>(json);

                if (result == TypeStatusCode.Ok)
                    return RedirectToAction("Index", "Professional", new { area = "" });

                return RedirectToAction("ErrorPage", "Error", new { area = "" });
            }

        }

        [HttpPost]
        [Route("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(ProfessionalProfileDto model)
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });

            var result = await UpdateProfessional(model);

            if (result != null)
                return RedirectToAction("Index", "Professional", new { area = "" });

            return RedirectToAction("ErrorPage", "Error", new { area = "" });
        }


        [HttpPost]
        [Route("UpdateAddress")]
        public async Task<IActionResult> UpdateAddress(ProfessionalProfileDto model)
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });

            var result = await UpdateProfessional(model);

            if (result != null)
                return View("Address", result);

            return RedirectToAction("ErrorPage", "Error", new { area = "" });
        }


        private async Task<ProfessionalProfileDto> UpdateProfessional(ProfessionalProfileDto model)
        {
          
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "professional/Update";
            using (HttpClient htppClient = new HttpClient())
            {
                var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);
                var professionalProfile = _mapper.Map<ProfessionalProfile>(model);
                professionalProfile.ClientId = Guid.Parse(idClient);

                var clientToken = await  _authenticateService.TokenAuth(HttpContext, htppClient);
                HttpResponseMessage response = clientToken.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(professionalProfile), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ProfessionalProfileDto>(json);

                return await Task.FromResult(result);
            }
        }

        private async Task<ProfessionalProfileDto> GetProfissional()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
              
                    var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);
                    string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "professional/GetProfessionalProfileId/" + idClient;

                    var clientToken = await _authenticateService.TokenAuth(HttpContext, client);
                    var response = await clientToken.GetStringAsync(url);
                    var result = JsonConvert.DeserializeObject<ProfessionalProfileDto>(response);
                    return await Task.FromResult(result);
                }
                catch (Exception ex)
                {
                    _ = CallError();
                    return null;
                }
            }
        }

        #endregion
    }
}
