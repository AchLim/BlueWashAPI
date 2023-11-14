namespace WebAPI.DAL
{
    public interface IAccessRightRepository
    {
        Task<IEnumerable<UserMenuCategoryContainer>> GetMenusAsync(string refreshToken);
        Task<bool> CheckMenuAccessRightAsync(string refreshToken, string menuName);
    }
}