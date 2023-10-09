using IMS.Domain.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Contents.Subjects
{
    public class SubjectDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public IList<Assignment> Assignments { get; set; }
        public IList<Class> Classes { get; set; }
        public IList<IssueSetting>? IssueSettings { get; set; }
        public IList<SubjectUser> SubjectUsers { get; set; }
    }
}
