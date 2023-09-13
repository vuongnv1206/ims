using IMS.Contract.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Systems.Authentications.Interfaces
{
    public interface IAuthService
    {
		Task<AuthResponse> Login(LoginModel input);
		Task Register(RegisterModel input);
		Task ForgotPassword(string email);

	}
}
