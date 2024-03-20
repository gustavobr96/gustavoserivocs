using MercadoPago.Resource.Payment;
using Newtonsoft.Json;
using Sistema.Bico.Domain.Integration.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Integration
{
    public class MercadoPagoIntegration : IMercadoPagoIntegration
    {
        public const string URL = "https://api.mercadopago.com/v1/";
        public const string TOKEN = "APP_USR-6586471225811590-030913-a2ec9e04767fccea1e46b9a631cf71c4-1326811828";

        public async Task<Payment> GetPayment(long id)
        {
    
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string urlPayment = URL + "payments/" + id;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                    var response = await client.GetStringAsync(urlPayment);
                    var result = JsonConvert.DeserializeObject<Payment>(response);
                    return result;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
