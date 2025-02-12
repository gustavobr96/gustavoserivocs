using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Sistema.Bico.Domain.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Service
{
    public class FirebaseNotificationService : IFirebaseNotificationService
    {
        private readonly string _projectId;
        private readonly string _firebaseCredentialsPath;
        private readonly HttpClient _httpClient;

        public FirebaseNotificationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _projectId = configuration["Firebase:ProjectId"]; // ID do projeto
            _firebaseCredentialsPath = configuration["Firebase:CredentialsPath"]; // Caminho do JSON
        }
        private async Task<string> GetAccessTokenAsync()
        {
            try {
                GoogleCredential credential = GoogleCredential.FromFile(_firebaseCredentialsPath)
                   .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");
                var token = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
                return token;
            }
            catch (Exception e) {
                return "";
            }
           
        }
        public async Task EnviarNotificacaoParaProfissionais(List<string> tokens, string titulo, string mensagem)
        {
            try 
            {

                if (tokens == null || tokens.Count == 0) return;

                string accessToken = await GetAccessTokenAsync();
                string url = $"https://fcm.googleapis.com/v1/projects/{_projectId}/messages:send";

                var payload = new
                {
                    message = new
                    {
                        token = tokens[0], // Para múltiplos tokens, precisa enviar um por um
                        notification = new
                        {
                            title = titulo,
                            body = mensagem
                        },
                        data = new
                        {
                            click_action = "FLUTTER_NOTIFICATION_CLICK",
                            route = "/trabalhosTab"
                        }
                    }
                };

                string jsonPayload = JsonSerializer.Serialize(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();

                Console.WriteLine(result);
            }
            catch(Exception e)
            {

            }
           
        }
    }
}
