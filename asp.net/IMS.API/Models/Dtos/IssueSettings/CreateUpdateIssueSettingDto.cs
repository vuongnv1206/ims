using IMS.Api.Models.Entities;

namespace IMS.Api.Models.Dtos.IssueSettings
{
    public class CreateUpdateIssueSettingDto
    {
        public int? ProjectId { get; set; }

        public int? ClassId { get; set; }

        public int? SubjectId { get; set; }

       
    }
}
