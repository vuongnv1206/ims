using IMS.Contract.Common.Responses;
using IMS.Contract.Systems.Authentications;
using IMS.Contract.Systems.Authentications.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.APIControllers.Systems
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : BaseController
	{
		private readonly IAuthService _authService;
		public AuthController(IAuthService authenticationService)
		{
			_authService = authenticationService;
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
