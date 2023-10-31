using AutoMapper;
using IMS.BusinessService.Service;
using IMS.Contract.Common.Sorting;
using IMS.Contract.Contents.Classes;
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
    public class ClassService : ServiceBase<Class>, IClassService
    {
        private readonly IMapper _mapper;

        public ClassService(IMSDbContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

        public async Task<ClassReponse> GetAllClass(ClassRequest request)
        {
            var classQuery = await context.Classes
             .Include(x => x.ClassStudents)
             .Include(x => x.Milestones)
             .Include(x => x.Projects)
             .Include(x => x.IssueSettings)
             .Where(u => string.IsNullOrWhiteSpace(request.KeyWords)
             || u.Name.Contains(request.KeyWords)
             || u.Description.Contains(request.KeyWords)).ToListAsync();

            if(request.SubjectId != null)
            {
                classes = classes.Where(x => x.SubjectId == request.SubjectId);
            }
            if (request.SettingId != null)
            {
                classes = classes.Where(x => x.SettingId == request.SettingId);
            }
            
            var classes = classQuery.Paginate(request);
            var classDtos = mapper.Map<List<ClassDto>>(classes);

            var response = new ClassReponse
            {
                Classes = classDtos,
                Page = GetPagingResponse(request, classQuery.Count()),
            };

            return response;
        }

        public async Task<ClassDto> GetClassById(int classId)
        {
            var classes = await context.Classes
           .Include(x => x.ClassStudents)
                 .Include(x => x.Milestones)
                 .Include(x => x.Projects)
                 .Include(x => x.IssueSettings)
            .FirstOrDefaultAsync(u => u.Id == classId);

            var classDto = mapper.Map<ClassDto>(classes);
            return classDto;
        }
    }
}
