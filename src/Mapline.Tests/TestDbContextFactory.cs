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
        private DbContextOptions<MaplineDbContext> options;

        public TestDbContextFactory(string databaseName = "Mapline")
        {
            this.options = new DbContextOptionsBuilder<MaplineDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

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

        public MaplineDbContext CreateDbContext()
        {
            return new TestDbContext(options);
        }
    }

    public class TestDbContext : MaplineDbContext
    {
        public TestDbContext(DbContextOptions<MaplineDbContext> options)
            : base(options)
        {

        }

        // Empty class to override the actual data insert of MaplineDbContext
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
       
        }
    }
}
