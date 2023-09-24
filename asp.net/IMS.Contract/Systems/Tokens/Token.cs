using IMS.Contract.Systems.Users;

namespace IMS.Contract.Systems.Tokens;

public class Token
{
    public string AccessToken { get; set; }
    public DateTime Expire { get; set; }
    public UserDto User { get; set; }
   
}
