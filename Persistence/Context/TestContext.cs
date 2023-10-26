using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;

public class TestContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    
    public TestContext(DbContextOptions options) : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}