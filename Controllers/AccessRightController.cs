using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Exception;
using WebAPI.Models;
using UnauthorizedAccessException = WebAPI.Exception.UnauthorizedAccessException;
using InvalidDataException = WebAPI.Exception.InvalidDataException;
using WebAPI.DAL;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize(Roles = "ADMIN,MANAGER,STAFF")]
    [Route("access-right")]
    public class AccessRightController : ControllerBase
    {
        private readonly ILogger<AccessRightController> _logger;
        private readonly IAccessRightRepository _accessRightRepository;

        public AccessRightController(ILogger<AccessRightController> logger,
                               IAccessRightRepository repository)
        {
            _logger = logger;
            _accessRightRepository = repository;
        }

        [HttpGet("get/menus")]
        public async Task<ActionResult<IEnumerable<UserMenuCategoryContainer>>> GetMenusAsync()
        {
            string? refreshToken = Request.Cookies["_bw_id"];

            if (refreshToken == null)
                return Unauthorized();

            if (refreshToken.Trim() == string.Empty)
                throw new InvalidDataException("Token autentikasi kedaluwarsa atau tidak ditemukan. Harap login kembali untuk melanjutkan.");

            IEnumerable<UserMenuCategoryContainer> userMenus = await _accessRightRepository.GetMenusAsync(refreshToken);

            return Ok(userMenus);
        }

        [HttpPost("check/menu")]
        public async Task<ActionResult<bool>> CheckMenuAccessRightAsync([FromBody] AccessRightDto accessRightDto)
        {
            string? refreshToken = Request.Cookies["_bw_id"];

            if (refreshToken == null)
                return Unauthorized();

            if (refreshToken.Trim() == string.Empty)
                throw new InvalidDataException("Token autentikasi kedaluwarsa atau tidak ditemukan. Harap login kembali untuk melanjutkan.");

            bool hasAccess = await _accessRightRepository.CheckMenuAccessRightAsync(refreshToken, accessRightDto.MenuName);

            return Ok(hasAccess);
        }

    }

    public class AccessRightDto
    {
        public required string MenuName { get; set; }
    }
}