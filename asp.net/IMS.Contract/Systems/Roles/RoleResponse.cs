using IMS.Contract.Common.Paging;

namespace IMS.Contract.Systems.Roles;

public class RoleResponse : PagingResponsse
{
    public List<RoleDto> Roles { get; set; }
}
