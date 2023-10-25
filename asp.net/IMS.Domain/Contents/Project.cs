using IMS.Domain.Abstracts;
using IMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IMS.Domain.Contents
{
    public class Project : Auditable
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? AvatarUrl { get; set; }
        public ProjectStatus Status { get; set; }
        public int ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        public virtual Class? Class { get; set; }
        public virtual ICollection<ProjectMember>? ProjectMembers { get; set; }
        public virtual ICollection<Milestone>? Milestones { get; set; } 
        public virtual ICollection<Issues>? Issues { get; set; }
        public virtual ICollection<IssueSetting>? IssueSettings{ get; set; }
     
    }
}
