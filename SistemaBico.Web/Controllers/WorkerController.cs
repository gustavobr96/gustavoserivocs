using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaBico.Web.Enum;
using SistemaBico.Web.Models;
using SistemaBico.Web.Models.Configuration;
using SistemaBico.Web.Models.Filters;
using SistemaBico.Web.Models.Reponse;
using SistemaBico.Web.Services;
using SistemaBico.Web.Services.Interfaces;
using SistemaBico.Web.Util;
using System.Globalization;
using System.Text;

namespace SistemaBico.Web.Controllers
{
    [Route("[controller]")]
    public class WorkerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticateService _authenticateService;
        private readonly IProfessionalClientService _professionalClientService;
        private readonly IConfiguration _configuration;

        public WorkerController(IMapper mapper, 
            IAuthenticateService authenticateService,
            IProfessionalClientService professionalClientService,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _authenticateService = authenticateService;
            _professionalClientService = professionalClientService;
            _configuration = configuration;
        }

        [Route("RequestWorker")]
        public async Task<IActionResult> RequestWorker()
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });

            return View("RequestWorker");
        }

        [Route("callError")]
        public IActionResult CallError()
        {
            return RedirectToAction("ErrorPage", "Error", new { area = "" });
        }

        [HttpPost("WorkerPage")]
        public async Task<IActionResult> WorkerPage(FilterPaginatedWorkerModel filter)
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });

            var result = await GetWorkerPaginated(filter);

            var profissional = await GetProfissional();
            if (profissional == null)
                return RedirectToAction("CreateProfessionalProfile", "ProfessionalProfile", new { area = "" });

            if (!profissional.Ativo)
                return RedirectToAction("ExpirationPlan", "Payment", new { area = "" });

            var clientApproval = await _professionalClientService.GetClientApproval(HttpContext);
            if(clientApproval.ProfessionalClient.Any())
                return RedirectToAction("ClientApproval", "Professional", new { area = "" });

            result.Page = filter.Page;
            result.City = filter.City;
            result.Area = filter.Area;

            var pagesSize = Math.Ceiling((decimal)result.CountRegister / filter.Take);
            result.PagesSize = (int)pagesSize;
            return View("index", result);
        }

        #region MyPublishWorkers
        
        [Route("MyPublishedWorkers")]
        public async Task<IActionResult> MyPublishedWorkers()
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });

            var filter = new FilterPaginatedWorkerModel();
            var result = await GetMyPublishWorkerClient(filter);

            var pagesSize = Math.Ceiling((decimal)result.CountRegister / filter.Take);
            result.PagesSize = (int)pagesSize;
            return View("MyPublishedWorkers", result);
        }

        // Paginated
        [HttpPost]
        [Route("MyPublishedWorkersPaginated")]
        public async Task<IActionResult> MyPublishedWorkersPaginated(FilterPaginatedWorkerModel filter)
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });

            var result = await GetMyPublishWorkerClient(filter);

            result.Page = filter.Page;
            result.City = filter.City;
            result.Area = filter.Area;

            var pagesSize = Math.Ceiling((decimal)result.CountRegister / filter.Take);
            result.PagesSize = (int)pagesSize;
            return View("MyPublishedWorkers", result);
        }


        #endregion



        #region MyWorkers

        [Route("RequestMyWorkers")]
        public async Task<IActionResult> RequestMyWorkers()
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });

            var profissional = await GetProfissional();
            if (profissional == null)
                return RedirectToAction("CreateProfessionalProfile", "ProfessionalProfile", new { area = "" });

            if (!profissional.Ativo)
                return RedirectToAction("ExpirationPlan", "Payment", new { area = "" });

            var clientApproval = await _professionalClientService.GetClientApproval(HttpContext);
            if (clientApproval.ProfessionalClient.Any())
                return RedirectToAction("ClientApproval", "Professional", new { area = "" });

            var filter = new FilterPaginatedWorkerModel();
            var result = await GetMyWorkersPaginated(filter);

            var pagesSize = Math.Ceiling((decimal)result.CountRegister / filter.Take);
            result.PagesSize = (int)pagesSize;
            return View("MyWorkers", result);
        }

        // Paginated
        [HttpPost]
        [Route("MyWorkersPaginated")]
        public async Task<IActionResult> MyWorkersPaginated(FilterPaginatedWorkerModel filter)
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });

            var result = await GetMyWorkersPaginated(filter);

            result.Page = filter.Page;
            result.City = filter.City;
            result.Area = filter.Area;

            var pagesSize = Math.Ceiling((decimal)result.CountRegister / filter.Take);
            result.PagesSize = (int)pagesSize;
            return View("MyWorkers", result);
        }

        #endregion


        public async Task<IActionResult> Index()
        {
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });


            var profissional = await GetProfissional();
            if (profissional == null)
                return RedirectToAction("CreateProfessionalProfile", "ProfessionalProfile", new { area = "" });

            if (!profissional.Ativo)
                return RedirectToAction("ExpirationPlan", "Payment", new { area = "" });

            var clientApproval = await _professionalClientService.GetClientApproval(HttpContext);
            if (clientApproval != null && clientApproval.ProfessionalClient.Any())
            {
                return RedirectToAction("ClientApproval", "Professional");             
            }

            var filter = new FilterPaginatedWorkerModel();
            var result = await GetWorkerPaginated(filter);

            var pagesSize = Math.Ceiling((decimal)result.CountRegister / filter.Take);
            result.PagesSize = (int)pagesSize;
            return View("index", result);
        }


        [HttpPost]
        [Route("WorkerPaginated")]
        public async Task<WorkerPaginationResponse> WorkerPaginated([FromBody] FilterPaginatedWorkerModel model)
        {
            return await GetWorkerPaginated(model);
        }

        private async Task<WorkerPaginationResponse> GetWorkerPaginated(FilterPaginatedWorkerModel model)
        {
           
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "worker/GetWorkerPaginated";
            using (HttpClient htppClient = new HttpClient())
            {
                var idClient =await  _authenticateService.ObterClientIdLogado(HttpContext);
                
                model.RemoveSpacing();
                model.ClientId = Guid.Parse(idClient);
                var clientToken = await _authenticateService.TokenAuth(HttpContext, htppClient);
                HttpResponseMessage response = clientToken.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<WorkerPaginationResponse>(json);

                return result;
            }
        }

        private async Task<WorkerPaginationResponse> GetMyPublishWorkerClient(FilterPaginatedWorkerModel model)
        {
            
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "worker/GetMyPublishWorkerClient";
            using (HttpClient htppClient = new HttpClient())
            {
                var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);
                model.RemoveSpacing();
                model.ClientId = Guid.Parse(idClient);
                var clientToken = await _authenticateService.TokenAuth(HttpContext, htppClient);
                HttpResponseMessage response = clientToken.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<WorkerPaginationResponse>(json);

                return result;
            }
        }

        private async Task<WorkerPaginationResponse> GetMyWorkersPaginated(FilterPaginatedWorkerModel model)
        {
          
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "worker/GetMyWorkers";
            using (HttpClient htppClient = new HttpClient())
            {
                var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);
                model.RemoveSpacing();
                model.ClientId = Guid.Parse(idClient);
                var clientToken = await  _authenticateService.TokenAuth(HttpContext, htppClient);
                HttpResponseMessage response = clientToken.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<WorkerPaginationResponse>(json);

                return result;
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(WorkerDto model)
        {
            Console.WriteLine("Valor: " + model.PriceString);

            if (!string.IsNullOrEmpty(model.PriceString))
            {
                model.Price = ConvertGeneric.ConvertPrice(model.PriceString);
                Console.WriteLine("Valor Convertido: " + model.Price);
            }
                
            if (await _authenticateService.VerifyAuthenticated(HttpContext))
                return RedirectToAction("Logoff", "Login", new { area = "" });
           
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "worker/Register";
            using (HttpClient htppClient = new HttpClient())
            {
                var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);
                var worker = _mapper.Map<Worker>(model);
                worker.ClientId = Guid.Parse(idClient);
                if(worker.Address != null)
                  worker.AddressId = worker.Address.Id;

                var clientToken = await _authenticateService.TokenAuth(HttpContext, htppClient);
                HttpResponseMessage response = clientToken.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(worker), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<object>(json);

                if (result == null)
                    return RedirectToAction("MyPublishedWorkers", "Worker", new { area = "" });

                return RedirectToAction("ErrorPage", "Error", new { area = "" });
            }
        }

        [HttpPost]
        [Route("ApplyWorker")]
        public async Task<WorkerDto> ApplyWorker([FromBody] GuidDto model)
        {
           
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "worker/ApplyWorker";
            using (HttpClient htppClient = new HttpClient())
            {
                var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);
                var worker = new { ClientId = idClient, WorkerId = model.Guid };

                var clientToken = await _authenticateService.TokenAuth(HttpContext, htppClient);
                HttpResponseMessage response = clientToken.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(worker), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<WorkerDto>(json);

                return result;
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

                    var clientToken = await  _authenticateService.TokenAuth(HttpContext, client);
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

        [HttpPost]
        [Route("GetMyWorkersClientIdBasic")]
        public async Task<List<WorkerDto>> GetMyWorkersClientIdBasic()
        {
         
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "worker/GetMyWorkersClientIdBasic";
            using (HttpClient htppClient = new HttpClient())
            {
                var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);
              
                var clientToken = await _authenticateService.TokenAuth(HttpContext, htppClient);
                HttpResponseMessage response = clientToken.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(new GuidDto { Guid = Guid.Parse(idClient) }), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<List<WorkerDto>>(json);

                return result;
            }
        }


        [HttpPost]
        [Route("WorkerDone")]
        public async Task<TypeStatusCode> WorkerDone([FromBody] WorkerDoneDto workerDone)
        {

            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "worker/WorkerDone";
            using (HttpClient htppClient = new HttpClient())
            {
                var idClient = await _authenticateService.ObterClientIdLogado(HttpContext);
                workerDone.ValidationWorkerDone(idClient);

                var clientToken = await _authenticateService.TokenAuth(HttpContext, htppClient);
                HttpResponseMessage response = clientToken.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(workerDone), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<TypeStatusCode>(json);

                return result;
            }
        }
    }
}
