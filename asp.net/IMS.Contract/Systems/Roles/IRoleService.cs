using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Systems.Roles
{
    public interface IRoleService
    {
		Task<List<RoleDto>> GetListAllAsync();

		Task AddRole(CreateUpdateRoleDto input);

		Task UpdateRole(Guid id, CreateUpdateRoleDto input);

		Task DeleteManyRole(Guid[] ids);
		Task<RoleDto> GetRoleById(Guid roleId);

		Task<PermissionDto> GetAllRolePermission(string roleId);

		Task SavePermission(PermissionDto input);
	}
}
