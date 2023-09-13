
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Systems
{
	public class AppUser : IdentityUser<Guid>
	{
		public string? FullName { set; get; }

		public string? Address { set; get; }

		public string? Avatar { get; set; }
		public DateTime? BirthDay { set; get; }
	}
}
