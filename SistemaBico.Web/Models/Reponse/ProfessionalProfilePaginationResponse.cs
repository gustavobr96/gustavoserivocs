using SistemaBico.Web.Models.Filters;

namespace SistemaBico.Web.Models.Reponse
{
    public class ProfessionalProfilePaginationResponse : FilterPaginatedProfessionalModel
    {
        public List<ProfessionalProfileDto> ProfessionalProfile { get; set; }
        public int CountRegister { get; set; }
        public int PagesSize { get; set; }
    }
}
