using System;
using System.Collections.Generic;

namespace Sistema.Bico.Domain.Entities
{
    public class Worker : Base
    {
        public Worker()
        {
            WorkerProfessional = new HashSet<WorkerProfessional>();
        }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Profession { get; set; }
        public string About { get; set; }
        public double? Price { get; set; }
        public Guid ClientId { get; set; }
        public Guid? AddressId { get; set; }
        public Guid? ProfessionalAreaId { get; set; }
        public Guid? ProfessionalProfileConcludedId { get; set; }
        public bool IsConcluded { get; set; } = false;
        public virtual Client Client { get; set; }
        public Address Address { get; set; }
        public ProfessionalArea ProfessionalArea { get; set; }
        public ProfessionalProfile ProfessionalProfileConcluded { get; set; }
        public virtual ICollection<WorkerProfessional>? WorkerProfessional { get; set; }

        public void InserirWorker(Guid professionalAreaId)
        {
            ProfessionalAreaId = professionalAreaId;
            ProfessionalArea = null;
            Id = Guid.NewGuid();

            if (Address == null)
                AddressId = null;

        }
    }
}
