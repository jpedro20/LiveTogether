using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
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
        private readonly IUsersRepository _usersRep;


        public AuthController(IUsersRepository usersRep, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _usersRep = usersRep;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody]AuthDto authUser)
        {
            var user = await _usersRep.Authenticate(authUser.Username, authUser.Password);

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
                Expires = DateTime.UtcNow.AddDays(3),
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