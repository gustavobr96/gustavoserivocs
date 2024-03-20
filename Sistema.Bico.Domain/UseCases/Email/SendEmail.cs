using MediatR;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Integration.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.UseCases.Email
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Unit>
    {
        private readonly ISmtpEmailComunication _smtpEmailComunication;
        public SendEmailCommandHandler(ISmtpEmailComunication smtpEmailComunication)
        {
            _smtpEmailComunication = smtpEmailComunication;
        }

        public async Task<Unit> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _smtpEmailComunication.SendEmail(request.Email, cancellationToken);
                return await Task.FromResult(Unit.Value);
            }
            catch (Exception e) { return Unit.Value; }
          
        }
    }
}
