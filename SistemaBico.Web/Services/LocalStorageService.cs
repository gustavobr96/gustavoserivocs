using Hanssens.Net;
using SistemaBico.Web.Services.Interfaces;

namespace SistemaBico.Web.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private LocalStorage localStorage;

        public void SetLocalStorage(string param, string value)
        {
            if (localStorage == null)
                localStorage = new LocalStorage();

            localStorage.Store(param, value);
        }
        public async Task<string> GetLocalStorage(string param)
        {

            return localStorage == null ? "" : await Task.FromResult(localStorage.Get(param).ToString());
        }
        public void Logout()
        {
            if(localStorage != null)
              localStorage.Clear();
        }
    }
}
