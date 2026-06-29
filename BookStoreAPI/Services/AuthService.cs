using BookStoreAPI.Helpers;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStoreAPI.Services
{
    public class AuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtHelper _jwtHelper;

        public AuthService(UserManager<AppUser> userManager, JwtHelper jwtHelper)
        {
            _userManager = userManager;
            _jwtHelper = jwtHelper;
        }

        public async Task<AuthResultDto> Register(RegisterDto dto)
        {
            
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                throw new Exception("Email already exists.");
            }

            var user = new AppUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }

            
            await _userManager.AddToRoleAsync(user, "Customer");

            var token = _jwtHelper.GenerateToken(user, "Customer");

            return new AuthResultDto
            {
                Token = token,
                Email = user.Email!,
                FullName = user.FirstName + " " + user.LastName,
                Role = "Customer"
            };
        }

        public async Task<AuthResultDto> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                throw new Exception("Invalid email or password.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "Customer";

            var token = _jwtHelper.GenerateToken(user, role);

            return new AuthResultDto
            {
                Token = token,
                Email = user.Email!,
                FullName = user.FirstName + " " + user.LastName,
                Role = role
            };
        }
    }
}
