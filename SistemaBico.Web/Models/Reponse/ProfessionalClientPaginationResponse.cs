using SistemaBico.Web.Models.Filters;

namespace SistemaBico.Web.Models.Reponse
{
    public class ProfessionalClientPaginationResponse : FilterPaginatedProfessionalModel
    {
        public List<ProfessionalClientDto> ProfessionalClient{ get; set; }
        public int CountRegister { get; set; }
        public int PagesSize { get; set; }
    }
}
