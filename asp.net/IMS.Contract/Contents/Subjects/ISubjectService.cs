using IMS.Contract.Common.UnitOfWorks;
using IMS.Domain.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Contents.Subjects
{
    public interface ISubjectService : IGenericRepository<Subject>
    {
        Task<SubjectReponse> GetSubjectAllAsync(SubjectRequest request);
        Task<SubjectDto> GetBySubjectByIdAsync(int subjectId);
    }
}
