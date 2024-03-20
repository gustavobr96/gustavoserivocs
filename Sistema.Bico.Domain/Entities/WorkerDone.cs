using System;

namespace Sistema.Bico.Domain.Entities
{
    public class WorkerDone : Base
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Avaliation { get; set; }
        public string Comment { get; set; }
        public Guid WorkerDoneProfessionalId { get; set; }
        public virtual WorkerDoneProfessional WorkerDoneProfessional { get; set; }
    }
}
