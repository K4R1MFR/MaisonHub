using MaisonHub.Areas.HouseKeeping.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MaisonHub.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Dishwasher>? Dishwashers { get; set; }

        public DbSet<Garbage>? Garbage { get; set; }
    }
}