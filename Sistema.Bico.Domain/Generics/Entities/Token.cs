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
        private const string SecretKey = "a1b2c3d4e5f6g7h8i9j0klmnopqrstuv";
        private const int ExpirationHours = 24;

        public Token(UserManager<ApplicationUser> userManager,
            IClientRepository clientRepository )
        {
            _userManager = userManager;
            _clientRepository = clientRepository;
        }

        public async Task<LoginResponse> GerarJwt(string email)
        {
            try
            {
                var User = await BuscarClientAsync(email);
                if (User == null)
                    throw new Exception("Usuário não encontrado.");

                var claims = await BuscarClaimsUser(User);
                if (claims == null)
                    throw new Exception("Falha ao buscar claims do usuário.");

                var identityClaims = new ClaimsIdentity();
                identityClaims.AddClaims(claims);

                if (string.IsNullOrEmpty(SecretKey) || SecretKey.Length < 16)
                    throw new Exception("Chave secreta inválida ou muito curta.");

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
                    TokenPhone = User.Client.TokenPhone,
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
            catch(Exception e)
            {
                return null;
            }
        
        }

        private async Task<IList<Claim>> BuscarClaimsUser(ApplicationUser User)
         => await _userManager.GetClaimsAsync(User);

        private async Task<ApplicationUser> BuscarEmailAsync(string email)
            => await _userManager.FindByEmailAsync(email);

        private async Task<ApplicationUser> BuscarClientAsync(string email)
          => await _clientRepository.GetClientFromEmail(email);
    }
}
