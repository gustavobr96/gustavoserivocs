using MediatR;
using System;

namespace Sistema.Bico.Domain.Command
{
    public class DoneWorkerCommand : IRequest<Unit>
    {
        public Guid ClientId { get; set; }
        public string PrestadorPerfil { get; set; }
        public Guid? WorkerId { get; set; }
        public int AvaliationDeadline { get; set; }
        public int AvaliationQuality { get; set; }
        public int AvaliationCommunication { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
    }
    
}
