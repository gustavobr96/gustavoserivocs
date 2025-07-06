using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Generics.Entities;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.Response;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace SistemaBico.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v{version:apiVersion}/api/client")]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<ProfessionalProfileController> _logger;

        public ClientController(IClientRepository clientRepository,
            IMediator mediator,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            ILogger<ProfessionalProfileController> logger)
        {
            _clientRepository = clientRepository;
            _mediator = mediator;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        [SwaggerOperation(Tags = new[] { "Client" })]
        public async Task<IActionResult> Post(AddClientCommand queueAddClientCommand)
        {
            try
            {
                var entity = await _clientRepository.VerifyRegisterUserExist(queueAddClientCommand.Email, queueAddClientCommand.CpfCnpj);
                
                if(entity == null)
                     _ = await _mediator.Send(queueAddClientCommand);
                else
                {
                    return BadRequest(2);
                }
                
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro em Register Cliente");
                return StatusCode(403, e.Message);
            }
        }


        [HttpPost("Update")]
        [SwaggerOperation(Tags = new[] { "Client" })]
        public async Task<IActionResult> Put(UpdateClientCommand updateClientCommand)
        {
            try
            {
                var model = await _mediator.Send(updateClientCommand);
                var response = _mapper.Map<ClientResponse>(model);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro em Atualizar Cliente");
                return BadRequest(null);
            }
        }

        [HttpPost("UpdatePassword")]
        [SwaggerOperation(Tags = new[] { "Client" })]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordCommand updatePasswordCommand)
        {
            try
            {
                var model = await _mediator.Send(updatePasswordCommand);
                if(model.Success)
                     return Ok(200);

                return BadRequest(400);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro em Atualizar Senha");
                return BadRequest(null);
            }
        }

        [HttpPost("UpdateToken")]
        [SwaggerOperation(Tags = new[] { "Client" })]
        public async Task<IActionResult> UpdateToken(UpdateClientTokenCommand updateClientTokenCommand) // Migrar para fila
        {
            try
            {
                var model = await _mediator.Send(updateClientTokenCommand);
                return Ok(200);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro em Atualizar Token");
                return BadRequest(null);
            }
        }


        [HttpGet("GetClientProfileId/{id}")]
        [SwaggerOperation(Tags = new[] { "Client" })]
        public async Task<IActionResult> GetClientProfileId(Guid id)
        {
            try
            {
                var entity = await _clientRepository.GetUserByClientId(id);
                var response = _mapper.Map<ClientResponse>(entity);

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao obter cliente");
                return BadRequest(null);
            }
        }
    }
}
