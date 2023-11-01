using IMS.Api.Models.Entities;
using IMS.Contract.Common.UnitOfWorks;
using IMS.Contract.Contents.Assignments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Dtos.Milestones
{
    public interface IMilestoneService : IGenericRepository<Milestone>
    {
        Task<MilestoneResponse> GetMilestone(MilestoneRequest request);
    }
}
