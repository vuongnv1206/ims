using IMS.Api.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Dtos.Subjects
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        [NotMapped]
        public IList<Assignment> Assignments { get; set; }
        [NotMapped]
        public IList<Class> Classes { get; set; }
        [NotMapped]
        public IList<IssueSetting>? IssueSettings { get; set; }
        [NotMapped]
        public IList<SubjectUser> SubjectUsers { get; set; }
    }
}
