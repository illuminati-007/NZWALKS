using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FP_NZWALKS.Controllers
{
    // http://localhost:5173/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // GET:  http://localhost:5173/api/students
        [HttpGet]
        public IActionResult GetAllStudents() {
            string[] studentNames = new string[] { "John", "Max", "Loh", "Julik", "Vlad", "Dodik" };
            return Ok(studentNames);
        }
    }
}
