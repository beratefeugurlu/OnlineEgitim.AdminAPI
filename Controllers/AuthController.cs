using Microsoft.AspNetCore.Mvc;
using OnlineEgitim.AdminAPI.Services;
using OnlineEgitim.AdminAPI.Data;
using OnlineEgitim.AdminAPI.Models;
using System.Linq; // ✅ LINQ için
using BCrypt.Net; // ✅ BCrypt için

namespace OnlineEgitim.AdminAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly AppDbContext _context;

        public AuthController(ITokenService tokenService, AppDbContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        // ✅ Kayıt
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (_context.Users.Any(u => u.Email.ToLower() == request.Email.ToLower()))
                return BadRequest(new { message = "Bu email zaten kayıtlı!" });

            // Şifreyi BCrypt ile hashle
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = passwordHash,
                Role = request.Role ?? "Student"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            // ✅ JSON response
            return Ok(new
            {
                message = "Kayıt başarılı! 🎉",
                user = new { user.Name, user.Email, user.Role }
            });
        }

        // ✅ Giriş
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null) return Unauthorized(new { message = "Kullanıcı bulunamadı!" });

            // Şifreyi hash karşılaştırması (BCrypt)
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isPasswordValid)
                return Unauthorized(new { message = "Şifre hatalı!" });

            var token = _tokenService.GenerateToken(user.Email, user.Role);
            return Ok(new { token = token, role = user.Role, name = user.Name });
        }
    }

    // ✅ DTO'lar
    public class LoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class RegisterRequest
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? Role { get; set; }
    }
}
