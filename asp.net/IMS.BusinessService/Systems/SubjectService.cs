using AutoMapper;
using IMS.BusinessService.Service;
using IMS.Contract.Common.Sorting;
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
    public class SubjectService : ServiceBase<Subject>, ISubjectService
    {
        private readonly IMapper _mapper;
        public SubjectService(IMSDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._mapper = mapper;
        }

        public async Task<SubjectReponse> GetListAllAsync(SubjectRequest request)
        {
            var subjectQuery = await context.Subjects.Where(u => string.IsNullOrWhiteSpace(request.KeyWords)
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
