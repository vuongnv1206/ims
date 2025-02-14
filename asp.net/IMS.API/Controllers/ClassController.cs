﻿using AutoMapper;
using IMS.Api.Common.Constants;
using IMS.Api.Common.UnitOfWorks;
using IMS.Api.Interfaces;
using IMS.Api.Models.Dtos.Classes;
using IMS.Api.Models.Dtos.ClassMembers;
using IMS.Api.Models.Dtos.Projects;
using IMS.Api.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IClassService _classService;
        public readonly IClassStudentService _classStudentService;
        public readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
  
        public ClassController(IClassService classService, IMapper mapper, IUnitOfWork unitOfWork , IClassStudentService classStudentService, UserManager<AppUser> userManager)
        {
            _classService = classService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _classStudentService = classStudentService;
            _userManager = userManager;
        }

        [Authorize(Permissions.Class.View)]
        [HttpGet("classes")]
        public async Task<ActionResult<ClassReponse>> GetAllClass([FromQuery] ClassRequest request)
        {   

            var data = await _classService.GetAllClass(request);
            return Ok(data); 
        }

        [Authorize(Permissions.Class.View)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassDto>> GetClassByid(int id)
        {
            var data = await _classService.GetClassById(id);
            return Ok(data);
        }

        [Authorize(Permissions.Class.Create)]
        [HttpPost]
        public async Task<IActionResult> CreateNewClass([FromBody] CreateAndUpdateClassDto data)
        {
            var map = _mapper.Map<Class>(data);
            var result = await _classService.InsertAsync(map);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Add successfully ");
        }

        [Authorize(Permissions.Class.Edit)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass(int id, [FromBody] CreateAndUpdateClassDto input)
        {
            var data = await _classService.GetById(id);
            if (data == null)
            {
                return BadRequest();
            }
            else
            {
                var map = _mapper.Map(input, data);
                var result = await _classService.UpdateAsync(map);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Update Successfully ");
            }
        }

        [HttpPost("add-student")]
        public async Task<IActionResult> AddStudentForClass([FromBody] List<ClassStudentDto> data)
        {
            var map = _mapper.Map<List<ClassStudent>>(data);
            var result = _classStudentService.InsertManyAsync(map);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Add Student Successful !!!");
        }

        [HttpDelete("delete-student/{id}")]
        public async Task<IActionResult> DeleteStudentForClass(int id)
        {
            var data = await _classStudentService.GetById(id);
            if (data != null)
            {
                await _classStudentService.DeleteAsync(data);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Delete Successful !!!");
            }else
            {
                return BadRequest();
            }
        }
    }
}
