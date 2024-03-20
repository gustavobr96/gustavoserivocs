namespace SistemaBico.Web.Models
{
    public class ClientDto
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? CpfCnpj { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? RPassword { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PerfilPicture { get; set; }
        public IFormFile? File { get; set; }
    }
}
