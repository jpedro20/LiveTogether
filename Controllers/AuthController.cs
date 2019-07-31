using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using LiveTogether.Utils.Configuration;
using LiveTogether.Data.Repositories;
using LiveTogether.Models.Dto;

namespace LiveTogether.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRep;


        public AuthController(IUserRepository userRep, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _userRep = userRep;
        }


        [HttpPost("[action]")]
        public IActionResult Login([FromBody]AuthDto authUser)
        {
            var user = _userRep.Authenticate(authUser.Username, authUser.Password);

            if(user == null) {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = signinCredentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new AuthUserDto {
                Name = user.Name,
                Token = tokenString
            });
        }
    }
}