
using Microsoft.AspNetCore.Identity;

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
