using IMS.Api.Models.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Api.Models.Entities
{
    public class Assignment : Auditable
    {
        public string Name{ get; set; }
        public string? Description{ get; set; }
        public int SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public virtual Subject? Subject { get; set; }
    }
}
