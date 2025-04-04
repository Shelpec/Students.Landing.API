﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Models;
using Students.Landing.Core.Models.DTOs;

namespace Students.Landing.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MajorsController : ControllerBase
    {
        private readonly IMajorService _service;

        public MajorsController(IMajorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        ///// <summary>
        ///// Создать специальность и привязать её к университету
        ///// </summary>
        //[HttpPost]
        //public async Task<IActionResult> CreateOrAssignMajorToUniversity([FromBody] MajorDTO model)
        //{
        //    if (model == null) return BadRequest("Invalid data");

        //    var newMajor = new Major
        //    {
        //        Name = model.Name,
        //        SpecializationDirectionId = model.SpecializationDirectionId
        //    };

        //    try
        //    {
        //        var created = await _service.CreateOrAssignMajorToUniversity(newMajor, model.UniversityId);
        //        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("byInstitution/{institutionId:guid}")]
        public async Task<IActionResult> GetByInstitutionId(Guid institutionId)
        {
            var majors = await _service.GetByInstitutionIdAsync(institutionId);
            return Ok(majors);
        }

    }
}
