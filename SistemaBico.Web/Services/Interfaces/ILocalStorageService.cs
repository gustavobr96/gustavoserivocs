namespace SistemaBico.Web.Services.Interfaces
{
    public interface ILocalStorageService
    {
        void SetLocalStorage(string param, string value);
        Task <string> GetLocalStorage(string param);
        void Logout();
    }
}
