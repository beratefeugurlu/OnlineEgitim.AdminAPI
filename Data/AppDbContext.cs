using Microsoft.EntityFrameworkCore;
using OnlineEgitim.AdminAPI.Models;

namespace OnlineEgitim.AdminAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Kurslar
        public DbSet<Course> Courses { get; set; }

        // Kullanıcılar
        public DbSet<User> Users { get; set; }

        // Sepetler
        public DbSet<Cart> Carts { get; set; }

        // Siparişler
        public DbSet<Order> Orders { get; set; }

        // Sipariş Kalemleri
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
