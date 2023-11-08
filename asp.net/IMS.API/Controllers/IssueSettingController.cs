using AutoMapper;
using IMS.Api.Common.UnitOfWorks;
using IMS.Api.EnityFrameworkCore;
using IMS.Api.Interfaces;
using IMS.Api.Models.Dtos.Issues;
using IMS.Api.Models.Dtos.IssueSettings;
using IMS.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace IMS.Api.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueSettingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IIssueSettingService _issueSettingService;
        public readonly IIssueService _issueService;
        public readonly IMapper _mapper;

        public IssueSettingController(IUnitOfWork unitOfWork, IIssueSettingService issueSettingService, 
            IIssueService issueService, IMapper mapper, IMSDbContext context)
        {
            _unitOfWork = unitOfWork;
            _issueSettingService = issueSettingService;
            _issueService = issueService;
            _mapper = mapper;
        }
        

        [HttpGet("IssueSetting")]
        public async Task<ActionResult<IssueSettingDto>> GetIssueSetting(int projectId, int classId, int subjectId)
        {
            var data = await _issueSettingService.GetIssueSetting(projectId, classId, subjectId);
            return Ok(data);
        }


        [HttpDelete("DeleteIssueSetting/{id}")]
        public async Task<IActionResult> DeleteIssueSetting(int id)
        {
            var data = await _issueSettingService.GetById(id);
            if (data != null)
            {
                Expression<Func<Issue, bool>> filter = e => e.IssueSettingId == id;
                
                await _issueService.DeleteManyAsync(filter);
                await _issueSettingService.DeleteAsync(data);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Delete successfully !!!");
            }
            else
            {
                return BadRequest("Delete Fail !!!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateIssueSetting([FromBody] CreateUpdateIssueSettingDto data)
        {
            var map = _mapper.Map<IssueSetting>(data);
            var result = await _issueSettingService.InsertAsync(map);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Add Successfully !!!");
        }     
    }
}
