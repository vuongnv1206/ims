using AutoMapper;
using IMS.Api.Common.Constants;
using IMS.Api.Common.Helpers.Extensions;
using IMS.Api.EnityFrameworkCore;
using IMS.Api.Interfaces;
using IMS.Api.Models.Dtos.Roles;
using IMS.Api.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Services
{
    public class RoleService : ServiceBase<AppRole>, IRoleService
	{
		private readonly RoleManager<AppRole> _roleManager;
		private readonly UserManager<AppUser> _userManager;

		public RoleService(UserManager<AppUser> userManager,
			RoleManager<AppRole> roleManager,
			IMapper mapper,
			IMSDbContext context)
			: base(context, mapper)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task AddRole(CreateUpdateRoleDto input)
		{
			if (await _roleManager.RoleExistsAsync(input.Name))
			{
				throw new Exception("Role name is existed");
			}
			await _roleManager.CreateAsync(new AppRole(input.Name.Trim(),input.Description));
		}

		public async Task DeleteManyRole(Guid[] ids)
		{
			foreach (var id in ids)
			{
				var role = await _roleManager.FindByIdAsync(id.ToString());
				if (role == null) if (role == null) throw new Exception();
				await _roleManager.DeleteAsync(role);
			}
		}

		

		public async Task<RoleResponse> GetListAllAsync(RoleRequest request)
		{
			var roles = await _roleManager.Roles
				.Where(x => string.IsNullOrWhiteSpace(request.KeyWords)
					|| x.Name.Contains(request.KeyWords)
					|| x.Description.Contains(request.KeyWords))
				.ToListAsync();

			var rolePaging = roles.Paginate(request);
			var roleDtos = mapper.Map<List<RoleDto>>(rolePaging);

			var response = new RoleResponse
			{
				Roles = roleDtos,
				Page = GetPagingResponse(request, roles.Count()),
			};
			return response;
		}

		public async Task<RoleDto> GetRoleById(Guid roleId)
		{
			var role = await _roleManager.FindByIdAsync(roleId.ToString());
			if (role == null) throw new Exception("Not found");
			return mapper.Map<AppRole, RoleDto>(role);
		}

	
		public async Task UpdateRole(Guid id, CreateUpdateRoleDto input)
		{
			var role = await _roleManager.FindByIdAsync(id.ToString());
			if (role == null) throw new Exception("Not found");

			role.Name = input.Name;
			role.Description = input.Description;

			await _roleManager.UpdateAsync(role);
		}

		public async Task<PermissionDto> GetAllRolePermission(string roleId)
		{
			var model = new PermissionDto();
			var allPermissions = new List<RoleClaimDto>();

			var types = typeof(Permissions).GetTypeInfo().DeclaredNestedTypes;
			foreach (var type in types)
			{
				allPermissions.GetPermissions(type);
			}

			var role = await _roleManager.FindByIdAsync(roleId);
			if (role == null) throw new Exception("Not found");

			model.RoleId = roleId;
			var claims = await _roleManager.GetClaimsAsync(role);
			var allClaimValues = allPermissions.Select(a => a.Value).ToList();
			var roleClaimValues = claims.Select(a => a.Value).ToList();
			var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
			foreach (var permission in allPermissions)
			{
				if (authorizedClaims.Any(a => a == permission.Value))
				{
					permission.Selected = true;
				}
			}
			model.RoleClaims = allPermissions;
			return model;
		}

		public async Task SavePermission(PermissionDto input)
		{
			var role = await _roleManager.FindByIdAsync(input.RoleId);
			if (role == null) throw new Exception("Not found");

			var claims = await _roleManager.GetClaimsAsync(role);
			foreach (var claim in claims)
			{
				await _roleManager.RemoveClaimAsync(role, claim);
			}
			var selectedClaims = input.RoleClaims.Where(a => a.Selected).ToList();
			foreach (var claim in selectedClaims)
			{
				await _roleManager.AddPermissionClaim(role, claim.Value);
			}
		}

	}
}
