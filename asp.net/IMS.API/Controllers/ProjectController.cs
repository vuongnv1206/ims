using AutoMapper;
using IMS.Api.Common.UnitOfWorks;
using IMS.Api.Interfaces;
using IMS.Api.Models.Dtos.ProjectMembers;
using IMS.Api.Models.Dtos.Projects;
using IMS.Api.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IMS.Api.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IProjectService _projectService;
        public readonly IProjectMemberService _projectMemberService;
        public readonly IMapper _mapper;

        public ProjectController(IProjectService projectService,IProjectMemberService projectMemberService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _projectService = projectService;
            _projectMemberService = projectMemberService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<ProjectReponse>> GetAllProject([FromQuery] ProjectRequest request)
        {
            var data = await _projectService.GetAllProjectAsync(request);
            return Ok(data);
        }

        [HttpGet("projectId")]
        public async Task<ActionResult<ProjectReponse>> GetProjectById(int projectId)
        {
            var data = await _projectService.GetProjectById(projectId);
            return Ok(data);
        }


        [HttpPost]
        public async Task<IActionResult> CreateNewProject([FromBody] CreateAndUpdateProjectDto data)
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

        [HttpPost("add-student")]
        public async Task<IActionResult> AddStudentsForProject([FromBody] List<ProjectMemberDto> data)
        {
            var map = _mapper.Map<List<ProjectMember>>(data);
            var result = _projectMemberService.InsertManyAsync(map);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Add student successfully");
        }

        [HttpDelete("delete-student/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var data = await _projectMemberService.GetById(id);
            if (data != null)
            {
                await _projectMemberService.DeleteAsync(data);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Delete Successfully !!!");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
