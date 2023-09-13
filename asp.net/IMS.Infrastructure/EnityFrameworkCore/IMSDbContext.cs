using IMS.Domain.Abstracts;
using IMS.Domain.Systems;
using IMS.Infrastructure.Seedings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Infrastructure.EnityFrameworkCore
{
	public class IMSDbContext : IdentityDbContext<AppUser, AppRole, Guid>
	{
		public IMSDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			//Configure using Fluent API
			//Ex:
			//builder.ApplyConfiguration(new UserConfiguration());
			//builder.ApplyConfiguration(new RoleConfiguration());
			//builder.ApplyConfiguration(new UserRoleConfiguration());

			builder.SeedData();
			base.OnModelCreating(builder);
		}

		public virtual async Task<int> SaveChangesAsync(string username = "SYSTEM")
		{
			foreach (var entry in base.ChangeTracker.Entries<Auditable>()
				.Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
			{
				entry.Entity.LastModificationTime = DateTime.Now;
				entry.Entity.LastModifiedBy = username;

				if (entry.State == EntityState.Added)
				{
					entry.Entity.CreationTime = DateTime.Now;
					entry.Entity.CreatedBy = username;
				}

			}
			var result = await base.SaveChangesAsync();

			return result;
		}

	}
}
