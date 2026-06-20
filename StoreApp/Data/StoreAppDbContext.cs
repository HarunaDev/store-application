using Microsoft.EntityFrameworkCore;
using StoreApp.Models;

namespace StoreApp.Data;

public class StoreAppDbContext : DbContext
{
    public StoreAppDbContext(
        DbContextOptions<StoreAppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<RefreshToken> RefreshTokens =>
        Set<RefreshToken>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Product> Products => Set<Product>();
}