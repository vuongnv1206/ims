using AutoMapper;
using IMS.Contract.Common.UnitOfWorks;
using IMS.Contract.Contents.Assignments;
using IMS.Domain.Contents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IAssignmentService _assignService;
        public readonly IMapper _mapper;

        public AssignmentController(IAssignmentService assignService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _assignService = assignService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("assignments")]
        public async Task<ActionResult<AssignmentResponse>> GetAllAssignment([FromQuery] AssignmentRequest request)
        {
            var data = await _assignService.GetAssignment(request);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssignmentDTO>> GetAssignmentId(int id)
        {
            try
            {
                var data = await _assignService.GetById(id);
                var result = _mapper.Map<AssignmentDTO>(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("delete-assignment/{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            var data = await _assignService.GetById(id);
            if (data != null)
            {
                await _assignService.DeleteAsync(data);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Delete Successfully !!!");
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateNewAssignment([FromBody] CreateUpdateAssignmentDTO data)
        {
            var map = _mapper.Map<Assignment>(data);
            var result = await _assignService.InsertAsync(map);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Add successfully ");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(int id, [FromBody] CreateUpdateAssignmentDTO input)
        {
            var data = await _assignService.GetById(id);
            if (data == null)
            {
                return BadRequest();
            }
            else
            {
                var map = _mapper.Map(input, data);
                var result = await _assignService.UpdateAsync(map);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Update Successfully ");
            }
        }

    }
}
