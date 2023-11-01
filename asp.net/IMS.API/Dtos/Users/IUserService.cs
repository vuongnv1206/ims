using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.Contract.Systems.Authentications;

namespace IMS.Api.Dtos.Users
{
    public interface IUserService
    {
        Task DeleteAsync(Guid id);

        Task<UserResponse> GetListAllAsync(UserRequest request);
        Task AssignRolesAsync(Guid userId, string[] roleNames);

        Task CreateUser(CreateUserDto userDto);

        Task UpdateUser(Guid id,UpdateUserDto userDto);
        
        Task<UserDto> GetUserByIdAsync(Guid id);
    }
}
