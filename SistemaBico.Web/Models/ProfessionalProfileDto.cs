using SistemaBico.Web.Enum;

namespace SistemaBico.Web.Models
{
    public class ProfessionalProfileDto
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string Perfil { get; set; }
        public string? Avaliation { get; set; }
        public string? PerfilPicture { get; set; }
        public bool Ativo { get; set; }
        public bool Premium { get; set; }

        public Guid? ClientId { get; set; }
        public List<string> Especiality { get; set; }
        public int Area { get; set; }
        public string? Sobre { get; set; }
        public string? Profession { get; set; }
        public string? Logradouro { get; set; }
        public string? Number { get; set; }
        public string? Bairro { get; set; }
        public string? Complemento { get; set; }
        public string? CEP { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Email { get; set; }
        public string? NameReduzido { get; set; }
        public bool Agree { get; set; }
        public IFormFile File { get; set; }

    }
}
