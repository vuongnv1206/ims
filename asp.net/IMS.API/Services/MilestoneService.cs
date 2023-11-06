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

            var milestones = milestoneQuery.Paginate(request);
            if(request.ProjectId != null)
            {
                milestones = milestones.Where(x => x.ProjectId == request.ProjectId);
            }
            if(request.ClassId != null)
            {
                milestones = milestones.Where(x => x.ClassId == request.ClassId);
            }
            if(request.StartDate != null ) 
            {
                milestones = milestones.Where(x => x.StartDate >= request.StartDate).ToList();
            }
            if(request.DueDate != null )
            {
                milestones = milestones.Where(x => x.DueDate <= request.DueDate).ToList();
            }
            var milestoneDtos = mapper.Map<List<MilestoneDto>>(milestones);



            var response = new MilestoneResponse
            {
                Milestones = milestoneDtos,
                Page = GetPagingResponse(request, milestoneQuery.Count()),
            };

            return response;
        }
    }
}
