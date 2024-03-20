using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaBico.Web.Enum;
using SistemaBico.Web.Models;
using SistemaBico.Web.Models.Configuration;
using SistemaBico.Web.Models.Reponse;
using SistemaBico.Web.Services.Interfaces;
using SistemaBico.Web.Util;
using System.Text;

namespace SistemaBico.Web.Controllers
{
    [Route("[controller]")]
    public class ClientController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticateService _authenticateService;
        private readonly ILocalStorageService _localStorageService;
        private readonly IConfiguration _configuration;

        public ClientController(IMapper mapper, IAuthenticateService authenticateService, ILocalStorageService localStorageService, IConfiguration configuration)
        {
            _mapper = mapper;
            _authenticateService = authenticateService;
            _localStorageService = localStorageService;
            _configuration = configuration;
        }

        #region views
        [Route("account")]
        public async Task<IActionResult> Account()
        {
            if(await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });

            var result = await GetClient();
            return View("account", result);
        }

        [Route("changePassword")]
        public async Task<IActionResult> ChangePassword()
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });

            var result = await GetClient();
            return View("ChangePassword", result);
        }


        [Route("GetMyProfessional")]
        public async Task<IActionResult> GetMyProfessional()
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });

            var result = await GetMyProfessionalFunction();
            return View("GetMyProfessional", result);
        }

        [Route("callError")]
        public IActionResult CallError()
        {
            return RedirectToAction("ErrorPage", "Error", new { area = "" });
        }

        #endregion

        #region Call Api

        [HttpPost]
        [Route("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(ClientDto model)
        {
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "client/Update";
            using (HttpClient htppClient = new HttpClient())
            {
                var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);
                var user = _mapper.Map<ApplicationUser>(model);
                user.ClientId = Guid.Parse(idClient);

                var clientToken = await  _authenticateService.TokenAuth(HttpContext, htppClient);
                HttpResponseMessage response = clientToken.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ClientDto>(json);

                HttpContext.Session.SetString("PerfilPicture", result.PerfilPicture);

                if (result != null)
                   return RedirectToAction("Index", "Professional", new { area = "" });

                return RedirectToAction("ErrorPage", "Error", new { area = "" });
            }
        }

        [HttpPost]
        [Route("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(ClientDto model)
        {
            var client = await GetClient();
         
            if (!Validate.ValidarSenha(model.Password))
            {
                ModelState.AddModelError(string.Empty, "Deve conter ao menos 6 caracteres.");
                return View("changePassword", client);
            }

            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "client/UpdatePassword";
            using (HttpClient htppClient = new HttpClient())
            {
                var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);

                var clientToken = await _authenticateService.TokenAuth(HttpContext, htppClient);
                HttpResponseMessage response = clientToken.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(new { ClientId = idClient, Senha = model.Password }), Encoding.UTF8, "application/json")).Result;


                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<TypeStatusCode>(json);

                if(result == TypeStatusCode.Ok)
                    return View("changePassword", client);

                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao atualizar a senha, tente mais tarde.");
                return View("changePassword", client);
            }
        }
        private async Task<ClientDto> GetClient()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);
                    string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "client/GetClientProfileId/" + idClient;

                    var clientToken = await _authenticateService.TokenAuth(HttpContext, client);
                    var response = await clientToken.GetStringAsync(url);
                    var result = JsonConvert.DeserializeObject<ClientDto>(response);
                    return await Task.FromResult(result);
                }
                catch (Exception ex)
                {
                    _ = CallError();
                    return null;
                }
            }
        }


        private async Task<ProfessionalClientPaginationResponse> GetMyProfessionalFunction()
        {

          
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "professionalClient/GetMyProfessionalClient";
            using (HttpClient htppClient = new HttpClient())
            {
                try
                {
                    var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);

                    var clientToken = await  _authenticateService.TokenAuth(HttpContext, htppClient);
                    HttpResponseMessage response = clientToken.PostAsync(url,
                    new StringContent(JsonConvert.SerializeObject(new GuidDto { Guid = Guid.Parse(idClient) } ), Encoding.UTF8, "application/json")).Result;

                    string json = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<List<ProfessionalClientDto>>(json);
                    return await Task.FromResult(new ProfessionalClientPaginationResponse { ProfessionalClient = result });
                }
                catch (Exception ex)
                {
                    _ = CallError();
                    return null;
                }
            }
        }


        [HttpPost]
        [Route("CancelApplyProfessional")]
        public async Task<TypeStatusCode> CancelApplyProfessional([FromBody] string perfil)
        {
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "professionalClient/CancelApplyProfessional";
            using (HttpClient htppClient = new HttpClient())
            {
                var idClient = await  _authenticateService.ObterClientIdLogado(HttpContext);
                var worker = new { ClientId = idClient, Perfil = perfil };

                var clientToken = await _authenticateService.TokenAuth(HttpContext, htppClient);
                HttpResponseMessage response = clientToken.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(worker), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<TypeStatusCode>(json);

                return result;
            }
        }

        [HttpPost]
        [Route("UpdateContratadoProfessional")]
        public async Task<TypeStatusCode> UpdateContratadoProfessional([FromBody] string perfil)
        {
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "professionalClient/UpdateProfessionalClient";
            using (HttpClient htppClient = new HttpClient())
            {
                var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);

                var clientToken = await _authenticateService.TokenAuth(HttpContext, htppClient);
                HttpResponseMessage response = clientToken.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(new { ClientId  = idClient, Perfil  = perfil, StatusWorker  = StatusWorker.AguardandoConfirmacao} ), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<TypeStatusCode>(json);

                return result;
            }

        }
        #endregion
    }
}
