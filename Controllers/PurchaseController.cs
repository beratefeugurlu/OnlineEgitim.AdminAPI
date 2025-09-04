using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineEgitim.AdminAPI.Data;
using OnlineEgitim.AdminAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineEgitim.AdminAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PurchaseController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Satın alma işlemi
        [HttpPost("Buy")]
        public async Task<IActionResult> Buy([FromBody] PurchaseRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return NotFound("Kullanıcı bulunamadı!");

            foreach (var courseId in request.CourseIds)
            {
                _context.PurchasedCourses.Add(new PurchasedCourse
                {
                    UserId = user.Id,
                    CourseId = courseId,
                    PurchaseDate = DateTime.Now
                });
            }

            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Satın alma başarılı" });
        }

        // ✅ Kullanıcının satın aldığı kursları getir
        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetUserPurchases(int userId)
        {
            var purchases = await _context.PurchasedCourses
                .Where(pc => pc.UserId == userId)
                .Include(pc => pc.Course)
                .Include(pc => pc.User)
                .ToListAsync();

            var result = purchases.Select(pc => new
            {
                UserName = pc.User.Name,
                UserEmail = pc.User.Email,
                CourseTitle = pc.Course.Title,
                CoursePrice = pc.Course.Price,
                PurchaseDate = pc.PurchaseDate
            });

            return Ok(result);
        }

        // ✅ Admin için: tüm kullanıcıların satın aldığı kursları getir
        [HttpGet("All")]
        public async Task<IActionResult> GetAllPurchases()
        {
            var purchases = await _context.PurchasedCourses
                .Include(pc => pc.Course)
                .Include(pc => pc.User)
                .OrderByDescending(pc => pc.PurchaseDate)
                .ToListAsync();

            var result = purchases.Select(pc => new
            {
                UserName = pc.User.Name,
                UserEmail = pc.User.Email,
                CourseTitle = pc.Course.Title,
                CoursePrice = pc.Course.Price,
                PurchaseDate = pc.PurchaseDate
            });

            return Ok(result);
        }
    }

    public class PurchaseRequest
    {
        public string Email { get; set; }
        public List<int> CourseIds { get; set; }
    }
}
