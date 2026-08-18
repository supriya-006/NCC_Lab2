using Microsoft.EntityFrameworkCore;
using WebApp6BySupriya.Models;

namespace WebApp6BySupriya.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; } = null!;
    }
}
