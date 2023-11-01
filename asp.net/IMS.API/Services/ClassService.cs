using AutoMapper;
using IMS.Api.EnityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.Api.Models.Entities;
using IMS.Api.Dtos.Classes;
using IMS.Api.Interfaces;
using IMS.Api.Helpers.Extensions;

namespace IMS.Api.Services
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
                classQuery = classQuery.Where(x => x.SubjectId == request.SubjectId).ToList();
            }
            if (request.SettingId != null)
            {
                classQuery = classQuery.Where(x => x.SettingId == request.SettingId).ToList();
            }

            var classDtos = mapper.Map<List<ClassDto>>(classQuery).Paginate(request).ToList();

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
