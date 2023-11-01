﻿
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Models.Entities
{
	public class AppRole : IdentityRole<Guid>
	{
		public string? Description { get; set; }

        public AppRole()
        {
            
        }
        public AppRole(string roleName,string? description) : this()
		{
			Name = roleName;
			Description = description;
		}
	}
}
