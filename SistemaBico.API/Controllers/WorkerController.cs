using AutoMapper;
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
    [Route("v{version:apiVersion}/api/worker")]
    public class WorkerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IWorkerRepository _workerRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IDapperWorkerRepository _dapperWorkerRepository;
        private readonly ILogger<WorkerController> _logger;

        public WorkerController(
            IMediator mediator,
            IMapper mapper,
            IWorkerRepository workerRepository,
            IClientRepository clientRepository,
            IDapperWorkerRepository dapperWorkerRepository,
             ILogger<WorkerController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _workerRepository = workerRepository;
            _clientRepository = clientRepository;
            _dapperWorkerRepository = dapperWorkerRepository;
            _logger = logger;
        }

        [HttpPost("Register")]
        [SwaggerOperation(Tags = new[] { "Worker" })]
        public async Task<IActionResult> Post(AddWorkerCommand queueAddWorkerCommand)
        {
            try
            {
                var model = await _mediator.Send(queueAddWorkerCommand);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(403, e.Message);
            }
        }

        [HttpPost("ApplyWorker")]
        [SwaggerOperation(Tags = new[] { "Worker" })]
        public async Task<IActionResult> ApplyWorker(ApplyWorkerCommand queueApplyWorkerCommand)
        {
            try
            {
                await _mediator.Send(queueApplyWorkerCommand);

                var worker = await _workerRepository.GetEntityById(queueApplyWorkerCommand.WorkerId);
                var response = _mapper.Map<WorkerResponse>(worker);

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(403, e.Message);
            }
        }

        [HttpPost("GetWorkerPaginated")]
        [SwaggerOperation(Tags = new[] { "Worker" })]
        public async Task<WorkerPaginationResponse> GetProfessionalPaginated(FilterWorkerCommand filter)
        {
            try
            {
                var (count, list) = await _workerRepository.GetProfessionalPagination(filter);
                return new WorkerPaginationResponse { CountRegister = count, Worker = list };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro em GetMyPublishWorkerClient");
                return null;
            }
        
        }

        [HttpPost("GetMyWorkers")]
        [SwaggerOperation(Tags = new[] { "Worker" })]
        public async Task<WorkerPaginationResponse> GetMyWorkers(FilterWorkerCommand filter)
        {
            var (count, list) = await _workerRepository.GetMyWorkersPagination(filter);

            return new WorkerPaginationResponse { CountRegister = count, Worker = list };
        }


        [HttpPost("GetMyPublishWorkerClient")]
        [SwaggerOperation(Tags = new[] { "Worker" })]
        public async Task<WorkerPaginationResponse> GetMyPublishWorkerClient(FilterWorkerCommand filter)
        {

            try
            {
                return await _dapperWorkerRepository.GetMyPublishWorkerClient(filter);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro em GetMyPublishWorkerClient");
                return null;
            }

        }

        [HttpPost("GetMyWorkersClientIdBasic")]
        [SwaggerOperation(Tags = new[] { "Worker" })]
        public async Task<List<WorkerResponse>> GetMyWorkersClientIdBasic(GuidResponse guid)
        {
            var entity = await _workerRepository.GetMyWorkersClientIdBasic(guid.Guid);
            var response = _mapper.Map<List<WorkerResponse>>(entity);

            return response;
        }


        [HttpPost("WorkerDone")]
        [SwaggerOperation(Tags = new[] { "Worker" })]
        public async Task<IActionResult> WorkerDone(DoneWorkerCommand queueDoneWorkerCommand)
        {
            try
            {
                await _mediator.Send(queueDoneWorkerCommand);
                return Ok(200);
            }
            catch (Exception e)
            {
                return StatusCode(403, e.Message);
            }
        }
    }
}
