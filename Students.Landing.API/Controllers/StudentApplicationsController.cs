using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Models;
using Students.Landing.Core.Services;

namespace Students.Landing.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentApplicationsController : ControllerBase
    {
        private readonly IStudentApplicationService _service;

        public StudentApplicationsController(IStudentApplicationService service)
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

        [HttpGet("available-companies/{studentId:guid}")]
        public async Task<IActionResult> GetAvailableCompanies(Guid studentId)
        {
            try
            {
                var companies = await _service.GetAvailableCompaniesForStudent(studentId);
                return Ok(companies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentApplication model)
        {
            var created = await _service.CreateApplication(model);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

    }
}
