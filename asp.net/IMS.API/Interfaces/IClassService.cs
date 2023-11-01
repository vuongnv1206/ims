using IMS.Api.Common.UnitOfWorks;
using IMS.Api.Dtos.Classes;
using IMS.Api.Models.Entities;
using IMS.Contract.Contents.Classes;

namespace IMS.Api.Interfaces
{
    public interface IClassService : IGenericRepository<Class>

    {
        Task<ClassReponse> GetAllClass(ClassRequest request);
        Task<ClassDto> GetClassById(int classId);
    }
}
