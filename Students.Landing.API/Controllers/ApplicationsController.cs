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
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _service;

        public ApplicationsController(IApplicationService service)
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Application model)
        {
            var created = await _service.CreateApplication(model);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

    }
}
