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
    /// <summary>
    /// Database context factory class that saves data in-memory for the unit tests.
    /// </summary>
    public class InMemoryContextFactory : IDbContextFactory<MaplineDbContext>
    {
        private readonly IDataBuilder dataBuilder;
        private readonly DbContextOptions<MaplineDbContext> options;

        private static IDataBuilder CreateDefaultDataBuilder()
        {
            var config = Config.GetAppJson().GetSection("Settings");
            return LanguagesBuilder.CreateDataBuilder(config);
        }

        /// <summary>
        /// Initializes the default instance of the class.
        /// </summary>
        public InMemoryContextFactory() 
            : this(CreateDefaultDataBuilder()) 
        { 

        }

        /// <summary>
        /// Initializes the instance with the specified data builder.
        /// </summary>
        /// <param name="dataBuilder">The data builder.s</param>
        public InMemoryContextFactory(IDataBuilder dataBuilder)
        {
            this.dataBuilder = dataBuilder ?? throw new ArgumentNullException(nameof(dataBuilder));
            this.options = new DbContextOptionsBuilder<MaplineDbContext>()
                 .UseInMemoryDatabase("Mapline")
                 .Options;
        }

        /// <summary>
        /// Creates the database context.
        /// </summary>
        /// <returns></returns>
        public MaplineDbContext CreateDbContext()
        {
            var context = new MaplineDbContext(this.options, this.dataBuilder);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }
    }
}
