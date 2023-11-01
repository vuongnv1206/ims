using IMS.Api.Common.UnitOfWorks;
using IMS.Api.Dtos.Projects;
using IMS.Api.Models.Entities;
namespace IMS.Api.Interfaces
{
    public interface IProjectService : IGenericRepository<Project>
    {
        Task<ProjectReponse> GetAllProjectAsync(ProjectRequest request);
        Task<ProjectDto> GetProjectById(int Id);
    }
}
