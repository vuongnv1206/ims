using AutoMapper;
using IMS.BusinessService.Service;
using IMS.Contract.Common.Sorting;
using IMS.Contract.Contents.Issues;
using IMS.Contract.Contents.Milestones;
using IMS.Domain.Contents;
using IMS.Infrastructure.EnityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BusinessService.Systems
{
    internal class IssueService : ServiceBase<Issue>, IIssueService
    {
        public IssueService(IMSDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<IssueResponse> GetIssue(IssueRequest request)
        {
            var issueQuery = await context.Issues
                 .Where(u => string.IsNullOrWhiteSpace(request.KeyWords)
                         || u.Name.Contains(request.KeyWords)).ToListAsync();

            var issues = issueQuery.Paginate(request);
            if (request.ProjectId != null)
            {
                issues = issues.Where(x => x.ProjectId == request.ProjectId);
            }
            if (request.AssigneeId != null)
            {
                issues = issues.Where(x => x.AssigneeId == request.AssigneeId);
            }
            if (request.IssueSettingId != null)
            {
                issues = issues.Where(x => x.IssueSettingId == request.IssueSettingId);
            }
            if (request.MilestoneId != null)
            {
                issues = issues.Where(x => x.MilestoneId == request.MilestoneId);
            }
            if (request.Name != null)
            {
                issues = issues.Where(x => x.Name == request.Name);
            }
            if (request.StartDate != null && request.DueDate != null)
            {
                issues = issues.Where(x => x.StartDate == request.StartDate && x.DueDate == request.DueDate) ;
            }
            var issueDtos = mapper.Map<List<IssueDto>>(issues);



            var response = new IssueResponse
            {
                Issues = issueDtos,
                Page = GetPagingResponse(request, issueQuery.Count()),
            };

            return response;
        }

        
    }
}
