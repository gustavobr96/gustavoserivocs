using System;

namespace Sistema.Bico.Domain.Response
{
    public class WebhookNotification
    {
        public string Action { get; set; }
        public string ApiVersion { get; set; }
        public WebhookData Data { get; set; }
        public DateTime DateCreated { get; set; }
        public long Id { get; set; }
        public bool LiveMode { get; set; }
        public string Type { get; set; }
        public long UserId { get; set; }
    }

    public class WebhookData
    {
        public long Id { get; set; }
    }
}
