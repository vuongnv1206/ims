using IMS.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Contents
{
    public class Issues : Auditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public Guid AssignedId { get; set; }
        public bool IsOpen {  get; set; }
        public int ProjectId { get; set; }
        public int IssueSettingId { get; set; }
        public int MilestoneId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Milestone Milestone { get; set; }
        public Project Project { get; set; }
        public IssueSetting IssueSetting { get; set; }
        public ICollection<Label> Labels { get; set; }

    }
}
