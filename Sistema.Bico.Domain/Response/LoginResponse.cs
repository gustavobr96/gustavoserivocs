using System;
using System.Collections.Generic;

namespace Sistema.Bico.Domain.Response
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public byte[] PerfilPicture { get; set; }
        public IEnumerable<PermissionsResponse> Permissions { get; set; }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
        public string TokenPhone { get; set; }
        public double ExpiresIn { get; set; }
        public UserResponse User { get; set; }
    }

    public class PermissionsResponse
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
