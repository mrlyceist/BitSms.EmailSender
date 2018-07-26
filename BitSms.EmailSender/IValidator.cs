using NLog;
using System.Net.Mail;

namespace BitSms.EmailSender
{
    public interface IValidator
    {
        void LogAndThrow(ILogger logger, string attribute);
        MailAddress ValidateAddress(string address, ILogger logger);
    }
}