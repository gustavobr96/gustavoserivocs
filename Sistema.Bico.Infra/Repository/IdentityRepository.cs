using Microsoft.AspNetCore.Identity;
using Sistema.Bico.Domain.Generics.Entities;
using Sistema.Bico.Domain.Interface;
using System.Threading.Tasks;

namespace Sistema.Bico.Infra.Repository
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;


        public IdentityRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterAsync(ApplicationUser identityUser, string password)
        {
            return await _userManager.CreateAsync(identityUser, password);
        }
        public async Task<IdentityResult> UpdateAsync(ApplicationUser identityUser)
        {
            return await _userManager.UpdateAsync(identityUser);
        }

    }
}
