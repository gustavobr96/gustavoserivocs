using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace SistemaBico.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("v{version:apiVersion}/api/WebSocket")]
    public class PaymentWebSocketController : ControllerBase
    {
        private static readonly Dictionary<string, WebSocket> ActiveConnections = new();

        [HttpGet("pagamento/{clientId}")]
        public async Task Get(string clientId)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                ActiveConnections[clientId] = webSocket;

                while (webSocket.State == WebSocketState.Open)
                {
                    await Task.Delay(1000);
                }

                ActiveConnections.Remove(clientId);
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }

        public static async Task NotifyClientAsync(string clientId, string status)
        {
            var key = $"clientId={clientId}"; // Adiciona o prefixo para buscar no dicionário

            if (ActiveConnections.TryGetValue(key, out var webSocket) && webSocket.State == WebSocketState.Open)
            {
                var buffer = Encoding.UTF8.GetBytes(status);
                await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}
