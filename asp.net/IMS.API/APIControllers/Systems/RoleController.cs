﻿using IMS.BusinessService.Constants;
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
		public async Task<ActionResult<RoleResponse>> GetAllRoles([FromQuery]RoleRequest request)
		{
			var data = await _roleService.GetListAllAsync(request);
			return Ok(data);
		}

		[HttpPost]
		public async Task<IActionResult> CreateRole([FromBody] CreateUpdateRoleDto input)
		{
			await _roleService.AddRole(input);
			return Ok("Add role succesfully");
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateRole(Guid id, [FromBody] CreateUpdateRoleDto input)
		{
			 await _roleService.UpdateRole(id, input);
			return Ok("Update role succesfully");
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteRoles([FromQuery] Guid[] ids)
		{	
			await _roleService.DeleteManyRole(ids);
			return Ok("Delete roles succesfully");
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<RoleDto>> GetRoleById(Guid id)
		{
			var data = await _roleService.GetRoleById(id);
			return Ok(data);
		}

		[HttpGet("{roleId}/permissions")]
		public async Task<ActionResult<PermissionDto>> GetAllRolePermissions(string roleId)
		{	
			var data = await _roleService.GetAllRolePermission(roleId);
			return Ok(data);
		}

		[HttpPut("permissions")]
		public async Task<IActionResult> SavePermission([FromBody] PermissionDto model)
		{
			await _roleService.SavePermission(model);
			return Ok("Update permission succesfully");
		}
	}
}
