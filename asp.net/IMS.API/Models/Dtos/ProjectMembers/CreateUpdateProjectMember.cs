namespace IMS.Api.Models.Dtos.ProjectMembers
{
    public class CreateUpdateProjectMember
    {
        public int ProjectId { get; set; }
        public Guid UserId { get; set; }
    }
}
