using Mapline.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapline.Tests.Helpers
{
    public class InMemoryDbContextOptionsBuilder
    {
        public static DbContextOptions<MaplineDbContext> GetDefault(IConfiguration config = null)
        {
            config ??= Config.GetAppJson().GetSection("Settings");            
            var databaseName = config.GetValue<string>("DatabaseName") 
                ?? throw new ArgumentException($"The configuration is missing the key 'DatabaseName'", nameof(config));

            var options = new DbContextOptionsBuilder<MaplineDbContext>()
             .UseInMemoryDatabase(databaseName)
             .Options;

            return options;
        }
    }
}
