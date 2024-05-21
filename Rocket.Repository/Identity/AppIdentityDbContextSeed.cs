using Microsoft.AspNetCore.Identity;
using Rocket.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Repository.Identity
{
    public class AppIdentityDbContextSeed
    {
        // 34an ykon fe one user 3la el 2a2l
        public static async Task SeedUserAsync(UserManager<AppUser> usermanager)
        {
            if (!usermanager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "abanoup maher",
                    Email = "pepomaher7@gmail.com",
                    UserName = "pepo",
                    PhoneNumber = "01204401329"

                };
                await usermanager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
