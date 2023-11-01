using IMS.Api.Dtos.Roles;

namespace IMS.Api.Interfaces
{
    public interface IRoleService
    {
        Task<RoleResponse> GetListAllAsync(RoleRequest request);

        Task AddRole(CreateUpdateRoleDto input);

        Task UpdateRole(Guid id, CreateUpdateRoleDto input);

        Task DeleteManyRole(Guid[] ids);
        Task<RoleDto> GetRoleById(Guid roleId);

        Task<PermissionDto> GetAllRolePermission(string roleId);

        Task SavePermission(PermissionDto input);
    }
}
