using IMS.Contract.Common.Responses;
using IMS.Contract.Systems.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Systems.Authentications
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(LoginModel input);
        Task Register(RegisterModel input);
        Task ForgotPassword(string email);
        Task<Token> GetFromTokenAsync(string token);

    }
}
