using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Interface;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace SistemaBico.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v{version:apiVersion}/api/term")]
    public class TermController : ControllerBase
    {
        private readonly ITermUseRepository _termUseRepository;

        public TermController(ITermUseRepository termUseRepository)
        {
            _termUseRepository = termUseRepository;
        }

        [HttpGet("GetTerm/{id}")]
        [SwaggerOperation(Tags = new[] { "Professional" })]
        public async Task<IActionResult> GetProfessionalProfileId(int id)
        {

            var response = await _termUseRepository.GetProfessionalProfileId((TypeTerm)id);
            return Ok(response);
        }
    }
}
