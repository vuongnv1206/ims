using IMS.Api.Models.Abstracts;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IMS.Api.Models.Entities
{
    public class Issue : Auditable
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? AssigneeId { get; set; }
        public bool IsOpen {  get; set; }
        public int? ProjectId { get; set; }
        public int? IssueSettingId { get; set; }
        public int? MilestoneId { get; set; }

        [ForeignKey(nameof(AssigneeId))]
        [JsonIgnore]
        public virtual AppUser? Assignee{ get; set; }
        [ForeignKey(nameof(MilestoneId))]
        [JsonIgnore]
        public virtual Milestone? Milestone { get; set; }
        [ForeignKey(nameof(ProjectId))]
        [JsonIgnore]
        public virtual Project? Project { get; set; }

        [ForeignKey(nameof(IssueSettingId))]
        [JsonIgnore]
        public virtual IssueSetting? IssueSetting { get; set; }
        

    }
}
