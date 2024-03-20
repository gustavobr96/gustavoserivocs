namespace Sistema.Bico.Domain.Entities
{
    public class Address : Base
    {
        public string Logradouro { get; set; }
        public string Number { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public void UpdateAddress(string logradouro, string number, string bairro, string complemento, string zipCode, string city, string state)
        {
            Logradouro = logradouro;
            Number = number;
            Bairro = bairro;
            Complemento = complemento;
            ZipCode = zipCode;
            City = city;
            State = state;
        }
    }
}
