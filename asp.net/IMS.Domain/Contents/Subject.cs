using IMS.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Contents
{
    public  class Subject : Auditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<Class> Classes { get; set; }
        public ICollection<IssueSetting> IssueSettings { get; set; }
        public ICollection<SubjectUser> SubjectUsers { get; set; }
    }
}
