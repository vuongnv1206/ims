using AutoMapper;
using IMS.BusinessService.Service;
using IMS.Contract.Common.Sorting;
using IMS.Contract.Contents.Projects;
using IMS.Contract.Contents.Subjects;
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
    public class ProjectService : ServiceBase<Project>, IProjectService
    {
        private readonly IMapper _mapper;
        public ProjectService(IMSDbContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

        public async Task<ProjectReponse> GetAllProjectAsync(ProjectRequest request)
        {
            var projectQuery = await context.Projects
                .Include(x => x.Milestones)
                .Include(x => x.IssueSettings)
                .Include(x => x.Issues)
                .Include(x => x.ProjectMembers)
                .Where(u => string.IsNullOrWhiteSpace(request.KeyWords)
            || u.Name.Contains(request.KeyWords)
            || u.Description.Contains(request.KeyWords)).ToListAsync();
            var projects = projectQuery.Paginate(request);
            var projectDtos = mapper.Map<List<ProjectDto>>(projects);

            var response = new ProjectReponse
            {
                Projects = projectDtos,
                Page = GetPagingResponse(request, projectQuery.Count()),
            };

            return response;
        }
    }
}
