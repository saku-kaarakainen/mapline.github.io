using Mapline.Web.Data;
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

            var context = new MaplineDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }
    }
}
