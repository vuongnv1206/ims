using AutoMapper;
using IMS.BusinessService.Service;
using IMS.Contract.Common.Paging;
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
                .Include(x => x.Milestone)
                .Include(x => x.Project)
                .Include(x => x.Assignee)
                .Include(x => x.IssueSetting)
                 .Where(u => string.IsNullOrWhiteSpace(request.KeyWords)
                         || u.Name.Contains(request.KeyWords)).ToListAsync();

           
            if (request.ProjectId != null)
            {
                issueQuery = (List<Issue>)issueQuery.Where(x => x.ProjectId == request.ProjectId);
            }
            if (request.AssigneeId != null)
            {
                issueQuery = (List<Issue>)issueQuery.Where(x => x.AssigneeId == request.AssigneeId);
            }
            if (request.IssueSettingId != null)
            {
                issueQuery = (List<Issue>)issueQuery.Where(x => x.IssueSettingId == request.IssueSettingId);
            }
            if (request.MilestoneId != null)
            {
                issueQuery = (List<Issue>)issueQuery.Where(x => x.MilestoneId == request.MilestoneId);
            }
            if (request.StartDate != null )
            {
                issueQuery = (List<Issue>)issueQuery.Where(x => x.StartDate >= request.StartDate ) ;
            }
            if ( request.DueDate != null)
            {
                issueQuery = (List<Issue>)issueQuery.Where(x => x.DueDate <= request.DueDate);
            }

            var issueDtos = mapper.Map<List<IssueDto>>(issueQuery);

            var issues = issueQuery.Paginate(request);

            var response = new IssueResponse
            {
                Issues = issueDtos,
                Page = GetPagingResponse(request, issues.Count()),
            };

            return response;
        }

        public async Task<IssueDto> GetIssueById(int Id)
        {
            var issue = await context.Issues
                .Include(x => x.Milestone)
                .Include(x => x.Project)
                .Include(x => x.Assignee)
                .Include(x => x.IssueSetting)
            .FirstOrDefaultAsync(u => u.Id == Id);

            var issueDto = mapper.Map<IssueDto>(issue);
            return issueDto;
        }



    }
}
