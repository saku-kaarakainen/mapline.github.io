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
        private static IConfiguration DefaultConfiguration => Config.GetAppJson().GetSection("Settings");
        private static IDataBuilder CreateDefaultDataBuilder() => LanguagesBuilder.CreateDataBuilder(DefaultConfiguration);        

        private readonly IConfiguration config;
        private readonly IDataBuilder dataBuilder;
        private readonly DbContextOptions<MaplineDbContext> options;

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
        public InMemoryContextFactory(IDataBuilder dataBuilder, IConfiguration config = null)
        {
            this.config = config ?? DefaultConfiguration;
            this.dataBuilder = dataBuilder ?? LanguagesBuilder.CreateDataBuilder(this.config);

            var databaseName = this.config.GetValue<string>("DatabaseName") ?? throw new ArgumentException($"The configuration is missing the key 'DatabaseName'", nameof(config));
            this.options = new DbContextOptionsBuilder<MaplineDbContext>()
                 .UseInMemoryDatabase(databaseName)
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
