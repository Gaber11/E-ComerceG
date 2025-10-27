using Microsoft.AspNetCore.Authorization;
using Shared.Dtos;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class AuthenticationController(IServiceManager serviceManager) : ApiController
    {
        //baseUrl/api/authentication/login
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDto>> Login(LoginDto loginDto)
        {
            var result = await serviceManager.authenticationService.LoginAsync(loginDto);
            return Ok(result);
        }
        //baseUrl/api/authentication/register
        [HttpPost("register")]
        public async Task<ActionResult<UserResultDto>> Register(RegisterDto registerDto)
        {
            var result = await serviceManager.authenticationService.RegisterAsync(registerDto);
            return Ok(result);
        }
        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            var result = await serviceManager.authenticationService.CheckEmailExist(email);
            return Ok(result);
        }
        [Authorize]
        [HttpGet] //Get:   baseUrl/api/authentication
        public async Task<ActionResult<UserResultDto>> GetCurrentUser()
        {
          var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.authenticationService.GetUserByEmail(email);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("Address")] //Get:   baseUrl/api/authentication/address
        public async Task<ActionResult<AddressDto>> GetAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.authenticationService.GetUserAddress(email);
            return Ok(result);
        }
        [Authorize]
        [HttpPut("Address")] //Put:   baseUrl/api/authentication/address
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.authenticationService.UpdateUserAddress(addressDto, email);
            return Ok(result);
        }
    }
}
