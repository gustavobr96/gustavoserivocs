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

        public ClientController(IClientRepository clientRepository,
            IMediator mediator,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mediator = mediator;
            _userManager = userManager;
            _mapper = mapper;
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
                return StatusCode(403, e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Forgot")]
        [SwaggerOperation(Tags = new[] { "Client" })]
        public async Task<IActionResult> Forgot(QueuePublishForgotCommand queueAddClientCommand)
        {
            _ = await _mediator.Send(queueAddClientCommand);
            return Ok();
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
                return BadRequest(null);
            }
        }

        [HttpGet("GetClientProfileId/{id}")]
        [SwaggerOperation(Tags = new[] { "Client" })]
        public async Task<IActionResult> GetClientProfileId(Guid id)
        {
            var entity = await _clientRepository.GetUserByClientId(id);
            var response = _mapper.Map<ClientResponse>(entity);

            return Ok(response);
        }
    }
}
