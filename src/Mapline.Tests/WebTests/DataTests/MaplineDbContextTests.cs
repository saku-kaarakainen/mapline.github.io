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
        public MaplineDbContextTests()
        {
        }

        [Fact]
        public void Test_CreateDbContextAndBuildModel()
        {
            var settings = new MaplineDbContextSettings
            {
                CreateModel = true,
                InitializeData = true
            };
            var factory = new TestDbContextFactory(settings);
            var context = factory.CreateDbContext();

            Assert.NotNull(context);
        }

    }
}
