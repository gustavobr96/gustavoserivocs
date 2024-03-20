namespace SistemaBico.Web.Models
{
    public class Worker
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public double Price { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string About { get; set; }
        public string Profession { get; set; }
        public Guid ClientId { get; set; }
        public Guid AddressId { get; set; }
        public Guid ProfessionalAreaId { get; set; }
        public bool IsConcluded { get; set; } = false;
        public virtual Client Client { get; set; }
        public virtual Address Address { get; set; }
        public virtual ProfessionalArea ProfessionalArea { get; set; }
       
    }
}
