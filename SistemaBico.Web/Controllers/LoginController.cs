using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaBico.Web.Models;
using SistemaBico.Web.Models.Configuration;
using SistemaBico.Web.Models.Reponse;
using SistemaBico.Web.Services.Interfaces;
using SistemaBico.Web.Util;
using System.Text;

namespace SistemaBico.Web.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly ILocalStorageService _localStorageService;
        private readonly IConfiguration _configuration;

        public LoginController(IAuthenticateService authenticateService, 
            ILocalStorageService localStorageService,
            IConfiguration configuration)
        {
            _authenticateService = authenticateService;
            _localStorageService = localStorageService;
            _configuration = configuration;
        }

        #region views

        [Route("login")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [Route("profissional")]
        public IActionResult Profissional()
        {
            return View("Professional");
        }


        [Route("contratante")]
        public IActionResult Contratante()
        {
            return View("Contratante");
        }

        [Route("register")]
        public IActionResult Register()
        {
            return View("Register");
        }

        #endregion

        #region Call API
        public async Task<IActionResult> Logoff()
        {
            await _authenticateService.Logoff(HttpContext);
            _localStorageService.Logout();
            return RedirectToAction("Index", "Login", new { area = "" });
        }

        [HttpPost, AllowAnonymous]
        [Route("login-profissional")]
        public async Task<IActionResult> LoginProfissional(Client model)
        {
            if (model.ValidateLogin())
            {
                ModelState.AddModelError(string.Empty, "Preencha o e-mail e a senha!");
                return View("index", model);
            }

            var user = new User
            {
                Email = model.Email,
                Password = model.Password
            };

            var loginResponse = await AutenticarLogin(user);

            if (loginResponse.Token == null)
            {
                ModelState.AddModelError(string.Empty, "Usuario ou senha invalido(s)");
                return View("index", model);
            }


            await _authenticateService.Login(HttpContext, loginResponse, true);

            var profissional = await GetProfissional();
            if (profissional != null && !profissional.Ativo)
                return RedirectToAction("ExpirationPlan", "Payment", new { area = "" });

            return RedirectToAction("Index", "Worker", new { area = "" });

        }

        [HttpPost, AllowAnonymous]
        [Route("login-contratante")]
        public async Task<IActionResult> LoginContratante(Client model)
        {
            if (model.ValidateLogin())
            {
                ModelState.AddModelError(string.Empty, "Preencha o e-mail e a senha!");
                return View("index", model);
            }

            var user = new User
            {
                Email = model.Email,
                Password = model.Password
            };

            var loginResponse = await AutenticarLogin(user);

            if (loginResponse.Token == null)
            {
                ModelState.AddModelError(string.Empty, "Usuario ou senha invalido(s)");
                return View("index", model);
            }


            await _authenticateService.Login(HttpContext, loginResponse, false);
            return RedirectToAction("Index", "Professional", new { area = "" });
        }

        private async Task<LoginResponse> AutenticarLogin(User user)
        {
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "token/CreateToken";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(json);
                return await Task.FromResult(loginResponse);
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
                    return null;
                }
            }
        }

        [HttpPost, AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Post(Client model)
        {
            if (model.ValidateRegister())
            {
                ModelState.AddModelError(string.Empty, "Preencha os dados obrigatórios!");
                return View("index", model);
            }
            if (!Validate.IsCpf(model.CpfCnpj))
            {
                ModelState.AddModelError(string.Empty, "O CPF não está válido!");
                return View("index", model);
            }

            if (!Validate.ValidarSenha(model.Password))
            {
                ModelState.AddModelError(string.Empty, "Deve conter ao menos 6 caracteres.");
                return View("index", model);
            }
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "client/Register";
            using (HttpClient htppClient = new HttpClient())
            {
                HttpResponseMessage response = htppClient.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                if(json == "2")
                {
                    ModelState.AddModelError(string.Empty, "E-mail ou CPF já cadastrado!");
                    return View("index", model);
                }
                else
                {
                    var responseLogin = await AutenticarLogin(new User { Email = model.Email, Password = model.Password });
                    if (responseLogin != null && responseLogin.Token != null)
                    {
                        await _authenticateService.Login(HttpContext, responseLogin);
                        return RedirectToAction("Index", "Professional", new { area = "" });
                    }
                    else
                    {
                        return View("index", model);
                    }
                }   
            }
        }

        [HttpPost, AllowAnonymous]
        [Route("Forgot")]
        public async Task<IActionResult> Forgot(Client model)
        {
            if (!Validate.IsCpf(model.CpfCnpj))
            {
                ModelState.AddModelError(string.Empty, "O CPF não está válido!");
                return View("index", model);
            }

            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "client/Forgot";
            using (HttpClient htppClient = new HttpClient())
            {
                HttpResponseMessage response = htppClient.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;

                return RedirectToAction("Forgot", "Validator", new { area = "" });
            }
        }

        #endregion
    }
}
