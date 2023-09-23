using IMS.BusinessService.Constants;
using IMS.BusinessService.Systems;
using IMS.Contract.Common.Responses;
using IMS.Contract.Systems.Authentications;
using IMS.Contract.Systems.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.APIControllers.Systems
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		private readonly IRoleService _roleService;
		public RoleController(IRoleService roleSerive)
		{
			_roleService = roleSerive;
		}
		//alo
		[HttpGet("all")]
		public async Task<IActionResult> GetAllRoles()
		{
			return Ok(await _roleService.GetListAllAsync());
		}

		[HttpPost]
		public async Task<IActionResult> CreateRole([FromBody] CreateUpdateRoleDto input)
		{
			return Ok(_roleService.AddRole(input));
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateRole(Guid id, [FromBody] CreateUpdateRoleDto input)
		{

			return Ok(_roleService.UpdateRole(id, input));
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteRoles([FromQuery] Guid[] ids)
		{
			return Ok(_roleService.DeleteManyRole(ids));
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<RoleDto>> GetRoleById(Guid id)
		{
			return Ok(_roleService.GetRoleById(id));
		}

		[HttpGet("{roleId}/permissions")]
		public async Task<ActionResult<PermissionDto>> GetAllRolePermissions(string roleId)
		{
			return Ok(_roleService.GetAllRolePermission);
		}

		[HttpPut("permissions")]
		public async Task<IActionResult> SavePermission([FromBody] PermissionDto model)
		{
			return Ok(_roleService.SavePermission(model));
		}
	}
}
