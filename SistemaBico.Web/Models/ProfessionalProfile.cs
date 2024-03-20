using SistemaBico.Web.Util;

namespace SistemaBico.Web.Models
{
    public class ProfessionalProfile
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Perfil { get; set; }
        public string Avaliation { get; set; }
        public byte[] PerfilPicture { get; set; }
        public Guid ClientId { get; set; }
        public Guid AddressId { get; set; }
        public Guid ProfessionalAreaId { get; set; }
        public string About { get; set; }
        public string Profession { get; set; }
        public List<ProfessionalEspeciality> Especiality { get; set; }
        public Client Client { get; set; }
        public Address Address { get; set; }
        public ProfessionalArea ProfissionalArea { get; set; }
      
    }
}
