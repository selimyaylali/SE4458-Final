using Microsoft.AspNetCore.Mvc;
using se4458_api.Model;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace se4458_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PharmacyController : ControllerBase
    {
        private readonly SelimContext _context;
        private readonly IConfiguration _configuration;

        public PharmacyController(SelimContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto registrationDto)
        {
            // Check if the pharmacy name already exists
            if (_context.Pharmacies.Any(p => p.Name == registrationDto.Name))
            {
                return BadRequest("Error occured while registering.");
            }

            // Hash the password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registrationDto.Password);

            // Create and save the new pharmacy
            var pharmacy = new Pharmacy
            {
                Name = registrationDto.Name,
                AuthenticationCredentials = passwordHash
            };
            _context.Pharmacies.Add(pharmacy);
            await _context.SaveChangesAsync();

            return Ok("Pharmacy registered successfully.");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {

            var pharmacy = await _context.Pharmacies
                                         .SingleOrDefaultAsync(p => p.Name == loginDto.Name);
            if (pharmacy == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, pharmacy.AuthenticationCredentials))
            {
                return Unauthorized("Name or password is incorrect.");
            }
            var token = GenerateJwtToken(pharmacy);

            return Ok(new { token });
        }
        private string GenerateJwtToken(Pharmacy pharmacy)
        {
            var secret = _configuration["JWT:Secret"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, pharmacy.PharmacyId.ToString()),
                    new Claim(ClaimTypes.Name, pharmacy.Name)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = credentials,
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



    }
}


