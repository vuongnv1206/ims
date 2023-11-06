using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Models.Dtos.Milestones

{
    public class CreateMilestoneDto
    {
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int? ProjectId { get; set; }
        public int? ClassId { get; set; }
    }
}
