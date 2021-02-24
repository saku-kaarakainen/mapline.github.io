using Mapline.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapline.Tests
{
    public class TestDbContextFactory : IDbContextFactory<MaplineDbContext>
    {
        public MaplineDbContextSettings Settings { get; }

        private DbContextOptions<MaplineDbContext> options;

        public TestDbContextFactory()
            : this(new MaplineDbContextSettings())
        {

        }

        public TestDbContextFactory(MaplineDbContextSettings settings)
        {
            this.Settings = settings;
            this.options = new DbContextOptionsBuilder<MaplineDbContext>()
                .UseInMemoryDatabase(Settings.DatabaseName)
                .Options;

            if (Settings.InitializeData)
            {
                using var initialData = CreateDbContext();
                initialData.Languages.Add(new Language
                {
                    Name = "Test1",
                    YearStart = -2000,
                    YearEnd = 0000
                });

                initialData.Languages.Add(new Language
                {
                    Name = "Test2",
                    YearStart = -1000,
                    YearEnd = 1000
                });

                initialData.SaveChanges();
            }
        }

        public MaplineDbContext CreateDbContext()
        {
            return new TestDbContext(options, Settings);
        }
    }

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
            if(Settings.CreateModel)
            {
                base.OnModelCreating(modelBuilder);
            }
        }
    }

    public class MaplineDbContextSettings
    {
        public string DatabaseName { get; set; } = "Mapline";
        public bool InitializeData { get; set; } = true;
        public bool CreateModel { get; set; } = false;
        public string LanguageFolder { get; set; } = ".\\..\\..\\..\\..\\..\\data\\Language";
    }
}
