using NLog;
using System;
using System.Net.Mail;

namespace BitSms.EmailSender
{
    public class Validator : IValidator
    {

        public void LogAndThrow(ILogger logger, string attribute)
        {
            var message = $"{attribute} must not be empty";
            logger.Error(message);
            throw new ApplicationException(message);
        }

        public MailAddress ValidateAddress(string address, ILogger logger)
        {
            try
            {
                var mailAddress = new MailAddress(address);
                return mailAddress;
            }
            catch
            {
                logger.Warn($"{address} is not a valid email address");
                return null;
            }
        }
    }
}