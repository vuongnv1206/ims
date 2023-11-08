using IMS.Api.Models.Entities;

namespace IMS.Api.Models.Dtos.ProjectMembers
{
    public class ProjectMemberDto
    {
        public int ProjectId { get; set; }
        public Guid UserId { get; set; }
    }
}
