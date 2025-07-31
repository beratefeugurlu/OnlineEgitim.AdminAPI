using Microsoft.EntityFrameworkCore;
using OnlineEgitim.AdminAPI.Models;
using System.Collections.Generic;

namespace OnlineEgitim.AdminAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
    }
}
