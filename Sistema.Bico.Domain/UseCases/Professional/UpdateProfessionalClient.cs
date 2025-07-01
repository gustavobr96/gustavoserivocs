using MediatR;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Generics.Result;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.Interface.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.Professional
{
    public class UpdateProfessionalClientCommandHandler : IRequestHandler<UpdateProfessionalClientCommand, Result>
    {
        private readonly IProfessionalClientRepository _professionalClientRepository;
        private readonly IDapperProfessionalClientRepository _professionalClientDapperRepository;
        private readonly INotificacoesService _notificacoesService;
        private readonly Generics.Interfaces.INotification _Notification;

        public UpdateProfessionalClientCommandHandler(IProfessionalClientRepository professionalClientRepository, 
            Generics.Interfaces.INotification notification,
            INotificacoesService notificacoesService,
            IDapperProfessionalClientRepository professionalClientDapperRepository)
        {
            _professionalClientRepository = professionalClientRepository;
            _Notification = notification;
            _notificacoesService = notificacoesService;
            _professionalClientDapperRepository = professionalClientDapperRepository;
        }

        public async Task<Result> Handle(UpdateProfessionalClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var professionalClient = await _professionalClientRepository.GetProfessionalClientByProfileIntencao(request.ClientId, request.Perfil);

                if (professionalClient != null)
                {
                    await _professionalClientDapperRepository.AtualizarStatus(professionalClient.Id, request.StatusWorker);

                    if (professionalClient.StatusWorker == StatusWorker.AguardandoConfirmacao)
                        await _notificacoesService.DispararNotificacaoPendenteAprovacao(request.Perfil);
                }

               return new Result(true);


            }catch(Exception e)
            {
                _Notification.Handle("Erro ao enfileirar o cancel apply professional!");
                return _Notification.Return();
            }
        }
    }
}
