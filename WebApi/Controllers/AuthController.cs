using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTO.AuthModels;
using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUserServices _userServices;

        private readonly IConfiguration _config;

        public AuthController(IUserServices userServices, IConfiguration configuration)
        {
            _userServices = userServices;
            _config = configuration;
        }

        [HttpPost("Login")]
        
        public async Task<ActionResult<string>> Login([FromBody] LoginModel user)
        {
            UserDTO loggedInUser = await _userServices.ValidateUser(user);

            if (loggedInUser != null)
            {
                var token = GenerateAccessToken(loggedInUser);
                return Ok(token);
            }
            else
            {
                throw new Exception("Invalid details");
            }
            throw new Exception("Failed to Login");
        }

        private string GenerateAccessToken(UserDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddDays(2),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterModel>> Register([FromBody] RegisterModel user)
        {
            if(await _userServices.RegisterUser(user))
            {
                return Created(nameof(Register), user);
            } 
            throw new Exception("Failed to Add User");
        }
    }
}