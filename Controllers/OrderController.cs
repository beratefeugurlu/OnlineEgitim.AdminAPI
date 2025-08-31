using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineEgitim.AdminAPI.Data;
using OnlineEgitim.AdminAPI.Models;

namespace OnlineEgitim.AdminAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Order/user/1 → Kullanıcının siparişleri
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserOrders(int userId)
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Course)
                .Where(o => o.UserId == userId)
                .ToListAsync();

            return Ok(orders);
        }

        // GET: api/Order → Tüm siparişler (sadece Admin)
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                .ThenInclude(i => i.Course)
                .ToListAsync();

            return Ok(orders);
        }

        // POST: api/Order/checkout/{userId} → Kullanıcının sepetini siparişe dönüştür
        [HttpPost("checkout/{userId}")]
        public async Task<IActionResult> Checkout(int userId)
        {
            var cartItems = await _context.Carts
                .Include(c => c.Course)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (!cartItems.Any())
                return BadRequest("Sepet boş, sipariş oluşturulamadı.");

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Items = new List<OrderItem>()
            };

            // Sepetteki ürünleri OrderItem’a dönüştür
            foreach (var c in cartItems)
            {
                order.Items.Add(new OrderItem
                {
                    CourseId = c.CourseId,
                    Quantity = c.Quantity,
                    Order = order   // ✅ Order ile ilişkiyi kur
                });
            }

            _context.Orders.Add(order);

            // ✅ siparişten sonra sepet temizlensin
            _context.Carts.RemoveRange(cartItems);

            await _context.SaveChangesAsync();

            return Ok(order);
        }
    }
}
