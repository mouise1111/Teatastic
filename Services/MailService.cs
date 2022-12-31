using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using NETCore.MailKit.Infrastructure.Internal;

namespace Teatastic.Services
{
    public class MailKitEmailSender : IEmailSender
    {
        public MailKitEmailSender(IOptions<MailKitOptions> options)
        {
            this.Options = options.Value;
        }

        public MailKitOptions Options { get; set; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(email, subject, message);
        }

        public Task Execute(string to, string subject, string message)
        {
            // create message
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(Options.SenderEmail);
            if (!string.IsNullOrEmpty(Options.SenderName))
                email.Sender.Name = Options.SenderName;
            email.From.Add(email.Sender);
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = message };

            // send email
            using (var smtp = new SmtpClient())
            {
                smtp.Connect(Options.Server, Options.Port, Options.Security);
                smtp.Authenticate(Options.Account, Options.Password);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            return Task.FromResult(true);
        }
    }
}

