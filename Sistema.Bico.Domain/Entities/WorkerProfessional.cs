using System;

namespace Sistema.Bico.Domain.Entities
{
    public class WorkerProfessional : Base
    {
        public Guid? ProfessionalProfileId { get; set; }
        public Guid? WorkerId { get; set; }
        public ProfessionalProfile ProfessionalProfile { get; set; }
        public Worker Worker { get; set; }
        public bool IsConcluded { get; set; } = false;
    }
}
