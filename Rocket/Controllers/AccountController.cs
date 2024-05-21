using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Rocket.Core.Entities.Identity;
using Rocket.Core.Services;
using Rocket.DTOS;
using Rocket.Errors;
using Rocket.Extensions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rocket.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _UserManager;

        private readonly SignInManager<AppUser> _SignInManager;
        private readonly ITokenServices _tokens;
        private readonly IMapper _map;

        public AccountController(UserManager<AppUser> usermanager, SignInManager<AppUser> signinmanager, ITokenServices tokens ,IMapper map)
        {
            _UserManager = usermanager;
            _SignInManager = signinmanager;
            _tokens = tokens;
            _map = map;
        }



        [HttpPost("login")] //   Api/Account/login
        public async Task <ActionResult<UserDTO>> LogIn(LogInDTO log)
        {
            var user = await _UserManager.FindByEmailAsync(log.Email);
            if(user == null)
            {
                return Unauthorized (new ApiResponse(401));
            } 
            var result = await _SignInManager.CheckPasswordSignInAsync(user, log.Password,false);
            if (!result.Succeeded)
            {
                return Unauthorized(new ApiResponse(401));
            }
            return Ok(new UserDTO 
            { 
                DisplayNAme=user.DisplayName,
                Email=user.Email,
                Token= await _tokens.CreateToken(user,_UserManager)
            });
        }



        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO Reg)
        {
            if(CheckEmailExist(Reg.Email).Result.Value)
            {
                return BadRequest(new ApiValidationErrorResponse()
                {
                    Errors = new[] { "This Email Already Exist" }
                });
            }

            var user = (new AppUser
            {
                DisplayName = Reg.DisplayName,
                Email = Reg.Email,
                PhoneNumber = Reg.PhoneNumber,
                UserName = Reg.Email.Split("@")[0]
            });

            var result = await _UserManager.CreateAsync(user, Reg.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(new UserDTO 
            { 
                DisplayNAme = user.DisplayName,
                Email = user.Email, 
                Token = await _tokens.CreateToken(user, _UserManager) 
            });
        }




        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> getcurrentuser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user=await _UserManager.FindByEmailAsync(email);
            return Ok(new UserDTO
                {
                DisplayNAme= user.DisplayName,
                Email = user.Email,
                Token=await _tokens.CreateToken(user, _UserManager)
            });

        }



        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto UpdatedAddress)
        {
            //  var email = User.FindFirstValue(ClaimTypes.Email);
            var address = _map.Map<AddressDto, Address>(UpdatedAddress);
            var Appuser = await _UserManager.FindWithAddressByEmailAsync(User);
            
            Appuser.Address = address;

            var result = await _UserManager.UpdateAsync(Appuser);
            if (!result.Succeeded) { return BadRequest(new ApiResponse(400)); }

            return Ok(_map.Map<Address,AddressDto>(Appuser.Address));

        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var Appuser = await _UserManager.FindWithAddressByEmailAsync(User);
            return Ok(_map.Map<Address, AddressDto>(Appuser.Address));
        }



        [HttpGet("emailexist")]
        public async Task<ActionResult<bool>> CheckEmailExist(string Email)
        {
            return await _UserManager.FindByEmailAsync(Email)!=null;
        }

    }
}
