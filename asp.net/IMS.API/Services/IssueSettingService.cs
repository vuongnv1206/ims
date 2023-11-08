using AutoMapper;
using IMS.Api.Common.UnitOfWorks;
using IMS.Api.EnityFrameworkCore;
using IMS.Api.Interfaces;
using IMS.Api.Models.Dtos.Issues;
using IMS.Api.Models.Dtos.IssueSettings;
using IMS.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.Api.Services
{
    internal class IssueSettingService : ServiceBase<IssueSetting>, IIssueSettingService
    {
        public IssueSettingService(IMSDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<IssueSettingDto> GetIssueSetting(int projectId, int classId, int subjectId)
        {
            var issueSetting = await context.IssueSettings
                .Where(x => x.ProjectId == projectId && x.ClassId == classId && x.SubjectId == subjectId)
                .FirstOrDefaultAsync();

            var issueSettingDto = mapper.Map<IssueSettingDto>(issueSetting);
            return issueSettingDto;

        }
    }
}
