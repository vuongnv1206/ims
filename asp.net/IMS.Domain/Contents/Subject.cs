using IMS.Domain.Abstracts;
using IMS.Domain.Systems;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public int ManagerId { get; set; }

        [ForeignKey(nameof(ManagerId))]
        public virtual AppUser Manager { get; set; }

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
