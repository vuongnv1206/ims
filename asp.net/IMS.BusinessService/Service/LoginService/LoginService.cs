using AutoMapper;
using IMS.BusinessService.IService.ILoginService;
using IMS.Contract.Common;
using IMS.Contract.Systems.Settings;
using IMS.Contract.Systems.Tokens;
using IMS.Domain.Systems;
using IMS.Infrastructure.EnityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IMS.BusinessService.Service.LoginService;

public class LoginService : ServiceBase, ILoginService
{
    private readonly AppSetting appSetting;
    public LoginService(
        IMSDbContext context,
        IMapper mapper,
        IOptions<AppSetting> appSetting) 
        : base(context, mapper)
    {
        this.appSetting = appSetting.Value;
    }

    public Token GetFromToken(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("Token Invalided.");
        }

        var jwtSecurityToken = new JwtSecurityToken(jwtEncodedString: token);
        var email = jwtSecurityToken.Claims.First(x => x.Type == "email").Value;
        var fullName = jwtSecurityToken.Claims.First(x => x.Type == "name").Value;
        var nickName = jwtSecurityToken.Claims.First(x => x.Type == "nickname").Value;


        if (string.IsNullOrEmpty(email))
        {
            throw new Exception("Email Invalided.");
        }

        var user = context.Users.FirstOrDefault(x => x.Email == email);

        var createToken = CreateToken(user);
        return createToken;
    }

    private Token CreateToken(AppUser user)
    {
        if (user == null) return null;

        var tokenHandle = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(appSetting.JWT.Secret);
        var expires = DateTime.UtcNow.AddSeconds(appSetting.JWT.Timeout);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(MyClaimType.UserAccount, user.UserName),
                new Claim(MyClaimType.UserId, user.Id.ToString()),
                new Claim(MyClaimType.FullName, user.FullName),
            }),
            Expires = expires,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandle.CreateToken(tokenDescriptor);
        var userMap = mapper.Map<Token.UserDto>(user);

        return new Token()
        {
            AccessToken = tokenHandle.WriteToken(token),
            Expire = expires,
            User = userMap,
        };
    }
}
