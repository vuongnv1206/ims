using Azure.Core;
using IMS.BusinessService.Common;
using IMS.BusinessService.Constants;
using IMS.BusinessService.Extension;
using IMS.Contract.Common.Responses;
using IMS.Contract.Systems.Authentications;
using IMS.Contract.Systems.Roles;
using IMS.Domain.Systems;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static IMS.BusinessService.Constants.Permissions;

namespace IMS.BusinessService.Systems
{
    public class AuthService : IAuthService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<AppRole> _roleManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly JwtSetting _jwtSettings;

		public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
			IOptions<JwtSetting> jwtSettings, RoleManager<AppRole> roleManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_jwtSettings = jwtSettings.Value;
			_roleManager = roleManager;
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
				EmailConfirmed = true
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
				//var result = await _signInManager.PasswordSignInAsync(input.Username, input.Password, true, true);
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
			var permissions = await this.GetPermissionsByUserIdAsync(user.Id.ToString());
			var roleClaims = new List<Claim>();

			for (int i = 0; i < roles.Count; i++)
			{
				roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
			}

			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, user.UserName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(CustomClaimType.Uid, user.Id.ToString()),
				new Claim(CustomClaimType.Permissions, JsonSerializer.Serialize(permissions)),
				new Claim(CustomClaimType.Uid, user.Id.ToString()),
				new Claim(CustomClaimType.Uid, user.Id.ToString()),
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
		private async Task<List<string>> GetPermissionsByUserIdAsync(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			var roles = await _userManager.GetRolesAsync(user);
			var permissions = new List<string>();

			var allPermissions = new List<RoleClaimDto>();
			if (roles.Contains(RoleDefault.Admin))
			{
				var types = typeof(Permissions).GetTypeInfo().DeclaredNestedTypes;
				foreach (var type in types)
				{
					allPermissions.GetPermissions(type);
				}
				permissions.AddRange(allPermissions.Select(x => x.Value));
			}
			else
			{
				foreach (var roleName in roles)
				{
					var role = await _roleManager.FindByNameAsync(roleName);
					var claims = await _roleManager.GetClaimsAsync(role);
					var roleClaimValues = claims.Select(x => x.Value).ToList();
					permissions.AddRange(roleClaimValues);
				}
			}
			return permissions.Distinct().ToList();
		}

		public Task ForgotPassword(string email)
		{
			throw new NotImplementedException();
		}
	}

}
