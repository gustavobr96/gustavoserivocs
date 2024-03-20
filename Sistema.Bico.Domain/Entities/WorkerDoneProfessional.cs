using System;
using System.Collections.Generic;

namespace Sistema.Bico.Domain.Entities
{
    public class WorkerDoneProfessional : Base
    {
        public Guid ProfessionalProfileId { get; set; }
        public ProfessionalProfile ProfessionalProfile { get; set; }
        public virtual ICollection<WorkerDone>? WorkerDone { get; set; }
    }
}
