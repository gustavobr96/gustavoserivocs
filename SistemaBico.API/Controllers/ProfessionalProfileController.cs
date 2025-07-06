using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Command.Filters;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.Response;
using Sistema.Bico.Infra.Dapper.Repository;
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
        private readonly IDapperProfessionalClientRepository _dapperProfessionalClientRepository;
        private readonly IProfessionalClientRepository _professionalClientRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<ProfessionalProfileController> _logger;


        public ProfessionalProfileController(IProfessionalProfileRepository professionalProfileRepository,
            IMediator mediator,
            IMapper mapper,
            IWorkerDoneRepository workerDoneRepository,
            IThreeAvaliationRepository threeAvaliationRepository,
            IDapperProfessionalClientRepository dapperProfessionalClientRepository,
            ILogger<ProfessionalProfileController> logger,
            IProfessionalClientRepository professionalClientRepository)
        {
            _professionalProfileRepository = professionalProfileRepository;
            _mediator = mediator;
            _mapper = mapper;
            _workerDoneRepository = workerDoneRepository;
            _threeAvaliationRepository = threeAvaliationRepository;
            _dapperProfessionalClientRepository = dapperProfessionalClientRepository;
            _logger = logger;
            _professionalClientRepository = professionalClientRepository;
        }

        [HttpGet("GetProfessionalProfileId/{id}")]
        [SwaggerOperation(Tags = new[] { "Professional" })] 
        public async Task<IActionResult> GetProfessionalProfileId(Guid id)
        {
            try
            {
                var response = await _professionalProfileRepository.GetProfessionalProfileIdTracking(id);
                return Ok(response);
            }
            catch(Exception ex) {
                _logger.LogError(ex, "Erro em GetProfessionalProfileId");
                return StatusCode(500);
            }
        }

        [HttpGet("GetVerifyProfissional/{id}")]
        [SwaggerOperation(Tags = new[] { "Professional" })]
        public async Task<IActionResult> GetVerifyProfissional(Guid id)
        {
            try
            {
                var entity = await _professionalProfileRepository.GetVerifyProfissional(id);

                if (entity != null)
                    return Ok(new { Profissional = true, Ativo = entity.Ativo, Premium = entity.Premium, VigenciaPremium = entity.VigenciaPremium?.ToString("dd/MM/yyyy") ?? null });


                return Ok(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro em GetVerifyProfissional");
                return StatusCode(500);
            }
           
        }

        [HttpPost("GetProfessionalPaginated")]
        [SwaggerOperation(Tags = new[] { "Professional" })]
        public async Task<ProfessionalProfilePaginationResponse> GetProfessionalPaginated(FilterProfessionalCommand filter)
        {
            try
            {
                var (count, list) = await _dapperProfessionalClientRepository.GetProfessionalPaginationWithSlapper(filter);
                var response = _mapper.Map<List<ProfessionalProfileResponse>>(list);

                return new ProfessionalProfilePaginationResponse { CountRegister = count, ProfessionalProfile = response };

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro em GetProfessionalPaginated");
                return null;
            }
          
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
                _logger.LogError(e, "Erro em Register");
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
                _logger.LogError(e, "Erro em Atualizar");
                return BadRequest(null);
            }
        }


        [HttpPost("GetInterested")]
        [SwaggerOperation(Tags = new[] { "Professional" })]
        public async Task<ProfessionalProfilePaginationResponse> GetInterested(GuidResponse request)
        {
            try
            {
                var list = await _professionalProfileRepository.GetProfessionalInterested(request.Guid);
                var response = _mapper.Map<List<ProfessionalProfileResponse>>(list);

                return new ProfessionalProfilePaginationResponse { ProfessionalProfile = response };
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Erro em GetInterested");
                return null;
            }
           
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

        [HttpGet("GetProfessionalPerfil/{id}")]
        [SwaggerOperation(Tags = new[] { "Professional" })]
        public async Task<ProfileWorkerProfessionalPaginationResponse> GetProfessionalPerfil(string id)
        {
            try
            {
                var profile = await _professionalClientRepository.GetMyProfessionalClientByPerfil(id);
                if (profile == null) return null;

                var threeAvaliation = await _threeAvaliationRepository.GetThreeAvaliationByProfessionalId(profile.ProfessionalProfile.Id);
                var workerDoneList = await _workerDoneRepository.GetListWorkerDoneProfile(id);

                var dto = new ProfileWorkerProfessionalPaginationResponse
                {
                    ProfessionalProfile = _mapper.Map<ProfessionalProfileResponse>(profile.ProfessionalProfile),
                    ThreeAvaliation = _mapper.Map<ThreeAvaliationResponse>(threeAvaliation),
                    WorkerDone = _mapper.Map<List<WorkerDoneResponse>>(workerDoneList)
                };

                return dto;

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Erro em GetProfessionalPerfil");
                return null;
            }


        }

    }
}
