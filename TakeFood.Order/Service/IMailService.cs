using Order.Model.Content;

namespace Order.Service;

public interface IMailService
{
    Task SendMail(MailContent mailContent);

    Task SendEmailAsync(string email, string subject, string htmlMessage);
}
