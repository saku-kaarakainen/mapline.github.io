using Mapline.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapline.Tests.Helpers
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


}
