using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Systems.Users
{
	public class CreateUserDto
	{
		public string FullName { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public DateTime? BirthDay { get; set; }
		public string? Avatar { get; set; }
		public string? Address { get; set; }
	}
}
