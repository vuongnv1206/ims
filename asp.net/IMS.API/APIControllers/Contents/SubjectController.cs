using AutoMapper;
using IMS.BusinessService.Systems;
using IMS.Contract.Common.UnitOfWorks;
using IMS.Contract.Contents.Subjects;
using IMS.Contract.Systems.Roles;
using IMS.Contract.Systems.Users;
using IMS.Domain.Contents;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.APIControllers.Contents
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
        public async Task<ActionResult<SubjectReponse>> GetAssignmentId(int id)
        {
            try
            {
                var data = await _subjectService.GetById(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("subject")]
        public async Task<ActionResult<SubjectReponse>> GetAllAssignment([FromQuery] SubjectRequest request)
        {
            var data = await _subjectService.GetListAllAsync(request);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
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
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateNewAssignment([FromBody] CreateUpdateSubjectDto data)
        {
            var map = _mapper.Map<Subject>(data);
            var result = await _subjectService.InsertAsync(map);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Add successfully ");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(int id, [FromBody] CreateUpdateSubjectDto input)
        {
            var data = await _subjectService.GetById(id);
            if (data == null)
            {
                return BadRequest();
            }
            else
            {
                var map = _mapper.Map(input, data);
                var result = await _subjectService.UpdateAsync(map);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Update Successfully");
            }
        }

    }
}
