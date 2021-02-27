using Mapline.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapline.Tests.Helpers
{
    public class TestDbContext : MaplineDbContext
    {
        public MaplineDbContextSettings Settings { get; }

        public TestDbContext(DbContextOptions<MaplineDbContext> options, MaplineDbContextSettings settings = null)
            : base(options)
        {
            Settings = settings ?? new MaplineDbContextSettings { CreateModel = false };

            if (!string.IsNullOrEmpty(Settings.LanguageFolder))
            {
                LanguageFolder = Settings.LanguageFolder;
            }
        }

        // Empty class to override the actual data insert of MaplineDbContext
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (Settings.CreateModel)
            {
                base.OnModelCreating(modelBuilder);
            }
        }
    }
}
