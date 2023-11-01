
using IMS.Api.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Models.Entities
{
    public class SubjectUser : Auditable
    {
        public Guid UserId { get; set; }
        public int SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public virtual Subject? Subject { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual AppUser? User { get; set; }
    }
}
