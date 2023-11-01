namespace IMS.Api.Helpers.Settings;
public class GitlabSetting 
{
    public const string Gitlab = "Gitlab";
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string AuthUri { get; set; }
    public string TokenUri { get; set; }
    public string UserUri { get; set; }
    public string GrantType { get; set; }
    public string RedirectUrl { get; set; }
}
