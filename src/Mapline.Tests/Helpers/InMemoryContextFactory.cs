using Mapline.Web.Data;
using Mapline.Web.Data.Building;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapline.Tests.Helpers
{
    public class InMemoryContextFactory : IDbContextFactory<MaplineDbContext>
    {
        private readonly IDataBuilder dataBuilder;

        private static IDataBuilder CreateDefaultDataBuilder()
        {
            var config = Config.GetAppJson().GetSection("Settings");
            return LanguagesBuilder.CreateDataBuilder(config);
        }

        public InMemoryContextFactory() 
            : this(CreateDefaultDataBuilder()) 
        { 

        }

        public InMemoryContextFactory(IDataBuilder dataBuilder)
        {
            this.dataBuilder = dataBuilder ?? throw new ArgumentNullException(nameof(dataBuilder));
        }

        public MaplineDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<MaplineDbContext>()
                 .UseInMemoryDatabase("Mapline")
                 .Options;

            var context = new MaplineDbContext(options, this.dataBuilder);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }
    }
}
