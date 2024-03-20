using MediatR;
using Sistema.Bico.Domain.Response;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Integration.Interfaces
{
    public interface ISmtpEmailComunication
    {
        Task<Unit> SendEmail(EmailDto request, CancellationToken cancellationToken);
    }
}
