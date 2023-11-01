
using IMS.Api.Models.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Api.Models.Entities
{
    public class ClassStudent : Auditable
    {
        public Guid UserId { get; set; }
        public int ClassId { get; set; }


        [ForeignKey(nameof(ClassId))]
        public virtual Class? Class { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual AppUser? User{ get; set; }
    }
}
