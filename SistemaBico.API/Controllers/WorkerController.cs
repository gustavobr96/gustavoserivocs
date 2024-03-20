using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Command.Filters;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.Response;
using Sistema.Bico.Infra.Repository;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public WorkerController(
            IMediator mediator, 
            IMapper mapper, 
            IWorkerRepository workerRepository,
            IClientRepository clientRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _workerRepository = workerRepository;
            _clientRepository = clientRepository;
        }

        [HttpPost("Register")]
        [SwaggerOperation(Tags = new[] { "Worker" })]
        public async Task<IActionResult> Post(QueueAddWorkerCommand queueAddWorkerCommand)
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
        public async Task<IActionResult> ApplyWorker(QueueApplyWorkerCommand queueApplyWorkerCommand)
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
            var (count, list) = await _workerRepository.GetProfessionalPagination(filter);
            var response = _mapper.Map<List<WorkerResponse>>(list);

            return new WorkerPaginationResponse { CountRegister = count, Worker = response };
        }

        [HttpPost("GetMyWorkers")]
        [SwaggerOperation(Tags = new[] { "Worker" })]
        public async Task<WorkerPaginationResponse> GetMyWorkers(FilterWorkerCommand filter)
        {
            var (count, list) = await _workerRepository.GetMyWorkersPagination(filter);
            var response = _mapper.Map<List<WorkerResponse>>(list);

            return new WorkerPaginationResponse { CountRegister = count, Worker = response };
        }


        [HttpPost("GetMyPublishWorkerClient")]
        [SwaggerOperation(Tags = new[] { "Worker" })]
        public async Task<WorkerPaginationResponse> GetMyPublishWorkerClient(FilterWorkerCommand filter)
        {
            var (count, list) = await _workerRepository.GetMyPublishWorkerClient(filter);
            var response = _mapper.Map<List<WorkerResponse>>(list);

            return new WorkerPaginationResponse { CountRegister = count, Worker = response };
        }     
        
        [HttpPost("GetMyWorkersClientIdBasic")]
        [SwaggerOperation(Tags = new[] { "Worker" })]
        public async Task<List<WorkerResponse>> GetMyWorkersClientIdBasic(GuidResponse guid)
        {
            var entity =  await _workerRepository.GetMyWorkersClientIdBasic(guid.Guid);
            var response = _mapper.Map<List<WorkerResponse>>(entity);

            return response;
        }

        // Done Worker
        [HttpPost("WorkerDone")]
        [SwaggerOperation(Tags = new[] { "Worker" })]
        public async Task<IActionResult> WorkerDone(QueueDoneWorkerCommand queueDoneWorkerCommand)
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
