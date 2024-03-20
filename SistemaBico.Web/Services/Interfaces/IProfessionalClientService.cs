using SistemaBico.Web.Models.Reponse;

namespace SistemaBico.Web.Services.Interfaces
{
    public interface IProfessionalClientService
    {
        Task<ProfessionalClientPaginationResponse> GetClientApproval(HttpContext ctx);
    }
}
