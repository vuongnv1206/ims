﻿using IMS.Domain.Abstracts;
using IMS.Domain.Contents;
using IMS.Domain.Systems;
using IMS.Infrastructure.Seedings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Label = IMS.Domain.Contents.Label;

namespace IMS.Infrastructure.EnityFrameworkCore
{
    public class IMSDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public IMSDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassStudent> ClassStudents { get; set; }

        public DbSet<Issues> Issues { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<IssueSetting> IssueSettings { get; set; }
        public DbSet<SubjectUser> SubjectUsers { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public DbSet<Label> Labels { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
         
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

            var entries = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added);
            foreach (var entityEntry in entries)
            {
                var dateCreatedProp = entityEntry.Entity.GetType().GetProperty("CreationTime");
                if (entityEntry.State == EntityState.Added
                    && dateCreatedProp != null)
                {
                    dateCreatedProp.SetValue(entityEntry.Entity, DateTime.Now);
                }
            }

            var result = await base.SaveChangesAsync();

            return result;
        }

    }
}
