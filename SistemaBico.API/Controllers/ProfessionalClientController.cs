using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.Response;
using Swashbuckle.AspNetCore.Annotations;

namespace SistemaBico.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v{version:apiVersion}/api/professionalClient")]
    public class ProfessionalClientController : ControllerBase
    {
        private readonly IProfessionalProfileRepository _professionalProfileRepository;
        private readonly IProfessionalClientRepository _professionalClientRepository;
        private readonly IDapperProfessionalClientRepository _dapperProfessionalClientRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProfessionalClientController(IProfessionalClientRepository professionalClientRepository,
            IMediator mediator,
            IMapper mapper,
            IProfessionalProfileRepository professionalProfileRepository,
            IDapperProfessionalClientRepository dapperProfessionalClientRepository)
        {
            _professionalClientRepository = professionalClientRepository;
            _mediator = mediator;
            _mapper = mapper;
            _professionalProfileRepository = professionalProfileRepository;
            _dapperProfessionalClientRepository = dapperProfessionalClientRepository;
        }


        [HttpPost("GetMyProfessionalClient")]
        [SwaggerOperation(Tags = new[] { "ProfessionalClient" })]
        public async Task<IActionResult> GetMyProfessionalClient(GuidResponse guid)
        {
            try
            {
                var list = await _professionalClientRepository.GetMyProfessionalClient(guid.Guid);
                var response = _mapper.Map<List<ProfessionalClientResponse>>(list);

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(403, e.Message);
            }
        }

        [HttpPost("GetMyProfessionalClientByPerfil")]
        [SwaggerOperation(Tags = new[] { "ProfessionalClient" })]
        public async Task<IActionResult> GetMyProfessionalClientByPerfil(string email)
        {
            try
            {
                var professionalClient = await _professionalClientRepository.GetMyProfessionalClientByPerfil(email);
                var response = _mapper.Map<ProfessionalClientResponse>(professionalClient);

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(403, e.Message);
            }
        }

        [HttpPost("GetClientApproval")]
        [SwaggerOperation(Tags = new[] { "ProfessionalClient" })]
        public async Task<IActionResult> GetClientApproval(GuidResponse guid)
        {
            try
            {
                var list = await _professionalClientRepository.GetClientApproval(guid.Guid);
                return Ok(list);
            }
            catch (Exception e)
            {
                return StatusCode(403, e.Message);
            }
        }


        [HttpPost("CancelApplyProfessional")]
        [SwaggerOperation(Tags = new[] { "ProfessionalClient" })]
        public async Task<IActionResult> CancelApplyProfessional(CancelApplyProfessionalCommand queueCancelApplyProfessionalCommand)
        {
            try
            {
                await _mediator.Send(queueCancelApplyProfessionalCommand);
                return Ok(200);
            }
            catch (Exception e)
            {
                return StatusCode(403, e.Message);
            }
        }


        [HttpPost("ApplyProfessional")]
        [SwaggerOperation(Tags = new[] { "ProfessionalClient" })]
        public async Task<IActionResult> ApplyProfessional(ApplyProfessionalCommand queueApplyProfessionalCommand)
        {
            try
            {
                await _mediator.Send(queueApplyProfessionalCommand);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(403, e.Message);
            }
        }

        [HttpPost("ClientApprovalOrRecused")]
        [SwaggerOperation(Tags = new[] { "ProfessionalClient" })]
        public async Task<IActionResult> ClientApprovalOrRecused(ApprovalOrRecusedCommand approvalOrRecusedCommand)
        {
            try
            {
                await _mediator.Send(approvalOrRecusedCommand);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(403, e.Message);
            }
        }


        [HttpPost("UpdateProfessionalClient")]
        [SwaggerOperation(Tags = new[] { "ProfessionalClient" })]
        public async Task<IActionResult> UpdateProfessionalClient(UpdateProfessionalClientCommand command)
        {
            try
            {
               var result =  await _mediator.Send(command);
               return result.Success ? Ok(200) : BadRequest(400);
            }
            catch (Exception e)
            {
                return StatusCode(403, e.Message);
            }
        }
    }
}
