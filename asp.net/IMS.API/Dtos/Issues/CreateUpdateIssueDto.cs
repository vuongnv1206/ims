﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Dtos.Issues;

public class CreateUpdateIssueDto
{
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
