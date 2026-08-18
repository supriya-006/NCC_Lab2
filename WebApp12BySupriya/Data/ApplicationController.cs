using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp12BySupriya.Models;

namespace WebApp12BySupriya.Data;

public class ApplicationContext : IdentityDbContext
{
    public ApplicationContext(
        DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}