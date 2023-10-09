using IMS.Contract.Common.UnitOfWorks;
using IMS.Contract.Systems.Roles;
using IMS.Contract.Systems.Users;
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
        Task<SubjectReponse> GetListAllAsync(SubjectRequest request);
    }
}
