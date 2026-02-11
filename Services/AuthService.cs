using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudentEnrollmentApi.DTOs;
using StudentEnrollmentApi.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using StudentEnrollmentApi.Models; 

namespace StudentEnrollmentApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager; 
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDto model)
        {
            var user = new User { UserName = model.Username, Email = model.Email }; 
            return await _userManager.CreateAsync(user, model.Password);
        }

        public async Task<string?> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return null;

            return GenerateToken(user);
        }

        private string GenerateToken(User user) 
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}