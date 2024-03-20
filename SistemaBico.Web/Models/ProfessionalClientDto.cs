using SistemaBico.Web.Enum;

namespace SistemaBico.Web.Models
{
    public class ProfessionalClientDto
    {
        public Guid Id { get; set; }
        public ProfessionalProfileDto? ProfessionalProfile { get; set; }
        public ClientDto? Client { get; set; }
        public StatusWorker StatusWorker { get; set; }
    }
}
