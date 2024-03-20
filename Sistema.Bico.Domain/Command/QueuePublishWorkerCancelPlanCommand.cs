using MediatR;
using Sistema.Bico.Domain.Generics.Result;

namespace Sistema.Bico.Domain.Command
{
    public class QueuePublishWorkerCancelPlanCommand : IRequest<Result>
    {
    }
}
