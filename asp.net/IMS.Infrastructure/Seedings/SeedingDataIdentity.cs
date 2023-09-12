using IMS.Domain.Systems;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Infrastructure.Seedings
{
	public static class SeedingDataIdentity
	{
		public static void SeedData(this ModelBuilder builder)
		{
			var roleUserId = new Guid("cac43a6e-f7bb-4448-baaf-1add431ccbbf");
			var roleAdminId = new Guid("cbc43a8e-f7bb-4445-baaf-1add431ffbbf");
			builder.Entity<AppRole>().HasData(
				new AppRole
				{
					Id = roleUserId,
					Description = "User role",
					Name = "User",
					NormalizedName = "USER",
					
				},
				new AppRole
				{
					Id = roleAdminId,
					Description = "Admin role",
					Name = "Admin",
					NormalizedName = "ADMIN",
					
				}
			);

			var hasher = new PasswordHasher<AppUser>();
			var adminId = new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9");
			var userId = new Guid("9e224968-33e4-4652-b7b7-8574d048cdb9");
			builder.Entity<AppUser>().HasData(
				 new AppUser
				 {
					 Id = adminId,
					 Email = "admin@gmail.com",
					 NormalizedEmail = "ADMIN@GMAIL.COM",
					 FullName = "System",
					 UserName = "admin@gmail.com",
					 NormalizedUserName = "ADMIN@GMAIL.COM",
					 PasswordHash = hasher.HashPassword(null, "Abcd@1234"),
					 EmailConfirmed = true
				 },
				 new AppUser
				 {
					 Id = userId,
					 Email = "user@gmail.com",
					 NormalizedEmail = "USER@GMAIL.COM",
					 FullName = "User",
					 UserName = "user@gmail.com",
					 NormalizedUserName = "USER@GMAIL.COM",
					 PasswordHash = hasher.HashPassword(null, "Abcd@1234"),
					 EmailConfirmed = true
				 }
			);

			builder.Entity<IdentityUserRole<Guid>>().HasData(
				new IdentityUserRole<Guid>
				{
					RoleId = roleAdminId,
					UserId = adminId
				},
				new IdentityUserRole<Guid>
				{
					RoleId = roleUserId,
					UserId = userId
				}
				);
		}
	}
}
