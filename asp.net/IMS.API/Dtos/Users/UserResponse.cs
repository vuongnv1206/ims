﻿using IMS.Contract.Common.Paging;

namespace IMS.Api.Dtos.Users;

public class UserResponse : PagingResponsse
{
    public List<UserDto> Users { get; set; }
}
