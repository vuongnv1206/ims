using Firebase.Auth;
using IMS.Contract.Common.Requests.LoginRequest;
using IMS.Contract.Common.Responses;
using IMS.Contract.Common.Responses.LoginResponse;
using IMS.Contract.Systems.Authentications;
using IMS.Contract.Systems.Settings;
using IMS.Contract.Systems.Tokens;
using IMS.Domain.Systems;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly UserManager<AppUser> userManager;
        private readonly IEmailSender emailSender;
        public AuthController(
            IAuthService authenticationService,
            IAuthService authService,
            HttpClient httpClient,
            IEmailSender emailSender,
            UserManager<AppUser> userManager,
            IOptions<GitlabSetting> gitlabSetting)
		{
			_authService = authenticationService;
			this.authService = authService;
            this.httpClient = httpClient;
            this.gitlabSetting = gitlabSetting.Value;
            this.userManager = userManager;
            this.emailSender = emailSender;
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
				var user = await _authService.Register(request);
                //Add token to verify the email...
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                //Encode để có thể đính kèm nó trên địa chỉ url

                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { token, email = user.Email }, Request.Scheme);

                emailSender.SendEmailAsync(user.Email, "Confirmation email link!", confirmationLink);

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
        public async Task<ActionResult<Token>> AuthenWithOauth2(OauthRequest request)
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

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await userManager.ConfirmEmailAsync(user, token);
                return Ok("Successfully email");
            }
            return BadRequest();
        }
    }
}
