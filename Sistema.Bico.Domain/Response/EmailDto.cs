using System.Collections.Generic;

namespace Sistema.Bico.Domain.Response
{
    public class EmailDto
    {
        public string From { get; set; }
        public List<string> To { get; set; }
        public List<string> Cc { get; set; }
        public List<string> Bcc { get; set; }
        public string Subject { get; set; }
        public string MessageBody { get; set; }
        public List<EmailAttachmentDto> EmailAttachments { get; set; }
    }
}
