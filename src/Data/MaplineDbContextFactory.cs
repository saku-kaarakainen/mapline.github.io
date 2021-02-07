using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mapline.Data
{
    // https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
    // Design time pattern
    public class MaplineDbContextFactory : IDesignTimeDbContextFactory<MaplineDbContext>
    {
        public MaplineDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MaplineDbContext>();

            // TODO: Get connection string from the appsettings.json
            var connectionString = "server=.\\SQLEXpress; database=Mapline;Trusted_Connection=True;";
            optionsBuilder
                    .UseSqlServer(connectionString, x => x.UseNetTopologySuite())
                    .EnableSensitiveDataLogging(true)
                    .EnableDetailedErrors(true);



            return new MaplineDbContext(optionsBuilder.Options);
        }
    }
}
