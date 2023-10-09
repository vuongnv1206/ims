using AutoMapper;
using IMS.Contract.Common.UnitOfWorks;
using IMS.Contract.Contents.Assignments;
using IMS.Contract.Contents.Labels;
using IMS.Domain.Contents;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.Contents
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly ILabelService _labelService;
        public readonly IMapper _mapper;

        public LabelController(ILabelService labelService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _labelService = labelService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("labels")]
        public async Task<ActionResult<LabelResponse>> GetAllLabel([FromQuery] LabelRequest request)
        {
            var data = await _labelService.GetLabel(request);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LabelResponse>> GetLabelId(int id)
        {
            try
            {
                var data = await _labelService.GetById(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("delete-label/{id}")]
        public async Task<IActionResult> DeleteLabel(int id)
        {
            var data = await _labelService.GetById(id);
            if (data != null)
            {
                await _labelService.DeleteAsync(data);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Delete Successfully !!!");
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateNewLabel([FromBody] CreateUpdateLabelDTO data)
        {
            var map = _mapper.Map<Label>(data);
            var result = await _labelService.InsertAsync(map);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Add successfully ");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLabel(int id, [FromBody] CreateUpdateLabelDTO input)
        {
            var data = await _labelService.GetById(id);
            if (data == null)
            {
                return BadRequest();
            }
            else
            {
                var map = _mapper.Map(input, data);
                var result = await _labelService.UpdateAsync(map);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Update Successfully ");
            }
        }
    }
}
