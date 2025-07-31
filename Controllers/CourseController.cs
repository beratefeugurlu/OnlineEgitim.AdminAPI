using Microsoft.AspNetCore.Mvc;
using OnlineEgitim.AdminAPI.Data;
using OnlineEgitim.AdminAPI.Models;

namespace OnlineEgitim.AdminAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Course
        [HttpGet]
        public IActionResult GetAll()
        {
            var courses = _context.Courses.ToList();
            return Ok(courses);
        }

        // GET: api/Course/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        // POST: api/Course
        [HttpPost]
        public IActionResult Create([FromBody] Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
        }

        // PUT: api/Course/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Course updatedCourse)
        {
            var course = _context.Courses.Find(id);
            if (course == null) return NotFound();

            course.Title = updatedCourse.Title;
            course.Description = updatedCourse.Description;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Course/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null) return NotFound();

            _context.Courses.Remove(course);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
