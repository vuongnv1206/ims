
using IMS.Api.Models.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IMS.Api.Models.Entities
{
    public class ClassStudent : Auditable
    {
        public Guid UserId { get; set; }
        public int ClassId { get; set; }


        [ForeignKey(nameof(ClassId))]
        [JsonIgnore]
        public virtual Class? Class { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(UserId))]
        public virtual AppUser? User{ get; set; }
    }
}
