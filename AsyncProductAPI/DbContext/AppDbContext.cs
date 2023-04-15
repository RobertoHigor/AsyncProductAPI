using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Context;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<ListingRequest> ListingRequests => Set<ListingRequest>();
}