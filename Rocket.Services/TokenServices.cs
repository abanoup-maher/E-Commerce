using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rocket.Core.Entities.Identity;
using Rocket.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _config;

        public TokenServices(IConfiguration config)
        {
            _config = config;
        }


        public async Task<string> CreateToken(AppUser user, UserManager<AppUser> usermanager)
        {
            //private vlaims mot8yra
            #region private claims
            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName,user.DisplayName)
            };


            var userRoles = await usermanager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role, role));
            } 
            #endregion


            // secret key
            var authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));

            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(_config["JWT:DurationInDays"])),
                claims: AuthClaims,
                signingCredentials: new SigningCredentials(authkey, SecurityAlgorithms.HmacSha256Signature));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
