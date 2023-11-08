namespace IMS.Api.Models.Dtos.Issues
{
    public class BatchUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid AssigneeId { get; set; }
        public bool IsOpen { get; set; }
        public int ProjectId { get; set; }
        public int? IssueSettingId { get; set; }
        public int MilestoneId { get; set; }
    }
}
