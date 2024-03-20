using Sistema.Bico.Domain.Enums;
using System;

namespace Sistema.Bico.Domain.Entities
{
    public class ProfessionalClient : Base
    {
        public Guid Id { get; set; }
        public Guid? ProfessionalProfileId { get; set; }
        public Guid? ClientId { get; set; }
        public ProfessionalProfile ProfessionalProfile { get; set; }
        public Client Client { get; set; }
        public StatusWorker StatusWorker { get; set; }
    }
}
