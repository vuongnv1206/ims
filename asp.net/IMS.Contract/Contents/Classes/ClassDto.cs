﻿using IMS.Domain.Contents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Contents.Classes
{
    public class ClassDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid? AssigneId { get; set; }
        public string? Description { get; set; }
        public int SubjectId { get; set; }
        public int SettingId { get; set; }
        public List<ClassStudent> ClassStudents { get; set; }
        public List<Milestone> Milestones { get; set; }
        public List<Project> Projects { get; set; }
        public List<IssueSetting>? IssueSettings { get; set; }
    }
}
