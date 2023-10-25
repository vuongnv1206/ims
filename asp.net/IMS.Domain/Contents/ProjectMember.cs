using IMS.Domain.Abstracts;
using IMS.Domain.Systems;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Contents
{
    public class ProjectMember : Auditable
    {
        public Guid UserId { get; set; }
        public int ProjectId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual AppUser? User { get; set; }
        [ForeignKey(nameof(ProjectId))]
        public virtual Project? Project { get; set; }
    }
}
