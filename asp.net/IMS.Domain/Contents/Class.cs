using IMS.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Contents
{
    public class Class : Auditable
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int SettingId { get; set; }
        public Setting Setting { get; set; }
        public ICollection<ClassStudent> ClassStudents { get; set; }
        public ICollection<Milestone> Milestones { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<IssueSetting> IssueSettings { get; set; }
    }
}
