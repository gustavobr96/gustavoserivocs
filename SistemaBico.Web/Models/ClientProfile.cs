using SistemaBico.Web.Enum;

namespace SistemaBico.Web.Models
{
    public class ClientProfile
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public byte[] PerfilPicture { get; set; }
        public string Email { get; set; }
        public TypePeople TypePeople { get; set; }
        public string CpfCnpj { get; set; }
        public bool IsServiceProvider { get; set; }
        public DateTime? Cancellation { get; set; }
        public bool Enable { get; set; }
    }
}
