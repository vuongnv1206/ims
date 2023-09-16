using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.Contract.Systems.Authentications;

namespace IMS.Contract.Systems.Users
{
    public interface IUserService
    {
        Task DeleteAsync(Guid id);

        Task<List<UserDto>> GetListAllAsync(string? keyword);
        Task AssignRolesAsync(Guid userId, string[] roleNames);
        
    }
}
