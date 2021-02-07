using Microsoft.EntityFrameworkCore;
using mapline.Models;

namespace mapline.Data
{
    /// <summary>
    /// The database context for the Mapline, 
    /// which is using the code first approach. 
    /// </summary>
    public partial class MaplineDbContext : DbContext
    {
        public MaplineDbContext(DbContextOptions options)
            : base(options)
        {
            // https://stackoverflow.com/a/25018458
            // Database.SetInitializer<MaplineDbContext>(new CreateDatabaseIfNotExists());
        }
    
        public DbSet<Language> Languages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>().ToTable("Language");
        }
    }
}