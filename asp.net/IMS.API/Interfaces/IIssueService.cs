using IMS.Api.Dtos.Issues;
using IMS.Api.Models.Entities;
using IMS.Contract.Common.Paging;
using IMS.Contract.Common.UnitOfWorks;
using IMS.Contract.Contents.Issues;
using IMS.Contract.Contents.Milestones;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Interfaces
{
    public interface IIssueService : IGenericRepository<Issue>
    {
        Task<IssueResponse> GetIssue(IssueRequest request);
        Task<IssueDto> GetIssueById(int issueId);
    }
}
