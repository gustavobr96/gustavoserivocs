namespace SistemaBico.Web.Models
{
    public class WorkerDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public double Price { get; set; }
        public string PriceString { get; set; }
        public string? Titulo { get; set; }
        public string? Phone { get; set; }
        public int Area { get; set; }
        public string? Sobre { get; set; }
        public string? Profession { get; set; }
        public string? CEP { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int? Interessados { get; set; }
    }
}
