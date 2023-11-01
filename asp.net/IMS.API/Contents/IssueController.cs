using AutoMapper;
using IMS.Contract.Common.Paging;
using IMS.Contract.Common.UnitOfWorks;
using IMS.Contract.Contents.Issues;
using IMS.Contract.Contents.Milestones;
using IMS.Contract.Contents.Projects;
using IMS.Domain.Contents;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.Contents
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IIssueService _issueService;
        public readonly IMapper _mapper;

        public IssueController(IUnitOfWork unitOfWork, IIssueService issueService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _issueService = issueService;
            _mapper = mapper;
        }

        [HttpGet("issue")]
        public async Task<ActionResult<IssueResponse>> GetAllIssue([FromQuery] IssueRequest request)
        {
            var data = await _issueService.GetIssue(request);
            return Ok(data);
        }

        [HttpGet("Id")]
        public async Task<ActionResult<IssueDto>> GetIssueByid(int Id)
        {
            var data = await _issueService.GetIssueById(Id);
            return Ok(data);
        }


        [HttpDelete("delete-issue/{id}")]
        public async Task<IActionResult> DeleteIssue(int id)
        {
            var data = await _issueService.GetById(id);
            if (data != null)
            {
                await _issueService.DeleteAsync(data);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Delete successfully !!!");
            }
            else
            {
                return BadRequest("Delete Fail !!!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateIssue([FromBody] CreateUpdateIssueDto data)
        {
            var map = _mapper.Map<Issue>(data);
            var result = await _issueService.InsertAsync(map);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Add Successfully !!!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIssue(int id, [FromBody] CreateUpdateIssueDto data)
        {
            var input = await _issueService.GetById(id);
            if (input != null)
            {
                var map = _mapper.Map(data, input);
                var result = await _issueService.UpdateAsync(map);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Update Successfully !!!!");
            }
            else { return BadRequest("Update Fail !!!"); }
        }
    }
}
