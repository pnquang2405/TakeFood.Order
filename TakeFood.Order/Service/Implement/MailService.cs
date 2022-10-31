using StoreService.Model.Content;
using StoreService.Settings;
using MailKit.Security;
using MimeKit;

namespace Order.Service.Implement;

public class MailService : IMailService
{
    private readonly MailSettings mailSettings;


    // mailSetting được Inject qua dịch vụ hệ thống
    // Có inject Logger để xuất log
    public MailService(AppSetting settings)
    {
        mailSettings = settings.MailSettings;
    }

    // Gửi email, theo nội dung trong mailContent
    public async Task SendMail(MailContent mailContent)
    {
        var email = new MimeMessage();
        email.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
        email.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
        email.To.Add(MailboxAddress.Parse(mailContent.To));
        email.Subject = mailContent.Subject;


        var builder = new BodyBuilder();
        builder.HtmlBody = mailContent.Body;
        email.Body = builder.ToMessageBody();

        // dùng SmtpClient của MailKit
        using var smtp = new MailKit.Net.Smtp.SmtpClient();

        try
        {
            smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
            await smtp.SendAsync(email);
        }
        catch (Exception ex)
        {
            // Gửi mail thất bại, nội dung email sẽ lưu vào thư mục mailssave
            System.IO.Directory.CreateDirectory("mailssave");
            var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
            await email.WriteToAsync(emailsavefile);
        }

        smtp.Disconnect(true);
    }
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        await SendMail(new MailContent()
        {
            To = email,
            Subject = subject,
            Body = htmlMessage
        });
    }
}
