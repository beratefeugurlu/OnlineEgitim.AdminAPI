using Microsoft.AspNetCore.Mvc;
using OnlineEgitim.AdminAPI.Models;
using OnlineEgitim.AdminAPI.Repositories;

namespace OnlineEgitim.AdminAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly IRepository<Course> _courseRepository;

        public CourseController(IRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        // GET: api/Course
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseRepository.GetAllAsync();
            return Ok(courses);
        }

        // GET: api/Course/approved → sadece onaylı kurslar
        [HttpGet("approved")]
        public async Task<IActionResult> GetApprovedCourses()
        {
            var courses = (await _courseRepository.GetAllAsync())
                          .Where(c => c.IsApproved).ToList();
            return Ok(courses);
        }

        // GET: api/Course/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        // POST: api/Course
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Course course)
        {
            course.IsApproved = false; // ✅ Yeni kurslar otomatik "onaysız" gelecek
            await _courseRepository.AddAsync(course);
            await _courseRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
        }

        // PUT: api/Course/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Course updatedCourse)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return NotFound();

            course.Title = updatedCourse.Title;
            course.Description = updatedCourse.Description;
            course.Price = updatedCourse.Price;
            course.Instructor = updatedCourse.Instructor;

            _courseRepository.Update(course);
            await _courseRepository.SaveChangesAsync();

            return NoContent();
        }

        // ✅ PATCH: api/Course/approve/5 → Admin kurs onayı
        [HttpPatch("approve/{id}")]
        public async Task<IActionResult> ApproveCourse(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return NotFound();

            course.IsApproved = true;
            _courseRepository.Update(course);
            await _courseRepository.SaveChangesAsync();

            return Ok(new { message = $"{course.Title} başarıyla onaylandı ✅" });
        }

        // DELETE: api/Course/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return NotFound();

            _courseRepository.Delete(course);
            await _courseRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
