using IMS.Api.Common.UnitOfWorks;
using IMS.Api.Dtos.Subjects;
using IMS.Api.Models.Entities;

namespace IMS.Api.Interfaces
{
    public interface ISubjectService : IGenericRepository<Subject>
    {
        Task<SubjectReponse> GetSubjectAllAsync(SubjectRequest request);
        Task<SubjectDto> GetBySubjectByIdAsync(int subjectId);
    }
}
