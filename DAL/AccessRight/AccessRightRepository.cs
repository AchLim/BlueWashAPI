using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Exception;
using WebAPI.Models;
using UnauthorizedAccessException = WebAPI.Exception.UnauthorizedAccessException;

namespace WebAPI.DAL
{
    public sealed class AccessRightRepository : IAccessRightRepository
    {
        private readonly ApplicationContext _context;

        public AccessRightRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserMenuCategoryContainer>> GetMenusAsync(string refreshToken)
        {
            ApplicationUser? user = null;
            IEnumerable<UserMenuCategoryContainer> userMenus = Enumerable.Empty<UserMenuCategoryContainer>();

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    user = await _context.Users.FirstOrDefaultAsync(user => user.RefreshToken.Equals(refreshToken));
                    if (user is not null)
                    {
                        userMenus = await _context.UserMenus.Join(_context.Menus.Join(_context.MenuCategories,
                                                                                      menu => menu.MenuCategoryId,
                                                                                      menuCategory => menuCategory.Id,
                                                                                      (menu, menuCategory) => new
                                                                                      {
                                                                                          MenuId = menu.Id,
                                                                                          MenuSequence = menu.MenuSequence,
                                                                                          MenuName = menu.MenuName,
                                                                                          MenuDisplayName = menu.MenuDisplayName,

                                                                                          CategorySequence = menuCategory.CategorySequence,
                                                                                          CategoryName = menuCategory.CategoryName,
                                                                                          CategoryDisplayName = menuCategory.CategoryDisplayName,
                                                                                      }),
                                                                  userMenu => userMenu.MenuId,
                                                                  menu => menu.MenuId,
                                                                  (userMenu, menu) => new
                                                                  {
                                                                      UserId = userMenu.ApplicationUserId,
                                                                      CategorySequence = menu.CategorySequence,
                                                                      CategoryName = menu.CategoryName,
                                                                      MenuSequence = menu.MenuSequence,
                                                                      MenuName = menu.MenuName,
                                                                      CategoryDisplayName = menu.CategoryDisplayName,
                                                                      MenuDisplayName = menu.MenuDisplayName,
                                                                  })
                                                            .Where(userMenu => userMenu.UserId.Equals(user.Id))
                                                            .GroupBy(userMenu => userMenu.CategoryName)
                                                            .OrderBy(group => group.First().CategorySequence)
                                                            .Select(group => new UserMenuCategoryContainer
                                                            {
                                                                CategoryName = group.Key,
                                                                CategoryDisplayName = group.First().CategoryDisplayName,
                                                                Menus = group.OrderBy(menu => menu.MenuSequence)
                                                                             .Select(menu => new UserMenuContainer
                                                                             {
                                                                                 MenuName = menu.MenuName,
                                                                                 MenuDisplayName = menu.MenuDisplayName
                                                                             }).ToList()
                                                            })
                                                            .ToListAsync();
                    }
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data Menu", ex);
                }
            }

            if (user is null)
                throw new UserNotFoundException("Token autentikasi kedaluwarsa atau tidak ditemukan. Harap login kembali untuk melanjutkan.");

            return userMenus;
        }
    
        public async Task<bool> CheckMenuAccessRightAsync(string refreshToken, string menuName)
        {
            ApplicationUser? user = null;
            bool hasAccess = false;
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    user = await _context.Users.FirstOrDefaultAsync(user => user.RefreshToken.Equals(refreshToken));
                    if (user is not null)
                    {
                        hasAccess = await _context.UserMenus.Join(_context.Menus,
                                                                  userMenu => userMenu.MenuId,
                                                                  menu => menu.Id,
                                                                  (userMenu, menu) => new
                                                                  {
                                                                      UserId = userMenu.ApplicationUserId,
                                                                      MenuName = menu.MenuName,
                                                                  })
                                                            .Where(userMenu => userMenu.MenuName.Equals(menuName))
                                                            .AnyAsync(userMenu => userMenu.UserId.Equals(user.Id));
                    }
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data Menu.", ex);
                }
            }

            if (user is null)
                throw new UserNotFoundException("Token autentikasi kedaluwarsa atau tidak ditemukan. Harap login kembali untuk melanjutkan.");

            if (!hasAccess)
            {
                throw new UnauthorizedAccessException("Kamu tidak punya hak akses untuk mengakses menu ini.");
            }

            return hasAccess;
        }
    }

    public class UserMenuCategoryContainer
    {
        public string CategoryName { get; set; } = default!;
        public string CategoryDisplayName { get; set; } = default!;
        public List<UserMenuContainer> Menus { get; set; } = new();
    }

    public class UserMenuContainer
    {
        public string MenuName { get; set; } = default!;
        public string MenuDisplayName { get; set; } = default!;
    }
}
