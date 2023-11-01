using IMS.Contract.Common.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IMS.Api.Dtos.Projects
{
    public interface IProjectService : IGenericRepository<Project>
    {
        Task<ProjectReponse> GetAllProjectAsync(ProjectRequest request);
        Task<ProjectDto> GetProjectById(int Id);
    }
}
