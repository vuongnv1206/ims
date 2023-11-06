using IMS.Api.Common.Paging;

namespace IMS.Api.Models.Dtos.Users;

public class UserResponse : PagingResponsse
{
    public List<UserDto> Users { get; set; }
}
