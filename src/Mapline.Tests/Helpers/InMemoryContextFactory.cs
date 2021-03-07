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

        /// <summary>
        /// Initializes the instance with the specified data builder.
        /// </summary>
        /// <param name="dataBuilder">The data builder.s</param>
        public InMemoryContextFactory(IDataBuilder dataBuilder = null, IConfiguration config = null)
        {
            this.dataBuilder = dataBuilder ?? TestDataBuilder.CreateDefault();
            this.options = InMemoryDbContextOptionsBuilder.GetDefault(config);
        }

        /// <summary>
        /// Creates the database context.
        /// </summary>
        /// <returns></returns>
        public MaplineDbContext CreateDbContext()
        {
            var context = new MaplineDbContext(this.options) 
            { 
                DataBuilder = this.dataBuilder 
            };
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }
    }
}
