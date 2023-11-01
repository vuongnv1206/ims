﻿using IMS.Api.Dtos.Authentications;
using IMS.Api.Helpers.Tokens;
using IMS.Api.Models.Entities;
using IMS.Contract.Common.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace IMS.Api.Interfaces
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
