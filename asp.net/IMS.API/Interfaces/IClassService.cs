using IMS.Api.Dtos.Classes;
using IMS.Contract.Common.UnitOfWorks;
using IMS.Domain.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Interfaces
{
    public interface IClassService : IGenericRepository<Class>
    {
        Task<ClassReponse> GetAllClass(ClassRequest request);
        Task<ClassDto> GetClassById(int classId);
    }
}
