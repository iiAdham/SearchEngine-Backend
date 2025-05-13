using Microsoft.AspNetCore.Identity;
using SearchEngine.Entity;

namespace SearchEngine.Service_Contract
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> manager);


    }
}
