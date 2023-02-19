using LibraryManagementSystemAPI.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystemAPI.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace LibraryManagementSystemAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly IConfiguration  _configuration;

        public AccountController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login(UserLogin userLogin)
        {

            var userData = _userRepository.AuthenticateUser(userLogin.Name, userLogin.Password);

            if (userData != null)
            {

                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier,Convert.ToString(userData.Id)),
                    new Claim(ClaimTypes.Name,userData.Name),
                    new Claim(ClaimTypes.Email,userData.Email),

                };

                var securityToken = new JwtSecurityToken(claims:claims,expires:DateTime.Now.AddMinutes(15),signingCredentials:credentials);
                userData.Token = new JwtSecurityTokenHandler().WriteToken(securityToken);

                return Ok(userData);

            }
            else
                return NotFound("Username or Passord is incorrect!");

        }

    }
}
