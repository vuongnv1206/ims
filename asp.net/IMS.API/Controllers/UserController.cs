using AutoMapper;
using IMS.Api.Interfaces;
using IMS.Api.Models.Dtos.Users;
using IMS.Api.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.APIControllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, UserManager<AppUser> userManager, IMapper mapper)
        {
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("users")]
        public async Task<ActionResult<UserResponse>> GetAllUsers([FromQuery] UserRequest request)
        {   
   
            var data = await _userService.GetListAllAsync(request);
            return Ok(data);
        }


        [HttpPost("assign-roles")]
        public async Task<IActionResult> AssignRoles(Guid userId, string[] roleNames)
        {
            await _userService.AssignRolesAsync(userId, roleNames);
            return Ok();
        }

        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return Ok("User deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            try
            {
                var data = await _userService.GetUserByIdAsync(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto input)
        {
            await _userService.CreateUser(input);
            return Ok("User create successfully.");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromForm] UpdateUserDto input)
        {
            await _userService.UpdateUser(id, input);
            return Ok("User updated successfully.");
        }
    }


}
