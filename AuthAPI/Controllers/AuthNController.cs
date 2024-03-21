using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SS.Backend.Security;
using SS.Backend.Services.EmailService;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthNController : Controller
    {
        private readonly SSAuthService _authService;
        private readonly IConfiguration _config;

        public AuthNController(SSAuthService authService, IConfiguration config)
        {
            _authService = authService;
            _config = config;
        }

        [HttpPost("sendOTP")]
        public async Task<IActionResult> SendOTP([FromBody] AuthenticationRequest request)
        {
            var (otp, response) = await _authService.SendOTP_and_SaveToDB(request);

            if (response.HasError == false)
            {
                string targetEmail = request.UserIdentity;
                string subject = "Verfication Code";
                string msg = "Verification code: " + otp;
                await MailSender.SendEmail(targetEmail, subject, msg);
            }

            if (response.HasError)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(new { otp });
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest request)
        {
            var (principal, response) = await _authService.Authenticate(request);

            if (response.HasError)
            {
                return BadRequest(response.ErrorMessage);
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Ok(token);
        }
    }
}
