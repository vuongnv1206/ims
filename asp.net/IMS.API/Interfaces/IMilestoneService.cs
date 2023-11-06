using IMS.Api.Common.UnitOfWorks;
using IMS.Api.Models.Dtos.Milestones;
using IMS.Api.Models.Entities;

namespace IMS.Api.Interfaces
{
    public interface IMilestoneService : IGenericRepository<Milestone>
    {
        Task<MilestoneResponse> GetMilestone(MilestoneRequest request);
    }
}
