using IMS.Api.Common.UnitOfWorks;
using IMS.Api.Models.Dtos.Issues;
using IMS.Api.Models.Entities;

namespace IMS.Api.Interfaces
{
    public interface IIssueService : IGenericRepository<Issue>
    {
        Task<IssueResponse> GetIssue(IssueRequest request);
        Task<IssueDto> GetIssueById(int issueId);
    }
}
