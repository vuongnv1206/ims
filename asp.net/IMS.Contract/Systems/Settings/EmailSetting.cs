namespace IMS.Contract.Systems.Settings;

public class EmailSetting
{
    public const string EmailConfig = "Email";
    public string Gmail { get; set; }
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
