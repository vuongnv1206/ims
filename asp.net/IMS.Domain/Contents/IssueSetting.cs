using IMS.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Contents
{
    public class IssueSetting : Auditable
    {
        public int? ProjectId { get; set; }
       
        public int? ClassId { get; set; }
       
        public int? SubjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public virtual Project? Project { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public virtual Subject? Subject { get; set; }
        [ForeignKey(nameof(ClassId))]
        public virtual Class? Class { get; set; }
        public virtual ICollection<Issue>? Issues { get; set; }

    }
}
