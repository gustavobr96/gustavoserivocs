using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Interface;
using Swashbuckle.AspNetCore.Annotations;

namespace SistemaBico.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v{version:apiVersion}/api/professionalArea")]
    public class ProfessionalAreaController : ControllerBase
    {
        private readonly IProfessionalAreaRepository _professionalAreaRepository;

        public ProfessionalAreaController(IProfessionalAreaRepository professionalAreaRepository)
        {
            _professionalAreaRepository = professionalAreaRepository;
        }

        [HttpGet("GetAllProfessionalArea")]
        [SwaggerOperation(Tags = new[] { "ProfessionalArea" })]
        public async Task<List<ProfessionalArea>> GetAllProfessionalArea()
        {
            var professionalArea = await _professionalAreaRepository.GetAllProfessionalArea();
            return professionalArea;
        }
    }
}
