using AutoMapper;
using IMS.BusinessService.Service;
using IMS.Contract.Common.Sorting;
using IMS.Contract.Contents.Assignments;
using IMS.Contract.Contents.Subjects;
using IMS.Domain.Contents;
using IMS.Api.EnityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Services
{
    public class SubjectService : ServiceBase<Subject>, ISubjectService
    {
        private readonly IMapper _mapper;

        public SubjectService(IMSDbContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

        public async Task<SubjectDto> GetBySubjectByIdAsync(int subjectId)
        {
            var subject = await context.Subjects
            .Include(x => x.Assignments)
            .Include(x => x.Classes)
            .Include(x => x.IssueSettings)
            .Include(x => x.SubjectUsers)
            .FirstOrDefaultAsync(u => u.Id == subjectId);

            var subjectDto = mapper.Map<SubjectDto>(subject);
            return subjectDto;
        }

        public async Task<SubjectReponse> GetSubjectAllAsync(SubjectRequest request)
        {
            var subjectQuery = await context.Subjects
                .Include(x => x.Assignments)
                .Include(x => x.Classes)
                .Include(x => x.IssueSettings)
                .Include(x => x.SubjectUsers)
                .Where(u => string.IsNullOrWhiteSpace(request.KeyWords)
            || u.Name.Contains(request.KeyWords)
            || u.Description.Contains(request.KeyWords)).ToListAsync();
            var subjects = subjectQuery.Paginate(request);
            var subjectDtos = mapper.Map<List<SubjectDto>>(subjects);

            var response = new SubjectReponse
            {
                Subjects = subjectDtos,
                Page = GetPagingResponse(request, subjectQuery.Count()),
            };

            return response;
        }
    }
}
