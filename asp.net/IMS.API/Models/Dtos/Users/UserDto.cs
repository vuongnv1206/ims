using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Models.Dtos.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDay { get; set; }
        public DateTime? CreationTime { get; set; }
        public string? Avatar { get; set; }
        public IList<string> Roles { get; set; }
    }
}
