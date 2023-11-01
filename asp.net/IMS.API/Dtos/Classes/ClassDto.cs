using IMS.Api.Models.Entities;

namespace IMS.Api.Dtos.Classes
{
    public class ClassDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid? AssigneId { get; set; }
        public string? Description { get; set; }
        public int SubjectId { get; set; }
        public int SettingId { get; set; }
        public List<ClassStudent> ClassStudents { get; set; }
        public List<Milestone> Milestones { get; set; }
        public List<Project> Projects { get; set; }
        public List<IssueSetting>? IssueSettings { get; set; }
    }
}
