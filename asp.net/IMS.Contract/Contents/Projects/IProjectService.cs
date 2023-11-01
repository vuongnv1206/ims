using IMS.Contract.Common.UnitOfWorks;
using IMS.Domain.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Contents.Projects
{
    public interface IProjectService : IGenericRepository<Project>
    {
        Task<ProjectReponse> GetAllProjectAsync(ProjectRequest request);
        Task<ProjectDto> GetProjectById(int Id);
    }
}
