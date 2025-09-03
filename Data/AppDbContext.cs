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

        // Satın alınan kurslar ✅
        public DbSet<PurchasedCourse> PurchasedCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Order - User ilişkisi (1-N)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderItem - Order ilişkisi (1-N)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderItem - Course ilişkisi (1-N)
            modelBuilder.Entity<OrderItem>()
                .HasOne<Course>()
                .WithMany()
                .HasForeignKey(oi => oi.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // PurchasedCourse - User ilişkisi (1-N)
            modelBuilder.Entity<PurchasedCourse>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(pc => pc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // PurchasedCourse - Course ilişkisi (1-N)
            modelBuilder.Entity<PurchasedCourse>()
                .HasOne<Course>()
                .WithMany()
                .HasForeignKey(pc => pc.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
