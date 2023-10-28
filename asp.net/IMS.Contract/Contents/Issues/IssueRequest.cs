﻿using IMS.Contract.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Contents.Issues
{
    public class IssueRequest : PagingRequestBase
    {
        public string? Name { get; set; }
        public Guid? AssigneeId { get; set; }
        public int? ProjectId { get; set; }
        public int? IssueSettingId { get; set; }
        public int? MilestoneId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
