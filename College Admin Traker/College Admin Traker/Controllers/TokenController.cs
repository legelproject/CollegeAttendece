using ClassLibrary.Dtos;
using ClassLibrary.Models;
using College_Admin_Traker.Dbcontext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace College_Admin_Traker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly CollegeDbContext collegeDbContext;
        private readonly IConfiguration configuration;

        public TokenController(CollegeDbContext collegeDbContext,IConfiguration configuration)
        {
            this.collegeDbContext = collegeDbContext;
            this.configuration = configuration;
        }
        [HttpPost]
        public IActionResult AdminLogin([FromBody] LoginDto loginDto)
        {
            var result = collegeDbContext.Users.FirstOrDefault(p => p.UserName == loginDto.UserName && p.Password == loginDto.Password);
            var tokenDto = new TokenDto();

            if (result != null)
            {
                var claims = new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, loginDto.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: configuration["JwtSettings:Issuer"],
                    audience: configuration["JwtSettings:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                    );

                string tokenKey = new JwtSecurityTokenHandler().WriteToken(token);
                if (tokenKey == null)
                {
                    return Unauthorized();
                }

                tokenDto.Tokens = tokenKey;
                tokenDto.RoleId = result.RoleId;
                tokenDto.UsersId = result.UsersId;
                tokenDto.UserName = result.UserName;

                // LoginTokenResponse loginTokenResponse = new LoginTokenResponse() { Token = tokenKey };
            }
            return Ok(tokenDto);
        }
    }
}

