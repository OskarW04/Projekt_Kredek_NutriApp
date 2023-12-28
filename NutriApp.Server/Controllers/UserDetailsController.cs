using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriApp.Server.Models;
using NutriApp.Server.Services.Interfaces;

namespace NutriApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserDetailsController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserDetailsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet(Name = "GetUserDetails")]
        public ActionResult<UserDto> GetUserDetails()
        {
            var userDetails = _userService.GetUserDetails();
            return Ok(userDetails);
        }

        [HttpPost(Name = "AddUserDetails")]
        public ActionResult<UserDto> AddUserDetails(AddUserDetailsRequest addUserDetailsRequest)
        {
            _userService.AddUserDetails(addUserDetailsRequest);
            return Created();
        }

        [HttpPut(Name = "UpdateUserDetails")]
        public ActionResult<UserDto> UpdateUserDetails(UpdateUserRequest updateUserRequest)
        {
            var userDto = _userService.UpdateUserDetails(updateUserRequest);
            return Ok(userDto);
        }
    }
}