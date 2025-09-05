using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineEgitim.AdminAPI.Data;
using OnlineEgitim.AdminAPI.Models;
using OnlineEgitim.AdminAPI.Repositories;

namespace OnlineEgitim.AdminAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly IRepository<Course> _courseRepository;
        private readonly AppDbContext _context;

        public CourseController(IRepository<Course> courseRepository, AppDbContext context)
        {
            _courseRepository = courseRepository
                                ?? throw new ArgumentNullException(nameof(courseRepository));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: api/Course
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseRepository.GetAllAsync();
            if (courses == null || !courses.Any())
                return Ok(new List<Course>());

            return Ok(courses);
        }

        // GET: api/Course/approved
        [HttpGet("approved")]
        public async Task<IActionResult> GetApprovedCourses()
        {
            var courses = (await _courseRepository.GetAllAsync())
                          .Where(c => c.IsApproved).ToList();

            return Ok(courses);
        }

        // GET: api/Course/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return NotFound(new { message = "Kurs bulunamadı." });

            return Ok(course);
        }

        // ✅ Satın alınan kurslar
        [HttpGet("PurchasedCourses")]
        public async Task<IActionResult> GetPurchasedCourses()
        {
            var purchased = await _context.PurchasedCourses
                .Include(pc => pc.User)
                .Include(pc => pc.Course)
                .Select(pc => new
                {
                    UserName = pc.User.Name,
                    UserEmail = pc.User.Email,
                    CourseTitle = pc.Course.Title,
                    CoursePrice = pc.Course.Price,
                    PurchaseDate = pc.PurchaseDate
                })
                .ToListAsync();

            return Ok(purchased);
        }

        // ✅ POST: api/Course (multipart/form-data destekli)
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CourseCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string imagePath = null;
            if (dto.Image != null)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                imagePath = "/uploads/" + fileName;
            }

            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                Instructor = dto.Instructor,
                ImagePath = imagePath, // ✅ artık resim yolu da DB’ye kaydediliyor
                IsApproved = false
            };

            await _courseRepository.AddAsync(course);
            await _courseRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
        }

        // PUT: api/Course/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Course updatedCourse)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return NotFound();

            course.Title = updatedCourse.Title;
            course.Description = updatedCourse.Description;
            course.Price = updatedCourse.Price;
            course.Instructor = updatedCourse.Instructor;
            course.ImagePath = updatedCourse.ImagePath; // ✅ resim güncelleme desteği

            _courseRepository.Update(course);
            await _courseRepository.SaveChangesAsync();

            return Ok(new { message = "Kurs güncellendi ✅" });
        }

        // PATCH: api/Course/approve/5
        [HttpPatch("approve/{id:int}")]
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
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return NotFound();

            _courseRepository.Delete(course);
            await _courseRepository.SaveChangesAsync();

            return Ok(new { message = "Kurs silindi ❌" });
        }
    }

    // ✅ DTO
    public class CourseCreateDto
    {
        public string Title { get; set; }
        public string Instructor { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
