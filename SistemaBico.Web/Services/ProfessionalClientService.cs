using Newtonsoft.Json;
using SistemaBico.Web.Models;
using SistemaBico.Web.Models.Configuration;
using SistemaBico.Web.Models.Reponse;
using SistemaBico.Web.Services.Interfaces;
using System.Text;

namespace SistemaBico.Web.Services
{
    public class ProfessionalClientService : IProfessionalClientService
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IConfiguration _configuration;

        public ProfessionalClientService(IAuthenticateService authenticateService, IConfiguration configuration)
        {
            _authenticateService = authenticateService;
            _configuration = configuration;
        }

        public async Task<ProfessionalClientPaginationResponse> GetClientApproval(HttpContext ctx)
        {
           
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "professionalClient/GetClientApproval";
            using (HttpClient htppClient = new HttpClient())
            {
                try
                {
                    var idClient = await _authenticateService.ObterClientIdLogado(ctx);

                    var clientToken = await _authenticateService.TokenAuth(ctx, htppClient);
                    HttpResponseMessage response = clientToken.PostAsync(url,
                    new StringContent(JsonConvert.SerializeObject(new GuidDto { Guid = Guid.Parse(idClient) }), Encoding.UTF8, "application/json")).Result;

                    string json = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<List<ProfessionalClientDto>>(json);
                    return await Task.FromResult(new ProfessionalClientPaginationResponse { ProfessionalClient = result });
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
