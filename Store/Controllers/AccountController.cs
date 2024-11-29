using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Dtos.Auth;
using Store.Core.Entities.Identity;
using Store.Core.Services.Contract;
using System.Security.Claims;
using Store.Service.Services.User;
using AutoMapper;
using Store.Core.Dtos;
using Store.Core;
using Microsoft.AspNetCore.Authorization;
using Store.Extensions;

namespace Store.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService,
                                 UserManager<AppUser> userManager,
                                 ITokenService tokenService,
                                 IMapper mapper
            )
        {
            _userService = userService;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }


        [HttpPost("login")]  // POST : /api/account/login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userService.LoginAsync(loginDto);

            if (user is null) return Unauthorized();
            
            return Ok(user);
        }


        [HttpPost("register")] // POST : /api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _userService.RegisterAsync(registerDto);

            if (user is null) return BadRequest();

            return Ok(user);
        }

        [Authorize]
        [HttpGet("GetCurrentUser")] // GET : /api/account/GetCurrentUser
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailWithAddressAsync(User);
            if (user is null) return BadRequest();
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
        }

        [Authorize]
        [HttpGet("Address")] // GET : /api/account/Address
        public async Task<ActionResult<UserDto>> GetCurrentUserAddress()
        {
            var user = await _userManager.FindByEmailWithAddressAsync(User);
            if (user is null) return BadRequest();

            return Ok(_mapper.Map<AddressDto>(user.Address));
        }



        [Authorize]
        [HttpPut("UpdateAddress")] // PUT: /api/account/UpdateAddress
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var user = await _userManager.FindByEmailWithAddressAsync(User);
            if (user is null) return BadRequest("User not found");

            user.Address = _mapper.Map<Address>(addressDto); 

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return BadRequest("Failed to update the address");

            return Ok(_mapper.Map<AddressDto>(user.Address));
        }

    }
}
