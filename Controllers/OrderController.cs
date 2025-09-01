using Microsoft.AspNetCore.Mvc;
using OnlineEgitim.AdminAPI.Models;
using OnlineEgitim.AdminAPI.Repositories;

namespace OnlineEgitim.AdminAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<Course> _courseRepository;

        public OrderController(
            IRepository<Order> orderRepository,
            IRepository<OrderItem> orderItemRepository,
            IRepository<Course> courseRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _courseRepository = courseRepository;
        }

        // Kullanıcının satın aldığı kursları getir
        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetOrdersByUser(int userId)
        {
            var orders = await _orderRepository.GetAllAsync();
            var userOrders = orders.Where(o => o.UserId == userId).ToList();

            var orderItems = await _orderItemRepository.GetAllAsync();
            var userItems = orderItems.Where(i => userOrders.Any(o => o.Id == i.OrderId)).ToList();

            var purchasedCourses = new List<Course>();
            foreach (var item in userItems)
            {
                var course = await _courseRepository.GetByIdAsync(item.CourseId);
                if (course != null)
                    purchasedCourses.Add(course);
            }

            return Ok(purchasedCourses);
        }
    }
}
