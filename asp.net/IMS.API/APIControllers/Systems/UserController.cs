using IMS.BusinessService.Systems;
using IMS.Contract.Systems.Roles;
using IMS.Contract.Systems.Users;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.APIControllers.Systems
{

	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
        public UserController(IUserService userSerive)
        {
			_userService = userSerive;
        }

		[HttpGet("users")]
		public async Task<IActionResult> GetAllUsers(string? keyword)
		{
			return Ok(await _userService.GetListAllAsync(keyword));
		}

		[HttpPost("assign-roles")]
		public async Task<IActionResult> AssignRoles(Guid userId, string[] roleNames)
		{
				return Ok(_userService.AssignRolesAsync(userId, roleNames));
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
			return Ok(_userService.GetUserByIdAsync(id));
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser([FromBody] CreateUserDto input)
		{
			return Ok(_userService.CreateUser(input));
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto input)
		{
			return Ok(_userService.UpdateUser(id,input));
		}
	}

	
}
