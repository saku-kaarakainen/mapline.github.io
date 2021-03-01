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
    // MapControllerTests           - default .ctor
    // AdministratorControllerTests - default .ctor


    public class TestDbContextFactory : IDbContextFactory<MaplineDbContext>
    {
        public IDataBuilder DataBuilder { get; set; }
        public IConfigurationSection Settings { get; }

        private DbContextOptions<MaplineDbContext> options;

        public TestDbContextFactory(IConfiguration config = null, IDataBuilder dataBuilder = null)
        {
            if(config == null)
            {
                config = Config.Get();
            }

            this.DataBuilder = dataBuilder ?? TestDataBuilder.CreateDefault();
            this.Settings = config.GetSection("Settings");
            this.options = new DbContextOptionsBuilder<MaplineDbContext>()
                .UseInMemoryDatabase(Settings.GetValue<string>("DatabaseName"))
                .Options;
        }

        public MaplineDbContext CreateDbContext()
        {
            var context = new TestDbContext(options, DataBuilder, Settings);
            return context;
        }
    }


}
