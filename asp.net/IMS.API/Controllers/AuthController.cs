
using Google.Apis.Auth;
using IMS.Api.Common.Requests;
using IMS.Api.Common.Responses;
using IMS.Api.Dtos.Authentications;
using IMS.Api.Helpers.Settings;
using IMS.Api.Helpers.Tokens;
using IMS.Api.Interfaces;
using IMS.Api.Models.Entities;
using IMS.Contract.Common.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Net.Mail;

namespace IMS.Api.APIControllers
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
        private readonly GoogleSetting googleSetting;
        public AuthController(
            IOptions<GoogleSetting> googleSetting,
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
            this.googleSetting = googleSetting.Value;
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

                //emailSender.SendEmailAsync(user.Email, "Confirmation email link!", confirmationLink);

                var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
                {
                    Credentials = new NetworkCredential("9cd54bc71362df", "d47b83bca4ce59"),
                    EnableSsl = true
                };
                client.Send("swdgroup6@gmail.com", $"{user.Email}", "Confirmation email link!", confirmationLink);

                // Trả về kết quả thành công nếu không có lỗi
                return Ok(new { message = "Registration successful" });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về trạng thái lỗi
                return BadRequest(ex.Message);
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

        [HttpPost("LoginWithGoogle")]
        public async Task<IActionResult> LoginWithGoogle([FromBody] string credential)
        {
            var setting = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { googleSetting.ClientId }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(credential, setting);

            if (payload == null)
            {
                return BadRequest("Invalid External Google Authentication");
            }
            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");

            if (!authService.CheckMailSetting(payload.Email, @"@.*"))
            {
                return BadRequest($"{payload.Email} invalid");
            }

            var user = await userManager.FindByEmailAsync(payload.Email);

            if (user != null)
            {
                await userManager.AddLoginAsync(user, info);
            }
            else
            {
                user = new AppUser
                {
                    Email = payload.Email,
                    UserName = payload.Email,
                };
                await userManager.CreateAsync(user);
                await userManager.AddToRoleAsync(user, "User");
                await userManager.AddLoginAsync(user, info);
            }

            if (user == null)
                return BadRequest("Invalid External Authentication.");

            var jwtSecurityToken = await authService.GenerateToken(user);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Ok(new
            {
                token,
                username = user.UserName
            });
        }
    }
}
