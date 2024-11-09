using AFF_BE.Core.Models.Mail;

namespace AFF_BE.Api.Services.Interfaces
{
    public interface IMailService
    {
        Task<bool> SendMail(SendMailRequest request);

    }
}
