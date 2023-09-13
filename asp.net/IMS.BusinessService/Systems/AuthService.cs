using IMS.BusinessService.Common;
using IMS.BusinessService.Constants;
using IMS.Contract.Common.Responses;
using IMS.Contract.Systems.Authentications;
using IMS.Contract.Systems.Authentications.Interfaces;
using IMS.Domain.Systems;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BusinessService.Systems
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly JwtSetting _jwtSettings;

		public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
			IOptions<JwtSetting> jwtSettings)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_jwtSettings = jwtSettings.Value;
		}
		public async Task Register(RegisterModel input)
		{
			var existingUser = await _userManager.FindByNameAsync(input.Username);

			if (existingUser != null)
			{
				throw new Exception($"Username '{input.Username}' already exists.");
			}
			//Add user into db
			var user = new AppUser
			{
				Email = input.Email,
				UserName = input.Username,
				SecurityStamp = Guid.NewGuid().ToString(),
				//EmailConfirmed = true
			};

			var existingEmail = await _userManager.FindByEmailAsync(input.Email);
			if (existingEmail == null)
			{
				var result = await _userManager.CreateAsync(user, input.Password);

				if (result.Succeeded)
				{
					await _userManager.AddToRoleAsync(user, "User");
				}
				else
				{
					throw new Exception($"{result.Errors}");
				}
			}
			else
			{
				throw new Exception($"Email {input.Email} already exists.");
			}
		}

		public async Task<AuthResponse> Login(LoginModel input)
		{
			//Checking the user
			var user = await _userManager.FindByNameAsync(input.Username);

			//Checking the password
			if (user != null && await _userManager.CheckPasswordAsync(user, input.Password))
			{
				JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

				AuthResponse response = new AuthResponse
				{
					Id = user.Id,
					Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
					Email = user.Email,
					UserName = user.UserName
				};

				return response;
			}
			else
			{
				throw new Exception($"Credentials for '{input.Username} aren't valid'.");

			}
		}

		private async Task<JwtSecurityToken> GenerateToken(AppUser user)
		{
			var userClaims = await _userManager.GetClaimsAsync(user);
			var roles = await _userManager.GetRolesAsync(user);

			var roleClaims = new List<Claim>();

			for (int i = 0; i < roles.Count; i++)
			{
				roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
			}

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(CustomClaimType.Uid, user.Id.ToString()),
				//new Claim(JwtRegisteredClaimNames.Email, user.Email),
			}
			.Union(userClaims)
			.Union(roleClaims);

			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
			var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

			var jwtSecurityToken = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				audience: _jwtSettings.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
				signingCredentials: signingCredentials);
			return jwtSecurityToken;
		}

		public Task ForgotPassword(string email)
		{
			throw new NotImplementedException();
		}
	}

}
