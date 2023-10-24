﻿using AutoMapper;
using IMS.Contract.Common.UnitOfWorks;
using IMS.Contract.Contents.Subjects;
using IMS.Domain.Contents;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.Contents
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public readonly ISubjectService _subjectService;
        public readonly IMapper _mapper;

        public SubjectController(ISubjectService subjectService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _subjectService = subjectService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDto>> GetSubjectById(int id)
        {
            try
            {
                var subject = await _subjectService.GetBySubjectByIdAsync(id);
                return Ok(subject);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<SubjectReponse>> GetAllSubject([FromQuery] SubjectRequest request)
        {
            var subjectList = await _subjectService.GetSubjectAllAsync(request);
            return Ok(subjectList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewSubject([FromBody] CreateUpdateSubjectDto data)
        {
            var map = _mapper.Map<Subject>(data);
            var result = await _subjectService.InsertAsync(map);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Add successfully ");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, [FromBody] CreateUpdateSubjectDto input)
        {
            var data = await _subjectService.GetById(id);
            if (data == null)
            {
                return NotFound("Not found subject");
            }
            else
            {
                var map = _mapper.Map(input, data);
                var result = await _subjectService.UpdateAsync(map);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Update Successfully");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var data = await _subjectService.GetById(id);
            if (data != null)
            {
                await _subjectService.DeleteAsync(data);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Delete Successfully !!!");
            }
            else
            {
                return NotFound("Not found subject");
            }
        }
    }
}
