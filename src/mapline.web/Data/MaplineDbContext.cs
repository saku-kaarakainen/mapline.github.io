using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mapline.Data
{
    public class MaplineDbContext : DbContext
    {
        public MaplineDbContext(DbContextOptions<MaplineDbContext> options) 
            : base(options)
        {

        }

        public DbSet<Language> Languages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>().ToTable("Language");
        }
    }
}
