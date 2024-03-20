using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Generics.Interfaces;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.Response;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Generics.Entities
{
    public class Token : IToken
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IClientRepository _clientRepository;
        private const string Issuer = "Bico.Securiry.Bearer";
        private const string Audience = "Bico.Securiry.Bearer";
        private const string SecretKey = "Secret_Key-AESKPERSK2816762";
        private const int ExpirationHours = 24;

        public Token(UserManager<ApplicationUser> userManager,
            IClientRepository clientRepository )
        {
            _userManager = userManager;
            _clientRepository = clientRepository;
        }

        public async Task<LoginResponse> GerarJwt(string email)
        {
            var User = await BuscarClientAsync(email);
            var claims = await BuscarClaimsUser(User);

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = Issuer,
                Audience = Audience,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey)), SecurityAlgorithms.HmacSha256Signature)
            });

            return new LoginResponse
            {
                ExpiresIn = TimeSpan.FromHours(ExpirationHours).TotalSeconds,
                Token = tokenHandler.WriteToken(token),
                User = new UserResponse
                {
                    Id = Guid.Parse(User.Id),
                    Email = User.Email,
                    Name = $"{User.Client.Name} {User.Client.LastName}",
                    ClientId = User.ClientId,
                    PerfilPicture = User.Client.PerfilPicture
                }
            };
        }

        private async Task<IList<Claim>> BuscarClaimsUser(ApplicationUser User)
         => await _userManager.GetClaimsAsync(User);

        private async Task<ApplicationUser> BuscarEmailAsync(string email)
            => await _userManager.FindByEmailAsync(email);

        private async Task<ApplicationUser> BuscarClientAsync(string email)
          => await _clientRepository.GetClientFromEmail(email);
    }
}
