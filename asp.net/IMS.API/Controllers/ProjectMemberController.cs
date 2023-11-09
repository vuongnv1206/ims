using AutoMapper;
using IMS.Api.Common.UnitOfWorks;
using IMS.Api.Interfaces;
using IMS.Api.Models.Dtos.Issues;
using IMS.Api.Models.Dtos.ProjectMembers;
using IMS.Api.Models.Dtos.Projects;
using IMS.Api.Models.Dtos.Users;
using IMS.Api.Models.Entities;
using IMS.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IMS.Api.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectMemberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IProjectService _projectService;
        public readonly IProjectMemberService _projectMemberService;
        public readonly IMapper _mapper;

        public ProjectMemberController(IProjectService projectService, IProjectMemberService projectMemberService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _projectService = projectService;
            _projectMemberService = projectMemberService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<List<ProjectMemberDto>> GetAllProject(int id)
        {
            var data = await _projectMemberService.GetAllAssignee(id);

            return data;
        }

        


    }
}