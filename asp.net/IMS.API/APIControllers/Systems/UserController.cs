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
		private readonly IRoleService _roleService;
		private readonly IUserService _userService;
        public UserController(IRoleService roleSerive, IUserService userSerive)
        {
			_roleService = roleSerive;
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
			try
			{
				await _userService.AssignRolesAsync(userId, roleNames);
				return Ok("Roles assigned successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
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

	}

	
}
