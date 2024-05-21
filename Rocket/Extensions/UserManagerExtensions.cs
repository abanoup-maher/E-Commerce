using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rocket.Core.Entities.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rocket.Extensions
{
    public static class UserManagerExtensions
    {
       public static async Task<AppUser> FindWithAddressByEmailAsync(this UserManager<AppUser> usermanager , ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user= await usermanager.Users.Include(u=>u.Address).SingleOrDefaultAsync(u=>u.Email == email);
            return user;

        }
    }
}
