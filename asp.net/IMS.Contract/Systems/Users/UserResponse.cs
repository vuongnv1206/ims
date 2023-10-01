using IMS.Contract.Common.Paging;

namespace IMS.Contract.Systems.Users;

public class UserResponse : PagingResponsse
{
    public List<UserDto> Users { get; set; }
}
