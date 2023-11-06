using IMS.Api.Common.UnitOfWorks;
using IMS.Api.Models.Dtos.Classes;
using IMS.Api.Models.Entities;

namespace IMS.Api.Interfaces
{
    public interface IClassService : IGenericRepository<Class>

    {
        Task<ClassReponse> GetAllClass(ClassRequest request);
        Task<ClassDto> GetClassById(int classId);
    }
}
