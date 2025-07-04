﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Command.Filters;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.Response;
using Swashbuckle.AspNetCore.Annotations;

namespace SistemaBico.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v{version:apiVersion}/api/professional")]
    public class ProfessionalProfileController : ControllerBase
    {
        private readonly IProfessionalProfileRepository _professionalProfileRepository;
        private readonly IWorkerDoneRepository _workerDoneRepository;
        private readonly IThreeAvaliationRepository _threeAvaliationRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProfessionalProfileController(IProfessionalProfileRepository professionalProfileRepository,
            IMediator mediator,
            IMapper mapper,
            IWorkerDoneRepository workerDoneRepository,
            IThreeAvaliationRepository threeAvaliationRepository)
        {
            _professionalProfileRepository = professionalProfileRepository;
            _mediator = mediator;
            _mapper = mapper;
            _workerDoneRepository = workerDoneRepository;
            _threeAvaliationRepository = threeAvaliationRepository;
        }

        [HttpGet("GetProfessionalProfileId/{id}")]
        [SwaggerOperation(Tags = new[] { "Professional" })] 
        public async Task<IActionResult> GetProfessionalProfileId(Guid id)
        {

            var entity = await _professionalProfileRepository.GetProfessionalProfileId(id);
            var response = _mapper.Map<ProfessionalProfileResponse>(entity);

            return Ok(response);
        }



        [HttpGet("GetVerifyProfissional/{id}")]
        [SwaggerOperation(Tags = new[] { "Professional" })]
        public async Task<IActionResult> GetVerifyProfissional(Guid id)
        {

            var entity = await _professionalProfileRepository.GetVerifyProfissional(id);

            if(entity != null)
                return Ok(new { Profissional = true, Ativo =  entity.Ativo, Premium = entity.Premium, VigenciaPremium = entity.VigenciaPremium?.ToString("dd/MM/yyyy") ?? null });


            return Ok(false);
        }

        [HttpPost("GetProfessionalPaginated")]
        [SwaggerOperation(Tags = new[] { "Professional" })]
        public async Task<ProfessionalProfilePaginationResponse> GetProfessionalPaginated(FilterProfessionalCommand filter)
        {
            var (count,list) =  await _professionalProfileRepository.GetProfessionalPagination(filter);
            var response = _mapper.Map<List<ProfessionalProfileResponse>>(list);

            return new ProfessionalProfilePaginationResponse { CountRegister = count, ProfessionalProfile = response };
        }

        [HttpPost("Register")]
        [SwaggerOperation(Tags = new[] { "Professional" })]
        public async Task<IActionResult> Post(AddProfessionalCommand addProfessionalCommand)
        {
            try
            {
              
                var model = await _mediator.Send(addProfessionalCommand);
                return Ok(200);
            }
            catch (Exception e)
            {
                return StatusCode(403, e.Message);
            }
        }

        [HttpPost("Update")]
        [SwaggerOperation(Tags = new[] { "Professional" })]
        public async Task<IActionResult> Put(UpdateProfessionalCommand updateProfessionalCommand)
        {
            try
            {
                var model = await _mediator.Send(updateProfessionalCommand);
                var response = _mapper.Map<ProfessionalProfileResponse>(model);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(null);
            }
        }


        [HttpPost("GetInterested")]
        [SwaggerOperation(Tags = new[] { "Professional" })]
        public async Task<ProfessionalProfilePaginationResponse> GetInterested(GuidResponse request)
        {
            var list = await _professionalProfileRepository.GetProfessionalInterested(request.Guid);
            var response = _mapper.Map<List<ProfessionalProfileResponse>>(list);

            return new ProfessionalProfilePaginationResponse { ProfessionalProfile = response };
        }

        [HttpPost("GetTopProfessionalAnonymos")]
        [SwaggerOperation(Tags = new[] { "Professional" })]
        [AllowAnonymous]
        public async Task<List<ProfileProfessionalTopResponse>> GetTopProfessionalAnonymos(StringResponse request)
        {
           var professionalProfileEntity =  await _professionalProfileRepository.GetTopProfessional(request.Descricao);
           var professionalResponse = _mapper.Map<List<ProfileProfessionalTopResponse>>(professionalProfileEntity);

           return professionalResponse;
        }

        [HttpPost("GetProfessionalPerfil")]
        [SwaggerOperation(Tags = new[] { "Professional" })]
        public async Task<ProfileWorkerProfessionalPaginationResponse> GetProfessionalPerfil(FilterProfileCommand filter)
        {

            var entity = await _professionalProfileRepository.GetProfessionalPerfilId(filter.Profile);
            var (count, list) = await _workerDoneRepository.GetWorkerDoneByProfilePagination(filter);
            var treeAvaliation = await _threeAvaliationRepository.GetThreeAvaliationByProfessionalId(entity.Id);

            var professionalProfile = _mapper.Map<ProfessionalProfileResponse>(entity);
            var workerDone = _mapper.Map<List<WorkerDoneResponse>>(list);
            var treeAvaliationResponse = _mapper.Map<ThreeAvaliationResponse>(treeAvaliation);

            return new ProfileWorkerProfessionalPaginationResponse { CountRegister = count, ProfessionalProfile = professionalProfile, WorkerDone = workerDone , ThreeAvaliation = treeAvaliationResponse };
        }

        [HttpGet("GetProfessionalPerfil/{id}")]
        [SwaggerOperation(Tags = new[] { "Professional" })]
        public async Task<ProfileWorkerProfessionalPaginationResponse> GetProfessionalPerfil(string id)
        {

            var entity = await _professionalProfileRepository.GetProfessionalProfileBasic(id);
            var treeAvaliation = await _threeAvaliationRepository.GetThreeAvaliationByProfessionalId(entity.Id);
            var list= await _workerDoneRepository.GetListWorkerDoneProfile(id);

            var workerDone = _mapper.Map<List<WorkerDoneResponse>>(list);
            var treeAvaliationResponse = _mapper.Map<ThreeAvaliationResponse>(treeAvaliation);

            return new ProfileWorkerProfessionalPaginationResponse { WorkerDone = workerDone, ThreeAvaliation = treeAvaliationResponse };
        }

    }
}
