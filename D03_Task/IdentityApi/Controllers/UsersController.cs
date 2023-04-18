using IdentityApi.Data.Models;
using IdentityApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<Employee> userManager;

        public UsersController(IConfiguration configuration, UserManager<Employee> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }

        #region Register user
        [HttpPost]
        [Route("registerUser")]
        public async Task<ActionResult> RegisterAsUser(RegisterUserDto registerDto)
        {
            var EmployeeToAdd = new Employee
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Department = registerDto.Department,
            };
            var result = await userManager.CreateAsync(EmployeeToAdd, registerDto.Password);
            if (!result.Succeeded)
            {
                return Unauthorized(result.Errors);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, EmployeeToAdd.Id),
                new Claim(ClaimTypes.Role, EmployeeToAdd.Department),
            };
            await userManager.AddClaimsAsync(EmployeeToAdd, claims);
            return NoContent();
        }
        #endregion

        #region Register admin
        [HttpPost]
        [Route("registerAdmin")]
        public async Task<ActionResult> RegisterAsAdmin(RegisterAdminDtop registerDto)
        {
            var EmployeeToAdd = new Employee
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Department = "Management",
            };
            var result = await userManager.CreateAsync(EmployeeToAdd, registerDto.Password);
            if (!result.Succeeded)
            {
                return Unauthorized(result.Errors);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, EmployeeToAdd.Id),
                new Claim(ClaimTypes.Role, EmployeeToAdd.Department),
            };
            await userManager.AddClaimsAsync(EmployeeToAdd, claims);
            return NoContent();
        }
        #endregion


        #region 
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<TokenDto>> Login (LoginDto loginDto){
            var user = await userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
            {
                return NotFound();
            }
            var isAuthenticated = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isAuthenticated)
            {
                return Unauthorized();
            }

            var claims = await userManager.GetClaimsAsync(user);

            var secretKeyString = configuration.GetValue<string>("SecretKey") ?? string.Empty;
            var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKeyString);
            var secretKey = new SymmetricSecurityKey(secretKeyInBytes);

            //Combination SecretKey, HashingAlgorithm
            var signingCredentials = new SigningCredentials(secretKey, 
                SecurityAlgorithms.HmacSha256Signature);
            var expiry = DateTime.Now.AddDays(1);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expiry,
                signingCredentials: signingCredentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);
            return new TokenDto(tokenString, expiry);
        }
        #endregion

    }
}
