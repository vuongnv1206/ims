using IMS.Contract.Common.Paging;
using IMS.Contract.Common.UnitOfWorks;
using IMS.Contract.Contents.Milestones;
using IMS.Domain.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Contents.Issues
{
    public interface IIssueService : IGenericRepository<Issue>
    {
        Task<IssueResponse> GetIssue(IssueRequest request);
        Task<IssueDto> GetIssueById(int issueId);
    }
}
