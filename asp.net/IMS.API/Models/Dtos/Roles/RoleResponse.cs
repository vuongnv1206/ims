using IMS.Api.Common.Paging;

namespace IMS.Api.Models.Dtos.Roles;

public class RoleResponse : PagingResponsse
{
    public List<RoleDto> Roles { get; set; }
}
