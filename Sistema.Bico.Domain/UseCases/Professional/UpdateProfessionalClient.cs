using MediatR;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Generics.Result;
using Sistema.Bico.Domain.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.Professional
{
    public class UpdateProfessionalClientCommandHandler : IRequestHandler<UpdateProfessionalClientCommand, Result>
    {
        private readonly IProfessionalClientRepository _professionalClientRepository;
        private readonly Generics.Interfaces.INotification _Notification;

        public UpdateProfessionalClientCommandHandler(IProfessionalClientRepository professionalClientRepository, Generics.Interfaces.INotification notification)
        {
            _professionalClientRepository = professionalClientRepository;
            _Notification = notification;
        }

        public async Task<Result> Handle(UpdateProfessionalClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var professionalClient = await _professionalClientRepository.GetProfessionalClientByProfileIntencao(request.ClientId, request.Perfil);

                if (professionalClient != null)
                {
                    professionalClient.StatusWorker = request.StatusWorker;
                    await _professionalClientRepository.Update(professionalClient);
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
