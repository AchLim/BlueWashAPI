using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Authentication;
using WebAPI.DAL;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Mapper;
using WebAPI.Utility;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize(Roles = "ADMIN,MANAGER,STAFF")]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IJwtProvider _jwtProvider;
        private readonly ApplicationContext _context;

        public AuthController(ILogger<AuthController> logger,
                               ApplicationContext context,
                               IJwtProvider jwtProvider)
        {
            _logger = logger;
            _context = context;
            _jwtProvider = jwtProvider;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            ApplicationUser? user = null;
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    user = await _context.Users
                                         .Where(u => u.Login.ToUpper() == login.Login.ToUpper())
                                         .FirstOrDefaultAsync();

                    if (user is not null)
                    {
                        _context.Entry(user)
                                .Collection(u => u.UserRoles)
                                .Query()
                                .Include(ur => ur.ApplicationRole)
                                .Load();
                    }
                }
                catch
                {
                    await transaction.RollbackAsync();
                    return BadRequest("Terjadi kesalahan pada database.");
                }
            }
            if (user is null)
            {
                return BadRequest("Akun tidak valid!");
            }
            if (!BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
            {
                return BadRequest("Akun tidak valid!");
            }

            string token = _jwtProvider.Generate(user);

            return Ok(token);
        }
    }

    public class LoginDto
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
}
}