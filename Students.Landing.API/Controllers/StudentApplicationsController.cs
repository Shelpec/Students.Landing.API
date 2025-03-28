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

        //[HttpGet("pending/university/{universityId}")]
        //public async Task<IActionResult> GetPendingApplicationsForUniversity(Guid universityId)
        //{
        //    var applications = await _service.GetPendingApplicationsForUniversity(universityId);
        //    return Ok(applications);
        //}

        //[HttpGet("pending/company/{companyId}")]
        //public async Task<IActionResult> GetPendingApplicationsForCompany(Guid companyId)
        //{
        //    var applications = await _service.GetPendingApplicationsForCompany(companyId);
        //    return Ok(applications);
        //}

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentApplication model)
        {
            var created = await _service.CreateApplication(model);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        //[HttpPost("{applicationId:guid}/approveUniversity")]
        //public async Task<IActionResult> ApproveByUniversity(Guid applicationId)
        //{
        //    return Ok(await _service.ApproveByUniversity(applicationId));
        //}

        //[HttpPost("{applicationId:guid}/rejectUniversity")]
        //public async Task<IActionResult> RejectByUniversity(Guid applicationId)
        //{
        //    return Ok(await _service.RejectByUniversity(applicationId));
        //}

        //[HttpPost("{applicationId:guid}/acceptCompany")]
        //public async Task<IActionResult> AcceptByCompany(Guid applicationId)
        //{
        //    return Ok(await _service.AcceptByCompany(applicationId));
        //}

        //[HttpPost("{applicationId:guid}/rejectCompany")]
        //public async Task<IActionResult> RejectByCompany(Guid applicationId)
        //{
        //    return Ok(await _service.RejectByCompany(applicationId));
        //}

        //[HttpPost("{applicationId:guid}/complete")]
        //public async Task<IActionResult> MarkAsCompleted(Guid applicationId)
        //{
        //    return Ok(await _service.MarkAsCompleted(applicationId));
        //}

        //[HttpPost("{applicationId:guid}/cancel")]
        //public async Task<IActionResult> CancelApplication(Guid applicationId)
        //{
        //    return Ok(await _service.CancelApplication(applicationId));
        //}
    }
}
