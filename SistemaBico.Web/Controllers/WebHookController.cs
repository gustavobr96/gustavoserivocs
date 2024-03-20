using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaBico.Web.Models;
using SistemaBico.Web.Models.Configuration;
using System.Text;

namespace SistemaBico.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WebHookController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public WebHookController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost, AllowAnonymous]
        [Route("WebHook")]
        public async Task<IActionResult> WebHook([FromBody] Object webHook)
        {
          
            string url = _configuration.GetSection("ApiBackEndSettings").Get<ApiBackEndSettings>().Url + "WebHook/WebHook";
            using (HttpClient htppClient = new HttpClient())
            {
                var teste = webHook.ToString();
                var teste2 = JsonConvert.DeserializeObject<WebHookDto>(teste);
                HttpResponseMessage response = htppClient.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(teste2), Encoding.UTF8, "application/json")).Result;
            }
            return Ok();
        }
    }
}
