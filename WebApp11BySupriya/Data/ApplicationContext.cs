using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp11BySupriya.Models;

namespace WebApp11BySupriya.Data;

public class ApplicationContext : IdentityDbContext
{
    public ApplicationContext(
        DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}