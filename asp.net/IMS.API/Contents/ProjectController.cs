using AutoMapper;
using IMS.BusinessService.Systems;
using IMS.Contract.Common.UnitOfWorks;
using IMS.Contract.Contents.Projects;
using IMS.Contract.Contents.Subjects;
using IMS.Contract.Systems.Users;
using IMS.Domain.Contents;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.Contents
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IProjectService _projectService;
        public readonly IMapper _mapper;

        public ProjectController(IProjectService projectService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _projectService = projectService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<ProjectReponse>> GetAllProject([FromQuery] ProjectRequest request)
        {
            var data = await _projectService.GetAllProjectAsync(request);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewSubject([FromBody] CreateAndUpdateProjectDto data)
        {
            var map = _mapper.Map<Project>(data);
            var result = await _projectService.InsertAsync(map);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Add successfully ");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] CreateAndUpdateProjectDto input)
        {
            var data = await _projectService.GetById(id);
            if (data == null)
            {
                return NotFound("Not found project");
            }
            else
            {
                var map = _mapper.Map(input, data);
                var result = await _projectService.UpdateAsync(map);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Update Successfully");
            }

        }
    }
}
