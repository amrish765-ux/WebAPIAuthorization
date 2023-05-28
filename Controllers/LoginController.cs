using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPIAuthorization.Model;

namespace WebAPIAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost,Route("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDTO.username)||string.IsNullOrEmpty(loginDTO.password))
                {
                    return BadRequest("username and/or password not specified");
                }
                if (loginDTO.username.Equals("joydip") && loginDTO.password.Equals("joydip123"))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisisasecretkey@123"));
                    var sigingcredentials=new SigningCredentials(secretKey,SecurityAlgorithms.HmacSha256);
                    var jsonsecuritytoken = new JwtSecurityToken(
                        issuer: "ABCXYZ",
                        audience: "http://localhost:51398",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials:sigingcredentials);
                    Ok(new JwtSecurityTokenHandler().WriteToken(jsonsecuritytoken));
                }
            }
            catch (Exception)
            {

                return BadRequest("an error occurred in generating the token");
            }
            return Unauthorized();
        }
    }
}
