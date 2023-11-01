using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Dtos.Users
{
	public class UpdateUserDto
	{
		public string FullName { get; set; }
		public DateTime? BirthDay { get; set; }
		public string? Avatar { get; set; }
		public string? Address { get; set; }
		public string? PhoneNumber { get; set; }
		public IFormFile? FileImage { get; set; }
	}
}
