using Entity.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration;

public class BaseContext : DbContext
{
    public BaseContext(DbContextOptions options) : base(options)
    { }

    public DbSet<Product> Products { get; set; }
}