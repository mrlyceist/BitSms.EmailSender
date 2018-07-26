using NLog;
using SmsCucumber.Sdk.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BitSms.EmailSender
{
    public class EmailClient : IEmailClient
    {
        private readonly ILogger logger;
        private readonly IValidator validator;

        public EmailClient(ILogger logger, IValidator validator)
        {
            this.logger = logger;
            this.validator = validator;
        }

        public async Task<Status> Send(Message message, Config config)
        {
            if (string.IsNullOrWhiteSpace(config.AddressFrom))
            {
                logger.Error("Misconfigured email plugin: no 'from' address provided");
                return Status.DeliveryError;
            }
            var sendFrom = validator.ValidateAddress(config.AddressFrom, logger);
            var sendTo = validator.ValidateAddress(message.Recipient, logger);
            if (sendTo == null || sendFrom == null)
                return Status.DeliveryError;

            var mail = new MailMessage(sendFrom, sendTo);
            var credentials = new NetworkCredential(config.Login, config.Password);
            var client = new SmtpClient
            {
                Port = config.Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Host = config.HostAddress,
                Credentials = credentials,
                EnableSsl = config.NeedsSsl
            };
            mail.Subject = config.DefaultMessageSubject;
            mail.Body = message.Text;

            try
            {
                await client.SendMailAsync(mail);
            }
            catch (Exception e)
            {
                logger.Warn($"Email send failed: {e.Message}");
            }
            return Status.Sent;
        }
    }
}