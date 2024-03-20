namespace SistemaBico.Web.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Logradouro { get; set; }
        public string Number { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
