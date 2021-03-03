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
    public class TestDbContext : MaplineDbContext
    {
        public IConfigurationSection Settings { get; }


        public TestDbContext(DbContextOptions<MaplineDbContext> options, IDataBuilder dataBuilder, IConfigurationSection settings)
            : base(options, dataBuilder)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        // Empty class to override the actual data insert of MaplineDbContext
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (Settings.GetValue<bool>("CreateModel"))
            {
                base.OnModelCreating(modelBuilder);
            }
        }
    }
}
