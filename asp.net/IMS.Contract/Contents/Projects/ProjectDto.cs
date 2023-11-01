using IMS.Domain.Contents;
using IMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Contents.Projects
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? AvatarUrl { get; set; }
        public ProjectStatus Status { get; set; }
        public int ClassId { get; set; }
        [NotMapped]
        public IList<ProjectMember> ProjectMembers { get; set; }
        [NotMapped]
        public IList<Milestone> Milestones { get; set; }
        [NotMapped]
        public IList<Issue> Issues { get; set; }
        [NotMapped]
        public IList<IssueSetting>? IssueSettings { get; set; }
    }
}
