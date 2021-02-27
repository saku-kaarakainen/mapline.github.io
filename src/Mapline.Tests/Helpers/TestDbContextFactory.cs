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
        public IConfigurationSection Settings { get; }

        private DbContextOptions<MaplineDbContext> options;

        public TestDbContextFactory() : this(Config.Get()) { }

        public TestDbContextFactory(IConfiguration config)
        {
            if(config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            this.Settings = config.GetSection("Settings");
            this.options = new DbContextOptionsBuilder<MaplineDbContext>()
                .UseInMemoryDatabase(Settings.GetValue<string>("DatabaseName"))
                .Options;

            if (Settings.GetValue<bool>("InitializeData"))
            {
                using var initialData = CreateDbContext();
                initialData.Languages.Add(new Language
                {
                    Name = "Test1",
                    YearStart = -2000,
                    YearEnd = 0000
                });

                initialData.Languages.Add(new Language
                {
                    Name = "Test2",
                    YearStart = -1000,
                    YearEnd = 1000
                });

                initialData.SaveChanges();
            }
        }

        public MaplineDbContext CreateDbContext()
        {
            var settings = Config.Get().GetSection("Settings");
            var context = new TestDbContext(options, settings);
            return context;
        }
    }


}
