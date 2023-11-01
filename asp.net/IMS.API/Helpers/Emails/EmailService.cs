using IMS.Contract.Systems.Settings;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace IMS.Api.Helpers.Emails;

public class EmailService : IEmailSender
{
    private readonly EmailSetting emailSettings;
    private readonly ILogger<EmailService> logger;


    public EmailService(
        IOptions<EmailSetting> emailSetting,
        ILogger<EmailService> logger)
    {
        emailSettings = emailSetting.Value;
        this.logger = logger;
        logger.LogInformation("Create SendMailService");
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage();
        message.Sender = new MailboxAddress(emailSettings.Username, emailSettings.Gmail);
        message.From.Add(new MailboxAddress(emailSettings.Username, emailSettings.Gmail));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = subject;

        var builder = new BodyBuilder();
        builder.HtmlBody = htmlMessage;
        message.Body = builder.ToMessageBody();

        // dùng SmtpClient của MailKit
        using var smtp = new MailKit.Net.Smtp.SmtpClient();

        try
        {
            smtp.Connect(emailSettings.SmtpServer, emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailSettings.Gmail, emailSettings.Password);
            await smtp.SendAsync(message);
        }
        catch (Exception ex)
        {
            // Gửi mail thất bại, nội dung email sẽ lưu vào thư mục mailssave
            Directory.CreateDirectory("mailssave");
            var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
            await message.WriteToAsync(emailsavefile);

            logger.LogInformation("Lỗi gửi mail, lưu tại - " + emailsavefile);
            logger.LogError(ex.Message);
        }

        smtp.Disconnect(true);

        logger.LogInformation("Send email to " + email);
    }
}
