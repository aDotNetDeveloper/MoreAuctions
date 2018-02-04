using System.Threading.Tasks;

namespace MoreAuctions
{
    public interface IEmailSender
    {
        Task Send(string subject, string body);
    }
}