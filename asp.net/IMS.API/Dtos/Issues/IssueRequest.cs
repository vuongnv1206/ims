using IMS.Api.Common.Paging;
using IMS.Api.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Dtos.Issues
{
    public class IssueRequest : PagingRequestBase
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid? AssigneeId { get; set; }
        public int? ProjectId { get; set; }
        public int? IssueSettingId { get; set; }
        public int? MilestoneId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? IsOpen { get; set; }
        public string? ProjectName { get; set; }
        public string? AssigneeName { get; set; }
        public string? MilestoneName { get; set; }

    }
}
