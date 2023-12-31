using KWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace KWalks.API.Data
{
    public class KWalksDbContext: DbContext
    {
        public KWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
