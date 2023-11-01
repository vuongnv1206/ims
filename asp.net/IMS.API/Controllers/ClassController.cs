using AutoMapper;
using IMS.Contract.Common.UnitOfWorks;
using IMS.Contract.Contents.Classes;
using IMS.Contract.Contents.Projects;
using IMS.Domain.Contents;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IClassService _classService;
        public readonly IMapper _mapper;

        public ClassController(IClassService classService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _classService = classService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [HttpGet("classes")]
        public async Task<ActionResult<ClassReponse>> GetAllClass([FromQuery] ClassRequest request)
        {
            var data = await _classService.GetAllClass(request);
            return Ok(data);
        }


        [HttpGet("classId")]
        public async Task<ActionResult<ProjectReponse>> GetClassByid(int classId)
        {
            var data = await _classService.GetClassById(classId);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewClass([FromBody] CreateAndUpdateClassDto data)
        {
            var map = _mapper.Map<Class>(data);
            var result = await _classService.InsertAsync(map);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Add successfully ");
        }

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

    }
}
