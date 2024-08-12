using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Request;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Model.Response;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _config;

        public LoginController(UserManager<User> userManager,
                               RoleManager<Role> roleManager,
                               IConfiguration configuration) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var user = await _userManager.FindByNameAsync(loginRequest.UserName);

            var passwordValid = await _userManager.CheckPasswordAsync(user!, loginRequest.Password);

            if (user == null || !passwordValid)
                return BadRequest("UserName or Password is incorrect!");

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, loginRequest.UserName),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var rolesFromDB = await _userManager.GetRolesAsync(user);
            foreach (var role in rolesFromDB)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecurityKey"]!));

            var token = new JwtSecurityToken(
                    issuer: _config["JWT:ValidIssuer"],
                    audience: _config["JWT:ValidAudience"],
                    expires: DateTime.UtcNow.AddMinutes(20),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha512Signature)
            );
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Dang nhap thanh cong",
                Data = new JwtSecurityTokenHandler().WriteToken(token) 
            }); ;
        }
    }
}
