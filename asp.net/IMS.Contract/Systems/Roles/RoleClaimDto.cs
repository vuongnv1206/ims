using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Systems.Roles
{
	public class RoleClaimDto
	{
		public required string Type { get; set; }
		public required string Value { get; set; }
		public string? DisplayName { get; set; }
		public bool Selected { get; set; }
	}
}
