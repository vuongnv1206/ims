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

		//Task<GetPermissionListResultDto> GetPermissionsAsync(string providerName, string providerKey);
		//Task UpdatePermissionsAsync(string providerName, string providerKey, UpdatePermissionsDto input);
	}
}
