namespace SistemaBico.Web.Models
{
    public class ApplicationUser
    {
        public string PhoneNumber { get; set; }
        public Guid ClientId { get; set; }
        public ClientProfile Client { get; set; }
    }
}
