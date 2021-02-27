using Mapline.Tests.Helpers;
using Mapline.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mapline.Tests.WebTests.DataTests
{
    public class MaplineDbContextTests 
    {
        private readonly TestDbContextFactory factory;

        public MaplineDbContextTests()
        {
            var settings = new MaplineDbContextSettings
            {
                CreateModel = true,
                InitializeData = true
            };
            this.factory = new TestDbContextFactory(settings);
        }

        [Fact]
        public void Test_CreateDbContextAndBuildModel()
        {
            var context = this.factory.CreateDbContext();

            Assert.NotNull(context);
        }
    }
}
