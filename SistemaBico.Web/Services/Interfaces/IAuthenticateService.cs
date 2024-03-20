using SistemaBico.Web.Models.Reponse;

namespace SistemaBico.Web.Services.Interfaces
{
    public interface IAuthenticateService
    {
        Task Login(HttpContext ctx, LoginResponse user, bool? professional = false);
        Task<HttpClient> TokenAuth(HttpContext ctx, HttpClient client);
        Task<bool> VerifyAuthenticated(HttpContext ctx);
        Task Logoff(HttpContext ctx);
        Task<string> ObterClientIdLogado(HttpContext contexto);
    }
}
