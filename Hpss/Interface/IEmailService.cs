using Hpss.Model;

namespace Hpss.Interface
{
    public interface IEmailService
    {
        Task SendEmail(MailRequest mailRequest);
    }
}
