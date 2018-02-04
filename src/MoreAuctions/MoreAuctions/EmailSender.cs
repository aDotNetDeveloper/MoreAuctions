using Microsoft.Extensions.Options;
using MoreAuctions.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MoreAuctions
{
    public class EmailSender : IEmailSender
    {
        private SmtpSettings _settings;
        private MailAddress _fromAddress;

        public EmailSender(IOptions<SmtpSettings> settings)
        {
            _settings = settings.Value;
            if (!string.IsNullOrEmpty(_settings.SourceEmailAddress))
                _fromAddress = new MailAddress(_settings.SourceEmailAddress);
        }

        public async Task Send(string subject, string body)
        {
            using (var client = new SmtpClient(_settings.SmtpServer, _settings.SmtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_settings.User, _settings.Pwd);
                var mailMessage = new MailMessage
                {
                    From = _fromAddress
                };
                mailMessage.To.Add(_settings.DestinationEmailAddress);
                mailMessage.Body = body;
                mailMessage.Subject = subject;
                await client.SendMailAsync(mailMessage);
            }
        }
    }
}