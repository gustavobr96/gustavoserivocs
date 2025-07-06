using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.Interface.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.Worker
{

    public class RegisterWorkertCommandHandler : IRequestHandler<AddWorkerCommand, Unit>
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly IMapper _mapper;
        private readonly IProfessionalAreaRepository _professionalAreaRepository;
        private readonly INotificacoesService _notificacoesServicosNovos;
        private readonly ILogger<RegisterWorkertCommandHandler> _logger;

        public RegisterWorkertCommandHandler(IWorkerRepository workerRepository,
            IMapper mapper,
            IProfessionalAreaRepository professionalAreaRepository,
            INotificacoesService notificacoesServicosNovos,
            ILogger<RegisterWorkertCommandHandler> logger)
        {
            _workerRepository = workerRepository;
            _mapper = mapper;
            _professionalAreaRepository = professionalAreaRepository;
            _notificacoesServicosNovos = notificacoesServicosNovos;
            _logger = logger;
        }

        public async Task<Unit> Handle(AddWorkerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var area = await _professionalAreaRepository.GetProfessionalAreaId(request.ProfessionalArea.Codigo);
                if (area != null)
                {
                    var worker = _mapper.Map<Domain.Entities.Worker>(request);
                    worker.InserirWorker(area.Id);

                    await _workerRepository.Add(worker);

                    string? cidade = request.Remote ? null : request.Address?.City;
                    await _notificacoesServicosNovos.DispararNotificacaoNovoServico(cidade, area.Codigo, request.Title);
                }

                return Unit.Value;
            }
            catch (Exception e)
            {
                _logger.LogError(e, " - Erro ao adicionar serviço");
                throw;
            }

        }
    }
}
