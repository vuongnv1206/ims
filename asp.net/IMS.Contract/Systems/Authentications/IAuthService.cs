﻿using IMS.Contract.Common.Responses;
using IMS.Contract.Systems.Tokens;
using IMS.Contract.Systems.Users;
using IMS.Domain.Systems;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Systems.Authentications
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(LoginModel input);
        Task<AppUser> Register(RegisterModel input);
        Task ForgotPassword(string email);
        Task<Token> GetFromTokenAsync(string token);
        Task<JwtSecurityToken> GenerateToken(AppUser user);
    }
}
