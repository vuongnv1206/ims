﻿using AutoMapper;
using IMS.Api.EnityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.Api.Models.Entities;
using IMS.Api.Interfaces;
using IMS.Api.Models.Dtos.Issues;
using IMS.Api.Common.Helpers.Extensions;
using IMS.Api.Common.UnitOfWorks;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IMS.Api.Services
{
    internal class IssueService : ServiceBase<Issue>, IIssueService
    {
      
        public IssueService(IMSDbContext context, IMapper mapper, IUnitOfWork unitOfWork) : base(context, mapper)
        { 
         
         
        }

        public async Task<Issue> GetIssueBatchUpdate(BatchUpdateDto data)
        {          
            var issue = await context.Issues.Where(x => x.Id == data.Id).FirstAsync();
            if (issue == null)
            {
                throw new Exception("Not found");
            }
            
            return issue;           
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
                issueQuery = issueQuery.Where(x => x.ProjectId == request.ProjectId).ToList();
            }
            if (request.AssigneeId != null)
            {
                issueQuery = issueQuery.Where(x => x.AssigneeId == request.AssigneeId).ToList();
            }
            if (request.IssueSettingId != null)
            {
                issueQuery = issueQuery.Where(x => x.IssueSettingId == request.IssueSettingId).ToList();
            }
            if (request.MilestoneId != null)
            {
                issueQuery = issueQuery.Where(x => x.MilestoneId == request.MilestoneId).ToList();
            }
            if (request.StartDate != null )
            {
                issueQuery = issueQuery.Where(x => x.StartDate >= request.StartDate).ToList();
            }
            if ( request.DueDate != null)
            {
                issueQuery = issueQuery.Where(x => x.DueDate <= request.DueDate).ToList();
            }

            var issueDtos = mapper.Map<List<IssueDto>>(issueQuery).Paginate(request).ToList();

            

            var response = new IssueResponse
            {
                Issues = issueDtos,
                Page = GetPagingResponse(request, issueQuery.Count()),
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
