using IMS.BusinessService.Constants;
using IMS.BusinessService.Extension;
using IMS.Contract.Common.Responses;
using IMS.Contract.Systems.Authentications;
using IMS.Contract.Systems.Roles;
using IMS.Domain.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using static IMS.BusinessService.Constants.Permissions;

namespace IMS.Api.APIControllers.Systems
{
    [Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<AppRole> _roleManager;

		public AuthController(IAuthService authenticationService, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
		{
			_authService = authenticationService;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		[HttpPost("login")]
		[ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
		public async Task<ActionResult<AuthResponse>> Login(LoginModel request)
		{
			return Ok(await _authService.Login(request));
		}

		[HttpPost("register")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> Register(RegisterModel request)
		{
			try
			{
				// Code xử lý đăng ký người dùng
				await _authService.Register(request);

				// Trả về kết quả thành công nếu không có lỗi
				return Ok(new { message = "Registration successful" });
			}
			catch (Exception ex)
			{
				// Xử lý lỗi và trả về trạng thái lỗi
				return BadRequest(new { message = ex.Message });
			}
		}

		
	}
}
