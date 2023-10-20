using IMS.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IMS.Domain.Contents
{
    public  class Subject : Auditable
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public ICollection<Assignment> Assignments { get; set; }
        [JsonIgnore]
        public ICollection<Class> Classes { get; set; }
        [JsonIgnore]
        public ICollection<IssueSetting>? IssueSettings { get; set; }
        [JsonIgnore]
        public ICollection<SubjectUser> SubjectUsers { get; set; }
    }
}
