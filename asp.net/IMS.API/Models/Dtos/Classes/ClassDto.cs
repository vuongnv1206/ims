using IMS.Api.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Api.Models.Dtos.Classes
{
    public class ClassDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid? AssigneeId { get; set; }
        public string? Description { get; set; }
        public int SubjectId { get; set; }
        public int SettingId { get; set; }
        public AppUser? Assignee { get; set; }  
        public Subject? Subject { get; set; }   
        public Setting? Setting { get; set; }
        [NotMapped]
        public List<ClassStudent> ClassStudents { get; set; }
        [NotMapped]
        public List<Milestone> Milestones { get; set; }
        [NotMapped]
        public List<Project> Projects { get; set; }
        [NotMapped]
        public List<IssueSetting>? IssueSettings { get; set; }
    }
}
