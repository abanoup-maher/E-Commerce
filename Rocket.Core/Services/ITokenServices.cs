using Microsoft.AspNetCore.Identity;
using Rocket.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Core.Services
{
    public interface ITokenServices
    {
        Task<string> CreateToken(AppUser user , UserManager<AppUser> usermanager);
    }
}
