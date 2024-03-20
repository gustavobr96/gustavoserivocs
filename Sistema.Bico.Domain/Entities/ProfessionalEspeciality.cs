using System;

namespace Sistema.Bico.Domain.Entities
{
    public class ProfessionalEspeciality : Base
    {
        public string Description { get; set; }
        public Guid IdProfessionalProfile { get; set; }
        public virtual ProfessionalProfile ProfessionalProfile { get; set; }
    }
}
