using IMS.Api.Common.UnitOfWorks;
using IMS.Api.Models.Dtos.ProjectMembers;
using IMS.Api.Models.Dtos.Users;
using IMS.Api.Models.Entities;

namespace IMS.Api.Interfaces
{
    public interface IProjectMemberService : IGenericRepository<ProjectMember>
    {
        Task<List<ProjectMemberDto>> GetAllAssignee(int pid);
    }
}
