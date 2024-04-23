using MediatR;
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
        private const string SMTP_SERVER = "mail.workfree.com.br";
        private const int SMTP_PORT = 8889;
        private const string EMAIL_CREDENTIAL = "nao-responder@workfree.com.br";
        private const string PASSWORDCREDENTIAL = "@Steelseries2023";
        private const bool SSLENABLE = false;

        public async Task<Unit> SendEmail(EmailDto request, CancellationToken cancellationToken = default)
        {
            Log.Information("SendEmail {@request}", request);

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


            await smtpClient.SendMailAsync(message, cancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}
