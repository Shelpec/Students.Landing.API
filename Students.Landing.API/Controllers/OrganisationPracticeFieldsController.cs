using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Models;

namespace Students.Landing.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganisationPracticeFieldsController : ControllerBase
    {
        private readonly IOrganisationPracticeFieldService _service;

        public OrganisationPracticeFieldsController(IOrganisationPracticeFieldService service)
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
        public async Task<IActionResult> Create([FromBody] OrganisationPracticeField model)
        {
            var created = await _service.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] OrganisationPracticeField model)
        {
            var updated = await _service.UpdateAsync(id, model);
            if (updated == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
