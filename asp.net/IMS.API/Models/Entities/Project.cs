using IMS.Api.Models.Abstracts;
using IMS.Api.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IMS.Api.Models.Entities
{
    public class Project : Auditable
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? AvatarUrl { get; set; }
        public ProjectStatus Status { get; set; }
        public int ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        [JsonIgnore]
        public virtual Class? Class { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProjectMember>? ProjectMembers { get; set; }
        [JsonIgnore]
        public virtual ICollection<Milestone>? Milestones { get; set; } 
        [JsonIgnore]
        public virtual ICollection<Issue>? Issues { get; set; }
        [JsonIgnore]
        public virtual ICollection<IssueSetting>? IssueSettings{ get; set; }
     
    }
}
