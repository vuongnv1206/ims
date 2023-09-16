using AutoMapper;
using IMS.Contract.Systems.Roles;
using IMS.Domain.Systems;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BusinessService.Systems
{
	public class RoleService : IRoleService
	{
		private readonly RoleManager<AppRole> _roleManager;
		private readonly UserManager<AppUser> _userManager;
		private readonly IMapper _mapper;

		public RoleService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_mapper = mapper;
		}

		public async Task AddRole(CreateUpdateRoleDto input)
		{
			if (await _roleManager.RoleExistsAsync(input.Name))
			{
				throw new Exception("Role name is existed");
			}
			await _roleManager.CreateAsync(new AppRole(input.Name.Trim(),input.Description));
		}

		public async Task<List<RoleDto>> GetListAllAsync()
		{
			var roles = await _roleManager.Roles.ToListAsync();
			var roleDtos = _mapper.Map<List<RoleDto>>(roles);

			return roleDtos;
		}
	}
}
