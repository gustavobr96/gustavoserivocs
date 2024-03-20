using Sistema.Bico.Domain.Enums;
using System;

namespace Sistema.Bico.Domain.Response
{
    public class ProfessionalClientResponse
    {
        public Guid Id { get; set; }
        public ProfessionalProfileResponse ProfessionalProfile { get; set; }
        public ClientResponse Client { get; set; }
        public StatusWorker StatusWorker { get; set; }
    }
}
