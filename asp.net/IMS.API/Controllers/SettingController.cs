using AutoMapper;
using IMS.Api.Common.UnitOfWorks;
using IMS.Api.Interfaces;
using IMS.Api.Models.Dtos.Settings;
using IMS.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService settingService;
        private readonly IMapper mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SettingController(
            ISettingService settingService,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.settingService = settingService;
            this.mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<SettingResponse> Get([FromQuery] SettingRequest request)
        {
            var response = settingService.Settings(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<SettingDto>> Create([FromBody] CreateUpdateSetting request)
        {
            var entity = mapper.Map<Setting>(request);
            var result = settingService.InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SettingDto>> Update([FromBody] CreateUpdateSetting request, int id)
        {
            var entity = await settingService.GetById(id);
            if (entity == null)
                return BadRequest("Khong tim thay");
            var entityMap = mapper.Map(request, entity);
            var result = settingService.UpdateAsync(entityMap);
            await _unitOfWork.SaveChangesAsync();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await settingService.GetById(id);
            if (entity == null)
                return BadRequest("Khong tim thay");
            var result = settingService.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Successfully");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SettingDto>> GetSettingByIdAsync(int id)
        {
            try
            {
                var data = await settingService.GetById(id);
                if (data == null)
                {
                    return NotFound("Khong tim thay");
                }
                var result = mapper.Map<SettingDto>(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
