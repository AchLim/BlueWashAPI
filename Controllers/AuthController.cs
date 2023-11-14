using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using WebAPI.Authentication;
using WebAPI.Data;
using WebAPI.Exception;
using WebAPI.Models;
using InvalidDataException = WebAPI.Exception.InvalidDataException;

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
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto login)
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
                        RefreshToken refreshToken = GenerateRefreshToken();
                        user.RefreshToken = refreshToken.Token;
                        user.TokenCreation = refreshToken.Created;
                        user.TokenExpiration = refreshToken.Expires;

                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();

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
                throw new UserNotFoundException("Kredensial tidak valid!");
            }

            if (!BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
            {
                throw new UserNotFoundException("Kredensial tidak valid!");
            }

            string token = _jwtProvider.Generate(user);

            Response.Cookies.Append("_bw_id", user.RefreshToken,
                new CookieOptions { MaxAge = TimeSpan.FromHours(12) });

            return Ok(new
            {
                username = user.Username,
                accessToken = token,
            });
        }

        [HttpGet("refresh-token")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> RefreshTokenAsync()
        {
            string? refreshToken = Request.Cookies["_bw_id"];

            if (refreshToken == null)
                return Unauthorized();

            if (refreshToken.Trim() == string.Empty)
                throw new InvalidDataException("Token autentikasi kedaluwarsa atau tidak ditemukan. Harap login kembali untuk melanjutkan.");

            ApplicationUser? user = null;
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    user = await _context.Users.Where(user => user.RefreshToken.Equals(refreshToken)).FirstOrDefaultAsync();


                    if (user is not null)
                    {
                        if (user.TokenExpiration < DateTime.Now)
                        {
                            return Unauthorized("Token Expired.");
                        }

                        RefreshToken newRefreshToken = GenerateRefreshToken();
                        user.RefreshToken = newRefreshToken.Token;
                        user.TokenCreation = newRefreshToken.Created;
                        user.TokenExpiration = newRefreshToken.Expires;

                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();

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
                return BadRequest("Invalid request.");
            }


            string token = _jwtProvider.Generate(user);

            Response.Cookies.Append("_bw_id", user.RefreshToken,
                new CookieOptions { MaxAge = TimeSpan.FromHours(12) });

            return Ok(new
            {
                username = user.Username,
                accessToken = token,
            });
        }

        [HttpGet("get-name")]
        public async Task<ActionResult<string>> GetNameAsync()
        {
            string? refreshToken = Request.Cookies["_bw_id"];

            if (refreshToken == null)
                return Unauthorized();

            if (refreshToken.Trim() == string.Empty)
                throw new InvalidDataException("Token autentikasi kedaluwarsa atau tidak ditemukan. Harap login kembali untuk melanjutkan.");


            ApplicationUser? user = null;
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    user = await _context.Users.Where(user => user.RefreshToken.Equals(refreshToken)).FirstOrDefaultAsync();


                    if (user is not null)
                    {
                        if (user.TokenExpiration < DateTime.Now)
                        {
                            return Unauthorized("Token Expired.");
                        }
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
                return Unauthorized();
            }

            return user.Username;

        }

        private static RefreshToken GenerateRefreshToken()
        {
            RefreshToken token = new()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7)
            };

            return token;
        }
    }

    public class LoginDto
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
}

}