using SmsCucumber.Sdk.Models;
using System.Threading.Tasks;

namespace BitSms.EmailSender
{
    public interface IEmailClient
    {
        Task<Status> Send(Message message, Config config);
    }
}