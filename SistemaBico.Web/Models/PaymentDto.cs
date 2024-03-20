namespace SistemaBico.Web.Models
{
    public class PaymentDto
    {
        public string Id { get; set; }
        public string token { get; set; }
        public string issuer_id { get; set; }
        public string payment_method_id { get; set; }
        public string cardholderName { get; set; }
        public decimal transaction_amount { get; set; }
        public int installments { get; set; }
        public PayerDto payer { get; set; }
    }
}
