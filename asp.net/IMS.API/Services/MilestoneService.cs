using AutoMapper;
using IMS.Api.EnityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.Api.Models.Entities;
using IMS.Api.Interfaces;
using IMS.Api.Models.Dtos.Milestones;
using IMS.Api.Common.Helpers.Extensions;

namespace IMS.Api.Services
{
    public class MilestoneService : ServiceBase<Milestone>, IMilestoneService
    {
        public MilestoneService(IMSDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<MilestoneResponse> GetMilestone(MilestoneRequest request)
        {
            var milestoneQuery = await context.Milestones.Include(x => x.Project).Include(x => x.Class)
                .Where(u => string.IsNullOrWhiteSpace(request.KeyWords)
                        || u.Description.Contains(request.KeyWords)).ToListAsync();

            if(request.ProjectId != null)
            {
                milestoneQuery = milestoneQuery.Where(x => x.ProjectId == request.ProjectId).ToList();
            }
            if(request.ClassId != null)
            {
                milestoneQuery = milestoneQuery.Where(x => x.ClassId == request.ClassId).ToList();
            }
            if(request.StartDate != null ) 
            {
                milestoneQuery = milestoneQuery.Where(x => x.StartDate >= request.StartDate).ToList();
            }
            if(request.DueDate != null )
            {
                milestoneQuery = milestoneQuery.Where(x => x.DueDate <= request.DueDate).ToList();
            }
            var milestoneDtos = mapper.Map<List<MilestoneDto>>(milestoneQuery).Paginate(request).ToList();



            var response = new MilestoneResponse
            {
                Milestones = milestoneDtos,
                Page = GetPagingResponse(request, milestoneQuery.Count()),
            };

            return response;
        }
    }
}
