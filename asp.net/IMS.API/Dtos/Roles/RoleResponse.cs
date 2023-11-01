using IMS.Api.Common.Paging;

namespace IMS.Api.Dtos.Roles;

public class RoleResponse : PagingResponsse
{
    public List<RoleDto> Roles { get; set; }
}
