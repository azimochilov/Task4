using Microsoft.EntityFrameworkCore;
using Task4.Domain.Entities;

namespace Task4.Data.DbContexts;
public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
}
