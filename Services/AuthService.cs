using BlueMediCore.Data;
using BlueMediCore.DTOs;
using BlueMediCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static BlueMediCore.DTOs.AuthDtos;


namespace BlueMediCore.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //Şifre Hashleme
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        // Register
        public async Task<ServiceResponse<string>> RegisterAsync(RegisterDto dto)
        {
            var userExists = await _context.Users
                .AnyAsync(u => u.Email == dto.Email);

            if (userExists)
                return ServiceResponse<string>.Fail("Bu email zaten kayıtlı.");

            var user = new User
            {
                Email = dto.Email,
                UserName = dto.UserName,
                Password = HashPassword(dto.Password),
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return ServiceResponse<string>.Ok("Kayıt başarılı");
        }

        //  Login
        public async Task<ServiceResponse<AuthResponseDto>> LoginAsync(LoginDto dto)
        {
            var hashedPassword = HashPassword(dto.Password);

            var user = await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Email == dto.Email &&
                    u.Password == hashedPassword);

            if (user == null)
                return ServiceResponse<AuthResponseDto>
                    .Fail("Email veya şifre hatalı.");

            var token = GenerateJwtToken(user);

            var response = new AuthResponseDto
            {
                Token = token,
                Role = user.Role
            };

            return ServiceResponse<AuthResponseDto>
                .Ok(response, "Giriş başarılı");
        }


        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role)
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(_configuration["Jwt:ExpireMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
