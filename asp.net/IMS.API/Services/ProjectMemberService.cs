using AutoMapper;
using IMS.Api.EnityFrameworkCore;
using IMS.Api.Interfaces;
using IMS.Api.Models.Entities;

namespace IMS.Api.Services
{
    public class ProjectMemberService : ServiceBase<ProjectMember>, IProjectMemberService
    {
        private readonly IMapper _mapper;

        public ProjectMemberService(IMSDbContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }
    }
}
