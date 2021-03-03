using Mapline.Web.Data;
using Mapline.Web.Data.Building;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapline.Tests.Helpers
{
    public class InMemoryContextFactory : IDbContextFactory<MaplineDbContext>
    {
        public MaplineDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<MaplineDbContext>()
                 .UseInMemoryDatabase("Mapline")
                 .Options;

            var config = Config.Get();
            var dataBuilder = LanguagesBuilder.CreateDataBuilder(config);
            var context = new MaplineDbContext(options, dataBuilder);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }
    }
}
