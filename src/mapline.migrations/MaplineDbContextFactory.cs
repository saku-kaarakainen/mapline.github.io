using mapline.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace mapline.migrations
{
    public class MaplineDbContextFactory : IDesignTimeDbContextFactory<MaplineDbContext>
    {
        public static string AssemblyName => typeof(MaplineDbContextFactory).Assembly.GetName().Name;

        public MaplineDbContext CreateDbContext(string[] args)
        {
            var connectionString = "server=.\\SQLExpress; database=Mapline;Trusted_Connection=True;";
            var optionsBuilder = new DbContextOptionsBuilder<MaplineDbContext>();
            optionsBuilder.UseSqlServer(connectionString, x => x.MigrationsAssembly(AssemblyName));            

            return new MaplineDbContext(optionsBuilder.Options);
        }
    }
}
