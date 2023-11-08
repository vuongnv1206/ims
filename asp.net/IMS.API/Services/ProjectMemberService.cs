using AutoMapper;
using IMS.Api.EnityFrameworkCore;
using IMS.Api.Interfaces;
using IMS.Api.Models.Dtos.Issues;
using IMS.Api.Models.Dtos.ProjectMembers;
using IMS.Api.Models.Dtos.Projects;
using IMS.Api.Models.Dtos.Users;
using IMS.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.Api.Services
{
    public class ProjectMemberService : ServiceBase<ProjectMember>, IProjectMemberService
    {
        private readonly IMapper _mapper;

        public ProjectMemberService(IMSDbContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<ProjectMemberDto>> GetAllAssignee(int pid)
        {
            var query = await context.ProjectMembers
                .Include(x => x.User)
                .Include(x => x.Project)
                .Where(u => u.ProjectId == pid).ToListAsync();

            var dtos = mapper.Map<List<ProjectMemberDto>>(query).ToList();

            return dtos;
        }
    }
}
