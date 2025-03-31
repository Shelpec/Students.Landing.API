using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Models;

namespace Students.Landing.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public ProfileController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var keycloakUserId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(keycloakUserId))
                return Unauthorized();

            var student = await _studentService.GetStudentByKeycloakIdAsync(keycloakUserId);
            if (student == null)
            {
                return NotFound("Студент не найден. Заполните профиль.");
            }

            return Ok(student);
        }

        [HttpGet("ping")]
        [Authorize]
        public IActionResult Ping()
        {
            return Ok("Вы вошли в систему");
        }
    }
}
