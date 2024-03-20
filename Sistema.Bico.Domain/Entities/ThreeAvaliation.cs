using System;

namespace Sistema.Bico.Domain.Entities
{
    public class ThreeAvaliation : Base
    {
        public decimal Deadline { get; set; }
        public decimal Quality { get; set; }
        public decimal Communication { get; set; }
        public int NumberAvaliation { get; set; }
        public Guid? ProfessionalProfileId { get; set; }
        public ProfessionalProfile ProfessionalProfile { get; set; }
    }
}
