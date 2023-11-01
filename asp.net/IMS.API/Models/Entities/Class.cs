
using IMS.Api.Models.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IMS.Api.Models.Entities
{
    public class Class : Auditable
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    
        public Guid? AssigneeId { get; set; }

        public int SubjectId { get; set; }
     
        public int SettingId { get; set; }

        [ForeignKey(nameof(AssigneeId))]
        public virtual AppUser? Assignee { get; set; }

        [ForeignKey(nameof(SettingId))]
        public virtual Setting? Setting { get; set; }

        [ForeignKey(nameof(SubjectId))]
        public virtual Subject? Subject { get; set; }

        [JsonIgnore]
        public virtual ICollection<ClassStudent>? ClassStudents { get; set; }
        [JsonIgnore]
        public virtual ICollection<Milestone>? Milestones { get; set; }
        [JsonIgnore]
        public virtual ICollection<Project>? Projects { get; set; }
        [JsonIgnore]
        public virtual ICollection<IssueSetting>? IssueSettings { get; set; }
    }
}
