using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SistemaBico.Web.Models.Configuration;
using SistemaBico.Web.Models;
using System.Text;

namespace SistemaBico.Web.Controllers
{
    [Route("[controller]")]
    public class SiteController : Controller
    {
        private readonly IConfiguration _configuration;

        public SiteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(string city)
        {
            var profissionais = await GetTopProfessionalProfile(city);
            ViewBag.Profissionais = profissionais;

            return View();
        }



        private async Task<List<ProfileProfessionalTopDto>> GetTopProfessionalProfile(string city = null)
        {
            using (HttpClient htppClient = new HttpClient())
            {
                string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "professional/GetTopProfessionalAnonymos";

                HttpResponseMessage response = htppClient.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(new { Descricao = city }), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<List<ProfileProfessionalTopDto>>(json);

                return result;
            }
        }
    }
}
