using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;
using Travel.Application.Common.Exceptions;
using Travel.Application.Common.Interfaces;
using Travel.Application.Dtos.Email;
using Travel.Domain.Settings;
using MailKit.Net.Smtp;


namespace Travel.Shared.Services
{
    public class EmailService : IEmailService
    {
        private MailSettings MailSettings { get; }
        private ILogger<EmailService> Logger { get; }

        public EmailService(IOptions<MailSettings> mailSettings, ILogger<EmailService> logger)
        {
            MailSettings = mailSettings.Value;
            Logger = logger;
        }

        public async Task SendAsync(EmailDto emailRequest)
        {
            try
            {
                // create message
                var email = new MimeMessage { Sender = MailboxAddress.Parse(emailRequest.From ?? MailSettings.EmailFrom) };
                email.To.Add(MailboxAddress.Parse(emailRequest.To));
                email.Subject = emailRequest.Subject;
                var builder = new BodyBuilder { HtmlBody = emailRequest.Body };
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (System.Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }
    }
}
