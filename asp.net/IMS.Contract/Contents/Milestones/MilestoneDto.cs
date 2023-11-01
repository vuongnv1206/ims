using IMS.Domain.Contents;
using Newtonsoft.Json;

namespace IMS.Contract.Contents.Milestones
{
    public class MilestoneDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int? ProjectId { get; set; }
        public int? ClassId { get; set; }
        public Project? Project { get; set; }
        public Class? Class { get; set; }

    }
}
