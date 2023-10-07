using IMS.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Contents
{
    public class Project : Auditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Visibility {  get; set; }
        public string AvatarUrl { get; set; }
        public int Status { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public ICollection<ProjectMember> ProjectMembers { get; set; }
        public ICollection<Milestone> Milestones { get; set; }  
        public ICollection<Issues> Issues { get; set; }
        public ICollection<IssueSetting> IssueSettings{ get; set; }
     
    }
}
