using AutoMapper;
using IMS.Api.EnityFrameworkCore;
using IMS.Api.Interfaces;
using IMS.Api.Models.Entities;

namespace IMS.Api.Services
{
    public class ClassStudentService : ServiceBase<ClassStudent> , IClassStudentService
    {
        private readonly IMapper _mapper;

        public ClassStudentService(IMSDbContext context,IMapper mapper ) : base(context,mapper) 
        { 
            _mapper = mapper;
        }
    }
}
