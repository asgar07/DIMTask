using WebApp.Models;

namespace WebApp.Service.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(AppUser user, List<string> roles);
    }
}
