using IMS.Api.Models.Dtos.Users;

namespace IMS.Api.Common.Helpers.Tokens;

public class Token
{
    public string AccessToken { get; set; }
    public DateTime Expire { get; set; }
    public UserDto User { get; set; }

}
