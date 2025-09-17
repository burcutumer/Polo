using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Data.Domain;
using API.Data.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _secretKey;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthService(IConfiguration config, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _secretKey = config["Jwt:Secret"];
            _userManager = userManager;
            _signInManager = signInManager;
        }

        private static string GenerateJwtToken(string secretKey, int expireMinutes, List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(expireMinutes),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<LoginResponseDto> CheckUserCredentials(LoginRequestDto loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);

            if (user == null)
            {
                return new LoginResponseDto { Error = "User not found" };
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);

            if (!result.Succeeded)
            {
                return new LoginResponseDto { Error = "Invalid password" };
            }
       //     var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginRequest.Email)
            };
         //   claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var token = GenerateJwtToken(_secretKey, 10, claims);

            return new LoginResponseDto { JwtToken = token };
        }
    }
}