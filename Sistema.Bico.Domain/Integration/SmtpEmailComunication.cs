using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Sistema.Bico.Domain.Generics.Extensions;
using Sistema.Bico.Domain.Integration.Interfaces;
using Sistema.Bico.Domain.Response;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Integration
{
    public class SmtpEmailComunication : ISmtpEmailComunication
    {
        private  string SMTP_SERVER = "smtp.hostinger.com";
        private  int SMTP_PORT = 587;
        private  string EMAIL_CREDENTIAL = "contato@bicoservicos.com.br";
        private  string PASSWORDCREDENTIAL = "#Steelseries2024";
        private  bool SSLENABLE = true;

        //public SmtpEmailComunication(IConfiguration configuration)
        //{
        //    // Ler as configurações do arquivo settings.json
        //    var emailSettings = configuration.GetSection("Email");
      
        //    SMTP_SERVER = emailSettings["SmtpServer"];
        //    SMTP_PORT = int.Parse(emailSettings["SmtpPort"]);
        //    EMAIL_CREDENTIAL = emailSettings["Email"];
        //    PASSWORDCREDENTIAL = emailSettings["Password"];
        //    SSLENABLE = bool.Parse(emailSettings["Ssl"]);

        //    Log.Information("emailSettings: {@emailSettings}", emailSettings);
        //}

        public async Task<Unit> SendEmail(EmailDto request, CancellationToken cancellationToken = default)
        {
            try
            {

                MailAddress sender = new(EMAIL_CREDENTIAL);
                MailAddress destinatario = new(request.To.ToSeparatedString(','));

                MailMessage message = new(sender, destinatario)
                {
                    Subject = request.Subject,
                    Body = request.MessageBody,
                    IsBodyHtml = true
                };

                if (request.Cc.IsNotNullOrEmpty())
                    message.CC.Add(new MailAddress(request.Cc.ToSeparatedString(',')));

                if (request.Bcc.IsNotNullOrEmpty())
                    message.Bcc.Add(new MailAddress(request.Bcc.ToSeparatedString(',')));

                if (request.EmailAttachments.IsNotNullOrEmpty())
                    request.EmailAttachments.ForEach(a => message.Attachments.Add(new Attachment(new MemoryStream(Convert.FromBase64String(a.Content)), a.FileName)));

                SmtpClient smtpClient = new(SMTP_SERVER, SMTP_PORT)
                {
                    Credentials = new NetworkCredential(EMAIL_CREDENTIAL, PASSWORDCREDENTIAL),
                    EnableSsl = SSLENABLE
                };

                Log.Information("message: {@message}", message);
                await smtpClient.SendMailAsync(message, cancellationToken);
                return Unit.Value;
            }
            catch(Exception e)
            {
                Log.Error($"SendEmail Error: {e.Message}");
                Log.Information($"SendEmail Error: {e.Message}");
                return Unit.Value;
            }
           
        }
    }
}
