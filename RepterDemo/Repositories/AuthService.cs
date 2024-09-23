using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RepterDemo.Data;
using RepterDemo.DTO;
using RepterDemo.Models;
using RepterDemo.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<RegistrationResponse> RegisterUserAsync(RegisterModel model)
    {
        try
        {
            if (_context.Users.Any(u => u.Username == model.Username))
            {
                return new RegistrationResponse { Success = false, Message = "Username already exists" };
            }

            var hasher = new PasswordHasher<User>();
            var user = new User
            {
                Username = model.Username,
                PasswordHash = hasher.HashPassword(null, model.Password),
                Email = model.Email,
                Role = model.Role,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new RegistrationResponse { Success = true, Message = "User registered successfully" };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during registration: {ex.Message}");
            return new RegistrationResponse { Success = false, Message = "Registration failed" };
        }
    }

    public async Task<LoginResult> LoginUserAsync(LoginModel model)
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == model.Username);
        if (user == null)
        {
            return null; // Invalid username or password
        }

        var hasher = new PasswordHasher<User>();
        var result = hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            return null; // Invalid username or password
        }

        var token = GenerateJwtToken(user);

        return new LoginResult
        {
            Token = token,
            UserId = user.UserID
        };
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
