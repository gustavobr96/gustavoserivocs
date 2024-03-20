namespace Sistema.Bico.Domain.Response
{
    public class PayerResponse
    {
        public string email { get; set; }
        public string first_name { get; set; }
        public IdentificationResponse identification { get; set; }
    }
}
