using IMS.Domain.Contents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IMS.Contract.Contents.Subjects
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        [NotMapped]
        public IEnumerable<Assignment> Assignments { get; set; }
        [NotMapped]
        public IEnumerable<Class> Classes { get; set; }
        [NotMapped]
        public IEnumerable<IssueSetting>? IssueSettings { get; set; }
        [NotMapped]
        public IEnumerable<SubjectUser> SubjectUsers { get; set; }
    }
}
