using Foody.Application.Services.EmailServices.Dtos;

namespace Foody.Application.Services.EmailServices
{
    public interface IEmailSenderService
    {
        Task SendMail(MailContent mailContent);

        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
