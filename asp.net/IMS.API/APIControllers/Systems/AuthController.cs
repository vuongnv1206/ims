using IMS.Contract.Common.Requests.LoginRequest;
using IMS.Contract.Common.Responses;
using IMS.Contract.Common.Responses.LoginResponse;
using IMS.Contract.Systems.Authentications;
using IMS.Contract.Systems.Settings;
using IMS.Domain.Systems;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;

namespace IMS.Api.APIControllers.Systems
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		private readonly IAuthService authService;
        private readonly HttpClient httpClient;
        private readonly GitlabSetting gitlabSetting;

        public AuthController(
			IAuthService authenticationService,
			IAuthService authService,
            HttpClient httpClient,
            IOptions<GitlabSetting> gitlabSetting)
		{
			_authService = authenticationService;

			this.authService = authService;
            this.httpClient = httpClient;
			this.gitlabSetting = gitlabSetting.Value;
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
		[HttpPost("authenWithOauth2")]
        public async Task<IActionResult> AuthenWithOauth2(OauthRequest request)
		{
            var baseAddress = gitlabSetting.TokenUri;
            var clientId = gitlabSetting.ClientId;
            var clientSecret = gitlabSetting.ClientSecret;
            var grantType = gitlabSetting.GrantType;
            var redirectUrl = gitlabSetting.RedirectUrl;

            var form = new Dictionary<string, string>
            {
                { "grant_type", grantType },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "code", request.Code },
                { "redirect_uri", redirectUrl },
            };

            HttpResponseMessage tokenResponse =
                await httpClient.PostAsync(
                    new Uri(baseAddress),
                    new FormUrlEncodedContent(form));

            var jsonContent = await tokenResponse.Content.ReadAsStringAsync();
            TokenResponse tok = JsonConvert.DeserializeObject<TokenResponse>(jsonContent);
            var result = await authService.GetFromTokenAsync(tok.IdToken);
            return Ok(result);
        }

    }
}
