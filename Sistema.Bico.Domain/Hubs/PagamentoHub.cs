using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Hubs
{
    public class PaymentHub : Hub
    {
        private static readonly Dictionary<string, string> UserConnections = new();

        public override Task OnConnectedAsync()
        {
            // Associa o UserId ao ConnectionId
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();
            UserConnections[userId] = Context.ConnectionId;

            Console.WriteLine($"Usuário {userId} conectado com ConnectionId {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            // Remove a conexão do usuário quando ele desconectar
            var userId = UserConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (userId != null)
            {
                UserConnections.Remove(userId);
                Console.WriteLine($"Usuário {userId} desconectado");
            }

            return base.OnDisconnectedAsync(exception);
        }

        public static string? GetConnectionId(string userId)
        {
            return UserConnections.ContainsKey(userId) ? UserConnections[userId] : null;
        }

        public static Dictionary<string, string> GetConnection()
        {
            return UserConnections;
        }
    }
}
