using WebAPI.Models;

namespace WebAPI.Authentication
{
    public interface IJwtProvider
    {
        string Generate(ApplicationUser user);
    }
}
