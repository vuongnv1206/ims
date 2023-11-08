using IMS.Api.Common.UnitOfWorks;
using IMS.Api.Models.Dtos.Issues;
using IMS.Api.Models.Dtos.IssueSettings;
using IMS.Api.Models.Entities;

namespace IMS.Api.Interfaces
{
    public interface IIssueSettingService : IGenericRepository<IssueSetting>
    {       
        Task<IssueSettingDto> GetIssueSetting(int projectId, int classId, int subjectId);
    }
}
