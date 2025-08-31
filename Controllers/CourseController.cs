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

            _courseRepository.Update(course);
            await _courseRepository.SaveChangesAsync();

            return NoContent();
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

