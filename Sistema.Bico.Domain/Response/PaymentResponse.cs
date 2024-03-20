namespace Sistema.Bico.Domain.Response
{
    public class PaymentResponse
    {
        public string Id { get; set; }
        public string token { get; set; }
        public string issuer_id { get; set; }
        public string payment_method_id { get; set; }
        public decimal transaction_amount { get; set; }
        public string cardholderName { get; set; }
        public int installments { get; set; }
        public PayerResponse payer { get; set; }
    }
}
