namespace IMS.Api.Helpers.Settings;

public class AppSetting
{
    public const string AppSettings = "AppSettings";
    public bool IsDebug { get; set; }
    public JWTConfig JWT { get; set; }
    public string ServerKey { get; set; }
    public string SenderKey { get; set; }
    public string ClientName { get; set; }
    public string ClientId { get; set; }
    public class JWTConfig
    {
        public string Secret { get; set; }
        public int Timeout { get; set; }
    }
}
