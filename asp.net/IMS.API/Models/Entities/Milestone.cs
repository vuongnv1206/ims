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
    public class Milestone : Auditable
    {
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int? ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        [JsonIgnore]
        public virtual Project? Project { get; set; }
        public int? ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        [JsonIgnore]
        public virtual Class? Class { get; set; }
        [JsonIgnore]
        public virtual ICollection<Issue>? Issues { get; set; } 

    }
}
