﻿using AutoMapper;
using IMS.Api.Common.UnitOfWorks;
using IMS.Api.Interfaces;
using IMS.Api.Models.Dtos.Milestones;
using IMS.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilestoneController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMilestoneService _milestoneService;
        public readonly IMapper _mapper;

        public MilestoneController(IUnitOfWork unitOfWork, IMilestoneService milestoneService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _milestoneService = milestoneService;
            _mapper = mapper;
        }

        [HttpGet("milestone")]
        public async Task<ActionResult<MilestoneResponse>> GetAllMilestone([FromQuery] MilestoneRequest request)
        {
            var data = await _milestoneService.GetMilestone(request);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MilestoneDto>> GetMilestoneId(int id)
        {
            try
            {
                var data = await _milestoneService.GetWithDetails(x => x.Id == id, x => x.Class, x => x.Project);
                var result = _mapper.Map<MilestoneDto>(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-milestone/{id}")]
        public async Task<IActionResult> DeleteMilestone(int id)
        {
            var data = await _milestoneService.GetById(id);
            if (data != null)
            {
                await _milestoneService.DeleteAsync(data);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Delete successfully !!!");
            }
            else
            {
                return BadRequest("Delete Fail !!!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMilestone([FromBody] CreateMilestoneDto data)
        {
            var map = _mapper.Map<Milestone>(data);
            var result = await _milestoneService.InsertAsync(map);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Add Successfully !!!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMilestone(int id, [FromBody] UpdateMilestoneDto data)
        {
            var input = await _milestoneService.GetById(id);
            if (input != null)
            {
                var map = _mapper.Map(data, input);
                var result = await _milestoneService.UpdateAsync(map);
                await _unitOfWork.SaveChangesAsync();
                return Ok("Update Successfully !!!!");
            }
            else { return BadRequest("Update Fail !!!"); }
        }
    }
}
